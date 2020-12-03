using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace InternetTest
{
    class Program
    {
        static async Task Main()
        {
            Stopwatch stopwatch = new Stopwatch();
            using var pinger = new Pinger();

            while (true)
            {
                try
                {
                    stopwatch.Restart();
                    DateTime pingTime = DateTime.Now;

                    var pings = new List<Task<Result>>();
                    for (int i = 0; i < Settings.AttemptsNumber; ++i)
                    {
                        pings.Add(pinger.TryPing(Settings.GoogleDnsAddress));
                    }

                    var results = await Task.WhenAll(pings);
                    bool isPingSuccessful = results.Any(x => x.IsSuccess);

                    await LogPing(pingTime, isPingSuccessful);

                    stopwatch.Stop();
                    TimeSpan remainingDelayTime =
                        TimeSpan.FromSeconds(Settings.PingIntervalInSeconds) - stopwatch.Elapsed;
                    if (remainingDelayTime > TimeSpan.Zero)
                    {
                        await Task.Delay(remainingDelayTime);
                    }
                }
                catch (Exception ex)
                {
                    await FileLogger.LogException(ex);
                }
            }

            // ReSharper disable once FunctionNeverReturns
        }

        static async Task LogPing(DateTime pingTime, bool isPingSuccessful)
        {
            string stateRepresentation = isPingSuccessful ? "Online" : "Offline";
            string timeRepresentation = pingTime.ToString("T");
            string log = $"{timeRepresentation};{stateRepresentation}";

            LogToConsole(log, isPingSuccessful);
            await FileLogger.Log(log);
        }

        private static void LogToConsole(string log, bool isPingSuccessful)
        {
            if (isPingSuccessful)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            
            Console.WriteLine(log);
        }
    }
}