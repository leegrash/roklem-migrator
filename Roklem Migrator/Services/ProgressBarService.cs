using Roklem_Migrator.Services.Interfaces;
using System.Text;

namespace Roklem_Migrator.Services
{
    internal class ProgressBarService: IProgressBarService
    {
        private const int _barWidth = 20;
        private const char _progressChar = '█';

        public void DisplayProgress(int currentStep, int totalSteps)
        {
            if (currentStep > totalSteps) currentStep = totalSteps;

            double progressRatio = (double)currentStep / totalSteps;
            int completedUnits = (int)(progressRatio * _barWidth);

            StringBuilder progressBar = new StringBuilder();
            progressBar.Append('[');
            progressBar.Append(new string(_progressChar, completedUnits));
            progressBar.Append(new string(' ', _barWidth - completedUnits));
            progressBar.Append($"] {progressRatio * 100:F1}%");

            Console.Write($"\r{progressBar}");
        }
    }
}
