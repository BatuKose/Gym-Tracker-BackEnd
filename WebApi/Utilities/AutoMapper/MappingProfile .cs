using AutoMapper;
using Entites.DataTransferObject;
using Entites.DataTransferObject.Exercise;
using Entites.DataTransferObject.ExerciseWithUser;
using Entites.DataTransferObject.User;
using Entites.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Exercise, ExerciseDto>(); 

            CreateMap<ExerciseForCreationDto, Exercise>();   // DTO -> Entity
            CreateMap<ExerciseForUpdateDto, Exercise>();    // DTO -> Entity
            CreateMap<User, GetUserDto>();
            CreateMap<CreateUserDto,User>();
            CreateMap<User,UserWithExercisesDto>();
            CreateMap<Exercise, ExerciseDtoo>();//Entity -> DTO
            CreateMap<UserForRegistration, UserBase>();
        }
    }
}
