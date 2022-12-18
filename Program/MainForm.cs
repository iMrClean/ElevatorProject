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
            elevator.StateChanged += (s, e) => Console.WriteLine($"Состояние лифта: {e}");
            elevator.LevelChanged += (s, e) => Console.WriteLine($"Лифт на этаже: {e}");
        }

        private async void CallUpButton_Click(object sender, EventArgs e)
        {
            int level = 10;
            await elevator.LevelPressed(level);
        }
        private async void CallDownButton_Click(object sender, EventArgs e)
        {
            int level = 1;
            await elevator.LevelPressed(level);
        }

    }
}
