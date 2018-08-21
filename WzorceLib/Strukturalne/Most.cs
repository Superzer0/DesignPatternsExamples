using System;

namespace WzorceLib.Strukturalne
{
    public abstract class Shape
    {
        private readonly IDrawable _drawLib;

        protected Shape(IDrawable drawLib)
        {
            _drawLib = drawLib;
        }

        public abstract string Draw();

        protected string DrawLine()
        {
            return _drawLib.DrawLine();
        }

        protected string DrawCircle()
        {
            return _drawLib.DrawCircle();
        }
    }


    public class Square : Shape
    {
        public Square(IDrawable drawLib)
            : base(drawLib)
        {
        }

        public override string Draw()
        {
            return base.DrawLine();
        }
    }


    public class Circle : Shape
    {
        public Circle(IDrawable drawLib)
            : base(drawLib)
        {
        }

        public override string Draw()
        {
            return base.DrawCircle();
        }
    }

    public interface IDrawable
    {
        string DrawLine();
        string DrawCircle();
    }

    public class DrawingLibOne : IDrawable
    {
        private readonly DrawingLegacyOne _legacyDrawingLib;

        public DrawingLibOne(DrawingLegacyOne legacyDrawingLib)
        {
            _legacyDrawingLib = legacyDrawingLib;
        }

        public string DrawLine()
        {
            return _legacyDrawingLib.Draw_Line();
        }

        public string DrawCircle()
        {
            return _legacyDrawingLib.Draw_Circle();
        }
    }

    public class DrawingLibTwo : IDrawable
    {
        private readonly DrawingLegacyTwo _drawingLib;

        public DrawingLibTwo(DrawingLegacyTwo drawingLib)
        {
            _drawingLib = drawingLib;
        }

        public string DrawLine()
        {
            return _drawingLib.Draw_Line();
        }

        public string DrawCircle()
        {
            return _drawingLib.Draw_Circle();
        }
    }


    public class DrawingLegacyOne
    {
        public string Draw_Line()
        {
            return "Drawing Line from Legacy Lib ONE";
        }

        public string Draw_Circle()
        {
            return "Drawing Circle from Legacy Lib ONE";
        }
    }

    public class DrawingLegacyTwo
    {
        public string Draw_Line()
        {
            return "Drawing Line from Legacy Lib TWO";
        }

        public string Draw_Circle()
        {
            return "Drawing Circle from Legacy Lib TWO";
        }
    }


    public class MostRunner : IExampleRunnable
    {
        public void Run()
        {
            var drawingLib1 = new DrawingLibTwo(new DrawingLegacyTwo());
            var drawingLib2 = new DrawingLibOne(new DrawingLegacyOne());

            var circleV1 = new Circle(drawingLib1);
            var circleV2 = new Circle(drawingLib2);

            var squareV1 = new Square(drawingLib1);
            var squareV2 = new Square(drawingLib2);

            var shapeList = new Shape[] {circleV1, circleV2, squareV1, squareV2};

            foreach (var shape in shapeList)
            {
                Console.WriteLine(shape.Draw());
            }
        }
    }

}
