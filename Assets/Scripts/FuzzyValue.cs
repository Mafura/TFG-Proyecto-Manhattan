using UnityEngine;
using System.Collections;

public class FuzzyValue : MonoBehaviour {

    internal LinguisticVariable Lv;
    internal double Value;

    public FuzzyValue(LinguisticVariable lv, double value)
    {
        Lv = lv;
        Value = value;
    }
}
