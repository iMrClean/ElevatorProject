using System;
using System.Collections.Generic;
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
        private async Task MoveUp(int level)
        {
            UpdateElevatorState(ElevatorState.UP);
            UpdateDoorState(DoorState.CLOSE);

            while (level <= MaxLevel)
            {
                if (stopList.Contains(CurrentLevel))
                {
                    await MoveStop(level);
                }
                if (stopList.Count == 0)
                {
                    break;
                }
                UpdateLevelState(++CurrentLevel);

                Console.WriteLine("Текущий этаж {0}, едем на {1}, статус {2}", CurrentLevel, level, ElevatorState);
                await Task.Delay(FUCKING_SLEEP);
            }
        }

        /**
        * Двигаться вниз
        */
        private async Task MoveDown(int level)
        {
            UpdateElevatorState(ElevatorState.DOWN);
            UpdateDoorState(DoorState.CLOSE);

            while (level >= MinLevel)
            {
                if (stopList.Contains(CurrentLevel))
                {
                    await MoveStop(level);
                }
                if (stopList.Count == 0)
                {
                    break;
                }
                UpdateLevelState(--CurrentLevel);

                Console.WriteLine("Текущий этаж {0}, едем на {1}, статус {2}", CurrentLevel, level, ElevatorState);
                await Task.Delay(FUCKING_SLEEP);
            }
        }

        /**
        * Остановили движение (добрались до нужного этажа)
        */
        private async Task MoveStop(int level)
        {
            Update(ElevatorState.WAIT, DoorState.OPEN, level);

            stopList.Remove(level);

            Console.WriteLine("Остановились на {0} этаже {1}", level, ElevatorState);
            await Task.Delay(FUCKING_SLEEP);
        }

        /**
         * Пользователь нажал на кнопку этажа внутри лифта
         */
        public async Task LevelPressed(int level)
        {
            Console.WriteLine("Выбран {0} этаж", level);
            CheckLevel(level);
            stopList.Add(level);
            Update(ElevatorState.WAIT, DoorState.CLOSE, level);

            if (CurrentLevel == level)
            {
                await MoveStop(level);
            }
            else if (CurrentLevel < level)
            {
                await MoveUp(level);
            }
            else if (CurrentLevel > level)
            {
                await MoveDown(level);
            }
        }
        private void Update(ElevatorState elevatorState, DoorState doorState, int level)
        {
            UpdateElevatorState(elevatorState);
            UpdateDoorState(doorState);
            UpdateLevelState(level);
        }

        private void UpdateElevatorState(ElevatorState elevatorState)
        {
            ElevatorState = elevatorState;
            StateChanged?.Invoke(this, ElevatorState);
        }

        private void UpdateDoorState(DoorState doorState)
        {
            DoorState = doorState;
            DoorChanged?.Invoke(this, DoorState);
        }

        private void UpdateLevelState(int level)
        {
            CurrentLevel = level;
            LevelChanged?.Invoke(this, CurrentLevel);
        }

    }

}
