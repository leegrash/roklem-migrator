using Roklem_Migrator.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Roklem_Migrator.Services
{
    internal class SpinnerService : ISpinnerService
    {
        public void ShowSpinner(CancellationToken cancellationToken, string spinnerMessage)
        {
            var spinner = new[] { '|', '/', '-', '\\' };
            int counter = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                Console.Write($"\r{spinnerMessage} {spinner[counter++ % spinner.Length]}");
                Thread.Sleep(100);
            }

            Console.Write("\rDone!                                                           \n");
        }
    }
}