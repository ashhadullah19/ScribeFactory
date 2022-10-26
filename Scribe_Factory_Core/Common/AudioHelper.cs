using Amazon.Polly.Model;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Scribe_Factory_Core.Common
{
    public class AudioHelper
    {
        public async Task<bool> SaveMp3(SynthesizeSpeechResponse response, string path)
        {
            try
            {
                Stream audioStream = response.AudioStream;

                using (MemoryStream ms = new MemoryStream())
                using (FileStream fs = new FileStream(path,
                       FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    audioStream.CopyTo(ms);

                    byte[] buffer = ms.ToArray();

                    await audioStream.ReadAsync(buffer, 0, buffer.Length);

                    await fs.WriteAsync(buffer, 0, buffer.Length);

                    fs.Flush();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
        public void ConvertMp3ToWav(string _inPath_, string _outPath_)
        {
            using (Mp3FileReader mp3 = new Mp3FileReader(_inPath_))
            {
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                {
                    WaveFileWriter.CreateWaveFile(_outPath_, pcm);
                }
            }
        }
    }
}
