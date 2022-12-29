using Program.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Program
{
    public partial class MainForm : Form
    {
        private const string PATH_TO_RESOURCES_IMAGES = "..\\..\\Resources\\images";

        private static readonly int FUCKING_SLEEP = 300;

        private Elevator elevator;

        public MainForm()
        {
            InitializeComponent();
            InitForm();
        }

        public void InitForm()
        {
            elevator = new Elevator(1, 20);

            elevator.LevelChanged += LevelChanged_Handler;
            elevator.StateChanged += ElevatorStateChanged_Handler;
            elevator.DoorChanged += DoorChanged_Handler;
        }

        private async void CallUpButton_Click(object sender, EventArgs e)
        {
            int level = 10;
            await elevator.LevelPressedAsync(level);
        }
        private async void CallDownButton_Click(object sender, EventArgs e)
        {
            int level = 1;
            await elevator.LevelPressedAsync(level);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int level = 5;
            await elevator.LevelPressedAsync(level);
        }

        private void LevelChanged_Handler(object sender, int currentLevel)
        {
            Console.WriteLine($"[LevelChanged] Выбран этаж: {currentLevel}");
        }

        private void ElevatorStateChanged_Handler(object sender, ElevatorState elevatorState)
        {
            Console.WriteLine($"[ElevatorStateChanged] Состояние лифта: {elevatorState}");
        }

        private void DoorChanged_Handler(object sender, DoorState doorState)
        {
            Console.WriteLine($"[DoorChanged] Двери лифта : {doorState}");
        }
    }
}
