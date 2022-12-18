using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program
{
    public partial class MainForm : Form
    {
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
        }

        private void CallUpButton_Click(object sender, EventArgs e)
        {
            int level = 10;
            elevator.LevelPressed(level);
        }
        private void CallDownButton_Click(object sender, EventArgs e)
        {
            int level = 1;
            elevator.LevelPressed(level);
        }

        private void LevelChanged_Handler(object sender, int currentLevel)
        {
            Console.WriteLine($"Лифт на этаже: {currentLevel}");
        }

        private void StateChanged_Handler(object sender, State state)
        {
            Console.WriteLine($"Состояние лифта: {state}");
        }
    }
}
