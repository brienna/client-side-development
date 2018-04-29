using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3
{
    public partial class Overlay : Form
    {

        public Overlay(String s)
        {
            InitializeComponent();
            label1.Text = s;
        }

        public Overlay()
        {
            InitializeComponent();
        }
    }
}
