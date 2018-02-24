using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MahjongNew
{
    public partial class TransparentCanvas : Panel
    {
        public TransparentCanvas()
        {
            InitializeComponent();
            SetStyle(ControlStyles.Opaque, true);
            //DoubleBuffered = true; //black
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }

        protected override void OnMove(EventArgs e)
        {
            RecreateHandle();
        }

        public void InvalidateEx()
        {
            if (Parent == null)
                return;

            Parent.Invalidate(new Rectangle(Location, Size));
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = 0x00000020;
                return cp;
            }
        }

    }
}
