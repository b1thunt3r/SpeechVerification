using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SpeechVerification.Extensions
{
    public static class FileExtensions
    {
        public static FileInfo ToWave(this FileInfo tempFile)
        {
            var newFile = new FileInfo($"{tempFile.FullName}.wav");

            var proc = new Process();
            proc.StartInfo.FileName = "ffmpeg";
            proc.StartInfo.Arguments = $"-i {tempFile.FullName} -acodec pcm_s16le -ac 1 -ar 16000 {newFile.FullName}";
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.UseShellExecute = false;
            if (!proc.Start())
            {
                throw new Exception($"Error starting conversion of {tempFile.FullName}");
            }
            var reader = proc.StandardError;
            String line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            proc.Close();

            return newFile;
        }

        public static async Task<FileInfo> Upload(this IFormFile file)
        {
            var tempFile = new FileInfo(Path.GetTempFileName());
            if (file.Length > 0)
            {
                using (var stream = tempFile.OpenWrite())
                {
                    await file.CopyToAsync(stream);
                }
            }

            var wav = tempFile.ToWave();

            return wav;
        }
    }
}
