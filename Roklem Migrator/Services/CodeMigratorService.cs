using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class CodeMigratorService : ICodeMigratorService
    {
        private readonly IInvokeAzureAIRequestResponseService _InvokeAzureAIRequestResponseService;
        private readonly IFileLocatorService _FileLocatorService;

        public CodeMigratorService(IInvokeAzureAIRequestResponseService invokeAzureAIRequestResponseService, IFileWriterService fileWriterService, IFileLocatorService fileLocator) { 
            _InvokeAzureAIRequestResponseService = invokeAzureAIRequestResponseService;
            _FileLocatorService = fileLocator;
        }

        public bool Migrate(string filePath)
        {
            try
            {
                List<string> files = _FileLocatorService.locateFiles(filePath);

                Console.WriteLine("Files located:");
                _FileLocatorService.printFileList(files);

                List<string> fileTypes = _FileLocatorService.getFileTypes(files);

                Console.WriteLine("\nFile types:");
                _FileLocatorService.printFileList(fileTypes);

                //var response = _InvokeAzureAIRequestResponseService.InvokeRequestResponse(codeLines).Result;
                //return response;
                return true;
            }
            catch
            {
                throw new Exception("Migration Error");
            }
        }
    }
}
