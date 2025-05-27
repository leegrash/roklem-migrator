using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roklem_Migrator.Services.Interfaces
{
    internal interface IProgressBarService
    {
        public void DisplayProgress(int currentStep, int totalSteps);
        public void stopProgressBar(string message);
    }
}
