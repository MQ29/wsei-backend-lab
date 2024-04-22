using ApplicationCore.Models.QuizAggregate;

namespace WebApi.Dto;

public class QuizItemDto
{
    public int Id { get; set; }
    public string Question { get; set; }
    public List<string> Options { get; set; }

    public static QuizItemDto Of(QuizItem quizItem)
    {
        var quizItemDto = new QuizItemDto
        {
            Id = quizItem.Id,
            Question = quizItem.Question,
            Options = new List<string>()
        };

        quizItemDto.Options.Add(quizItem.CorrectAnswer);
        quizItemDto.Options.AddRange(quizItem.IncorrectAnswers);

        var random = new Random();
        quizItemDto.Options = quizItemDto.Options.OrderBy(x => random.Next()).ToList();

        return quizItemDto;
    }
}
