using PCLStorage;
using System.Threading.Tasks;

namespace TimeManager.Helpers
{
    public static class StorageHelper
    {
        public static async Task WriteToFileAsync(string content, string fileName, string folderName)
        {
            try
            {
                IFolder rootFolder = FileSystem.Current.LocalStorage;
                IFolder folder = await rootFolder.CreateFolderAsync(folderName,
                    CreationCollisionOption.OpenIfExists);
                IFile file = await folder.CreateFileAsync(fileName,
                    CreationCollisionOption.ReplaceExisting);
                await file.WriteAllTextAsync(content);
            }
            catch
            {
            }
        }

        public static async Task<string> ReadTextFromFileAsync(string fileName, string folderName)
        {
            try
            {
                IFolder rootFolder = FileSystem.Current.LocalStorage;
                IFolder folder = await rootFolder.CreateFolderAsync(folderName,
                    CreationCollisionOption.OpenIfExists);
                IFile file = await folder?.CreateFileAsync(fileName,
                    CreationCollisionOption.OpenIfExists);
                return await file?.ReadAllTextAsync();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
