using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineeringSystem.Backend.Domain.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EngineeringSystem.Backend.Infrastructure.Persistence.SqlServer.Configurations.Projects
{
    internal sealed class ProjectConfiguration
     : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {

            builder.ToTable("Projects");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Status).IsRequired();
            builder.Ignore(p => p.Stages);
        }
    }
}
