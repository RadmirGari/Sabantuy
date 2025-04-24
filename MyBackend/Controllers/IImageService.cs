// Services/IImageService.cs
using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IImageService
{
    Task<IEnumerable<Image>> GetAllAsync();
    Task<Image?> GetByIdAsync(int id);
    Task<Image> UpsertAsync(Image image, string password);
    Task DeleteAsync(int id, string password);
}
