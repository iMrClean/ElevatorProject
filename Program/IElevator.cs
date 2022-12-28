using Program.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    internal interface IElevator
    {
        DoorState DoorState { get; }

        event EventHandler<int> LevelChanged;

        event EventHandler<ElevatorState> StateChanged;

        event EventHandler<DoorState> DoorChanged;

        Task LevelPressed(int level, CallState callState);


    }

}
