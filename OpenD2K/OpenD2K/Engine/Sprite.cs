using System.Drawing;

namespace OpenD2K.Engine
{
    public class Sprite
    {
        public Vector2 Position;
        public Vector2 Size;
        public Color Color;
        public Image Image;

        public Sprite(Vector2 position, Image image)
        {
            Position = position;
            Image = image;

            Size = new Vector2(image.Width, image.Height);
        }

        public Sprite(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
            Color = Color.Black;
        }

        public Sprite(Vector2 position, Vector2 size, Color color)
        {
            Position = position;
            Size = size;
            Color = color;
        }
    }
}
