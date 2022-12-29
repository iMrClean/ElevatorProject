using Program.Enum;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program
{

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
        public List<int> stopList { get; set; }

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
            Console.WriteLine("MoveUp Текущий этаж {0}, едем на {1}, статус {2}", CurrentLevel, level, ElevatorState);
            UpdateElevatorState(ElevatorState.UP);
            UpdateDoorState(DoorState.CLOSE);

            while (level <= MaxLevel)
            {

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
            Console.WriteLine("MoveDown Текущий этаж {0}, едем на {1}, статус {2}", CurrentLevel, level, ElevatorState);
            UpdateElevatorState(ElevatorState.DOWN);
            UpdateDoorState(DoorState.CLOSE);

            while (level >= MinLevel)
            {

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
            Console.WriteLine("MoveStop Остановились на {0} этаже {1}", level, ElevatorState);
            Update(ElevatorState.WAIT, DoorState.OPEN, level);

            stopList.Remove(level);
            Thread.Sleep(FUCKING_SLEEP);
        }

        /**
         * Пользователь нажал на кнопку этажа внутри лифта
         */
        public async Task LevelPressed(int level, CallState callState)
        {
            try
            {
                Console.WriteLine("Метод LevelPressed Выбран {0} этаж, вызов {1}", level, callState);
                CheckLevel(level);
                await AddToStopList(level);
                if (ElevatorState == ElevatorState.WAIT)
                {
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private async Task AddToStopList(int level)
        {
            stopList.ForEach(i => {
                Console.WriteLine("Текущее состояние лифта " + ElevatorState.ToString());
                Console.WriteLine("THIS IS SPAAAAARTAAAAAAAAAAAAA item " + i);
            });
            stopList.Add(level);
            await Task.Delay(FUCKING_SLEEP);
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
