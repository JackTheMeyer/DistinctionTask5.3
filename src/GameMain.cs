using System;
using SwinGameSDK;

namespace MyGame
{
    public class GameMain
    {
        private enum ShapeKind
        {
            Rectangle,
            Circle,
            Line
        }

        public static void Main()
        {
            Shape.RegisterShape("Rectangle", typeof(Rectangle));
            Shape.RegisterShape("Circle", typeof(Circle));
            Shape.RegisterShape("Line", typeof(Line));


            //Start the audio system so sound can be played
            SwinGame.OpenAudio();
            //Open the game window
            SwinGame.OpenGraphicsWindow("GameMain", 800, 600);
            SwinGame.ShowSwinGameSplashScreen();
            
            //Run the game loop

            Drawing myDrawing = new Drawing();
            ShapeKind KindToAdd = new ShapeKind();
            KindToAdd = ShapeKind.Circle;

            while (false == SwinGame.WindowCloseRequested())
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();

                //Clear the screen and draw the framerate
                SwinGame.ClearScreen(Color.White);
                myDrawing.Draw();
                SwinGame.DrawFramerate(0, 0);

                // Adds shape if left click
                if (SwinGame.MouseClicked(MouseButton.LeftButton))
                {
                    
                    Shape newShape;

                    if (KindToAdd == ShapeKind.Circle)
                    {
                        Circle newCircle = new Circle();
                        newCircle.PosX = SwinGame.MouseX();
                        newCircle.PosY = SwinGame.MouseY();
                        newShape = newCircle;
                    }
                    else if (KindToAdd == ShapeKind.Rectangle)
                    {
                        Rectangle newRect = new Rectangle();
                        newRect.PosX = SwinGame.MouseX();
                        newRect.PosY = SwinGame.MouseY();
                        newShape = newRect;
                    }
                    else
                    {
                       Line newLine = new Line();
                       newLine.PosX = SwinGame.MouseX() - 40;
                       newLine.PosY = SwinGame.MouseY();
                       newLine.PosXEnd = SwinGame.MouseX() + 40;
                       newLine.PosYEnd = SwinGame.MouseY();

                        newShape = newLine;
                    }
                    myDrawing.AddShape(newShape);
                }

                // Selects || Deselects shape
                if (SwinGame.MouseClicked(MouseButton.RightButton))
                {
                    myDrawing.SelectShapesAt(SwinGame.MousePosition());
                }
                    
                // Deletes selected shapes
                if ((SwinGame.KeyDown(KeyCode.BackspaceKey)) || (SwinGame.KeyDown(KeyCode.DeleteKey)))
                {
                    myDrawing.RemoveShapes();
                }

                // Changes background color if space pressed.
                if (SwinGame.KeyDown(KeyCode.SpaceKey))
                {
                    myDrawing.Background = SwinGame.RandomColor();
                }
                
                //Press R change KindToAdd to Rectangle
                if (SwinGame.KeyDown(KeyCode.RKey))
                {
                    KindToAdd = ShapeKind.Rectangle;
                }

                //Press C KindToAdd to change to Circle
                if (SwinGame.KeyDown(KeyCode.CKey))
                {
                    KindToAdd = ShapeKind.Circle;
                }

                //Press L KindToAdd to change to Line
                if (SwinGame.KeyDown(KeyCode.LKey))
                {
                    KindToAdd = ShapeKind.Line;
                }

                if (SwinGame.KeyDown(KeyCode.SKey))
                {
                    myDrawing.Save("ShapeSaveFile.txt");
                }

                if (SwinGame.KeyDown(KeyCode.OKey))
                {
                    try
                    {
                        myDrawing.Load("ShapeSaveFile.txt");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error loading file: {0}", e.Message);
                    }
                }
                SwinGame.RefreshScreen(60);
            }
        }
    }
}