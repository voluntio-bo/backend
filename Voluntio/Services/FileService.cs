using Azure.Storage.Blobs;
using ImageMagick;
using Voluntio.Exceptions;

namespace Voluntio.Services
{
    public class FileService : IFileService
    {
        private readonly BlobServiceClient _blobServiceClient;
        public FileService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        private IFormFile compressImage(IFormFile file)
        {
            long length = file.Length;
            using var fileStream = file.OpenReadStream();
            byte[] bytes = new byte[length];
            fileStream.Read(bytes, 0, (int)file.Length);
            var compressedImage = new MagickImage();

            const int size = 750;
            const int quality = 85;

            compressedImage = new MagickImage(bytes);

            compressedImage.Resize(size, size);
            compressedImage.Strip();
            compressedImage.Quality = quality;

            byte[] data = compressedImage.ToByteArray();
            var stream = new MemoryStream(data);
            IFormFile compressedFile = new FormFile(stream, 0, data.Length, file.Name, file.FileName);
            //compressedFile.
            return compressedFile;
        }

        private bool checkDimensions(IFormFile file)
        {
            long length = file.Length;
            return length < 15000;
        }

        public async Task<string> UploadFile(IFormFile fileB)
        {

            /*var blobContainer = _blobServiceClient.GetBlobContainerClient("upload-file");

            var blobClient = blobContainer.GetBlobClient(file.FileName);

            await blobClient.UploadAsync(file.OpenReadStream());*/
            bool tooSmall = checkDimensions(fileB);
            if (tooSmall)
            {
                throw new InvalidImageException($"Dimensions for image are too small. Dimensions must be more than 200 x 200. Recommended size is 750 x 750");
            }
            var file = compressImage(fileB);

            //aqui hay que cambiar el path para que se pueda guardar en el container correspondiente en vez de images debería ser un parámetro?
            string pathToSave = "https://donationappimages.blob.core.windows.net/images/";
            string extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid().ToString()}{extension}";
            //var fileName = file.FileName;
            var imageFilePath = Path.Combine(pathToSave, fileName);

            try
            {
                var blobContainer = _blobServiceClient.GetBlobContainerClient("images");
                var blobClient = blobContainer.GetBlobClient(fileName);
                await blobClient.UploadAsync(file.OpenReadStream());
            }
            catch
            {
                throw new FileLoadException("The API could not connect with the blob container service. Try again. If the problem persist contact technical support.");
            }

            return imageFilePath;
            //return $"https://donationappimages.blob.core.windows.net/images/{file.FileName}";
        }
        
    }
}
