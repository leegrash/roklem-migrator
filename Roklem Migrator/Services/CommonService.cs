using Roklem_Migrator.Services.Interfaces;

namespace Roklem_Migrator.Services
{
    internal class CommonService : ICommonService
    {
        public void printList(List<string> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
    }
}
