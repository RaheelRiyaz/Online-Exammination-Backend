using Microsoft.AspNetCore.Mvc;
using OnlineExammination.Application.Abstractions.IServices;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;

namespace OnlineExammination.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService service;

        public SubjectsController(ISubjectService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<SubjectResponse>>> GetSubjects()
        {
            return await service.Subjects();
        }


        [HttpGet("{term}")]
        public async Task<ApiResponse<IEnumerable<SubjectResponse>>> SearchSubjects(string term)
        {
            return await service.SerachSubjects(term);
        }


        [HttpGet("{id:guid}")]
        public async Task<ApiResponse<SubjectResponse>> GetSubjectById(Guid id)
        {
            return await service.SubjectById(id);
        }


        [HttpDelete("{id:guid}")]
        public async Task<ApiResponse<SubjectResponse>> Delete(Guid id)
        {
            return await service.Delete(id);
        }

        [HttpPost]
        public async Task<ApiResponse<SubjectResponse>> AddSubject(SubjectRequest model)
        {
            return await service.AddSubject(model);
        }
    }
}
