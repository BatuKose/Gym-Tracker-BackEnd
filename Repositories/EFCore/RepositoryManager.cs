using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IExerciseRepository> _exerciseRepository;
        private readonly Lazy<IUserProfileRepository> _profileRepository;
        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _exerciseRepository= new Lazy<IExerciseRepository>(() => new ExerciseRepository(_context));
            _profileRepository= new Lazy<IUserProfileRepository>(() => new UserProfileRepository(_context));
        }

        public IUserRepository UserRepository => _userRepository.Value; 
        public IExerciseRepository ExerciseRepository => _exerciseRepository.Value;
        public IUserProfileRepository userProfileRepository => _profileRepository.Value;

       

        public async Task Save()
        {
           await _context.SaveChangesAsync();
        }
    }

}
