// Services/ImageService.cs
using Data.DBContext;
using Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class ImageService : IImageService
{
    private readonly MyDbContext _db;
    private readonly IWebHostEnvironment _env;
    private readonly string _adminPwd;

    private const string PWD_ENV = "SECTION_ADMIN_PASSWORD";
    private const int CREATE_NEW = -1;

    public ImageService(MyDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
        _adminPwd = Environment.GetEnvironmentVariable(PWD_ENV) ?? throw new InvalidOperationException($"Env var {PWD_ENV} not set");
    }

    public async Task<IEnumerable<Image>> GetAllAsync()
        => await _db.Images.ToListAsync();

    public async Task<Image?> GetByIdAsync(int id)
        => await _db.Images.FindAsync(id);

    public async Task<Image> UpsertAsync(Image image, string password)
    {
        if (password != _adminPwd) 
            throw new UnauthorizedAccessException("Invalid password");

        if (image.Id == CREATE_NEW)
        {
            _db.Images.Add(image);
            await _db.SaveChangesAsync();
            return image;
        }

        var existing = await _db.Images.FindAsync(image.Id)
                       ?? throw new KeyNotFoundException("Image not found");

        existing.Url         = image.Url;
        existing.FileName    = image.FileName;
        existing.ContentType = image.ContentType;
        existing.SectionId   = image.SectionId;

        await _db.SaveChangesAsync();
        return existing;
    }

    public async Task DeleteAsync(int id, string password)
    {
        if (password != _adminPwd) 
            throw new UnauthorizedAccessException("Invalid password");

        var img = await _db.Images.FindAsync(id)
                  ?? throw new KeyNotFoundException("Image not found");

        // delete file from disk
        var relative = img.Url.TrimStart('/');
        var fullPath = Path.Combine(_env.WebRootPath, relative);
        if (File.Exists(fullPath))
            File.Delete(fullPath);

        _db.Images.Remove(img);
        await _db.SaveChangesAsync();
    }
}
