using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class QuizItemNotFoundException : QuizException
    {
        public QuizItemNotFoundException(int id) : base($"Quiz item with id: {id} wasn't found.")
        {
        }
    }
}
