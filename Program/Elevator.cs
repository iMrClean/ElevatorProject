using Program.Enum;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program
{

    internal class Elevator : IElevator
    {
        /**
         * 
         */
        private static readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

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
         * Этажи на которых нужно остановиться
         */
        public List<int> stopList;

        /**
         * Конструктор
         */
        public Elevator(int minLevel, int maxLevel)
        {
            this.CurrentLevel = 1;
            this.MinLevel = minLevel;
            this.MaxLevel = maxLevel;
            this.ElevatorState = ElevatorState.WAIT;
            this.DoorState = DoorState.CLOSE;
            this.stopList = new List<int>();
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
            Console.WriteLine("Текущий этаж {0}, едем на {1}, статус {2}", CurrentLevel, level, ElevatorState);
            UpdateElevatorState(ElevatorState.UP);
            UpdateDoorState(DoorState.CLOSE);

            while (level <= MaxLevel)
            {
                stopList.ForEach(i => Console.WriteLine("Бутон Армагеддон " + i));
                if (stopList.Contains(CurrentLevel))
                {
                    MoveStop(level);
                }
                if (stopList.Count == 0)
                {
                    break;
                }
                UpdateLevelState(++CurrentLevel);

                Thread.Sleep(FUCKING_SLEEP);
            }
        }

        /**
        * Двигаться вниз
        */
        private void MoveDown(int level)
        {
            Console.WriteLine("Текущий этаж {0}, едем на {1}, статус {2}", CurrentLevel, level, ElevatorState);
            UpdateElevatorState(ElevatorState.DOWN);
            UpdateDoorState(DoorState.CLOSE);

            while (level >= MinLevel)
            {
                stopList.ForEach(i => Console.WriteLine("Бутон Армагеддон " + i));
                if (stopList.Contains(CurrentLevel))
                {
                    MoveStop(level);
                }
                if (stopList.Count == 0)
                {
                    break;
                }
                UpdateLevelState(--CurrentLevel);

                Thread.Sleep(FUCKING_SLEEP);
            }
        }

        /**
        * Остановили движение (добрались до нужного этажа)
        */
        private void MoveStop(int level)
        {
            Console.WriteLine("Остановились на {0} этаже {1}", level, ElevatorState);
            Update(ElevatorState.WAIT, DoorState.OPEN, level);

            stopList.Remove(level);
            Thread.Sleep(FUCKING_SLEEP);
        }

        /**
         * Пользователь нажал на кнопку этажа внутри лифта
         */
        public async Task LevelPressed(int level, CallState callState)
        {
            Console.WriteLine("Выбран {0} этаж, вызов {1}", level, callState);
            CheckLevel(level);
            AddToStopList(level);
            Update(ElevatorState.WAIT, DoorState.CLOSE, CurrentLevel);

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
        public void AddToStopList(int level)
        {
            Console.WriteLine("Start counting sync with lock...");
            stopList.Add(level);
        }

        /*
         * Обновить все евенты
         */
        private void Update(ElevatorState elevatorState, DoorState doorState, int level)
        {
            UpdateElevatorState(elevatorState);
            UpdateDoorState(doorState);
            UpdateLevelState(level);
        }

        /*
         * Обновить евент лифта
         */
        private void UpdateElevatorState(ElevatorState elevatorState)
        {
            ElevatorState = elevatorState;
            StateChanged?.Invoke(this, ElevatorState);
        }

        /*
         * Обновить евент двери
         */
        private void UpdateDoorState(DoorState doorState)
        {
            DoorState = doorState;
            DoorChanged?.Invoke(this, DoorState);
        }
        /*
         * Обновить евент этажа
         */
        private void UpdateLevelState(int level)
        {
            CurrentLevel = level;
            LevelChanged?.Invoke(this, CurrentLevel);
        }

    }

}
