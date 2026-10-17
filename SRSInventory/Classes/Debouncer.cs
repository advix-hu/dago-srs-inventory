namespace SRSInventory.Classes
{
    public sealed class Debouncer
    {
        private CancellationTokenSource? _cts;

        public void Debounce(int milliseconds, Action action)
        {
            _cts?.Cancel();

            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            _ = Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(milliseconds, token);

                    if (!token.IsCancellationRequested)
                    {
                        action();
                    }
                }
                catch (TaskCanceledException)
                {
                }
            });
        }
    }
}
