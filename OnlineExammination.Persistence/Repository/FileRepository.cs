using OnlineExammination.Application.Abstractions.IRepository;
using OnlineExammination.Domain.Entities;
using OnlineExammination.Persistence.Data;

namespace OnlineExammination.Persistence.Repository
{
    internal class FileRepository:BaseRepository<AppFile>,IFileRepository
    {
        public FileRepository(OnlineExamminationDbContext context):base(context)
        {
        }
        public async Task<AppFile?> GetFilePathByEntityId(Guid entityId)
        {
           return await context.FirstOrDefaulyAsync<AppFile>("SELECT * FROM AppFiles WHERE Entityid=@entityId", new { entityId });
        }

        public async Task<IEnumerable<AppFile>> GetAllImages()
        {
            string query = $@"SELECT * FROM AppFiles WHERE Module = 3";

            return await context.QueryAsync<AppFile>(query);
        }
    }
}
