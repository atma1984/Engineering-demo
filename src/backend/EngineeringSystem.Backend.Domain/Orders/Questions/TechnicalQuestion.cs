using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Domain.Common;

namespace EngineeringSystem.Backend.Domain.Orders.Questions
{

    public class TechnicalQuestion : Entity
    {
        public string Question { get; private set; }
        public TechnicalQuestionStatus Status { get; private set; }
        public string? Answer { get; private set; }

        // Закрытый конструктор для ORM
        private TechnicalQuestion() { }

        public TechnicalQuestion(Guid id, string question)
        {
            Id = id;

            if (string.IsNullOrWhiteSpace(question))
                throw new ArgumentException(
                    "Технический вопрос не может быть пустым.",
                    nameof(question));

            Question = question;
            Status = TechnicalQuestionStatus.Open;
        }
        public void Close(string answer)
        {
            if (Status == TechnicalQuestionStatus.Closed)
                throw new InvalidOperationException(
                    "Технический вопрос уже закрыт.");

            if (string.IsNullOrWhiteSpace(answer))
                throw new ArgumentException(
                    "Ответ на технический вопрос не может быть пустым.",
                    nameof(answer));

            Answer = answer;
            Status = TechnicalQuestionStatus.Closed;
        }
    }
}
