using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace s2gsAutomationForms
{
    class S2GSExtractor
    {
        // Ascii: 's2gs  US'
        static byte[] magicBfr = { 0x73, 0x32, 0x67, 0x73, 0x00, 0x00, 0x55, 0x53 };


        MemoryReader reader;
        public S2GSExtractor()
        {
            reader = new MemoryReader();
        }

        public HashSet<String> extract()
        {

            HashSet<String> hashes = new HashSet<String>();
            int totalCount = 0;

            uint maxMemoryChunk = 10 * 1024 * 1024; // 10 megabytes
            byte[] buffer = new byte[maxMemoryChunk];
            while (true)
            {
                int bytesRead = reader.readNextMemoryRegion(buffer, maxMemoryChunk);
                if (0 == bytesRead)
                {
                    Console.WriteLine("Finished reading all regions");
                    break;
                }

                int n = bytesRead - 8;
                for (int i = 0; i < bytesRead; ++i)
                {
                    if ('s' == buffer[i] && isMagic(buffer, i))
                    {
                        totalCount += 1;
                        hashes.Add(BitConverter.ToString(buffer, i + 8, 32).ToLower().Replace("-", ""));
                    }
                }
           }

            Console.WriteLine("Number of unique s2gs hash strings found: " + hashes.Count());
            Console.WriteLine("Number of total s2gs hash strings found: " + totalCount);

            return hashes;
        }

        static bool isMagic(byte[] buffer, int i)
        {
            for (int j = 0; j < 8; ++j)
            {
                if (buffer[i + j] != magicBfr[j])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
