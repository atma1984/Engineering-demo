using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Domain.Common;

namespace EngineeringSystem.Backend.Domain.Projects
{
    public class Project : AggregateRoot
    {
        // ----------------------------
        // Основные данные проекта
        // ----------------------------
        public ProjectStatus Status { get; private set; }

        // ----------------------------
        // Этапы проекта
        // ----------------------------
        private readonly List<ProjectStage> _stages = new();
        public IReadOnlyCollection<ProjectStage> Stages => _stages.AsReadOnly();

        // ----------------------------
        // Конструкторы
        // ----------------------------
        private Project() { }
        public Project(Guid id)
        {
            Id = id;
            Status = ProjectStatus.Draft;
        }
    }
}
