using Entites.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entites.Enums.ActivityLevelEnum;
using static Entites.Enums.FitnessGoalEnum;

namespace Repositories.EFCore.Config
{
    public class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {/*
            builder.HasData(
                new UserProfile() { Id=1,UserId=1,Weight=70,Height=1.73,ActivityLevel= ActivityLevel.VeryActive, fitnessGoal=FitnessGoal.MuscleGain }
                );
            */
        }
    }
}
