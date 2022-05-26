using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.Audio
{
    public class WaveFile
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        private struct Header
        {
            public string RIFF_FILE;
            public readonly int size;
            public string HEADER_TYPE;
            public string FORMAT_MARKER;
            public int FORMAT_LENGTH;
            public short PCM_TYPE;
            public short CHANNEL_COUNT;
            public readonly int sampleRate;
            public readonly int byteRate;
            public readonly short blockAlign;
            public readonly short bitsPerSample;
            public string DATA_SECTION;
            public readonly int dataSectionSize;
            public Header(int sampleRate)
            {
                RIFF_FILE = "RIFF";
                HEADER_TYPE = "WAVE";
                FORMAT_MARKER = "fmt ";
                FORMAT_LENGTH = 16;
                PCM_TYPE = 1;
                CHANNEL_COUNT = 1;
                DATA_SECTION = "data";
                this.size = (sampleRate * sizeof(short)) + Unsafe.SizeOf<Header>();
                this.sampleRate = sampleRate;
                bitsPerSample = sizeof(short) * 8;
                byteRate = (sampleRate * bitsPerSample * CHANNEL_COUNT) / 8;
                blockAlign = (short)((bitsPerSample * CHANNEL_COUNT) / 8);
                dataSectionSize = (bitsPerSample / 8) * sampleRate;
            }
        }

        public static byte[] GetHeaderBytes()
        {
            byte[] headerBytes = new byte[44];
            var header = new Header(44100);
            var writer = new BinaryWriter(new MemoryStream(headerBytes));
            writer.Write(Encoding.ASCII.GetBytes(header.RIFF_FILE));
            writer.Write(header.size);
            writer.Write(Encoding.ASCII.GetBytes(header.HEADER_TYPE));
            writer.Write(Encoding.ASCII.GetBytes(header.FORMAT_MARKER));
            writer.Write(header.FORMAT_LENGTH);
            writer.Write(header.PCM_TYPE);
            writer.Write(header.CHANNEL_COUNT);
            writer.Write(header.sampleRate);
            writer.Write(header.byteRate);
            writer.Write(header.blockAlign);
            writer.Write(header.bitsPerSample);
            writer.Write(Encoding.ASCII.GetBytes(header.DATA_SECTION));
            writer.Write(header.dataSectionSize);
            return headerBytes;
            //Array.Copy(Encoding.ASCII.GetBytes(header.RIFF_FILE), headerBytes, 4);
        }
    }
}
