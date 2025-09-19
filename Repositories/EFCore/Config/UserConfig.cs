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
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.HasData(
                new User() { Id=1 ,isAdmin=true,Username="Batuhan",Email="batuhankose36@gmail.com",PasswordHash="12345",CreatedAt = new DateTime(2025, 08, 23, 12, 0, 0),cepTel="5378102935"}
                );
        }
    }
}
