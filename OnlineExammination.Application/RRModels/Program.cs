using OnlineExammination.Domain.Entities;

namespace OnlineExammination.Application.RRModels
{
    public class ProgramRequest
    {
        public string Name { get; set; } = null!;
    }

    public class ProgramResponse:Program
    {
        
    }
}
