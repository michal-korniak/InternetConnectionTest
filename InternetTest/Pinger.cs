using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace InternetTest
{
    public class Pinger : IDisposable
    {
        private readonly Ping _pinger;

        public Pinger()
        {
            _pinger = new Ping();
        }

        public async Task<Result> TryPing(string address)
        {
            using var pinger = new Ping();
            try
            {
                PingReply reply =
                    await pinger.SendPingAsync(address, Settings.PingTimeoutInSeconds * 1000);
                return Result.SuccessIf(reply.Status == IPStatus.Success, "Fail");
            }
            catch (PingException)
            {
                return Result.Failure("Exception");
            }
        }

        public void Dispose()
        {
            _pinger?.Dispose();
        }
    }
}