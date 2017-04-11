using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class FuzzyRule : MonoBehaviour {
    List<FuzzyExpression> Premises;
    FuzzyExpression Conclusion;

    public FuzzyRule(String ruleStr, FuzzySystem fuzzySystem)
    {
        ruleStr = ruleStr.ToUpper();

        String[] rule = ruleStr.Split(new String[] { " THEN " }, StringSplitOptions.RemoveEmptyEntries);
        if(rule.Length == 2)
        {
            rule[0] = rule[0].Remove(0, 2);
            String[] prem = rule[0].Trim().Split(new String[] { " AND " }, StringSplitOptions.RemoveEmptyEntries);
            Premises = new List<FuzzyExpression>();
            foreach(String exp in prem)
            {
                String[] res = exp.Split(new String[] { " IS " }, StringSplitOptions.RemoveEmptyEntries);
                if(res.Length == 2)
                {
                    FuzzyExpression fexp = new FuzzyExpression(fuzzySystem.LinguisticVariableByName(res[0]), res[1]);
                    Premises.Add(fexp);
                }
            }
            String[] conclu = rule[1].Split(new String[] { " IS " }, StringSplitOptions.RemoveEmptyEntries);
            if(conclu.Length == 2)
            {
                Conclusion = new FuzzyExpression(fuzzySystem.LinguisticVariableByName(conclu[0]), conclu[1]);
            }
        }
    }
	
    public FuzzyRule(List<FuzzyExpression> prem, FuzzyExpression concl)
    {
        Premises = prem;
        Conclusion = concl;
    }

    internal FuzzySet Apply(List<FuzzyValue> Problem)
    {
        double degree = 1;
        foreach(FuzzyExpression premise in Premises)
        {
            double localDegree = 0;
            LinguisticValue val = null;
            foreach(FuzzyValue pb in Problem)
            {
                if(premise.Lv == pb.Lv)
                {
                    val = premise.Lv.LinguisticValueByName(premise.LinguisticValueName);
                    if(val != null)
                    {
                        localDegree = val.DegreeAtValue(pb.Value);
                        break;
                    }
                }
            }
            if(val == null)
            {
                return null;
            }

            degree = Math.Min(degree, localDegree);
        }
        return Conclusion.Lv.LinguisticValueByName(Conclusion.LinguisticValueName).Fs * degree;
    }
}
