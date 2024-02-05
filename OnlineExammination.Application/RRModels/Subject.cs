using OnlineExammination.Domain.Entities;

namespace OnlineExammination.Application.RRModels
{
    public class SubjectRequest
    {
        public string Name { get; set; } = null!;
    }

    public class SubjectResponse:Subject
    {
    }

    public class UpdateSubjectRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
