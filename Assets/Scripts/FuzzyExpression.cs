using UnityEngine;
using System.Collections;
using System;

public class FuzzyExpression : MonoBehaviour {

	internal LinguisticVariable Lv { get; set; }
    internal String LinguisticValueName { get; set; }

    public FuzzyExpression (LinguisticVariable lv, String value)
    {
        Lv = lv;
        LinguisticValueName = value;
    }
}
