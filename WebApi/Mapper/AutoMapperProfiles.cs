using ApplicationCore.Models;
using ApplicationCore.Models.QuizAggregate;
using AutoMapper;
using Infrastructure.EF.Entities;
using WebApi.Dto;

namespace WebApi.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<QuizItem, QuizItemDto>()
                 .ForMember(
                     q => q.Options,
                     op => op.MapFrom(i => new List<string>(i.IncorrectAnswers) { i.CorrectAnswer }));
            CreateMap<Quiz, QuizDto>()
                .ForMember(
                    q => q.Items,
                    op => op.MapFrom<List<QuizItem>>(i => i.Items)
                );
            CreateMap<NewQuizDto, Quiz>();
            CreateMap<QuizItemUserAnswer, QuizItemUserAnswerDto>();
            CreateMap<Quiz, QuizEntity>();
            CreateMap<User, UserEntity>();
            CreateMap<UserEntity, User>();
            CreateMap<QuizItemUserAnswerEntity, QuizItemUserAnswer>();
            CreateMap<QuizItemEntity, QuizItem>();
            CreateMap<QuizEntity, Quiz>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => new HashSet<QuizItemEntity>(src.Items)));
        }
    }
}
