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

        private bool isFilled = false; // тановится true как только включаем галочку использовать заливку
        private bool isPainting = false; // становится true как только мы рисуем по канвасу

        private List<MyRectangle> figures; // здесь хранятся все фигуры, которые мы рисуем

        Graphics graphics;  // это объект графики из канваса на котором мы рисуем

        public DrawForm()
        {
            InitializeComponent();

            graphics = canvas.CreateGraphics(); //
            figures = new List<MyRectangle>();  //
            pen = new Pen(Color.Black, 2);      //
            brush = new SolidBrush(Color.Red);  //
                                                // создали pen для рисования незакрашенных прямоугольников и 
                                                // brush для закрашенных
        }

        private void RedrawContent(bool isClearRequired = false) // перерисовка фигур
        {
            if (isClearRequired) //если нужна очистка, то здесь будет true
                graphics.Clear(canvas.BackColor);

            var rectangles = figures.Where(fig => fig.IsDrawn)      // берём все фигуры у который флаг isDrawn == true, т.е. фигуры которые мы хотим отрисовать
                                    .Select(fig => fig.Rectangle)   // из полученных объектов MyRectangle для каждого объекта достаём поле Rectangle, 
                                                                    // которое можно отрисовывать на канвасе (Rectangle это встроенный класс уже, 
                                                                    // а MyRectangle прописанный мной класс с дополнительным полем isDrawn для определения рисовать фигуру или нет) 
                                    .ToArray();                     // преобразовываем в массив

            if (rectangles.Length == 0) // если нечего рисовать
                return;

            if (isFilled)   // если стоит галочка рисовать закрашенные прямоугольники
                graphics.FillRectangles(brush, rectangles); // рисуем закрашенный прямоуглоьник
            else            
                graphics.DrawRectangles(pen, rectangles);   // рисуем обычный прямоуглоьник
        }

        private void ClearAll(object sender, EventArgs e)  // очистка всего и перерисовка всех фигур
        {
            figures.Clear();
            RedrawContent(true);
        }

        private void Undo(object sender, EventArgs e)  // при нажатии кнопки undo
        {
            if (figures.All(fig => !fig.IsDrawn))   //  проверка если все фигуры уже ненарисованы, то выход
                return;
 
            figures.Last(fig => fig.IsDrawn).IsDrawn = false;  //иначе берём последнюю нарисованную фигуру и ставим флаг не отрисовывать false

            RedrawContent(true);    // перерисовываем все фигуры
        }

        private void Redo(object sender, EventArgs e) // при нажатии кнопки redo
        {
            if (figures.All(fig => fig.IsDrawn))    //  проверка если все фигуры уже нарисованы, то выход
                return;

            figures.First(fig => !fig.IsDrawn).IsDrawn = true;  //иначе берём последнюю ненарисованную фигуру и ставим флаг отрисовывать true
            RedrawContent(true);    // перерисовываем все фигуры
        }

        private void RedoLastThree(object sender, EventArgs e)  // при нажатии кнопки redo3 (по твоему заданию)
        {
            if (figures.All(fig => fig.IsDrawn))    //  проверка если все фигуры уже нарисованы, то выход
                return;

            var values = figures.Where(fig => !fig.IsDrawn).ToList();   // в values записывются все ненарисованные фигуры

            values.Take(values.Count > 3 ? 3 : values.Count)    // если длина values > 3 то берём первые 3 фигуры 
                                                                // (иначе если длина меньше 3ёх, то берём сколько есть)
            .ToList().ForEach(v => v.IsDrawn = true);           // для каждого элемента ставим флаг отрисовывать true
                                                                                                        
            
            RedrawContent(true);    // перерисовываем все фигуры
        }

        private void OnMouseDown(object sender, MouseEventArgs e)   // как только нажали мышь
        {
            if (e.Button == MouseButtons.Left)  // проверка наажли ли мы левую кнопку мыши
            {
                startCursorPoint = endCursorPoint = e.Location; //запоминаем начальную и сразу конечную позицию курсора 
                                                                //(вдруг пользователь не будет рисовать и нажмёт на мышку случайно)
                isPainting = true;                              // тот самый флаг, что мы в находимся в процессе рисования ставим true
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)   // вызывается как мы водим мышь по канвасу (даже когда не нажали на мышку)
        {
            if (isPainting && e.Button == MouseButtons.Left)    // проверям нажата ли левая клавиша мыши 
                                                                // (т.е. проверяем рисуем ли мы или просто водим мышку по экрану)
            {
                endCursorPoint = e.Location;                    // запоминаем позицию курсора
                canvas.Refresh();                               // обновляем сам canvas необходимо, чтобы прямоугольник постоянно обновлял свои размеры, когда мы двигаем мышку
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)     // когда отпустили клавишу мыши
        {
            figures.Add(new MyRectangle(rectangle));                // доабвляем новую фигуру
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
