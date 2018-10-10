using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
using System.IO;

namespace MyGame
{
    class Drawing
    {
        private List<Shape> _shapes = new List<Shape>();
        private Color _background;

        public Drawing(Color background)
        {
            _shapes = new List<Shape>();
            _background = background;
        }

        public Drawing() : this (Color.White)
        {

        }

        public List<Shape> Shapes
        {
            get
            {
                return _shapes;
            }
        }
        public List<Shape> SelectedShapes
        {
            get
            {
                List<Shape> _result = new List<Shape>();

                foreach (Shape s in _shapes)
                {
                    if (s.Selected == true)
                    {
                        _result.Add(s);
                    }
                }
                return _result;
            }
        }

        public int ShapeCount
        {
            get
            {
                return _shapes.Count;
            }
           
        }

        public Color Background
        {
            get
            {
                return _background;
            }
            set
            {
                _background = value;
            }
        }

        public void Save(string filename)
        {
            StreamWriter writer = new StreamWriter(Path.Combine("/users/jack/Desktop/", filename));
            

            writer.WriteLine(Background.ToArgb());
            writer.WriteLine(ShapeCount);

            foreach (Shape s in _shapes)
            {
                s.SaveTo(writer);
            }

            writer.Dispose();
        }
        public void Draw()
        {
            SwinGame.ClearScreen(_background);
            foreach (Shape s in _shapes)
            {
                s.Draw();
            }
        }

        public void RemoveShapes()
        {
            foreach (Shape s in SelectedShapes)
            {
                    _shapes.Remove(s);
            }
        }

        public void SelectShapesAt(Point2D pt)
        {
            foreach (Shape s in _shapes)
            {
                if ((s.IsAt(pt)) && s.Selected == true)
                {
                    s.Selected = false;
                }
                else
                {
                    if (s.IsAt(pt))
                    {
                        s.Selected = true;
                    }
                }
                
            }
        }

        public void AddShape(Shape s)
        {
            _shapes.Add(s);
        }

        public void Load(string filename)
        {
            StreamReader reader = new StreamReader(Path.Combine("/users/jack/Desktop/", filename));
            try
            {
                Background = Color.FromArgb(reader.ReadInteger());
                int count = reader.ReadInteger();

                
                for (int i = 0; i < count; i++)
                {
                    Shape s = Shape.CreateShape(reader.ReadLine());
                    s.LoadFrom(reader);
                    _shapes.Add(s);
                }
                
            }
            finally
            {
                reader.Close();
            } 
        }
    }
}
