using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LinguisticVariable : MonoBehaviour {
    internal String Name { get; set; }
    List<LinguisticValue> Values { get; set; }
    internal Double MinValue { get; set; }
    internal Double MaxValue { get; set; }

    public LinguisticVariable(String name, double min, double max)
    {
        Values = new List<LinguisticValue>();
        Name = name;
        MinValue = min;
        MaxValue = max;
    }

    public void AddValue(LinguisticValue lv)
    {
        Values.Add(lv);
    }

    public void AddValue(String name, FuzzySet fs)
    {
        Values.Add(new LinguisticValue(name, fs));
    }

    internal LinguisticValue LinguisticValueByName(String name)
    {
        name = name.ToUpper();
        foreach(LinguisticValue val in Values)
        {
            if (val.Name.ToUpper().Equals(name))
            {
                return val;
            }
        }
        return null;
    }
}
