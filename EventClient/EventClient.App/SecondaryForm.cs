using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventClient.App
{
    public partial class SecondaryForm : Form
    {
        public SecondaryForm()
        {
            InitializeComponent();
        }
        //Se usa para mostrar un mensaje en el label del formulario
        public string DisplayMessage
        {
            set => lblMessage.Text = value;
        }
    }
}
