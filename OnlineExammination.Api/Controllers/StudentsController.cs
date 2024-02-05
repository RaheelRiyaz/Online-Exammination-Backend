using Microsoft.AspNetCore.Mvc;
using OnlineExammination.Application.Abstractions.IServices;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;

namespace OnlineExammination.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IAdminService service;
        private readonly IFileService fileService;

        public StudentsController(IAdminService service,IFileService fileService)
        {
            this.service = service;
            this.fileService = fileService;
        }


        [HttpGet("my-paper/{examId:guid}")]
        public async Task<ApiResponse<StudentPaperResponse>> GetPaperForStudentByExamId(Guid examId)
        {
            return await this.service.GetQuestionPaperForStudentByExamId(examId);
        }

        [HttpPost("submit-paper")]
        public async Task<ApiResponse<int>> SubmitPaper(SubmitPaper model)
        {
            return await service.SubmitStudentResult(model);
        }

        [HttpPost("check-result")]
        public async Task<ApiResponse<IEnumerable<StudentResult>>> Checkresult(ResultRequest model)
        {
            return await service.GetStudentResultByRegNumber(model);
        }


        [HttpGet("all-previous-questions/{examId:guid}/{semesterId:guid}")]
        public async Task<ApiResponse<PreviousPaperResponse>> GetAllPreviousPapers(Guid examId,Guid semesterId)
        {
            return await service.GetAllPreviousPapers(examId,semesterId);
        }

        [HttpPost("upload-profile")]
        public async Task<ApiResponse<AppFileResponse>> UploadProfile([FromForm] AppFileRequst model)
        {
            return await fileService.UploadFileAsync(model);
        }
    }
}
