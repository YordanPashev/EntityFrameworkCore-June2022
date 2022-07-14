using System.IO;

namespace P02.VillainNames.Readers
{
    internal class SQLQueryReader : IFilePathReader
    {
        public SQLQueryReader(string fileNmae)
        {
            FileName = fileNmae;
        }

        public string FileName { get; set; }

        public string ReadFileText()
        {
            string currDirectoryPath = Directory.GetCurrentDirectory();
            string querryfullPath = Path.Combine(currDirectoryPath, $"..\\..\\..\\Queries/{this.FileName}.sql");

            return File.ReadAllText(querryfullPath);
        }
    }
}
