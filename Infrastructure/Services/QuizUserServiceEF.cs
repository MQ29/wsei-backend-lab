using ApplicationCore.Interfaces.UserService;
using ApplicationCore.Models;
using ApplicationCore.Models.QuizAggregate;
using AutoMapper;
using Infrastructure.EF.Entities;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class QuizUserServiceEF : IQuizUserService
    {
        private readonly QuizDbContext _context;
        private readonly IMapper _mapper;
        public QuizUserServiceEF(QuizDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Quiz CreateAndGetQuizRandom(int count)
        {
            throw new NotImplementedException();
        }

        public List<Quiz> FindAll()
        {
            var quizzes = _context.Quizzes.ToList();
            var mappedQuizzes = _mapper.Map<List<Quiz>>(quizzes);

            return mappedQuizzes;
        }

        public Quiz? FindQuizById(int id)
        {
            var quiz = _context.Quizzes.FirstOrDefault(x => x.Id == id);
            var mappedQuiz = _mapper.Map<Quiz>(quiz);

            return mappedQuiz;
        }

        public IQueryable<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId)
        {
            throw new NotImplementedException();
        }

        public void SaveUserAnswerForQuiz(int quizId, int userId, int quizItemId, string answer)
        {
            var quizzEntity = _context.Quizzes.FirstOrDefault(x => x.Id == quizId);
            if (quizzEntity is null)
            {
                throw new QuizNotFoundException(quizId);
            }
            var item = _context.QuizItems.FirstOrDefault(x => x.Id == quizItemId);
            if (item is null)
            {
                throw new QuizItemNotFoundException(quizItemId);
            }
            QuizItemUserAnswerEntity entity = new QuizItemUserAnswerEntity()
            {
                QuizId = quizId,
                UserAnswer = answer,
                UserId = userId,
                QuizItemId = quizItemId
            };
            var savedEntity = _context.Add(entity).Entity;
            _context.SaveChanges();
            var mappedQuizz = _mapper.Map<QuizItemUserAnswer>(savedEntity);
        }
    }
}
