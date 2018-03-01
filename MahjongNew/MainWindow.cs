using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MahjongNew
{
    public partial class MainWindow : Form
    {
        MahjongBoard Board;

        public MainWindow()
        {
            InitializeComponent();

            Board = new MahjongBoard(this);
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            Board = new MahjongBoard(this);
        }
    }
}
