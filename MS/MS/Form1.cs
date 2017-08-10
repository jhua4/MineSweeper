using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace MS
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		const int buttonSize = 30;

		private int topMargin = 40;
		private int leftMargin = 40;

		private int rows = 16;
		private int cols = 30;
		private int mines = 99;
		private int flags = 0;
		private int spacesLeft = 0;

		private mButton[][] buttons;

		//used to show if left/right button was clicked
		private enum ButtonClicked { Left, Right };
		ButtonClicked bClicked;

		private bool firstClick = true;

		private Stopwatch sw;

		//creates all buttons and adds handlers
		private void Form1_Load(object sender, EventArgs e)
		{
			buttons = new mButton[cols][];

			for (int i = 0; i < cols; i++)
			{
				buttons[i] = new mButton[rows];
			}

			for (int i = 0; i < cols; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					mButton m = new mButton(buttonSize);
					m.Location = new Point(i * buttonSize + leftMargin, j * buttonSize + topMargin);
					m.x = i;
					m.y = j;

					buttons[i][j] = m;

					m.MouseDown += ((o, ee) =>
					{
						if (ee.Button.ToString() == "Left")
							bClicked = ButtonClicked.Left;
						else if (ee.Button.ToString() == "Right")
							bClicked = ButtonClicked.Right;
					});

					m.MouseUp += mButton_MouseUp;

					this.Controls.Add(m);
				}
			}

			newGame(16, 30, 99);
		}

		//handles all methods for creating a new game
		private void newGame(int r, int c, int m)
		{
			rows = r;
			cols = c;
			mines = m;

			timer1.Stop();
			sw = new Stopwatch();

			//make all buttons invisible (16x30 is the max number)
			for (int i = 0; i < 30; i++)
				for (int j = 0; j < 16; j++)
					buttons[i][j].Visible = false;

			for (int i = 0; i < cols; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					buttons[i][j].Text = "";
					buttons[i][j].BackColor = SystemColors.ControlDark;
					buttons[i][j].ForeColor = Color.Black;
					buttons[i][j].Image = null;
					buttons[i][j].value = 0;
					buttons[i][j].Visible = true;
				}
			}

			int[][] matrix = new int[cols][];
			for (int i = 0; i < cols; i++)
			{
				matrix[i] = new int[rows];
			}

			int[] set = new int[rows * cols];
			List<int> currentSet = new List<int>();
			for (int i = 0; i < mines; i++)
			{
				int[] result = CreateMineIndex(currentSet, rows * cols);

				int x_index = result[0];
				int y_index = result[1];
				int index = result[2];

				currentSet.Add(index);

				matrix[x_index][y_index] = 1;
			}

			for (int i = 0; i < cols; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					if (matrix[i][j] == 1)
					{
						buttons[i][j].value = -1;
					}
				}
			}

			MapValues(matrix);
			
			flags = 0;

			spacesLeft = rows * cols - mines;

			for (int i = 0; i < cols; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					if (buttons[i][j].value == 0)
						spacesLeft--;
				}
			}

			spacesLeftLabel.Text = "Spaces Left: " + spacesLeft.ToString();

			firstClick = true;
			minesLeftLabel.Text = "Mines Left: " + mines.ToString();
			MessageLabel.Text = "";
			timerLabel.Text = "00:00";
		}

		//set values in grid
		private void MapValues(int[][] matrix)
		{
			for (int i = 0; i < cols; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					if (buttons[i][j].value != -1)
					{
						buttons[i][j].value = MapButtonValue(i, j, matrix);
					}
				}
			}
		}

		//set individual button value
		private int MapButtonValue(int x, int y, int[][] matrix)
		{
			int mineCount = 0;

			if (x == 0) //LEFT BORDER
			{
				if (y == 0) //TOP BORDER
				{
					if (matrix[x][y + 1] == 1)
						mineCount++;
					if (matrix[x + 1][y + 1] == 1)
						mineCount++;
				} else if (y == rows - 1) //BOTTOM BORDER
				{
					if (matrix[x][y - 1] == 1)
						mineCount++;
					if (matrix[x + 1][y - 1] == 1)
						mineCount++;
				}
				else //NO TOP/BOTTOM BORDER
				{
					if (matrix[x][y - 1] == 1)
						mineCount++;
					if (matrix[x][y + 1] == 1)
						mineCount++;
					if (matrix[x + 1][y - 1] == 1)
						mineCount++;
					if (matrix[x + 1][y + 1] == 1)
						mineCount++;
				}

				if (matrix[x + 1][y] == 1)
					mineCount++;
			}
			else if (x == cols - 1) //RIGHT BORDER
			{
				if (y == 0) //TOP BORDER
				{
					if (matrix[x][y + 1] == 1)
						mineCount++;
					if (matrix[x - 1][y + 1] == 1)
						mineCount++;
				}
				else if (y == rows - 1) //BOTTOM BORDER
				{
					if (matrix[x][y - 1] == 1)
						mineCount++;
					if (matrix[x - 1][y - 1] == 1)
						mineCount++;
				}
				else //NO TOP/BOTTOM BORDER
				{
					if (matrix[x][y - 1] == 1)
						mineCount++;
					if (matrix[x][y + 1] == 1)
						mineCount++;
					if (matrix[x - 1][y - 1] == 1)
						mineCount++;
					if (matrix[x - 1][y + 1] == 1)
						mineCount++;
				}

				if (matrix[x - 1][y] == 1)
					mineCount++;
			} else //NO LEFT/RIGHT BORDER
			{
				if (y == 0) //TOP BORDER
				{
					if (matrix[x][y + 1] == 1)
						mineCount++;
					if (matrix[x + 1][y + 1] == 1)
						mineCount++;
					if (matrix[x - 1][y + 1] == 1)
						mineCount++;
				}
				else if (y == rows - 1) //BOTTOM BORDER
				{
					if (matrix[x][y - 1] == 1)
						mineCount++;
					if (matrix[x + 1][y - 1] == 1)
						mineCount++;
					if (matrix[x - 1][y - 1] == 1)
						mineCount++;
				}
				else //NO TOP/BOTTOM BORDER
				{
					if (matrix[x][y - 1] == 1)
						mineCount++;
					if (matrix[x][y + 1] == 1)
						mineCount++;
					if (matrix[x + 1][y - 1] == 1)
						mineCount++;
					if (matrix[x + 1][y + 1] == 1)
						mineCount++;
					if (matrix[x - 1][y - 1] == 1)
						mineCount++;
					if (matrix[x - 1][y + 1] == 1)
						mineCount++;
				}

				if (matrix[x - 1][y] == 1)
					mineCount++;
				if (matrix[x + 1][y] == 1)
					mineCount++;
			}

			return mineCount;
		}

		//used to randomly generate mines in grid
		private int[] CreateMineIndex(List<int> exclude, int max)
		{
			bool found = false;

			while (!found)
			{
				Random r = new Random();
				int value = r.Next(0, max);

				if (!exclude.Contains(value))
				{
					found = true;

					int y_index = (int)Math.Floor((double)(value / cols));
					int x_index = value % cols;

					return new int[3] { x_index, y_index, value };
				}
			}

			return null;
		}

		//workaround method which basically handles all clicking on buttons
		private void mButton_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			mButton b = (mButton)sender;

			if (e.Location.X >= 0 && e.Location.X < buttonSize && e.Location.Y >= 0 && e.Location.Y < buttonSize)
			{
				if (bClicked == ButtonClicked.Left)
				{
					//LEFT CLICK
					if (b.value != - 1)
					{
						if (firstClick) //first click of the game
						{
							firstClick = false;
							sw.Start();
							timer1.Start();
						}

						if (b.value == 0)
						{
							mapZeroSection(b);
						} else
						{
							setButtonText(b);
						}
					} else
					{
						if (firstClick)
						{
							firstClick = false;
							moveMine(b);
							updateButtonFromMine(b);
							setButtonText(b);
						} else
						{
							b.Image = (Image)MS.Properties.Resources.ResourceManager.GetObject("Mine");
							lostGame();
						}

					}
				} else if (bClicked == ButtonClicked.Right)
				{
					//RIGHT CLICK
					if (!b.isFlagged)
					{
						b.isFlagged = true;
						b.Image = (Image)MS.Properties.Resources.ResourceManager.GetObject("Flag");
						flags++;
						minesLeftLabel.Text = "Mines Left: " + (mines - flags).ToString();
					} else
					{
						b.isFlagged = false;
						b.Image = null;
						flags--;
						minesLeftLabel.Text = "Mines Left: " + (mines - flags).ToString();
					}

				}
			}
		}

		//show that the user lost and reveal all spaces
		private void lostGame()
		{
			timer1.Stop();

			for (int i = 0; i < cols; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					if (buttons[i][j].value == 0)
					{
						buttons[i][j].BackColor = SystemColors.ActiveBorder;
					}
					else if (buttons[i][j].value != -1)
					{
						setButtonText(buttons[i][j]);
					} else
					{
						buttons[i][j].Image = (Image)MS.Properties.Resources.ResourceManager.GetObject("Mine");
					}
				}
			}

			MessageLabel.Text = "You lost...";
			MessageLabel.ForeColor = Color.Blue;
		}

		//if a blank space is clicked, then show all adjacent spaces with recursion
		private void mapZeroSection(mButton b)
		{
			if (b.BackColor == SystemColors.ControlDarkDark)
				return;

			b.BackColor = SystemColors.ControlDarkDark;

			if (b.x == 0) //LEFT BORDER
			{
				if (b.y == 0) //TOP BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y + 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value == 0)
						{
							mapZeroSection(buttons[t.Item1][t.Item2]);
						}
						else if (buttons[t.Item1][t.Item2].value != -1)
						{
							setButtonText(buttons[t.Item1][t.Item2]);
						}
					}
				}
				else if (b.y == rows - 1) //BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value == 0)
						{
							mapZeroSection(buttons[t.Item1][t.Item2]);
						}
						else if (buttons[t.Item1][t.Item2].value != -1)
						{
							setButtonText(buttons[t.Item1][t.Item2]);
						}
					}
				}
				else //NO TOP/BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value == 0)
						{
							mapZeroSection(buttons[t.Item1][t.Item2]);
						}
						else if (buttons[t.Item1][t.Item2].value != -1)
						{
							setButtonText(buttons[t.Item1][t.Item2]);
						}
					}
				}

				if (buttons[b.x + 1][b.y].value == 0)
					mapZeroSection(buttons[b.x + 1][b.y]);
				else if (buttons[b.x + 1][b.y].value != -1)
					setButtonText(buttons[b.x + 1][b.y]);
			}
			else if (b.x == cols - 1) //RIGHT BORDER
			{
				if (b.y == 0) //TOP BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y + 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value == 0)
						{
							mapZeroSection(buttons[t.Item1][t.Item2]);
						}
						else if (buttons[t.Item1][t.Item2].value != -1)
						{
							setButtonText(buttons[t.Item1][t.Item2]);
						}
					}
				}
				else if (b.y == rows - 1) //BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value == 0)
						{
							mapZeroSection(buttons[t.Item1][t.Item2]);
						}
						else if (buttons[t.Item1][t.Item2].value != -1)
						{
							setButtonText(buttons[t.Item1][t.Item2]);
						}
					}
				}
				else //NO TOP/BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value == 0)
						{
							mapZeroSection(buttons[t.Item1][t.Item2]);
						}
						else if (buttons[t.Item1][t.Item2].value != -1)
						{
							setButtonText(buttons[t.Item1][t.Item2]);
						}
					}
				}

				if (buttons[b.x - 1][b.y].value == 0)
					mapZeroSection(buttons[b.x - 1][b.y]);
				else if (buttons[b.x - 1][b.y].value != -1)
					setButtonText(buttons[b.x - 1][b.y]);
			}
			else //NO LEFT/RIGHT BORDER
			{
				if (b.y == 0) //TOP BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y + 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value == 0)
						{
							mapZeroSection(buttons[t.Item1][t.Item2]);
						} else if (buttons[t.Item1][t.Item2].value != -1)
						{
							setButtonText(buttons[t.Item1][t.Item2]);
						}
					}

				}
				else if (b.y == rows - 1) //BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value == 0)
						{
							mapZeroSection(buttons[t.Item1][t.Item2]);
						}
						else if (buttons[t.Item1][t.Item2].value != -1)
						{
							setButtonText(buttons[t.Item1][t.Item2]);
						}
					}
				}
				else //NO TOP/BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));
					indices.Add(Tuple.Create(b.x - 1, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value == 0)
						{
							mapZeroSection(buttons[t.Item1][t.Item2]);
						}
						else if (buttons[t.Item1][t.Item2].value != -1)
						{
							setButtonText(buttons[t.Item1][t.Item2]);
						}
					}
				}

				if (buttons[b.x - 1][b.y].value == 0)
					mapZeroSection(buttons[b.x - 1][b.y]);
				else if (buttons[b.x - 1][b.y].value != -1)
					setButtonText(buttons[b.x - 1][b.y]);

				if (buttons[b.x + 1][b.y].value == 0)
					mapZeroSection(buttons[b.x + 1][b.y]);
				else if (buttons[b.x + 1][b.y].value != -1)
					setButtonText(buttons[b.x + 1][b.y]);
			}
		}

		//show button value and set color
		private void setButtonText(mButton b)
		{
			int val = b.value;

			if (b.BackColor == SystemColors.ActiveBorder)
				return; //button text has already been set
			else
				b.BackColor = SystemColors.ActiveBorder;
			
			switch (val)
			{
				case 1:
					b.Text = "1";
					b.ForeColor = Color.Blue;
					break;
				case 2:
					b.Text = "2";
					b.ForeColor = Color.Green;
					break;
				case 3:
					b.Text = "3";
					b.ForeColor = Color.Red;
					break;
				case 4:
					b.Text = "4";
					b.ForeColor = Color.Navy;
					break;
				case 5:
					b.Text = "5";
					b.ForeColor = Color.Maroon;
					break;
				case 6:
					b.Text = "6";
					b.ForeColor = Color.Teal;
					break;
				case 7:
					b.Text = "7";
					b.ForeColor = Color.Black;
					break;
				case 8:
					b.Text = "8";
					b.ForeColor = Color.Orange;
					break;
			}

			spacesLeft--;
			spacesLeftLabel.Text = "Spaces Left: " + spacesLeft.ToString();
			if (spacesLeft == 0)
			{
				wonGame();
			}
		}

		//show that the user won the game and reveal all spaces/bombs
		private void wonGame()
		{
			timer1.Stop();
			MessageLabel.Text = "You won in " + sw.Elapsed.ToString(@"mm\:ss\!");
			MessageLabel.ForeColor = Color.Green;
			minesLeftLabel.Text = "Mines Left: 0";

			for (int i = 0; i < cols; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					if (buttons[i][j].value == 0)
					{
						buttons[i][j].BackColor = SystemColors.ActiveBorder;
					}
					else if (buttons[i][j].value != -1)
					{
						setButtonText(buttons[i][j]);
					}
					else
					{
						buttons[i][j].Image = (Image)MS.Properties.Resources.ResourceManager.GetObject("Mine");
					}
				}
			}
		}

		//only used if a mine is clicked on the first click
		//change button value from being a mine to however many mines it is touching
		private void updateButtonFromMine(mButton b)
		{
			int mineCount = 0;

			if (b.x == 0) //LEFT BORDER
			{
				if (b.y == 0) //TOP BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y + 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value--;
						else
							mineCount++;
					}
				}
				else if (b.y == rows - 1) //BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value--;
						else
							mineCount++;
					}
				}
				else //NO TOP/BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value--;
						else
							mineCount++;
					}
				}

				if (buttons[b.x + 1][b.y].value != -1)
					buttons[b.x + 1][b.y].value--;
				else
					mineCount++;
			}
			else if (b.x == cols - 1) //RIGHT BORDER
			{
				if (b.y == 0) //TOP BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y + 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value--;
						else
							mineCount++;
					}
				}
				else if (b.y == rows - 1) //BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value--;
						else
							mineCount++;
					}
				}
				else //NO TOP/BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value--;
						else
							mineCount++;
					}
				}

				if (buttons[b.x - 1][b.y].value != -1)
					buttons[b.x - 1][b.y].value--;
				else
					mineCount++;
			}
			else //NO LEFT/RIGHT BORDER
			{
				if (b.y == 0) //TOP BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y + 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value--;
						else
							mineCount++;
					}

				}
				else if (b.y == rows - 1) //BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value--;
						else
							mineCount++;
					}
				}
				else //NO TOP/BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));
					indices.Add(Tuple.Create(b.x - 1, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value--;
						else
							mineCount++;
					}
				}

				if (buttons[b.x - 1][b.y].value != -1)
					buttons[b.x - 1][b.y].value--;
				else
					mineCount++;

				if (buttons[b.x + 1][b.y].value != -1)
					buttons[b.x + 1][b.y].value--;
				else
					mineCount++;
			}

			b.value = mineCount;

			if (b.value == 0)
			{
				mapZeroSection(b);
			}
		}

		//only used if a mine is clicked on the first click
		//change a button to a mine and update adjacent buttons
		private void updateButtonToMine(mButton b)
		{
			//set value to mine value and update surrounding tiles
			b.value = -1;
			
			if (b.x == 0) //LEFT BORDER
			{
				if (b.y == 0) //TOP BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y + 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value++;
					}
				}
				else if (b.y == rows - 1) //BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value++;
					}
				}
				else //NO TOP/BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value++;
					}
				}

				if (buttons[b.x + 1][b.y].value != -1)
					buttons[b.x + 1][b.y].value++;
			}
			else if (b.x == cols - 1) //RIGHT BORDER
			{
				if (b.y == 0) //TOP BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y + 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value++;
					}
				}
				else if (b.y == rows - 1) //BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value++;
					}
				}
				else //NO TOP/BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value++;
					}
				}

				if (buttons[b.x - 1][b.y].value != -1)
					buttons[b.x - 1][b.y].value++;
			}
			else //NO LEFT/RIGHT BORDER
			{
				if (b.y == 0) //TOP BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y + 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value++;
					}

				}
				else if (b.y == rows - 1) //BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value++;
					}
				}
				else //NO TOP/BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));
					indices.Add(Tuple.Create(b.x - 1, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value++;
					}
				}

				if (buttons[b.x - 1][b.y].value != -1)
					buttons[b.x - 1][b.y].value++;
				if (buttons[b.x + 1][b.y].value != -1)
					buttons[b.x + 1][b.y].value++;
			}
		}

		//only used if a mine is clicked on the first click
		//move mine to top left corner if it is blank, unless user clicked top left corner or there is already a mine there
		//otherwise search for first non-mine tile from left to right, up to down
		private void moveMine(mButton b)
		{
			if (b.x == 0 && b.y == 0)
			{
				//MessageBox.Show("7");
				for (int i = 0; i < cols; i++)
				{
					for (int j = 0; j < rows; j++)
					{
						if (buttons[i][j].value != -1)
						{
							updateButtonToMine(buttons[i][j]);
							return;
						}
					}
				}
			} else
			{
				if (buttons[0][0].value == -1)
				{
					for (int i = 0; i < cols; i++)
					{
						for (int j = 0; j < rows; j++)
						{
							if (buttons[i][j].value != -1)
							{
								updateButtonToMine(buttons[i][j]);
								return;
							}
						}
					}
				} else //moved mine to top left corner, increment surrounding tiles
				{
					buttons[0][0].value = -1;
					if (buttons[0][1].value != -1)
					{
						buttons[0][1].value++;
					}
					if (buttons[1][1].value != -1)
					{
						buttons[1][1].value++;
					}
					if (buttons[1][0].value != -1)
					{
						buttons[1][0].value++;
					}
				}
			}
		}

		//handle new game options and custom limits
		private void newGameButton_Click(object sender, EventArgs e)
		{
			if (beginnerRB.Checked)
			{
				newGame(9, 9, 10);
			} else if (intermediateRB.Checked)
			{
				newGame(16, 16, 40);
			} else if (expertRB.Checked)
			{
				newGame(16, 30, 99);
			} else if (customRB.Checked)
			{
				try
				{
					int rows = Convert.ToInt32(rowsBox.Text);
					int columns = Convert.ToInt32(columnsBox.Text);
					int mines = Convert.ToInt32(minesBox.Text);

					if (rows < 1)
						rows = 1;
					else if (rows > 16)
						rows = 16;

					if (columns < 8)
						columns = 8;
					else if (columns > 30)
						columns = 30;

					if (mines >= (rows * columns - 1))
						mines = rows * columns - 1;
					else if (mines < 0)
						mines = 0;

					rowsBox.Text = rows.ToString();
					columnsBox.Text = columns.ToString();
					minesBox.Text = mines.ToString();

					newGame(rows, columns, mines);
				} catch
				{
					rowsBox.Text = "16";
					columnsBox.Text = "30";
					minesBox.Text = "99";
					newGame(16, 30, 99);
				}
			}
		}

		//update timer label
		private void timer1_Tick(object sender, EventArgs e)
		{
			timerLabel.Text = sw.Elapsed.ToString(@"mm\:ss");
		}

		//make custom options visible on click and invisible on other click
		private void customRB_CheckedChanged(object sender, EventArgs e)
		{
			if (customRB.Checked)
				customOptionsPanel.Visible = true;
			else
				customOptionsPanel.Visible = false;
		}
	}
}










