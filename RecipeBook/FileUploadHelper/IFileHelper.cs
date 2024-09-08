namespace RecipeBook.FileUploadHelper
{
    public interface IFileHelper
    {
        Task<string> SaveFileAsync(IFormFile imageFile);
        void DeleteFile(string fileNameWithExtension);
    }
}
