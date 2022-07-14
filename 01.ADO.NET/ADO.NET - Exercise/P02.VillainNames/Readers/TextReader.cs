namespace P02.VillainNames.Readers
{
    using System.IO;

    internal class TextReader : IFilePathReader
    {
        public TextReader(string fileNmae)
        {
            FileName = fileNmae;
        }

        public string FileName { get; set; }

        public string Read()
        {
            string currDirectoryPath = Directory.GetCurrentDirectory();
            string querryfullPath = Path.Combine(currDirectoryPath, $"..\\..\\..\\Queries/{this.FileName}");

            return File.ReadAllText(querryfullPath);
        }
    }
}
