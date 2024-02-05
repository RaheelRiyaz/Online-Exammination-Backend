using AutoMapper;
using OnlineExammination.Application.Abstractions.IRepository;
using OnlineExammination.Application.Abstractions.IServices;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;
using OnlineExammination.Domain.Entities;
using System.Net;

namespace OnlineExammination.Application.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository repository;
        private readonly IMapper mapper;

        public SubjectService(ISubjectRepository repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<SubjectResponse>> AddSubject(SubjectRequest model)
        {
            var isExists=await repository.IsExists(_=>_.Name==model.Name);
            if (isExists) return new ApiResponse<SubjectResponse>() { Message = "Subject Already Added" };


            var sub = mapper.Map<Subject>(model);
            var res = await repository.Add(sub);
            if (res > 0)
            {
                return new ApiResponse<SubjectResponse>()
                {
                    IsSuccess = true,
                    Message = "Subject Added Successfully",
                    Result = mapper.Map<SubjectResponse>(sub),
                    StatusCode = HttpStatusCode.Found
                };
            }

            return new ApiResponse<SubjectResponse>()
            {
                Message = "Some isue",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }

        public async Task<ApiResponse<SubjectResponse>> Delete(Guid id)
        {
            var subject = await repository.GetById(id);
            if(subject is null) return new ApiResponse<SubjectResponse>() { Message="No Subject Found",StatusCode=HttpStatusCode.NotFound};

            var res = await repository.Delete(subject);
            if (res > 0)
            {
                return new ApiResponse<SubjectResponse>()
                {
                    IsSuccess = true,
                    Message = "Subject Deleted Successfully",
                    Result = mapper.Map<SubjectResponse>(subject),
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new ApiResponse<SubjectResponse>()
            {
                Message = "There is some issue please try again later",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }

        public async Task<ApiResponse<IEnumerable<SubjectResponse>>> SerachSubjects(string term)
        {
            var subjects = await repository.Filter(_=>_.Name.StartsWith(term));

            return new ApiResponse<IEnumerable<SubjectResponse>>()
            {
                IsSuccess = true,
                Message = $"Found {subjects.Count()} subjects",
                Result = mapper.Map<IEnumerable<SubjectResponse>>(subjects),
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ApiResponse<SubjectResponse>> SubjectById(Guid id)
        {
            var subject = await repository.GetById(id);
            if(subject is not null)
            {
                return new ApiResponse<SubjectResponse>()
                {
                    IsSuccess = true,
                    Message = "Success",
                    Result = mapper.Map<SubjectResponse>(subject),
                    StatusCode = HttpStatusCode.Found
                };
            }

            return new ApiResponse<SubjectResponse>()
            {
                Message = "No subject Found",
                StatusCode = HttpStatusCode.NotFound
            };
        }


        public async Task<ApiResponse<IEnumerable<SubjectResponse>>> Subjects()
        {
            var subjects = await repository.GetAll();
            if (subjects.Any())
            {
                return new ApiResponse<IEnumerable<SubjectResponse>>()
                {
                    IsSuccess = true,
                    Message = $"Found {subjects.Count()} Subjects",
                    Result = subjects.Select(_ => new SubjectResponse()
                    {
                        Name = _.Name,
                        Id = _.Id,
                        UpdatedOn = _.UpdatedOn,
                        CeatedOn = _.CeatedOn
                    }),
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new ApiResponse<IEnumerable<SubjectResponse>>()
            {
                Message = "No subjects Found",
                StatusCode = HttpStatusCode.NotFound
        };
        }
    }
}
