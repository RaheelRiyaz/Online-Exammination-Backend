using AutoMapper;
using OnlineExammination.Application.Abstractions.IRepository;
using OnlineExammination.Application.Abstractions.IServices;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;
using OnlineExammination.Domain.Entities;
using OnlineExammination.Domain.Enums;
using System.Net;

namespace OnlineExammination.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdmin repository;
        private readonly IMapper mapper;
        private readonly IExamRepository examRepository;
        private readonly IResultRepository resultRepository;

        public AdminService(IAdmin repository, IMapper mapper, IExamRepository examRepository,IResultRepository resultRepository)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.examRepository = examRepository;
            this.resultRepository = resultRepository;
        }




        public async Task<ApiResponse<PaperResponse>> AddPaper(PaperRequest model)
        {
            var exam=await examRepository.GetCompactExamById(model.ExamId);
            var examQuestions=await repository.GetQuestionPaperByExamId(model.ExamId);
            if (exam is not null && exam.TotalNoOfQuestions == examQuestions.Count()) return new ApiResponse<PaperResponse>() { Message = "Total No Of Questions in this Exam Exceeded" };


            var paper = mapper.Map<QuestionPaper>(model);
            var res = await repository.AddPaper(paper);
            if (res > 0)
            {
                return new ApiResponse<PaperResponse>()
                {
                    IsSuccess = true,
                    Message = "Paper added",
                    Result = mapper.Map<PaperResponse>(paper),
                    StatusCode = HttpStatusCode.OK
                };
            }
            return new ApiResponse<PaperResponse>()
            {
                Message = "There is some error",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }





        public async Task<ApiResponse<ProgramResponse>> AddProgram(ProgramRequest model)
        {
            var program = mapper.Map<Program>(model);
            var res = await this.repository.AddProgram(program);
            if (res > 0)
            {
                return new ApiResponse<ProgramResponse>()
                {
                    IsSuccess = true,
                    Message = "Program Added",
                    Result = mapper.Map<ProgramResponse>(program),
                    StatusCode = HttpStatusCode.Created
                };
            }
            return new ApiResponse<ProgramResponse>()
            {
                Message = "There is some error",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }






        public async Task<ApiResponse<SemesterResponse>> AddSemster(SemesterRequest model)
        {
            var sem = mapper.Map<Semester>(model);
            var res = await this.repository.AddSemester(sem);
            if (res > 0)
            {
                return new ApiResponse<SemesterResponse>()
                {
                    IsSuccess = true,
                    Message = "Semester Added",
                    Result = mapper.Map<SemesterResponse>(sem),
                    StatusCode = HttpStatusCode.Created
                };
            }
            return new ApiResponse<SemesterResponse>()
            {
                Message = "There is some error",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }





        public async Task<ApiResponse<int>> ConductExam(Guid examId)
        {
            var exam = await examRepository.GetCompactExamById(examId);
            if (exam?.StartDate < DateTime.Now || exam?.EndDate < DateTime.Now) return new ApiResponse<int>() { Message = "Cannot conduct exam after or before due date" };


            var res = await repository.ConductExam(examId);
            if (res > 0)
            {
                return new ApiResponse<int>()
                {
                    IsSuccess = true,
                    Message = "Exam status Changed",
                    StatusCode = HttpStatusCode.OK,
                    Result = res
                };
            }


            return new ApiResponse<int>()
            {
                Message = "There is some error",
                StatusCode = HttpStatusCode.InternalServerError,
                Result = -1
            };
        }





        public async Task<ApiResponse<PreviousPaperResponse>> GetAllPreviousPapers(Guid examId,Guid semesterId)
        {
            var res = await repository.GetAllPreviousPapers(examId,semesterId);
            if (res.Any())
            {
                var response = new PreviousPaperResponse()
                {
                    Batch = res.First().Batch,
                    ExamId = examId,
                    PaperTitle = res.First().PaperTitle,
                    ProgramName = res.First().ProgramName,
                    Semester = res.First().Semester,
                    StartDate = res.First().StartDate,
                    SubjectName = res.First().SubjectName,
                    Questions = res.Select(_ => new PreviousQuestionPaper()
                    {
                        Question = _.Question,
                        Id=_.PaperId,
                        OptionA = _.OptionA,
                        OptionB = _.OptionB,
                        OptionC = _.OptionC,
                        OptionD = _.OptionD
                    })
                };

                return new ApiResponse<PreviousPaperResponse>()
                {
                    IsSuccess = true,
                    Message = $"Found {res.Count()} Papers",
                    Result = response,
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new ApiResponse<PreviousPaperResponse>()
            {
                Message = $"No Papers Found",
                StatusCode = HttpStatusCode.NotFound
            };
        }





        public async Task<ApiResponse<IEnumerable<ProgramResponse>>> GetProgramList()
        {
            var programs = await repository.GetPrograms();
            if (programs.Any())
            {
                return new ApiResponse<IEnumerable<ProgramResponse>>()
                {
                    IsSuccess = true,
                    Message = $"Found {programs.Count()} programs",
                    Result = programs.Select(_ => new ProgramResponse()
                    {
                        CeatedOn = _.CeatedOn,
                        Id = _.Id,
                        Name = _.Name,
                        UpdatedOn = _.UpdatedOn
                    }),
                    StatusCode = HttpStatusCode.OK
                };
            }
            return new ApiResponse<IEnumerable<ProgramResponse>>()
            {
                Message = "No Program Found",
                StatusCode = HttpStatusCode.NotFound
            };
        }






        public async Task<ApiResponse<IEnumerable<PaperResponse>>> GetQuestionPaperByExamId(Guid id)
        {
            var res = await repository.GetQuestionPaperByExamId(id);
            if (res.Any())
            {
                return new ApiResponse<IEnumerable<PaperResponse>>()
                {
                    IsSuccess = true,
                    Message = "Success",
                    Result = res,
                    StatusCode = HttpStatusCode.OK
                };
            }
            return new ApiResponse<IEnumerable<PaperResponse>>()
            {
                StatusCode = HttpStatusCode.NotFound
            };
        }





        public async Task<ApiResponse<StudentPaperResponse>> GetQuestionPaperForStudentByExamId(Guid id)
        {
            var exam = await examRepository.GetCompactExamById(id);
            if (exam?.EndDate < DateTime.Now) return new ApiResponse<StudentPaperResponse>() { Message = "Exam has been ended", StatusCode = HttpStatusCode.BadRequest };

            var res = await repository.GetQuestionPaperForStudentByExamId(id);
            if (res.Any())
            {
                if (exam?.StartDate < DateTime.Now)
                {
                    var response = new StudentPaperResponse()
                    {
                        Batch = exam.Batch,
                        ExamDuration = exam.ExamDuration,
                        ExamId = exam.Id,
                        PaperTitle = exam.Description,
                        EachQuestionMarks = exam.EachQuestionMarks,
                        ExamTotalMarks = exam.ExamTotalMarks,
                        ProgramName = res.First().ProgramName,
                        Semester = res.First().Semester,
                        StartDate = exam.StartDate,
                        SubjectName = res.First().SubjectName,
                        TotalNoOfQuestions = exam.TotalNoOfQuestions,
                        ExamPassMarks = exam.ExamPassMarks,
                        Questions = res.Select(_ => new PreviousQuestionPaper()
                        {
                            Question = _.Question,
                            Id = _.PaperId,
                            OptionA = _.OptionA,
                            OptionB = _.OptionB,
                            OptionC = _.OptionC,
                            OptionD = _.OptionD
                        })
                    };
                    return new ApiResponse<StudentPaperResponse>()
                    {
                        IsSuccess = true,
                        Message = $"Found {res.Count()} Questions",
                        Result = response,
                        StatusCode = HttpStatusCode.OK
                    };
                }

                return new ApiResponse<StudentPaperResponse>()
                {
                    Message = $"Exam will start on {res.First().StartDate}",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            return new ApiResponse<StudentPaperResponse>()
            {
                Message = "No Paper Found",
                StatusCode = HttpStatusCode.NotFound
            };
        }




        public async Task<ApiResponse<IEnumerable<SemesterResponse>>> GetSemesterList()
        {
            var sems = await repository.GetSemesters();
            if (sems.Any())
            {
                return new ApiResponse<IEnumerable<SemesterResponse>>()
                {
                    IsSuccess = true,
                    Message = $"Found {sems.Count()} semesters",
                    Result = sems.Select(_ => new SemesterResponse()
                    {
                        CeatedOn = _.CeatedOn,
                        Id = _.Id,
                        Sem = _.Sem,
                        UpdatedOn = _.UpdatedOn
                    }),
                    StatusCode = HttpStatusCode.OK
                };
            }
            return new ApiResponse<IEnumerable<SemesterResponse>>()
            {
                Message = "No Semester Found",
                StatusCode = HttpStatusCode.NotFound
            };
        }





        public async Task<ApiResponse<IEnumerable<StudentResult>>> GetStudentResultByRegNumber(ResultRequest model)
        {
                var res = await repository.GetStudentResultByRegNo(model);
                if (res.Any())
                {
                    return new ApiResponse<IEnumerable<StudentResult>>()
                    {
                        IsSuccess = true,
                        Message = "Result Found",
                        Result = res,
                        StatusCode = HttpStatusCode.OK
                    };
                }
            return new ApiResponse<IEnumerable<StudentResult>>()
            {
                Message = "No result Found",
                StatusCode = HttpStatusCode.NotFound
            };
        }



        public async Task<ApiResponse<IEnumerable<ProgramResponse>>> SearchPrograms(string term)
        {
            var programs = await repository.SearchPrograms(term);

            return new ApiResponse<IEnumerable<ProgramResponse>>()
            {
                IsSuccess = true,
                Message = $"Found {programs.Count()} Programs",
                Result = mapper.Map<IEnumerable<ProgramResponse>>(programs),
                StatusCode = HttpStatusCode.OK
            };
        }




        public async Task<ApiResponse<IEnumerable<SemesterResponse>>> SearchSemesters(int sem)
        {
            return new ApiResponse<IEnumerable<SemesterResponse>>()
            {
                IsSuccess = true,
                Message = $"Success",
                Result = await repository.SearchSemesters(sem),
                StatusCode = HttpStatusCode.OK
            };
        }




        public async Task<ApiResponse<int>> SubmitStudentResult(SubmitPaper model)
        {
            var res = await repository.GetQuestionPaperByExamId(model.ExamId);
            var exam = await examRepository.GetById(model.ExamId);
            var result = await repository.CheckIfPaperIsALreadySubmittedByStudent(model.EntityId,model.ExamId);

            if (result is not null) return new ApiResponse<int>() { Message = "Paper ALready Submitted", StatusCode = HttpStatusCode.AlreadyReported };
            if (exam is null) return new ApiResponse<int>() { Message = "No such exam found" };
            if (exam.EndDate < DateTime.Now) return new ApiResponse<int>() { Message = "Time is over cannot submit paper now", StatusCode = HttpStatusCode.BadRequest };

            int marks = 0;
            if (res.Any())
            {
                foreach (var question in model.QuestionAnswers)
                {
                    var questionAnswerInExam = res.FirstOrDefault(_ => _.Id == question.QuestionId);
                    if (question.Answer == questionAnswerInExam?.CorrectOption) marks++;
                }

                var studentResult = new Result()
                {
                    EntityId = model.EntityId,
                    ExamId = model.ExamId,
                    Marks = marks,
                    ResultStatus = marks >= exam.ExamPassMarks ? ResultStatus.Pass : ResultStatus.Fail,
                    TotalAttempts = model.QuestionAnswers.Count()
                };

                var stdresult = await repository.SubmitStudentResult(studentResult);
                if (stdresult > 0)
                {
                    return new ApiResponse<int>()
                    {
                        IsSuccess = true,
                        Message = "Paper Submitted Successfully",
                        StatusCode = HttpStatusCode.OK
                    };
                }

                return new ApiResponse<int>()
                {
                    Message = "There is some issue",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }


            return new ApiResponse<int>()
            {
                Message = "No Such Exam Found",
                StatusCode = HttpStatusCode.NotFound,
                Result = -1
            };
        }

        public async Task<ApiResponse<int>> UploadResult(UploadResultResquest model)
        {
            var result = await resultRepository.GetResultByExamId(model.ExamId);
            if (result is null) return new ApiResponse<int>() { Message = "No Result Found" ,StatusCode=HttpStatusCode.NotFound};
            if (result.IsReleased==1) return new ApiResponse<int>() { Message = "Result Already uploaded" ,StatusCode=HttpStatusCode.AlreadyReported};
            var res = await resultRepository.UploadResult(model.ExamId);
            if (res > 0)
            {
                return new ApiResponse<int>()
                {
                    IsSuccess = true,
                    Message = "Result Uploaded Successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new ApiResponse<int>()
            {
                Message = "There is some error ",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}
