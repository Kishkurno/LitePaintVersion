using System;
using System.Drawing;
using System.Windows.Forms;

namespace Grafic_editor
{
    public partial class Form1 : Form
    {

        Graphics g;
        Pen pen;
        Bitmap bmp;

        private Point old_coord;
        private Point current_coord;
        private string figure_type = "dot";

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pen = new Pen(Color.Black, 4);
            g = Graphics.FromImage(bmp);
        }

        void DrawFigure()
        {

            int x = Math.Min(old_coord.X, current_coord.X);
            int y = Math.Min(old_coord.Y, current_coord.Y);

            switch (figure_type)
            {

                case "line":
                    {
                        g.DrawLine(pen, old_coord, current_coord);
                        break;
                    }
                case "rectangle":
                    {
                        g.DrawRectangle(pen, x, y, Math.Abs(old_coord.X - current_coord.X), Math.Abs(old_coord.Y - current_coord.Y));
                        break;
                    }
                case "sector":
                    {

                        g.DrawPie(pen, new Rectangle(x, y, Math.Abs(old_coord.X - current_coord.X), Math.Abs(old_coord.Y - current_coord.Y)), 90, 270);
                        break;
                    }
                case "ellipse":
                    {
                        g.DrawEllipse(pen, x, y, Math.Abs(old_coord.X - current_coord.X), Math.Abs(old_coord.Y - current_coord.Y));
                        break;
                    }

            }
            pictureBox1.Image = bmp;

        }

        private void button1_Click (object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pen.Color = dialog.Color;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            figure_type = "rectangle";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            figure_type = "sector";
        }

        private void button6_Click (object sender, EventArgs e)
        {
            figure_type = "ellipse";
        }

        private void button7_Click (object sender, EventArgs e)
        {
            figure_type = "line";
        }
        private void button2_Click (object sender, EventArgs e)
        {
            g.Clear(Color.White);
        }

        private void pictureBox1_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                old_coord = new Point(e.X, e.Y);
            }

        }

        private void pictureBox1_Click_1 (object sender, MouseEventArgs e)
        {
            current_coord = new Point(e.X, e.Y);
            DrawFigure();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            figure_type = "dot";
        }
        private void button8_Click (object sender, EventArgs e)
        {
            figure_type = "eraser";
        }

        private void pictureBox1_Click_2(object sender, MouseEventArgs e)
        {
            textBox1.Text = e.X.ToString();
            textBox2.Text = e.Y.ToString();
            if (e.Button == MouseButtons.Left)
            {
                if (figure_type == "dot")
                    g.DrawEllipse(new Pen(pen.Color, 3), e.X - 1, e.Y - 1, 2, 2);

                else if (figure_type == "eraser")
                {
                    g.DrawEllipse(new Pen(Brushes.White, 3), e.X - 10, e.Y - 10, 20, 20);
                    g.FillEllipse(Brushes.White, e.X - 10, e.Y - 10, 20, 20);
                }

            }
            pictureBox1.Image = bmp;

        }

        private void button9_Click (object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "PNG|*.png";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(fileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                MessageBox.Show("Изображение не сохранено", "Диалог сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

     
    }
}
