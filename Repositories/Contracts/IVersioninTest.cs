using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IVersioninTest<T>
    {
        Task<List<T>> FindAllAsync(bool trackChanges);
    }

}
