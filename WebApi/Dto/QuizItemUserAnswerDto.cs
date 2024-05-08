using ApplicationCore.Models;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto;

public class QuizItemUserAnswerDto
{
    [Microsoft.Build.Framework.Required]
    [NotNull]
    //[Length(minimumLength: 1, maximumLength: 200)]
    public string? Answer { get; set; }

    public static QuizItemUserAnswerDto Of(QuizItemUserAnswer userAnswer)
    {
        var userAnswerDto = new QuizItemUserAnswerDto
        {
            Answer = userAnswer.Answer,
        };

        return userAnswerDto;   
    }
}