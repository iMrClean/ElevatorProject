using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    internal class LevelCounter
    {
        public delegate void MethodContainer();
        
        //Событие OnCount c типом делегата MethodContainer.
        public event MethodContainer onCount;

        public void CountUp()
        {
            for (int i = 1; i < 10; i++)
            {

            }
        }

        public void CountDown()
        {
            for (int i = 1; i < 10; i++)
            {

            }
        }

    }
}
