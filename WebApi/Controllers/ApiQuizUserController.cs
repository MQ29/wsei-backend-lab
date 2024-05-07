using ApplicationCore.Interfaces.UserService;
using ApplicationCore.Models.QuizAggregate;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/v1/user/quizzes")]
public class ApiQuizUserController : ControllerBase
{
    private readonly IQuizUserService _service;
    private readonly IMapper _mapper;

    public ApiQuizUserController(IQuizUserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [Route("{id}")]
    [HttpGet]
    [Authorize(Policy = "Bearer")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<QuizDto> GetQuiz(int id)
    {
        var quiz = _service.FindQuizById(id);
        if (quiz is null)
        {
            return NotFound();
        }

        return QuizDto.Of(quiz);
    }

    [HttpGet]
    public ActionResult<IEnumerable<QuizDto>> FindAll()
    {
        var mappedQuizzes = new List<QuizDto>();
        var quizes = _service.FindAll();

        foreach (var quiz in quizes)
        {
            mappedQuizzes.Add(QuizDto.Of(quiz));
        }

        return mappedQuizzes;
    }

    [Route("{quizId}/items/{itemId}/answers/{userId}")]
    [HttpPost]
    [Authorize(Policy = "Bearer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<object> SaveAnswer(
        int quizId,
        int itemId,
        int userId,
        QuizItemUserAnswerDto dto,
        LinkGenerator linkGenerator
    )
    {
        _service.SaveUserAnswerForQuiz(quizId, userId, itemId, dto.Answer ?? "");
        return Created(
            linkGenerator.GetUriByAction(HttpContext, nameof(GetQuizFeedback), null,
                new { quizId = quizId, userId = 1 }),
            new
            {
                answer = dto.Answer,
            });
    }

    [Route("{quizId}/answers/{userId}")]
    [HttpGet]
    public ActionResult<FeedbackDto> GetQuizFeedback(int quizId, int userId)
    {
        var quiz = _service.FindQuizById(quizId);
        if (quiz is null)
        {
            return NotFound($"Quiz with id: {quizId} wasn't found.");
        }
        var userAnswers = _service.GetUserAnswersForQuiz(quizId, userId);
        if (userAnswers is null)
        {
            return NotFound("User answers was not found.");
        }

        var feedbackDto = new FeedbackDto()
        {
            QuizId = quizId,
            UserId = userId,
            TotalQuestions = quiz.Items.Count(),
            Answers = _mapper.Map<List<QuizItemUserAnswerDto>>(userAnswers)
        };
        return Ok(feedbackDto);
    }

}