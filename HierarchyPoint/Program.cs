using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Point2
{
    
    enum Color
    {
        Black,
        White,
        Red,
        Yellow,
        Green,
        Blue
    }
    class Point
    {
        public double X{ get; set; }
        public double Y { get; set; }
        public Point()
        {
            X = 0;
            Y = 0;
        }
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        public static Point operator + (Point p1, Point p2)
        {
            Point temp = new Point();
            temp.X = p1.X + p2.X;
            temp.Y = p1.Y + p2.Y;
            return temp;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (object.ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            Point tmp = obj as Point;
            if (tmp == null) return false;
            if (this.X == tmp.X && this.Y == tmp.Y) return true;
            else return false;
        }
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();
        public override string ToString()
        {
            return $"X: {X} Y: {Y}";
        }
        public void Show()
        {
            Console.WriteLine(this.ToString());
        }
        
    }
    class ColoredPoint : Point
    {
        public Color Color { get; set; }
        public ColoredPoint() : base()
        {
            Color = Color.Black;
        }
        public ColoredPoint(double x, double y, Color color) : base(x, y)
        {
            Color = color;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (object.ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            ColoredPoint tmp = obj as ColoredPoint;
            if (tmp == null) return false;
            if (this.X == tmp.X && this.Y == tmp.Y && this.Color == tmp.Color) return true;
            else return false;
        }
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();

        public override string ToString()
        {
            return base.ToString() + $" Color: {Color}";
        }
    }
    class Line : IComparable
    {
        protected Point[] coord;
        public Line()
        {
            coord = new Point[2];
            coord[0] = new Point(0, 0);
            coord[1] = new Point(1, 1);
        }
        public Line(Point start, Point end)
        {
            coord = new Point[2];
            coord[0] = start;
            coord[1] = end;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (object.ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            Line tmp = obj as Line;
            if (tmp == null) return false;
            if (this.coord[0].X == tmp.coord[0].X && this.coord[0].Y == tmp.coord[0].Y && 
                this.coord[1].X == tmp.coord[1].X && this.coord[1].Y == tmp.coord[1].Y)
                return true;
            else return false;
        }
        public int CompareTo(object obj)
        {
            double lenThis, lenObj;
            Line tmp = obj as Line;
            lenThis = Math.Sqrt(Math.Pow(this.coord[0].X - this.coord[1].X, 2) + Math.Pow(this.coord[0].Y - this.coord[1].Y, 2));
            lenObj = Math.Sqrt(Math.Pow(tmp.coord[0].X - tmp.coord[1].X, 2) + Math.Pow(tmp.coord[0].Y - tmp.coord[1].Y, 2));
            if (lenThis > lenObj) return 1;
            if (lenThis < lenObj) return -1;
            return 0;
        }
        public override int GetHashCode() => this.coord[0].GetHashCode();
        public virtual void PrintCoord()
        {
            foreach (var point in coord)
                point.Show();
        }
    }


    class ColoredLine : Line
    {
        public Color Color { get; set; }
        public ColoredLine():base()
        {
            Color = Color.Black;
        }
        public ColoredLine(Point start, Point end, Color color):base(start, end)
        {
            Color = color;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (object.ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            ColoredLine tmp = obj as ColoredLine;
            if (tmp == null) return false;
            if (this.coord[0].X == tmp.coord[0].X && this.coord[0].Y == tmp.coord[0].Y &&
                this.coord[1].X == tmp.coord[1].X && this.coord[1].Y == tmp.coord[1].Y &&
                this.Color == tmp.Color)
                return true;
            else return false;
        }
        public override int GetHashCode() => this.coord[0].GetHashCode();
        public override void PrintCoord()
        {
            base.PrintCoord();
            Console.WriteLine($"Color: {Color}");
        }
    }

    class PolyLine : Line
    {
        public PolyLine(params Point[] coord)
        {
            this.coord = coord;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (object.ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            PolyLine tmp = obj as PolyLine;
            if (tmp == null) return false;
            int j = 0;
            for (int i = 0; i < coord.Length; i++)
            {
                if (this.coord[i].X != tmp.coord[i].X || this.coord[i].Y != tmp.coord[i].Y) j += 1;
            }
            if (j == 0) return true;
            else return false;
        }
        public override int GetHashCode() => this.coord[0].GetHashCode();
        public void MoveX (int mx)
        {
            foreach (var point in coord)
                point.X += mx;
        }
        public void MoveY (int my)
        {
            foreach (var point in coord)
                point.Y += my;
        }
        public void MoveXY(int mx, int my)
        {
            MoveX(mx);
            MoveY(my);   
        }
    }

    internal class Program
    { 
        static void Main(string[] args)
        {
            Point point = new Point(3.3, 2);
            Point point1 = new Point(2, 3);
            Point point2;

            ColoredPoint clpoint = new ColoredPoint(5, 6, Color.Yellow);
            ColoredPoint clpoint1 = new ColoredPoint(5, 6, Color.Yellow);

            Line line = new Line(new Point(2,3), new Point(5,4));
            Line line1 = new Line(new Point(2, 3), new Point(6, 4));

            ColoredLine clline = new ColoredLine(new Point(0, 0), new Point(0, 1), Color.White);
            ColoredLine clline1 = new ColoredLine(new Point(0, 0), new Point(0, 1), Color.White);

            PolyLine polyline = new PolyLine(new Point(3, 2), new Point(4, 4), new Point(6, 6));
            PolyLine polyline1 = new PolyLine(new Point(2, 2), new Point(4, 4), new Point(6, 6));

            Console.WriteLine("Точка1: ");
            point.Show();
            Console.WriteLine("Точка2: ");
            point1.Show();
            Console.WriteLine("Сложение точек: ");
            point2 = point + point1;
            point2.Show();

            Console.WriteLine("Цветная точка: ");
            clpoint.Show();
            Console.WriteLine("Линия: ");
            line.PrintCoord();
            Console.WriteLine("Цветная линия: ");
            clline.PrintCoord();
            Console.WriteLine("Многоугольник: ");
            polyline.PrintCoord();

            bool pointEqualsPoint1 = point.Equals(point1);
            Console.WriteLine(pointEqualsPoint1);

            bool clpointEqualsClpoint1 = clpoint.Equals(clpoint1);
            Console.WriteLine(clpointEqualsClpoint1);

            bool linetEqualsLine1 = line.Equals(line1);
            Console.WriteLine(linetEqualsLine1);

            bool cllinetEqualsCline1 = clline.Equals(clline1);
            Console.WriteLine(cllinetEqualsCline1);

            bool polylineEqualsPolyline1 = polyline.Equals(polyline1);
            Console.WriteLine(polylineEqualsPolyline1);

            int lineCompareToLine1 = line.CompareTo(line1);
            Console.WriteLine(lineCompareToLine1);

            int cllineCompareToClline1 = clline.CompareTo(clline1);
            Console.WriteLine(cllineCompareToClline1);

            polyline.MoveX(5);
            Console.WriteLine("Многоугольник x+5: ");
            polyline.PrintCoord();
            polyline.MoveY(4);
            Console.WriteLine("Многоугольник y+4: ");
            polyline.PrintCoord();
            polyline.MoveXY(1,2);
            Console.WriteLine("Многоугольник x+1, y+2: ");
            polyline.PrintCoord();

            Console.ReadKey();
        }
    }
}