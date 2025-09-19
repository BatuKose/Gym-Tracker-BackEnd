using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.RequestFeatures
{
    public class UserWithExerciseParameters:RequestParameters
    {
        public string? SearchExercise { get; set; }
        public uint? MinSet { get; set; }
        public uint? MaxSet { get; set; } = 15;
        public bool ValidSetRange => MaxSet>MinSet;
      


    }
}
