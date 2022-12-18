using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program
{
    public enum DoorState
    {
        /**
         * Закрыто
         */
        CLOSE,
        /**
         * Открыто
         */
        OPEN
    }

    public enum ElevatorState
    {
        /**
         * Остановлен
         */
        WAIT,
        /**
         * Едем вверх
         */
        UP,
        /**
         * Едем вниз
         */
        DOWN
    }

    internal class Elevator : IElevator
    {
        /**
         * Время за которое лифт перемещается на этаж
         */
        private static readonly int FUCKING_SLEEP = 750;

        /**
         * Минимальный этаж
         */
        private readonly int MinLevel;
        /**
         * Максимальный этаж
         */
        private readonly int MaxLevel;

        /**
         * Текущий этаж
         */
        public int CurrentLevel { get; set; }
        /**
         * Текущее состояние лифта
         */
        public ElevatorState ElevatorState { get; set; }
        /**
         * Текущеее состояние дверей
         */
        public DoorState DoorState { get; set; }
        /**
         * Событие которое следит за изменением этажа
         */
        public event EventHandler<int> LevelChanged;
        /**
         *Событие которое следит за изменением открытости
         */
        public event EventHandler<ElevatorState> StateChanged;
        /**
         *Событие которое следит за изменением открытости
         */
        public event EventHandler<DoorState> DoorChanged;
        /**
         * Инициализация лифта с количеством этажей в нем
         */
        public Elevator(int minLevel, int maxLevel)
        {
            this.CurrentLevel = 1;
            this.MinLevel = minLevel;
            this.MaxLevel = maxLevel;
            this.ElevatorState = ElevatorState.WAIT;
            this.DoorState = DoorState.CLOSE;
        }

        /**
         * Проверка на долбаеба
         */
        private void CheckLevel(int level)
        {
            if (level > MaxLevel)
            {
                MessageBox.Show(String.Format("Инвалид указал несуществующий этаж {0}", level), "Статус", MessageBoxButtons.OK);
            }
            if (level < MinLevel)
            {
                MessageBox.Show(String.Format("Инвалид указал несуществующий этаж {0}", level), "Статус", MessageBoxButtons.OK);
            }
        }

        /**
         * Двигаться вверх
         */
        private void MoveUp(int level)
        {
            ElevatorState = ElevatorState.UP;
            StateChanged?.Invoke(this, ElevatorState);
            
            DoorState = DoorState.CLOSE;
            DoorChanged?.Invoke(this, DoorState);

            while (level <= MaxLevel)
            {
                if (level == CurrentLevel)
                {
                    MoveStop(level);
                    break;
                }

                CurrentLevel++;
                LevelChanged?.Invoke(this, CurrentLevel);

                Console.WriteLine("Текущий этаж {0}, едем на {1}, статус {2}", CurrentLevel, level, ElevatorState);
                Thread.Sleep(FUCKING_SLEEP);
            }
        }

        /**
        * Двигаться вниз
        */
        private void MoveDown(int level)
        {
            ElevatorState = ElevatorState.DOWN;
            StateChanged?.Invoke(this, ElevatorState);

            DoorState = DoorState.CLOSE;
            DoorChanged?.Invoke(this, DoorState);

            while (level >= MinLevel)
            {
                if (level == CurrentLevel)
                {
                    MoveStop(level);
                    break;
                }

                CurrentLevel--;
                LevelChanged?.Invoke(this, CurrentLevel);

                Console.WriteLine("Текущий этаж {0}, едем на {1}, статус {2}", CurrentLevel, level, ElevatorState);
                Thread.Sleep(FUCKING_SLEEP);
            }
        }

        /**
        * Остановили движение (добрались до нужного этажа)
        */
        private void MoveStop(int level)
        {
            ElevatorState = ElevatorState.WAIT;
            StateChanged?.Invoke(this, ElevatorState);
            
            DoorState = DoorState.OPEN;
            DoorChanged?.Invoke(this, DoorState);

            CurrentLevel = level;
            LevelChanged?.Invoke(this, CurrentLevel);

            Console.WriteLine("Остановились на {0} этаже {1}", level, ElevatorState);
            Thread.Sleep(FUCKING_SLEEP);
        }

        /**
         * Пользователь нажал на кнопку этажа внутри лифта
         */
        public void LevelPressed(int level)
        {
            Console.WriteLine("Выбран {0} этаж", level);
            CheckLevel(level);

            if (CurrentLevel == level)
            {
                MoveStop(level);
            }
            else if (CurrentLevel < level)
            {
                MoveUp(level);
            }
            else if (CurrentLevel > level)
            {
                MoveDown(level);
            }
        }
    }

}
