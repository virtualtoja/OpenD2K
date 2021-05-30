using System.Drawing;

namespace OpenD2K.Engine
{
    public struct Vector2
    {
        public int X;
        public int Y;

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point ToPoint() => new Point(X, Y);
        public Size ToSize() => new Size(X, Y);
    }
}
