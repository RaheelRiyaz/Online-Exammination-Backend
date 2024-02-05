using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;

namespace OnlineExammination.Application.Abstractions.IServices
{
    public interface ISubjectService
    {
        Task<ApiResponse<IEnumerable<SubjectResponse>>> Subjects();
        Task<ApiResponse<IEnumerable<SubjectResponse>>> SerachSubjects(string term);
        Task<ApiResponse<SubjectResponse>> SubjectById(Guid id);
        Task<ApiResponse<SubjectResponse>> Delete(Guid id);
        Task<ApiResponse<SubjectResponse>> AddSubject(SubjectRequest model);
    }
}
