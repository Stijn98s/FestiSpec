using System.Threading.Tasks;
using FestiDB.Domain;

namespace FestiApp.persistence
{
    public interface IPictureRepository
    {
        Task SyncAsync();
        Task UploadFileAsync(string filePath, DrawQuestion drawQuestion);
    }
}