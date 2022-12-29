using Program.Enum;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program
{
    internal class Elevator
    {
        /**
         * Локер
         */
        private static readonly object locker = new object();

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
         * Список остановок
         */
        public List<int> stopList;

        /**
         * Конструктор
         */
        public Elevator(int minLevel, int maxLevel)
        {
            this.MinLevel = minLevel;
            this.MaxLevel = maxLevel;
            this.CurrentLevel = 1;
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
         * Пользователь нажал на кнопку этажа внутри лифта
         */
        public async Task LevelPressedAsync(int level)
        {
            try
            {
                Console.WriteLine("[LevelPressed] Выбран {0} этаж.", level);
                CheckLevel(level);
                await ElevatorRouterAsync(level);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /**
         * Рассчет пути лифта
         */
        private async Task ElevatorRouterAsync(int level)
        {
            if (CurrentLevel < level)
            {
                await Task.Run(() => MoveUp(level));
            }

            if (CurrentLevel > level)
            {
                await Task.Run(() => MoveDown(level));
            }

            if (CurrentLevel == level)
            {
                await Task.Run(() => MoveStop(level));
            }
        }

        /**
         * Двигаться вверх
         */
        private void MoveUp(int level)
        {
            lock (locker)
            {
                while (CurrentLevel < level)
                {
                    Console.WriteLine("[MoveUp] Текущий этаж {0}, едем на {1}, статус {2}", CurrentLevel, level, ElevatorState);
                    CurrentLevel++;
                    Thread.Sleep(FUCKING_SLEEP);
                }
            }
        }

        /**
         * Двигаться вниз
         */
        private void MoveDown(int level)
        {
            lock (locker)
            {
                while (CurrentLevel > level)
                {
                    Console.WriteLine("[MoveDown] Текущий этаж {0}, едем на {1}, статус {2}", CurrentLevel, level, ElevatorState);
                    CurrentLevel--;
                    Thread.Sleep(FUCKING_SLEEP);
                }
            }
        }

        /**
         * Остановиться
         */
        private void MoveStop(int level)
        {
            Console.WriteLine("[MoveStop] Остановились на {0} этаже {1}", level, ElevatorState);
        }

    }
}
