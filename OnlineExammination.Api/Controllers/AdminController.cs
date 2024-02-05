using Microsoft.AspNetCore.Mvc;
using OnlineExammination.Application.Abstractions.IServices;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;

namespace OnlineExammination.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService service;

        public AdminController(IAdminService service)
        {
            this.service = service;
        }
        [HttpPost("program")]
        public async Task<ApiResponse<ProgramResponse>> AddProgram(ProgramRequest model)
        {
            return await this.service.AddProgram(model);
        }

        [HttpPost("semester")]
        public async Task<ApiResponse<SemesterResponse>> AddSemester(SemesterRequest model)
        {
            return await this.service.AddSemster(model);
        }


        [HttpGet("programs")]
        public async Task<ApiResponse<IEnumerable<ProgramResponse>>> GetPrograms()
        {
            return await service.GetProgramList();
        }

        [HttpGet("programs/{term}")]
        public async Task<ApiResponse<IEnumerable<ProgramResponse>>> SearchPrograms(string term)
        {
            return await service.SearchPrograms(term);
        }
        [HttpGet("semesters")]
        public async Task<ApiResponse<IEnumerable<SemesterResponse>>> GetSemesters()
        {
            return await service.GetSemesterList();
        }


        [HttpGet("semesters/{sem}")]
        public async Task<ApiResponse<IEnumerable<SemesterResponse>>> SearchSemesters(int sem)
        {
            return await service.SearchSemesters(sem);
        }

        [HttpPost("add-paper")]
        public async Task<ApiResponse<PaperResponse>> Add(PaperRequest model)
        {
            return await this.service.AddPaper(model);
        }

        [HttpGet("paper/{examId:guid}")]
        public async Task<ApiResponse<IEnumerable<PaperResponse>>> GetPaperByExamId(Guid examId) 
        {
            return await this.service.GetQuestionPaperByExamId(examId);
        }



        [HttpPost("conduct-exam/{examId:guid}")]

        public async Task<ApiResponse<int>> ChangePaperStatus(Guid examId)
        {
            return await service.ConductExam(examId);
        }


        [HttpPost("upload-result")]

        public async Task<ApiResponse<int>> UploadResult(UploadResultResquest model)
        {
            return await service.UploadResult(model);
        }
    }
}
