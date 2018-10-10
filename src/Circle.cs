using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
    class Circle : Shape
    {
        private int _radius;
        private Color _clr;

        public Circle() : this(Color.Blue, 50)
        {
            
        }

        public Circle(Color clr, int radius)
        {
            _radius = radius;
            _clr = clr;
        }
        public int Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
            }
        }

        public override void Draw()
        {
            if (Selected)
                DrawOutline();
            SwinGame.FillCircle(Color.Red, PosX, PosY, _radius);
        }

        public override void DrawOutline()
        {
            SwinGame.DrawCircle(Color.Black, PosX, PosY, (_radius + 2));
        }

        public override bool IsAt(Point2D pt)
        {
            if (SwinGame.PointInCircle(pt, PosX, PosY, Radius) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void SaveTo(StreamWriter writer)
        {
            //writer.WriteLine("Circle");
            base.SaveTo(writer);
            writer.WriteLine(Radius);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            Radius = reader.ReadInteger();
        }
    }
}
