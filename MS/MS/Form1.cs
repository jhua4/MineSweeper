using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		private int rows = 16;
		private int cols = 30;
		private int bombs = 99;
		private int flags = 0;
		private int spacesLeft = 0;

		private mButton[][] buttons;

		private enum ButtonClicked { Left, Right };
		ButtonClicked bClicked;

		private bool firstClick = true;

		private Stopwatch sw;

		private void Form1_Load(object sender, EventArgs e)
		{
			buttons = new mButton[rows][];

			for (int i = 0; i < rows; i++)
			{
				buttons[i] = new mButton[cols];
			}

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					mButton m = new mButton(buttonSize);
					m.Location = new Point(i * buttonSize, j * buttonSize);
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

			newGame();
		}

		private void newGame()
		{
			timer1.Stop();
			sw = new Stopwatch();

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					buttons[i][j].Text = "";
					buttons[i][j].BackColor = SystemColors.ControlDark;
					buttons[i][j].ForeColor = Color.Black;
					buttons[i][j].Image = null;
					buttons[i][j].value = 0;
				}
			}

			int[][] matrix = new int[rows][];
			for (int i = 0; i < rows; i++)
			{
				matrix[i] = new int[cols];
			}

			int[] set = new int[rows * cols];
			List<int> currentSet = new List<int>();
			for (int i = 0; i < bombs; i++)
			{
				int[] result = CreateBombIndex(currentSet, rows * cols);

				int x_index = result[0];
				int y_index = result[1];
				int index = result[2];

				currentSet.Add(index);

				matrix[x_index][y_index] = 1;
			}

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					if (matrix[i][j] == 1)
					{
						buttons[i][j].value = -1;
					}
				}
			}

			MapValues(matrix);
			
			flags = 0;

			spacesLeft = rows * cols - bombs;

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					if (buttons[i][j].value == 0)
						spacesLeft--;
				}
			}

			firstClick = true;
			bombsLeftLabel.Text = "Bombs Left: " + bombs.ToString();
			MessageLabel.Text = "";
		}

		private void MapValues(int[][] matrix)
		{
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					//MapButtonValue(rows * i + j);
					if (buttons[i][j].value != -1)
					{
						buttons[i][j].value = MapButtonValue(i, j, matrix);
					}
				}
			}
		}

		private int MapButtonValue(int x, int y, int[][] matrix)
		{
			int bombCount = 0;

			if (x == 0) //LEFT BORDER
			{
				if (y == 0) //TOP BORDER
				{
					if (matrix[x][y + 1] == 1)
						bombCount++;
					if (matrix[x + 1][y + 1] == 1)
						bombCount++;
				} else if (y == cols - 1) //BOTTOM BORDER
				{
					if (matrix[x][y - 1] == 1)
						bombCount++;
					if (matrix[x + 1][y - 1] == 1)
						bombCount++;
				}
				else //NO TOP/BOTTOM BORDER
				{
					if (matrix[x][y - 1] == 1)
						bombCount++;
					if (matrix[x][y + 1] == 1)
						bombCount++;
					if (matrix[x + 1][y - 1] == 1)
						bombCount++;
					if (matrix[x + 1][y + 1] == 1)
						bombCount++;
				}

				if (matrix[x + 1][y] == 1)
					bombCount++;
			}
			else if (x == rows - 1) //RIGHT BORDER
			{
				if (y == 0) //TOP BORDER
				{
					if (matrix[x][y + 1] == 1)
						bombCount++;
					if (matrix[x - 1][y + 1] == 1)
						bombCount++;
				}
				else if (y == cols - 1) //BOTTOM BORDER
				{
					if (matrix[x][y - 1] == 1)
						bombCount++;
					if (matrix[x - 1][y - 1] == 1)
						bombCount++;
				}
				else //NO TOP/BOTTOM BORDER
				{
					if (matrix[x][y - 1] == 1)
						bombCount++;
					if (matrix[x][y + 1] == 1)
						bombCount++;
					if (matrix[x - 1][y - 1] == 1)
						bombCount++;
					if (matrix[x - 1][y + 1] == 1)
						bombCount++;
				}

				if (matrix[x - 1][y] == 1)
					bombCount++;
			} else //NO LEFT/RIGHT BORDER
			{
				if (y == 0) //TOP BORDER
				{
					if (matrix[x][y + 1] == 1)
						bombCount++;
					if (matrix[x + 1][y + 1] == 1)
						bombCount++;
					if (matrix[x - 1][y + 1] == 1)
						bombCount++;
				}
				else if (y == cols - 1) //BOTTOM BORDER
				{
					if (matrix[x][y - 1] == 1)
						bombCount++;
					if (matrix[x + 1][y - 1] == 1)
						bombCount++;
					if (matrix[x - 1][y - 1] == 1)
						bombCount++;
				}
				else //NO TOP/BOTTOM BORDER
				{
					if (matrix[x][y - 1] == 1)
						bombCount++;
					if (matrix[x][y + 1] == 1)
						bombCount++;
					if (matrix[x + 1][y - 1] == 1)
						bombCount++;
					if (matrix[x + 1][y + 1] == 1)
						bombCount++;
					if (matrix[x - 1][y - 1] == 1)
						bombCount++;
					if (matrix[x - 1][y + 1] == 1)
						bombCount++;
				}

				if (matrix[x - 1][y] == 1)
					bombCount++;
				if (matrix[x + 1][y] == 1)
					bombCount++;
			}

			return bombCount;
		}

		private int[] CreateBombIndex(List<int> exclude, int max)
		{
			bool found = false;

			while (!found)
			{
				Random r = new Random();
				int value = r.Next(0, max);

				if (!exclude.Contains(value))
				{
					found = true;

					int x_index = (int)Math.Floor((double)(value / cols));
					int y_index = value % cols;

					return new int[3] { x_index, y_index, value };
				}
			}

			return null;
		}

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
							//int value = b.value;
							//b.Text = value.ToString();
						}
					} else
					{
						if (firstClick)
						{
							firstClick = false;
							moveMine(b);
							updateButtonFromBomb(b);
							setButtonText(b);
						} else
						{
							b.Image = (Image)MS.Properties.Resources.ResourceManager.GetObject("Bomb");
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
						bombsLeftLabel.Text = "Bombs Left: " + (bombs - flags).ToString();
					} else
					{
						b.isFlagged = false;
						b.Image = null;
						flags--;
						bombsLeftLabel.Text = "Bombs Left: " + (bombs - flags).ToString();
					}

				}
			}
		}

		private void lostGame()
		{
			timer1.Stop();

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
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
						buttons[i][j].Image = (Image)MS.Properties.Resources.ResourceManager.GetObject("Bomb");
					}
				}
			}

			MessageLabel.Text = "You lost...";
			MessageLabel.ForeColor = Color.Blue;
		}

		private void mapZeroSection(mButton b)
		{
			if (b.BackColor == Color.Gray)
				return;

			b.BackColor = Color.Gray;

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
							//int val = buttons[t.Item1][t.Item2].value;
							//buttons[t.Item1][t.Item2].Text = val.ToString();
						}
					}
				}
				else if (b.y == cols - 1) //BOTTOM BORDER
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
							//int val = buttons[t.Item1][t.Item2].value;
							//buttons[t.Item1][t.Item2].Text = val.ToString();
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
							//int val = buttons[t.Item1][t.Item2].value;
							//buttons[t.Item1][t.Item2].Text = val.ToString();
						}
					}
				}

				if (buttons[b.x + 1][b.y].value == 0)
					mapZeroSection(buttons[b.x + 1][b.y]);
				else if (buttons[b.x + 1][b.y].value != -1)
					setButtonText(buttons[b.x + 1][b.y]);
					//buttons[b.x + 1][b.y].Text = buttons[b.x + 1][b.y].value.ToString();
			}
			else if (b.x == rows - 1) //RIGHT BORDER
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
							//int val = buttons[t.Item1][t.Item2].value;
							//buttons[t.Item1][t.Item2].Text = val.ToString();
						}
					}
				}
				else if (b.y == cols - 1) //BOTTOM BORDER
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
							//int val = buttons[t.Item1][t.Item2].value;
							//buttons[t.Item1][t.Item2].Text = val.ToString();
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
							//int val = buttons[t.Item1][t.Item2].value;
							//buttons[t.Item1][t.Item2].Text = val.ToString();
						}
					}
				}

				if (buttons[b.x - 1][b.y].value == 0)
					mapZeroSection(buttons[b.x - 1][b.y]);
				else if (buttons[b.x - 1][b.y].value != -1)
					setButtonText(buttons[b.x - 1][b.y]);
					//buttons[b.x - 1][b.y].Text = buttons[b.x - 1][b.y].value.ToString();
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
							//int val = buttons[t.Item1][t.Item2].value;
							//buttons[t.Item1][t.Item2].Text = val.ToString();
						}
					}

				}
				else if (b.y == cols - 1) //BOTTOM BORDER
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
							//int val = buttons[t.Item1][t.Item2].value;
							//buttons[t.Item1][t.Item2].Text = val.ToString();
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
							//int val = buttons[t.Item1][t.Item2].value;
							//buttons[t.Item1][t.Item2].Text = val.ToString();
						}
					}
				}

				if (buttons[b.x - 1][b.y].value == 0)
					mapZeroSection(buttons[b.x - 1][b.y]);
				else if (buttons[b.x - 1][b.y].value != -1)
					setButtonText(buttons[b.x - 1][b.y]);
					//buttons[b.x - 1][b.y].Text = buttons[b.x - 1][b.y].value.ToString();

				if (buttons[b.x + 1][b.y].value == 0)
					mapZeroSection(buttons[b.x + 1][b.y]);
				else if (buttons[b.x + 1][b.y].value != -1)
					setButtonText(buttons[b.x + 1][b.y]);
					//buttons[b.x + 1][b.y].Text = buttons[b.x + 1][b.y].value.ToString();
			}
		}

		private void setButtonText(mButton b)
		{
			int val = b.value;
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
			if (spacesLeft == 0)
			{
				wonGame();
			}
		}

		private void wonGame()
		{
			timer1.Stop();
			MessageLabel.Text = "You won in " + sw.Elapsed.ToString(@"mm\:ss\!");
			MessageLabel.ForeColor = Color.Green;
			bombsLeftLabel.Text = "Bombs Left: 0";

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
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
						buttons[i][j].Image = (Image)MS.Properties.Resources.ResourceManager.GetObject("Bomb");
					}
				}
			}
		}

		private void updateButtonFromBomb(mButton b)
		{
			int bombCount = 0;

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
							bombCount++;
					}
				}
				else if (b.y == cols - 1) //BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x + 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value--;
						else
							bombCount++;
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
							bombCount++;
					}
				}

				if (buttons[b.x + 1][b.y].value != -1)
					buttons[b.x + 1][b.y].value--;
				else
					bombCount++;
			}
			else if (b.x == rows - 1) //RIGHT BORDER
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
							bombCount++;
					}
				}
				else if (b.y == cols - 1) //BOTTOM BORDER
				{
					List<Tuple<int, int>> indices = new List<Tuple<int, int>>();
					indices.Add(Tuple.Create(b.x, b.y - 1));
					indices.Add(Tuple.Create(b.x - 1, b.y - 1));

					foreach (Tuple<int, int> t in indices)
					{
						if (buttons[t.Item1][t.Item2].value != -1)
							buttons[t.Item1][t.Item2].value--;
						else
							bombCount++;
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
							bombCount++;
					}
				}

				if (buttons[b.x - 1][b.y].value != -1)
					buttons[b.x - 1][b.y].value--;
				else
					bombCount++;
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
							bombCount++;
					}

				}
				else if (b.y == cols - 1) //BOTTOM BORDER
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
							bombCount++;
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
							bombCount++;
					}
				}

				if (buttons[b.x - 1][b.y].value != -1)
					buttons[b.x - 1][b.y].value--;
				else
					bombCount++;

				if (buttons[b.x + 1][b.y].value != -1)
					buttons[b.x + 1][b.y].value--;
				else
					bombCount++;
			}

			b.value = bombCount;
		}

		private void updateButtonToBomb(mButton b)
		{
			//set value to bomb value and update surrounding tiles
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
				else if (b.y == cols - 1) //BOTTOM BORDER
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
			else if (b.x == rows - 1) //RIGHT BORDER
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
				else if (b.y == cols - 1) //BOTTOM BORDER
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
				else if (b.y == cols - 1) //BOTTOM BORDER
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

		private void moveMine(mButton b)
		{
			//move mine to top left corner if it is blank, unless user clicked top left corner or there is already a bomb there
			//otherwise search for first non-bomb tile from left to right, up to down
			if (b.x == 0 && b.y == 0)
			{
				//MessageBox.Show("7");
				for (int i = 0; i < rows; i++)
				{
					for (int j = 0; j < cols; j++)
					{
						if (buttons[i][j].value != -1)
						{
							updateButtonToBomb(buttons[i][j]);
							return;
						}
					}
				}
			} else
			{
				if (buttons[0][0].value == -1)
				{
					for (int i = 0; i < rows; i++)
					{
						for (int j = 0; j < cols; j++)
						{
							if (buttons[i][j].value != -1)
							{
								updateButtonToBomb(buttons[i][j]);
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

		private void newGameButton_Click(object sender, EventArgs e)
		{
			newGame();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			timerLabel.Text = sw.Elapsed.ToString(@"mm\:ss");
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
				else if (b.y == cols - 1) //BOTTOM BORDER
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
			else if (b.x == rows - 1) //RIGHT BORDER
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
				else if (b.y == cols - 1) //BOTTOM BORDER
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
				else if (b.y == cols - 1) //BOTTOM BORDER
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
