namespace Roklem_Migrator.Services.Interfaces
{
    internal interface ISpinnerService
    {
        void ShowSpinner(CancellationToken cancellationToken, string spinnerMessage);
    }
}
