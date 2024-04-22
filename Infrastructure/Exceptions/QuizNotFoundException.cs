using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class QuizNotFoundException : QuizException
    {
        public QuizNotFoundException(int id) : base($"Quiz with Id: {id} wasnt found.")
        {
        }
    }
}
