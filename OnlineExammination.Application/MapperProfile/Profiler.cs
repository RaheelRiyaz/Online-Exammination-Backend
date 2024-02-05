using AutoMapper;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Domain.Entities;

namespace OnlineExammination.Application.MapperProfile
{
    public class ProgramProfile:Profile
    {
        public ProgramProfile()
        {
            CreateMap<ProgramRequest, Program>();
            CreateMap<Program, ProgramResponse>();
        }
    }

    public class SemesterProfil : Profile
    {
        public SemesterProfil()
        {
            CreateMap<SemesterRequest, Semester>();
            CreateMap<Semester,SemesterResponse>();
        }
    }
    public class Paper : Profile
    {
        public Paper()
        {
            CreateMap<PaperRequest, QuestionPaper>();
            CreateMap<QuestionPaper, PaperResponse>();
        }
    }
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<SubjectRequest, Subject>();
            CreateMap<Subject, SubjectResponse>();
            CreateMap<UpdateSubjectRequest, Subject>();
        }
    }

    public class ExamPorfile : Profile
    {
        public ExamPorfile()
        {
            CreateMap<Exam, ExamResponse>();
            CreateMap<ExamRequest, Exam>();
        }
    }

    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationRequest, Notification>();
            CreateMap<Notification, NotificationResponse>();
        }
    }

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, LoginResponse>();
        }
    }

    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<AppFileRequst, AppFile>();
            CreateMap<AppFile, AppFileResponse>();
            CreateMap<AppFile, GalleryResponse>();
        }
    }
}
