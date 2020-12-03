using System;
using System.IO;
using System.Threading.Tasks;

namespace InternetTest
{
    public static class FileLogger
    {
        public static async Task Log(string log)
        {
            string fileName = $"{DateTime.Now.Date.ToString("yyyy-MM-dd")}.log";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            using (var streamWriter = new StreamWriter(filePath, append: true))
            {
                await streamWriter.WriteLineAsync(log);
            }
        }

        public static async Task LogException(Exception ex)
        {
            string fileName = "errors.log";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            using (var streamWriter = new StreamWriter(filePath, append: true))
            {
                await streamWriter.WriteLineAsync($"{DateTime.Now};{ex}");
            }
        }
    }
}