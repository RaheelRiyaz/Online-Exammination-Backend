using OnlineExammination.Application.Abstractions.IRepository;
using OnlineExammination.Domain.Entities;
using OnlineExammination.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExammination.Persistence.Repository
{
    public class GalleryRepository : BaseRepository<AppFile>,IGalleryrepository
    {
        public GalleryRepository(OnlineExamminationDbContext context) : base(context) { }
    }
}
