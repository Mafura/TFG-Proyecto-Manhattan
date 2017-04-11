using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class FuzzySystem : MonoBehaviour {

	String Name { get; set; }
    List<LinguisticVariable> Inputs;
    LinguisticVariable Output;
    List<FuzzyRule> Rules;
    List<FuzzyValue> Problem;

    public FuzzySystem(String name)
    {
        Name = name;
        Inputs = new List<LinguisticVariable>();
        Rules = new List<FuzzyRule>();
        Problem = new List<FuzzyValue>();
    }

    public void addInputVariable(LinguisticVariable lv)
    {
        Inputs.Add(lv);
    }

    public void addOutputVariable(LinguisticVariable lv)
    {
        Output = lv;
    }
   
    public void addFuzzyRule(FuzzyRule fuzzyRule)
    {
        Rules.Add(fuzzyRule);
    }

    public void SetInputVariable(LinguisticVariable inputVar, double value)
    {
        Problem.Add(new FuzzyValue(inputVar, value));
    }

    public void ResetCase()
    {
        Problem.Clear();
    }

    internal LinguisticVariable LinguisticVariableByName(String name)
    {
        foreach(LinguisticVariable input in Inputs)
        {
            if (input.Name.ToUpper().Equals(name))
            {
                return input;
            }
        }
        if (Output.Name.ToUpper().Equals(name))
        {
            return Output;
        }
        return null;
    }

    public double Solve()
    {
        FuzzySet res = new FuzzySet(Output.MinValue, Output.MaxValue);
        res.Add(Output.MinValue, 0);
        res.Add(Output.MaxValue, 0);

        foreach(FuzzyRule rule in Rules)
        {
            res = res | rule.Apply(Problem);
        }

        return res.Centroid();
    }

    public void addFuzzyRule(String ruleStr)
    {
        FuzzyRule rule = new FuzzyRule(ruleStr, this);
        Rules.Add(rule);
    }
}
