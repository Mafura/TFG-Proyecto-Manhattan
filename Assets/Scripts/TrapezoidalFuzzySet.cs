﻿using UnityEngine;
using System.Collections;

public class TrapezoidalFuzzySet : FuzzySet {

    public TrapezoidalFuzzySet(double min, double max, double baseLeft, double heightLeft, double heightRight, double baseRight): base(min, max)
    {
        Add(new Point2D(min, 0));
        Add(new Point2D(baseLeft, 0));
        Add(new Point2D(heightLeft, 0));
        Add(new Point2D(heightRight, 0));
        Add(new Point2D(baseRight, 0));
        Add(new Point2D(max, 0));
    }

	
}
