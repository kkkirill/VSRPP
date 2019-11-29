using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace MiniDraw
{
    public partial class DrawForm : Form
    {
        private Point startCursorPoint, endCursorPoint;
        private Rectangle rectangle;
        private Pen pen;
        private SolidBrush brush;

        private bool isFilled = false;
        private bool isPainting = false;

        private List<MyRectangle> figures;

        Graphics graphics;

        public DrawForm()
        {
            InitializeComponent();

            graphics = canvas.CreateGraphics();
            figures = new List<MyRectangle>();
            pen = new Pen(Color.Black, 2);
            brush = new SolidBrush(Color.Red);
        }

        private void RedrawContent(bool isClearRequired = false)
        {
            if (isClearRequired) 
                graphics.Clear(canvas.BackColor);

            var rectangles = figures.Where(fig => fig.IsDrawn)
                                    .Select(fig => fig.Rectangle)
                                    .ToArray();

            if (rectangles.Length == 0)
                return;

            if (isFilled)
                graphics.FillRectangles(brush, rectangles);
            else
                graphics.DrawRectangles(pen, rectangles);
        }

        private void ClearAll(object sender, EventArgs e)
        {
            figures.Clear();
            RedrawContent(true);
        }

        private void Undo(object sender, EventArgs e)
        {
            if (figures.All(fig => !fig.IsDrawn))
                return;

            figures.Last(fig => fig.IsDrawn).IsDrawn = false;

            RedrawContent(true);
        }

        private void Redo(object sender, EventArgs e)
        {
            if (figures.All(fig => fig.IsDrawn))
                return;

            figures.First(fig => !fig.IsDrawn).IsDrawn = true;
            RedrawContent(true);
        }

        private void RedoLastThree(object sender, EventArgs e)
        {
            if (figures.All(fig => fig.IsDrawn))
                return;

            var values = figures.Where(fig => !fig.IsDrawn).ToList();

            values.Take(values.Count > 3 ? 3 : values.Count).ToList().ForEach(v => v.IsDrawn = true);
            RedrawContent(true);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startCursorPoint = endCursorPoint = e.Location;
                isPainting = true;
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isPainting && e.Button == MouseButtons.Left)
            {
                endCursorPoint = e.Location;
                canvas.Refresh();
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            figures.Add(new MyRectangle(rectangle));
            isPainting = false;
        }

        private void DrawRectangle()
        {
            rectangle = new Rectangle(Math.Min(startCursorPoint.X, endCursorPoint.X),
                                      Math.Min(startCursorPoint.Y, endCursorPoint.Y),
                                      Math.Abs(endCursorPoint.X - startCursorPoint.X),
                                      Math.Abs(endCursorPoint.Y - startCursorPoint.Y));

            if (isFilled)
                graphics.FillRectangle(brush, rectangle);
            else
                graphics.DrawRectangle(pen, rectangle);

        }

        private void FillCheckboxStateChanged(object sender, EventArgs e)
        {
            isFilled = fillButton.Checked;
            RedrawContent();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (isPainting)
                DrawRectangle();
            RedrawContent();
        }
    }
}
