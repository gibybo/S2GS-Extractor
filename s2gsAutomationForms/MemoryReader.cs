using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace s2gsAutomationForms
{
    class MemoryReader
    {
        // Import a bunch of native windows API stuff. A good chunk of these
        // are probably natively available in C#.NET in a different form,
        // but I am basically just porting my C++ code from my ladder reader
        // so using the same functions is easier.

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(
          IntPtr hProcess,
          IntPtr lpBaseAddress,
          byte[] lpBuffer,
          int dwSize,
          out int lpNumberOfBytesRead
         );

        [StructLayout(LayoutKind.Sequential, Size = 40)]
        private struct PROCESS_MEMORY_COUNTERS
        {
            public uint cb;
            public uint PageFaultCount;
            public IntPtr PeakWorkingSetSize;
            public IntPtr WorkingSetSize;
            public IntPtr QuotaPeakPagedPoolUsage;
            public IntPtr QuotaPagedPoolUsage;
            public IntPtr QuotaPeakNonPagedPoolUsage;
            public IntPtr QuotaNonPagedPoolUsage;
            public IntPtr PagefileUsage;
            public IntPtr PeakPagefileUsage;
        }

        [DllImport("psapi.dll", SetLastError = true)]
        static extern bool GetProcessMemoryInfo(IntPtr hProcess, out PROCESS_MEMORY_COUNTERS counters, uint size);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool OpenProcessToken(IntPtr ProcessHandle,
            UInt32 DesiredAccess, out IntPtr TokenHandle);

        public const UInt32 STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        public const UInt32 STANDARD_RIGHTS_READ = 0x00020000;
        public const UInt32 TOKEN_ASSIGN_PRIMARY = 0x0001;
        public const UInt32 TOKEN_DUPLICATE = 0x0002;
        public const UInt32 TOKEN_IMPERSONATE = 0x0004;
        public const UInt32 TOKEN_QUERY = 0x0008;
        public const UInt32 TOKEN_QUERY_SOURCE = 0x0010;
        public const UInt32 TOKEN_ADJUST_PRIVILEGES = 0x0020;
        public const UInt32 TOKEN_ADJUST_GROUPS = 0x0040;
        public const UInt32 TOKEN_ADJUST_DEFAULT = 0x0080;
        public const UInt32 TOKEN_ADJUST_SESSIONID = 0x0100;
        public const UInt32 TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);
        public const UInt32 TOKEN_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | TOKEN_ASSIGN_PRIMARY |
            TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY | TOKEN_QUERY_SOURCE |
            TOKEN_ADJUST_PRIVILEGES | TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT |
            TOKEN_ADJUST_SESSIONID);

        [StructLayout(LayoutKind.Sequential)]
        public struct LUID
        {
            public uint LowPart;
            public int HighPart;
        }

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LookupPrivilegeValue(string lpSystemName, string lpName,
            out LUID lpLuid);

        [StructLayout(LayoutKind.Sequential)]
        public struct TOKEN_PRIVILEGES
        {
            public UInt32 PrivilegeCount;
            public LUID Luid;
            public UInt32 Attributes;
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
           [MarshalAs(UnmanagedType.Bool)]bool DisableAllPrivileges,
           ref TOKEN_PRIVILEGES NewState,
           UInt32 BufferLengthInBytes,
           ref TOKEN_PRIVILEGES PreviousState,
           out UInt32 ReturnLengthInBytes);

        public const string SE_DEBUG_NAME = "SeDebugPrivilege";
        public const int SE_PRIVILEGE_ENABLED = 0x00000002;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

        [Flags]
        enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        private IntPtr pHandle_;
        private long nextAddr_;
        private long baseAddr_;
        private long pSize_;

        public MemoryReader()
        {
            nextAddr_ = baseAddr_ = 0;
            pHandle_ = openSC2();
            pSize_ = getSize(pHandle_);

            Console.WriteLine("size: " + pSize_);
        }

        static private long getSize(IntPtr pHandle)
        {
            PROCESS_MEMORY_COUNTERS pCounters = new PROCESS_MEMORY_COUNTERS();
            GetProcessMemoryInfo(pHandle, out pCounters, (uint)Marshal.SizeOf(pCounters));
            return pCounters.WorkingSetSize.ToInt64();
        }

        public int readNextMemoryRegion(byte[] buffer, uint maxSize)
        {
            MEMORY_BASIC_INFORMATION mbi = new MEMORY_BASIC_INFORMATION();
            int amtRead;
            while (true)
            {
                if (nextAddr_ <= pSize_ + baseAddr_)
                {
                    if (0 == VirtualQueryEx(pHandle_, new IntPtr(nextAddr_), out mbi, (uint)Marshal.SizeOf(mbi)))
                    {
                        Console.WriteLine("VirtualQueryEx failure");
                    }
                    int rgnSize = mbi.RegionSize.ToInt32();
                    if (rgnSize > maxSize)
                    {
                        nextAddr_ += rgnSize;
                        continue;
                    }

                    ReadProcessMemory(pHandle_, new IntPtr(nextAddr_), buffer, rgnSize, out amtRead);
                    nextAddr_ += rgnSize;

                    if (0 != amtRead)
                    {
                        return amtRead;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        static private IntPtr openSC2()
        {
            Console.WriteLine("opening SC2..");

            if (!elevatePrivileges()) {
                Console.WriteLine("Failed to get necessary privileges to inspect SC2 memory, exiting");
            }

            IntPtr sc2Wnd = FindWindow(null, "StarCraft II");
            uint sc2pid;
            GetWindowThreadProcessId(sc2Wnd, out sc2pid);

            Console.WriteLine("found sc2 pid: " + sc2pid.ToString());

            return OpenProcess(ProcessAccessFlags.All, false, (int)sc2pid);
        }

        static private bool elevatePrivileges()
        {
            IntPtr hToken;
            TOKEN_PRIVILEGES token_privileges;
            token_privileges.PrivilegeCount = 1;
            if (!OpenProcessToken(GetCurrentProcess(), TOKEN_ALL_ACCESS, out hToken))
            {
                return false;
            }

            token_privileges.Attributes = SE_PRIVILEGE_ENABLED;
            if (!LookupPrivilegeValue(null, SE_DEBUG_NAME, out token_privileges.Luid))
            {
                CloseHandle(hToken);
                return false;
            }

            CloseHandle(hToken);
            return true;
        }
    }
}
