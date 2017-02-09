using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe.View
{
    public partial class Settings : Form
    {
        //Constructors

        public Settings()
        {
            InitializeComponent();
        }


        //Attributes

        SettingsController _controller;



        //Methods

        public void SetController(SettingsController sC)
        {
            _controller = sC;
        }

        //Events

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            string selected = box.GetItemText(box.SelectedItem);
            _controller.SetAiDifficulty(selected);
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            string selected = box.GetItemText(box.SelectedItem);
            _controller.ChangeFirstPlayer(selected);
        }
    }
}
