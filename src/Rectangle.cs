using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
    class Rectangle : Shape
    {
        private int _width;
        private int _height;
        private Color _clr;

        public Rectangle() : this (Color.Green, 0, 0, 100, 100)
        {

        }

        public Rectangle(Color clr, float x, float y, int width, int height)
            : base(clr)
        {

            PosX = x;
            PosY = y;
            Width = width;
            Height = height;
            _clr = clr;
        }

       public int Width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public override void Draw()
        {
            if (Selected)
                DrawOutline();
            SwinGame.FillRectangle(_clr, PosX, PosY, Width, Height);
        }

        public override void DrawOutline()
        {
            SwinGame.DrawRectangle(Color.Black, (PosX - 2), (PosY - 2), (Width + 4), (Height + 4));
        }

        public override bool IsAt(Point2D pt)
        {
            if (SwinGame.PointInRect(pt, PosX, PosY, Width, Height) == true)
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
            //writer.WriteLine("Rectangle");
            base.SaveTo(writer);
            writer.WriteLine(Width);
            writer.WriteLine(Height);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            Width = reader.ReadInteger();
            Height = reader.ReadInteger();
        }

    }

}
