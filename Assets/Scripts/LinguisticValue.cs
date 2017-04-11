using UnityEngine;
using System.Collections;
using System;

public class LinguisticValue : MonoBehaviour {
    internal FuzzySet Fs { get; set; }
    internal String Name { get; set; }

    public LinguisticValue(String name, FuzzySet fs)
    {
        Name = name;
        Fs = fs;
    }

    internal double DegreeAtValue(double val)
    {
        return Fs.DegreeAtValue(val);
    }
}
