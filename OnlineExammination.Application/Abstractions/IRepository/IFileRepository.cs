using OnlineExammination.Domain.Entities;

namespace OnlineExammination.Application.Abstractions.IRepository
{
    public interface IFileRepository:IBaseRepository<AppFile>
    {
        Task<AppFile?> GetFilePathByEntityId(Guid entityId);
        Task<IEnumerable<AppFile>> GetAllImages();
    }
}
