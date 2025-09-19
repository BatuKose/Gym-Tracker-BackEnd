using Entites.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
    internal class ExerciseConfig : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            
            builder.HasData(
                new Exercise() { Id=1,UserId=1,DefaultReps=3,DefaultSets=10,MuscleGroup="back"
                ,Description="back traning",Name="back day"}
                );
            
        }
    }
}
