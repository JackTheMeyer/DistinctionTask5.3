using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
    class Line : Shape
    {
        private float _xend;
        private float _yend;
        private Color _clr;
        
        public Line() : this(Color.Blue, 0, 0, 100, 100)
        {

        }

        public Line(Color clr, float XStart, float YStart, float XFin, float YFin)
        {
            _xend = XFin;
            _yend = YFin;
            _clr = clr;
        }

        public float PosXEnd
        {
            get
            {
                return _xend;
            }
            set
            {
                _xend = value;
            }
        }

        public float PosYEnd
        {
            get
            {
                return _yend;
            }
            set
            {
                _yend = value;
            }
        }

        public override void Draw()
        {
            if (Selected)
                DrawOutline();
            SwinGame.DrawLine(_clr, PosX, PosY, PosXEnd, PosYEnd);
        }

        public override void DrawOutline()
        {
            SwinGame.DrawCircle(Color.Black, (PosX), (PosY), 20);
            SwinGame.DrawCircle(Color.Black, PosXEnd, PosYEnd, 20);
        }

        public override bool IsAt(Point2D pt)
        {
            if (SwinGame.PointOnLine(pt, PosX, PosY, PosXEnd, PosYEnd))
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
            //writer.WriteLine("Line");
            base.SaveTo(writer);
            writer.WriteLine(PosXEnd);
            writer.WriteLine(PosYEnd);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            PosXEnd = reader.ReadInteger();
            PosYEnd = reader.ReadInteger();
        }

    }
}
