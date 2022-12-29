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

        private IElevator elevator;

        public MainForm()
        {
            InitializeComponent();
            InitForm();
        }

        public void InitForm()
        {
            elevator = new Elevator(1, 20);
            elevator.LevelChanged += LevelChanged_Handler;
            elevator.StateChanged += StateChanged_Handler;
            elevator.DoorChanged += DoorChanged_Handler;
        }

        private async void CallUpButton_Click(object sender, EventArgs e)
        {
            int level = 10;
            await elevator.LevelPressed(level, CallState.OUTSIDE);
        }
        private async void CallDownButton_Click(object sender, EventArgs e)
        {
            int level = 1;
            await elevator.LevelPressed(level, CallState.OUTSIDE);
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            int level = 10;
            var task = Task.Run(() => {
                elevator.LevelPressed(level, CallState.OUTSIDE);
            });
        }

        private async void button2_Click(object sender, EventArgs e)
        {

            try
            {
                int level = 1;
                var task = Task.Run(() => {
                    elevator.LevelPressed(level, CallState.OUTSIDE);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int level = 5;
                var task = Task.Run(() => {
                    elevator.LevelPressed(level, CallState.OUTSIDE);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LevelChanged_Handler(object sender, int currentLevel)
        {
            Console.WriteLine($"Лифт на этаже: {currentLevel}");
            displayPictureBox.Image = System.Drawing.Image.FromFile(PATH_TO_RESOURCES_IMAGES + "\\number\\" + currentLevel + ".png");
            //Refresh();
        }

        private void StateChanged_Handler(object sender, ElevatorState elevatorState)
        {
            Console.WriteLine($"Состояние лифта: {elevatorState} Двери {elevator.DoorState}");
            switch (elevatorState)
            {
                case ElevatorState.WAIT:
                    if (elevator.DoorState == DoorState.CLOSE)
                    {
                        //OpenDoor();
                    }
                    if (elevator.DoorState == DoorState.OPEN)
                    {
                        //CloseDoor();
                    }
                    break;
                case ElevatorState.UP:
                    if (elevator.DoorState == DoorState.OPEN)
                    {
                        //CloseDoor();
                    }
                    break;
                case ElevatorState.DOWN:
                    if (elevator.DoorState == DoorState.OPEN)
                    {
                        //CloseDoor();
                    }
                    break;
                default:
                    break;
            }
            //Refresh();
        }
        private void DoorChanged_Handler(object sender, DoorState doorState)
        {
            Console.WriteLine($"Двери лифта : {doorState}");
        }

        private void OpenDoor()
        {
            elevatorPictureBox.Image = System.Drawing.Image.FromFile(PATH_TO_RESOURCES_IMAGES + "\\elevator\\elevator-2.png");
            Refresh();
            Thread.Sleep(FUCKING_SLEEP);
            elevatorPictureBox.Image = System.Drawing.Image.FromFile(PATH_TO_RESOURCES_IMAGES + "\\elevator\\elevator-3.png");
        }

        private void CloseDoor()
        {
            elevatorPictureBox.Image = System.Drawing.Image.FromFile(PATH_TO_RESOURCES_IMAGES + "\\elevator\\elevator-2.png");
            Refresh();
            Thread.Sleep(FUCKING_SLEEP);
            elevatorPictureBox.Image = System.Drawing.Image.FromFile(PATH_TO_RESOURCES_IMAGES + "\\elevator\\elevator-1.png");
        }

    }
}
