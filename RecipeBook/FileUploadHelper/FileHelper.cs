using System;

namespace RecipeBook.FileUploadHelper
{
    public class FileHelper : IFileHelper
    {
        private readonly IWebHostEnvironment _environment;
        public FileHelper(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<string> SaveFileAsync(IFormFile imageFile)
        {
            var contentPath = _environment.WebRootPath;
            var path = Path.Combine(contentPath, "images");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var ext = Path.GetExtension(imageFile.FileName);

            var fileName = $"{Guid.NewGuid().ToString()}{ext}";
            var fileNameWithPath = Path.Combine(path, fileName);
            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await imageFile.CopyToAsync(stream);
            return fileName;
        }


        public void DeleteFile(string fileNameWithExtension)
        {
            var contentPath = _environment.WebRootPath;
            var path = Path.Combine(contentPath, $"images", fileNameWithExtension);

            File.Delete(path);
        }
    }
}
