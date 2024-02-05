using OnlineExammination.Domain.Entities;

namespace OnlineExammination.Application.RRModels
{
    public class SemesterRequest
    {
        public int Sem { get; set; }
    }

    public class SemesterResponse:Semester
    {
    }
}
