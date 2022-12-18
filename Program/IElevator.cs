using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    internal interface IElevator
    {
        
        event EventHandler<int> LevelChanged;

        event EventHandler<State> StateChanged;

        void LevelPressed(int level);

    }

}
