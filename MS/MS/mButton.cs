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
		//a value of -1 signifies a mine
		//a value of 0 is a blank space
		//all other values signify the number of mines the space is touching (which is the value to be displayed)
		public int value = 0; 

		//x and y values used to show the button's position in the grid, with top left corner being the origin
		public int x = 0;
		public int y = 0;

		//boolean to represent if the user has flagged it (right-clicked)
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
