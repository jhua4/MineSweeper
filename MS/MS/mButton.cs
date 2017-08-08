using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MS
{
	class mButton : Button
	{
		public int value = 0;
		public int x = 0;
		public int y = 0;
		public bool isFlagged = false;

		public mButton(int size)
		{
			this.Size = new System.Drawing.Size(size, size);
			this.BackColor = SystemColors.ControlDarkDark;
			this.Font = new Font("Lucida Console", 18, FontStyle.Bold);
			this.FlatStyle = FlatStyle.Flat;
			this.FlatAppearance.BorderSize = 1;
			this.FlatAppearance.BorderColor = Color.Teal;
			value = 0;
		}
	}
}
