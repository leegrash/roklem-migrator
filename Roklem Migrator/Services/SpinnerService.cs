using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class SpinnerService : ISpinnerService
    {
        private CancellationTokenSource? _cts;
        private Task? _spinnerTask;

        public void StartSpinner(string spinnerMessage, string completeMessage)
        {
            StopSpinner();

            _cts = new CancellationTokenSource();
            _spinnerTask = Task.Run(() => Spinner(_cts.Token, spinnerMessage, completeMessage));
        }

        public void StopSpinner()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _spinnerTask?.Wait();
                _cts.Dispose();
                _cts = null;
                _spinnerTask = null;
            }
        }

        private void Spinner(CancellationToken cancellationToken, string spinnerMessage, string completeMessage)
        {
            var spinner = new[] { '|', '/', '-', '\\' };
            int counter = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                Console.Write($"\r{spinnerMessage} {spinner[counter++ % spinner.Length]}");
                Thread.Sleep(100);
            }

            Console.Write($"\r{completeMessage}                                                                       \n");
        }
    }
}