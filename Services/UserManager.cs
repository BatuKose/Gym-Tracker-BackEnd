using AutoMapper;
using Entites.DataTransferObject.ExerciseWithUser;
using Entites.DataTransferObject.User;
using Entites.Exceptions;
using Entites.Models;
using Entites.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserManager:IUserService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;
        public UserManager(IRepositoryManager manager, ILoggerService loggerService,IMapper mapper)
        {
            _manager = manager;
            _loggerService = loggerService;
            _mapper = mapper;
        }      
        public async Task CreateUser(User user)
        {
            
            if (user == null) throw new ArgumentNullException(nameof(user));
            DateTime today = DateTime.Today;
            int yas = today.Year - user.BirthDate.Year;
            if (yas < 14) throw new BadRequestExeption("14 yaşından küçükler kayıt edilemez");
            if (!user.Email.Contains("@")) throw new BadRequestExeption("E posta adresini geçersiz");
            bool emailExists = _manager.UserRepository.FinAllByCondition(x => x.Email == user.Email, false).Any();
            if (emailExists) throw new BadRequestExeption("Bu e-posta adresi zaten kayıtlı");
            if(user.cepTel.Length==10) user.cepTel="0"+user.cepTel;
            if(user.cepTel.Length!=11) throw new BadRequestExeption("Cep telefonu 11 haneli olmalıdır.");
            if (!System.Text.RegularExpressions.Regex.IsMatch(user.cepTel, @"^\d+$")) throw new BadRequestExeption("Telefon sadece rakamlardan oluşmalıdır.");
            bool phoneNumberExists = _manager.UserRepository.FinAllByCondition(x => x.cepTel == user.cepTel, false).Any();
            if (phoneNumberExists) throw new BadRequestExeption("Bu cep telefonu zaten kayıtlı");
            _manager.UserRepository.CreateUser(user);   
          await  _manager.Save();
            _loggerService.LogInfo(user.Username+"kullanıcı isimli kayıt eklendi.");
        }

        public async Task DeleteUser(int id, bool trackChanges)
        {
            
           var getUser =  await _manager.UserRepository.GetUserByIdAsync(id,false);
            if (getUser == null) throw new UserNotFoundExeption();
            if (getUser.isAdmin) throw new BadRequestExeption("Admin rolü eklenmiş kullanıcıyı silemezsin");
           _manager.UserRepository.DeleteUser(getUser);
            await  _manager.Save();
           _loggerService.LogWarning(getUser.Username+"adlı kullanıcı silindi.");
        }

        public async Task<List<User>> GetAllUsersAsync(bool trackChanges)
        {
            return await _manager.UserRepository.GetAllUserAsync(trackChanges);
        }


        public async Task< IEnumerable<GetUserDto>> GetAllUsersAsync(UserParameters userParameters,bool trackChanges)
        {
            var Users = await _manager.UserRepository.GetAllUserAsync(userParameters,trackChanges);
            return _mapper.Map<IEnumerable<GetUserDto>>(Users);

        }

       

        public async Task< User> GetUserByIdAsync(int id, bool trackChanges)
        {
            
            var user= await _manager.UserRepository.GetUserByIdAsync(id, trackChanges);
            if (user == null) throw new UserNotFoundExeption();
            return user;
            
        }

       
            
        public UserWithExercisesDto GetUserWithExercises(UserWithExerciseParameters userWithExerciseParameters, int id)
        {

            var user = _manager.UserRepository.GetUserWithExercises(userWithExerciseParameters,id);
            if (user is null) throw new ExerciseNotFoundExeption();

            var userDto = new UserWithExercisesDto
            {
                Username = user.Username,    
                Email = user.Email,
                Exercise = user.Exercises.Select(e => new ExerciseDtoo
                {
                    Id = e.Id,
                    Name = e.Name,
                    MuscleGroup = e.MuscleGroup,
                    Description = e.Description,
                    DefaultReps = e.DefaultReps,
                    DefaultSets = e.DefaultSets,
                    UserId = e.UserId
                }).ToList()
            };
            return userDto;
        }

        public async Task UpdateUser(int id, User user, bool trackChanges)
        {
            var Getuser = await _manager.UserRepository.GetUserByIdAsync(id, true);
            if (Getuser == null)  throw new BadRequestExeption("Kullanıcı bilgilerini giriniz.");
            if (user is null) throw new UserNotFoundExeption();
            if (user.Id != Getuser.Id) throw new BadRequestExeption("Girilen kullanıcı no ile güncelleme yapılcak kullanıcı aynı değildir");
            if (!user.Email.Contains("@")) throw new BadRequestExeption("E postayı doğru yazınız");
            if(user.BirthDate>DateTime.Today) throw new BadRequestExeption("Doğum tarihini yanlış girilmiştir.");
            bool emailExists = _manager.UserRepository.FinAllByCondition(x => x.Email == user.Email, false).Any();
            if (emailExists) throw new DublicateExeptions("Bu e-posta adresi zaten kayıtlı");
            if (user.cepTel.Length == 10) user.cepTel = "0" + user.cepTel;
            if (user.cepTel.Length != 11) throw new BadRequestExeption("Cep telefonu 11 haneli olmalıdır.");
            if (!System.Text.RegularExpressions.Regex.IsMatch(user.cepTel, @"^\d+$")) throw new BadRequestExeption("Telefon sadece rakamlardan oluşmalıdır.");
            bool phoneNumberExists = _manager.UserRepository.FinAllByCondition(x=>x.cepTel==user.cepTel,false).Any();
            if (phoneNumberExists) throw new DublicateExeptions("Bu cep telefonu zaten kayıtlı");
            bool nameExists = _manager.UserRepository.FinAllByCondition(x => x.Username==user.Username, false).Any();
            if (nameExists) throw new DublicateExeptions("Kullanıcı adı zaten kayıtlı");
            Getuser.Email = user.Email;
            Getuser.isAdmin = user.isAdmin;
            Getuser.Username = user.Username;
            Getuser.BirthDate = user.BirthDate;
            Getuser.erkekMi = user.erkekMi;
            Getuser.PasswordHash = user.PasswordHash;
             _manager.UserRepository.UpdateUser(Getuser);
           await _manager.Save();
            _loggerService.LogInfo(Getuser.Username + "kullanıcı isimli kayıt eklendi.");

        }

        
    }
}
