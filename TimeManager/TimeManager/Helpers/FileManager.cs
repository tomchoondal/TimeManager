using System.Threading.Tasks;

namespace TimeManager.Helpers
{
    class FileManager
    {
        private string fileName;
        private string folderName;

        public FileManager(string fileName, string folderName = "BaseFolder")
        {
            this.fileName = fileName;
            this.folderName = folderName;
        }

        public async Task SetTextAsync(string content)
        {
            await StorageHelper.WriteToFileAsync(content, fileName, folderName);
        }

        public async Task<string> GetTextAsync()
        {
            return await StorageHelper.ReadTextFromFileAsync(fileName, folderName);
        }
    }
}
