namespace TechZone.Services
{
    using Data;
    using System.IO;
    using System.Threading.Tasks;
    using Dropbox.Api;
    using Dropbox.Api.Files;
    using Dropbox.Api.Stone;

    public abstract class Service
    {
        protected Service()
        {
            this.Context = new TechZoneContext();
        }

        protected TechZoneContext Context { get; }

        protected void Upload(DropboxClient dbx, string folder, string file, byte[] content)
        {
            using (var mem = new MemoryStream(content))
            {
                var updated = dbx.Files.UploadAsync(
                    folder + "/" + file,
                    WriteMode.Overwrite.Instance, 
                    body: mem);
                updated.Wait();
            }
        }
        protected Task<byte[]> DownloadAsync(DropboxClient dbx, string folderName, string fileName)
        {
            Task<IDownloadResponse<FileMetadata>> downloadTask = dbx.Files.DownloadAsync($"/{folderName}/{fileName}");
            downloadTask.Wait();
            return downloadTask.Result.GetContentAsByteArrayAsync();
        }
    }
}