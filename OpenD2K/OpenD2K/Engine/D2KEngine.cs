using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Threading;
using System;

namespace OpenD2K.Engine
{
    public class DBWindow : Form
    {
        public DBWindow(int w, int h, string title)
        {
            this.Width = w;
            this.Height = h;
            this.Text = title;

            this.DoubleBuffered = true;
        }
    }

    public abstract class D2KEngine
    {
        #region Variables
        public Color bg;
        public int fps = 60;

        public int MouseX;
        public int MouseY;

        private MouseButtons button;
        private bool mouse;

        private List<Keys> keys;
        private bool keyPress;


        private List<Sprite> sprites;
        private Thread thrd;
        private DBWindow window;
        #endregion

        #region Constructor
        public D2KEngine(int w, int h, string title)
        {
            window = new DBWindow(w, h, title);
            thrd = new Thread(Thrd);
            sprites = new List<Sprite>();

            window.Paint += Window_Paint;
            window.KeyDown += Window_KeyDown;
            window.KeyUp += Window_KeyUp;
            window.MouseDown += Window_MouseDown;
            window.MouseUp += Window_MouseUp;
            window.MouseMove += Window_MouseMove;

            thrd.Start();
            Start();
            Application.Run(window);
        }


        #endregion

        #region Methods 
        public abstract void Update();
        public abstract void Start();

        private Image ResizeImage(Image imgToResize, Vector2 newSize)
        {
            Size size = newSize.ToSize();
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void Thrd()
        {
            Thread.Sleep(500);

            try
            {
                while (true)
                {
                    window.BeginInvoke((MethodInvoker)delegate { window.Refresh(); });
                    Thread.Sleep(1000 / fps);
                    Update();
                }
            }
            catch (Exception e)
            {
                if(!e.Message.StartsWith("Invoke or BeginInvoke"))
                {
                    MessageBox.Show(e.ToString());
                }
                Application.Exit();
            }
        }

        public bool GetKey(Keys key) => (keys.Contains(key) && keyPress);
        public bool GetMouseButton(MouseButtons btn) => (btn == button && mouse);

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.Location;
            MouseX = point.X;
            MouseY = point.Y;
        }

        private void Window_MouseUp(object sender, MouseEventArgs e)
        {
            button = MouseButtons.None;
            mouse = false;
        }

        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            button = e.Button;
            mouse = true;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            List<Keys> newKeys = new List<Keys>();
            
            foreach(Keys key in keys)
            {
                if (key != e.KeyCode)
                    newKeys.Add(key);
            }
            keys.Clear();

            foreach(Keys key in newKeys)
            {
                keys.Add(key);
            }

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            keys.Add(e.KeyCode);
            keyPress = true;
        }

        public void AddSprite(Sprite sprite)
        {
            sprites.Add(sprite);
        }

        private void Window_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(bg);

            foreach(Sprite sprite in sprites)
            {
                if (sprite.Image == null)
                    e.Graphics.FillRectangle(new SolidBrush(sprite.Color), new Rectangle(sprite.Position.ToPoint(), sprite.Size.ToSize()));
                else
                    e.Graphics.DrawImage(ResizeImage(sprite.Image, sprite.Size), sprite.Position.ToPoint());
            }
        }
        #endregion
    }
}
