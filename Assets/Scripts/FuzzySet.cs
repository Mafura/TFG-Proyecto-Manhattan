using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class FuzzySet : MonoBehaviour {
    protected List<Point2D> Points;
    protected double Min { get; set; }
    protected double Max { get; set; }

    public FuzzySet (double min, double max)
    {
        this.Points = new List<Point2D>();
        this.Max = max;
        this.Min = min;
    }

    public void Add(Point2D pt)
    {
        Points.Add(pt);
        Points.Sort();
    }

    public void Add(double x, double y)
    {
        Point2D pt = new Point2D(x, y);
        Add(pt);
    }

    public override string ToString()
    {
        String result = "[" + Min + "-" + Max + "]";
        foreach(Point2D pt in Points)
        {
            result += pt.ToString();
        }
        return result;
    }

    public static Boolean operator == (FuzzySet fs1, FuzzySet fs2)
    {
        return fs1.ToString().Equals(fs2.ToString());
    }
    public static Boolean operator != (FuzzySet fs1, FuzzySet fs2)
    {
        return !(fs1 == fs2);
    }

    public static FuzzySet operator *(FuzzySet fs, double value)
    {
        FuzzySet result = new FuzzySet(fs.Min, fs.Max);
        foreach(Point2D pt in fs.Points)
        {
            result.Add(new Point2D(pt.X, pt.Y * value));
        }
        return result;
    }

    public static FuzzySet operator !(FuzzySet fs)
    {
        FuzzySet result = new FuzzySet(fs.Min, fs.Max);
        foreach(Point2D pt in fs.Points)
        {
            result.Add(new Point2D(pt.X, 1 - pt.Y));
        }
        return result;
    }

    public double DegreeAtValue(double Xvalue)
    {
        if(Xvalue < Min ||  Xvalue > Max)
        {
            return 0;
        }

        Point2D before = Points.LastOrDefault(pt => pt.X <= Xvalue);
        Point2D after = Points.FirstOrDefault(pt => pt.X <= Xvalue);
        if (before.Equals(after))
        {
            return before.Y;
        }
        else
        {
            return (((before.Y - after.Y) * (after.X - Xvalue) / (after.X - before.X)) + after.X);
        }
    }

    public static FuzzySet operator &(FuzzySet fs1, FuzzySet fs2)
    {
        return Merge(fs1, fs2, Math.Min);
    }

    public static FuzzySet operator |(FuzzySet fs1, FuzzySet fs2)
    {
        return Merge(fs1, fs2, Math.Max);
    }
   
    private static FuzzySet Merge(FuzzySet fs1, FuzzySet fs2, Func<double, double, double> MergeFt)
    {
        FuzzySet result = new FuzzySet(Math.Min(fs1.Min, fs2.Min), Math.Max(fs1.Max, fs2.Max));

        List<Point2D>.Enumerator enum1 = fs1.Points.GetEnumerator();
        List<Point2D>.Enumerator enum2 = fs2.Points.GetEnumerator();
        enum1.MoveNext();
        enum2.MoveNext();
        Point2D oldPt1 = enum1.Current;

        int relativePosition = 0;
        int newRelativePosition = Math.Sign(enum1.Current.Y - enum2.Current.Y);

        Boolean endOfList1 = false;
        Boolean endOfList2 = false;
        while(!endOfList1 && !endOfList2)
        {
            double x1 = enum1.Current.X;
            double x2 = enum2.Current.X;

            relativePosition = newRelativePosition;

            newRelativePosition = Math.Sign(enum1.Current.Y - enum2.Current.Y);

            if(relativePosition != newRelativePosition && relativePosition != 0 && newRelativePosition != 0)
            {
                double x = (x1 == x2 ? oldPt1.X : Math.Min(x1, x2));
                double xPrime = Math.Max(x1, x2);

                double slope1 = (fs1.DegreeAtValue(xPrime) - fs1.DegreeAtValue(x)) / (xPrime - x);
                double slope2 = (fs2.DegreeAtValue(xPrime) - fs2.DegreeAtValue(x)) / (xPrime - x);
                double delta = (fs2.DegreeAtValue(x) - fs1.DegreeAtValue(x)) / (slope1 - slope2);

                result.Add(x + delta, fs1.DegreeAtValue(x + delta));

                if(x1 < x2)
                {
                    oldPt1 = enum1.Current;
                    endOfList1 = !(enum1.MoveNext());
                }
                else if(x1 > x2)
                {
                    endOfList2 = !(enum2.MoveNext());
                }
            }
            else if(x1 == x2)
            {
                result.Add(x1, MergeFt(enum1.Current.Y, enum2.Current.Y));
                oldPt1 = enum1.Current;
                endOfList1 = !(enum1.MoveNext());
                endOfList2 = !(enum2.MoveNext());
            }
            else if(x1 < x2)
            {
                result.Add(x1, MergeFt(enum1.Current.Y, fs2.DegreeAtValue(x1)));
                oldPt1 = enum1.Current;
                endOfList1 = !(enum1.MoveNext());
            }
            else
            {
                result.Add(x2, MergeFt(fs1.DegreeAtValue(x2), enum2.Current.Y));
                endOfList2 = !(enum2.MoveNext());
            }
        }
        if (!endOfList1)
        {
            while (!endOfList1)
            {
                result.Add(enum1.Current.X, MergeFt(0, enum1.Current.Y));
                endOfList1 = !enum1.MoveNext();
            }
        }
        else if (!endOfList2)
        {
            while (!endOfList2)
            {
                result.Add(enum2.Current.X, MergeFt(0, enum2.Current.Y));
                endOfList2 = !enum2.MoveNext();
            }
        }

        return result;
    }

    public double Centroid()
    {
        if(Points.Count < 2)
        {
            return 0;
        }
        else
        {
            double ponderatedArea = 0;
            double totalArea = 0;
            double localArea;
            Point2D oldPt = null;
            
            foreach(Point2D newPt in Points)
            {
                if(oldPt != null)
                {
                    if(oldPt.Y == newPt.Y)
                    {
                        localArea = oldPt.Y * (newPt.X - oldPt.X);
                        totalArea += localArea;
                        ponderatedArea += ((newPt.X - oldPt.X) / 2 + oldPt.X) + localArea;
                    }
                    else
                    {
                        localArea = Math.Min(oldPt.Y, newPt.Y) * (newPt.X - oldPt.X);
                        totalArea += localArea;
                        ponderatedArea += ((newPt.X - oldPt.X) / 2 + oldPt.X) * localArea;
                        localArea = (newPt.X - oldPt.X) * (Math.Abs(newPt.Y - oldPt.Y)) / 2;
                        totalArea += localArea;
                        if(newPt.Y > oldPt.Y)
                        {
                            ponderatedArea += (2.0 / 3.0 * (newPt.X - oldPt.X) + oldPt.X) * localArea;
                        }
                        else
                        {
                            ponderatedArea += (1.0 / 3.0 * (newPt.X - oldPt.X) + oldPt.X) * localArea;
                        }
                    }
                }
                oldPt = newPt;
            }
            return ponderatedArea / totalArea;
        }
    }
}