/* cases
			if (b.x == 0) //LEFT BORDER
			{
				if (b.y == 0) //TOP BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y + 1));

					foreach (Tuple<int, int> t in indices)
					{
						
					}
				}
				else if (b.y == rows - 1) //BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						
					}
				}
				else //NO TOP/BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						
					}
				}

				if (buttons[b.x + 1][b.y].value == 0)
					mapZeroSection(buttons[b.x + 1][b.y]);
				else if (buttons[b.x + 1][b.y].value != -1)
					setButtonText(buttons[b.x + 1][b.y]);
			}
			else if (b.x == cols - 1) //RIGHT BORDER
			{
				if (b.y == 0) //TOP BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y + 1));

					foreach (Tuple<int, int> t in indices)
					{
						
					}
				}
				else if (b.y == rows - 1) //BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						
					}
				}
				else //NO TOP/BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						
					}
				}

				if (buttons[b.x - 1][b.y].value == 0)
					mapZeroSection(buttons[b.x - 1][b.y]);
				else if (buttons[b.x - 1][b.y].value != -1)
					setButtonText(buttons[b.x - 1][b.y]);
			}
			else //NO LEFT/RIGHT BORDER
			{
				if (b.y == 0) //TOP BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y + 1));

					foreach (Tuple<int, int> t in indices)
					{
						
					}

				}
				else if (b.y == rows - 1) //BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						
					}
				}
				else //NO TOP/BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y + 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));
					indices.Add(Tuple.Create(b.x - 1, b.y + 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						
					}
				}

				if (buttons[b.x - 1][b.y].value == 0)
					mapZeroSection(buttons[b.x - 1][b.y]);
				else if (buttons[b.x - 1][b.y].value != -1)
					setButtonText(buttons[b.x - 1][b.y]);

				if (buttons[b.x + 1][b.y].value == 0)
					mapZeroSection(buttons[b.x + 1][b.y]);
				else if (buttons[b.x + 1][b.y].value != -1)
					setButtonText(buttons[b.x + 1][b.y]);
			}
 * */
