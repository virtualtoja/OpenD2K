using OpenD2K.Engine;
using System.Drawing;

namespace OpenD2K.Game
{
    public class MainGame : D2KEngine
    {
        public MainGame() : base(1280, 720, "OpenD2K") { }

        Sprite spr;

        public override void Start()
        {
            spr = new Sprite(new Vector2(0, 0), new Vector2(50, 50), Color.White);
            AddSprite(spr);
        }

        public override void Update()
        {
            spr.Position = new Vector2(MouseX, MouseY);
        }
    }
}
