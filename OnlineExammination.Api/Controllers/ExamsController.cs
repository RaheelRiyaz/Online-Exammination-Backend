using Microsoft.AspNetCore.Mvc;
using OnlineExammination.Application.Abstractions.IServices;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;
using OnlineExammination.Domain.Entities;

namespace OnlineExammination.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService service;

        public ExamsController(IExamService service)
        {
            this.service = service;
        }


        [HttpGet]
        public async Task<ApiResponse<IEnumerable<ExamResponse>>> GetExams()
        {
            return await service.GetCompactExams();
        }


        [HttpGet("search/{name}")]
        public async Task<ApiResponse<IEnumerable<ExamResponse>>> SearchExams(string name)
        {
            return await service.SearchExams(name);
        }


        [HttpGet("{examId:guid}")]
        public async Task<ApiResponse<ExamResponse>> GetExamById(Guid examId)
        {
            return await service.GetCompactExamById(examId);
        }


        [HttpPost()]
        public async Task<ApiResponse<Exam>> AddExam(ExamRequest model)
        {
            return await service.AddNewExam(model);
        }


        [HttpPut()]
        public async Task<ApiResponse<ExamResponse>> UpdateExam(UpdateExamRequest model)
        {
            return await service.UpdateExam(model);
        }

    }
}
