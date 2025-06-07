namespace Roklem_Migrator.Services.Interfaces
{
    internal interface ISpinnerService
    {
        public void StartSpinner(string spinnerMessage, string completeMessage);
        public void StopSpinner();
    }
}
