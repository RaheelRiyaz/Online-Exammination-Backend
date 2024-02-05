using AutoMapper;
using OnlineExammination.Application.Abstractions.IRepository;
using OnlineExammination.Application.Abstractions.IServices;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;
using OnlineExammination.Domain.Entities;
using System.Net;

namespace OnlineExammination.Application.Services
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository repository;
        private readonly IMapper mapper;

        public ExamService(IExamRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<Exam>> AddNewExam(ExamRequest model)
        {
            var exam = mapper.Map<Exam>(model);
            var res = await repository.Add(exam);

            if (res > 0)
            {
                return new ApiResponse<Exam>()
                {
                    IsSuccess = true,
                    Message = $"Exam Added Successfully",
                    Result = exam,
                    StatusCode = HttpStatusCode.OK
                };
            }


            return new ApiResponse<Exam>()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Some issue"
            };
        }

        public async Task<ApiResponse<ExamResponse>> GetCompactExamById(Guid id)
        {
            var exam = await repository.GetCompactExamById(id);
            if (exam is not null)
            {
                return new ApiResponse<ExamResponse>()
                {
                    IsSuccess = true,
                    Message = $"Exam found",
                    Result = exam,
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new ApiResponse<ExamResponse>()
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "No exam found"
            };
        }

        public async Task<ApiResponse<IEnumerable<ExamResponse>>> GetCompactExams()
        {
            var exams = await repository.GetCompactExams();
            if (exams.Any())
            {
                return new ApiResponse<IEnumerable<ExamResponse>>()
                {
                    IsSuccess = true,
                    Message = $"Found {exams.Count()} Exams",
                    Result = exams,
                    StatusCode = HttpStatusCode.OK
                };
            }


            return new ApiResponse<IEnumerable<ExamResponse>>()
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "No exam found"
            };
        }

        public async Task<ApiResponse<IEnumerable<ExamResponse>>> SearchExams(string term)
        {
            var exams = await repository.GetCompactExams();
            var filteredExams = exams.Where(_ => _.Name.ToLower().StartsWith(term.ToLower()));
            return new ApiResponse<IEnumerable<ExamResponse>>()
            {
                IsSuccess = true,
                Message = $"found {filteredExams.Count()} Exams",
                Result = mapper.Map<IEnumerable<ExamResponse>>(filteredExams),
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ApiResponse<ExamResponse>> UpdateExam(UpdateExamRequest model)
        {
            var oldExam = await repository.GetById(model.Id);
            if (oldExam is not null)
            {
                var exam = mapper.Map<Exam>(model);
                exam.CeatedOn = oldExam.CeatedOn;
                var res=await repository.Update(exam);
                if (res > 0)
                {
                    return new ApiResponse<ExamResponse>()
                    {
                        IsSuccess = true,
                        Message = $"Exam updated successfully",
                        Result = mapper.Map<ExamResponse>(exam),
                        StatusCode = HttpStatusCode.OK
                    };
                }

                return new ApiResponse<ExamResponse>()
                {
                    Message = $"There is some issue please try again later",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            else
            {
                return new ApiResponse<ExamResponse>()
                {
                    Message = "No exam found",
                    StatusCode = HttpStatusCode.NotFound,
                };
            }
        }
    }
}
