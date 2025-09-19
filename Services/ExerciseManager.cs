using AutoMapper;
using Entites.DataTransferObject.Exercise;
using Entites.DataTransferObject.User;
using Entites.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ExerciseManager : IExerciseService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public ExerciseManager(IRepositoryManager manager, ILoggerService logger,IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task CreateExercise(ExerciseForCreationDto dto, int UserId)
        {
            if (UserId<=0) throw new  Exception("Kullanıcı Bilgileri boş geçilemez");
            var getuser = await _manager.UserRepository.GetUserByIdAsync(UserId, false);
            if (getuser is null) throw new Exception("Antreman ekleyeceğiniz kullanıcı bulunmamaktadır.");
            if (dto.DefaultSets<=0) throw new Exception("Set sayısı birden küçük olamaz");
            if (dto.DefaultReps<=0) throw new Exception("Tekrar sayısı birden küçük olamaz");
            var exercise=_mapper.Map<Exercise>(dto);
            exercise.UserId = UserId;
             _manager.ExerciseRepository.CreateExercise(exercise);
           await _manager.Save();
            
        }

        public void DeleteExerciseById(int id)
        {
            var getExercise = _manager.ExerciseRepository.GetExerciseByid(id,false);
            if (getExercise is null) throw new Exception("Silinecek Egzersiz bulunamadı.");
            _manager.ExerciseRepository.DeleteExercise(getExercise);
        }

        // hatalı bir ara düzelcek
        public GetExerciseDto GetExerciseById(int id, bool trackChanges)
        {
            var exercise = _manager.ExerciseRepository.GetExerciseByid(id, trackChanges);
            if (exercise == null)
                throw new Exception("Egzersiz bulunamadı.");

            // Entity → DTO map
            var dto = new GetExerciseDto
            {
                Id = exercise.Id,
                Name = exercise.Name,
                MuscleGroup = exercise.MuscleGroup,
                Description = exercise.Description,
                DefaultReps = exercise.DefaultReps,
                DefaultSets = exercise.DefaultSets,
                UserId = exercise.UserId
            };

            return dto;
        }

         
        public async Task UpdateExercise(ExerciseForUpdateDto exerciseDto, int id)
        {
            var exercise = _manager.ExerciseRepository.GetExerciseByid(id, true);
            if (exercise == null)
                throw new Exception("Güncellenecek egzersiz bulunamadı.");
            if (exerciseDto.DefaultReps <= 0) throw new Exception("Tekarar sayısı birden küçük olamaz");
            if (exerciseDto.DefaultSets <= 0) throw new Exception("Set sayısı birden küçük olamaz"); 
            exercise.Name = exerciseDto.Name;
            exercise.MuscleGroup = exerciseDto.MuscleGroup;
            exercise.DefaultReps = exerciseDto.DefaultReps;
            exercise.DefaultSets = exerciseDto.DefaultSets;
            exercise.Description = exerciseDto.Description;
            _manager.ExerciseRepository.UpdateExercise(exercise);
            await _manager.Save();
        }

       
    }
}
