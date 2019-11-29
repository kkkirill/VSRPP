using System.Drawing;

namespace MiniDraw
{
    class MyRectangle
    {
        public Rectangle Rectangle { get; set; }
        public bool IsDrawn { get; set; }

        public MyRectangle(Rectangle rectangle, bool isDrawn = true)
        {
            Rectangle = rectangle;
            IsDrawn = isDrawn;
        }
    }
}
