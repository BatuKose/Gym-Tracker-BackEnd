using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }
        IExerciseRepository ExerciseRepository { get; }
        IUserProfileRepository userProfileRepository { get; }
        Task Save();
    }
}
