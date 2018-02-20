using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleRequestSender
{
    public class RequestSender : IDisposable
    {
        public RequestSender(List<string> urls, uint userCount = 5)
        {
            _userCount = userCount;
            _urls = urls;
        }

        public void Run()
        {
            Console.WriteLine($"Starting {_userCount} users ...");
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < _userCount; i++)
            {
                tasks.Add(StartUserAsync(i, _cts.Token));
                Thread.Sleep(3);
            }
            Console.WriteLine($"{_userCount} users started.");
            Task.WaitAll(tasks.ToArray());
        }

        public void Dispose()
        {
            _cts.Cancel();
        }

        #region Private
        private async Task StartUserAsync(int userNum, CancellationToken cancellationToken)
        {
            ulong cnt = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var url in _urls)
                {
                    try
                    {
                        await BlockAsync().ConfigureAwait(false);
                        cnt = (cnt + 1) % (ulong.MaxValue - 100);
                        var client = new HttpClient();
                        var rst = await client.GetAsync(url).ConfigureAwait(false);
                        Log($"[User {userNum}: Req# {cnt}] Status code: {rst.StatusCode} Url: {url}. ");
                    }
                    catch { }
                }
            }
        }

        private async Task BlockAsync()
        {
            while (true)
            {
                var minute = DateTime.Now.Minute;
                if ((minute / 15) % 2 == 0)
                {
                    return;
                }
                Log("Blocking");
                await Task.Delay(10000).ConfigureAwait(false);
            }
        }

        private void Log(string msg)
        {
            lock (this)
            {
                var now = DateTimeOffset.UtcNow;
                if (now - _lastLogTime > TimeSpan.FromMilliseconds(500))
                {
                    _lastLogTime = now;
                    Console.WriteLine(msg);
                }
            }
        }

        private readonly uint _userCount;
        private readonly List<string> _urls;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private DateTimeOffset _lastLogTime = DateTimeOffset.MinValue;
        #endregion
    }
}
