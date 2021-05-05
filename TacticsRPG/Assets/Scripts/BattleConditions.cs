using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleConditions : MonoBehaviour
{
    public BattleUnit battleUnit;

    public List<BaseCondition> conditions;

    public void ExecuteEffects()
    {
        foreach(BaseCondition condition in conditions)
        {
            condition.Effect();
        }
    }

    public void CheckTimer()
    {
        List<BaseCondition> effectsToEnd = new List<BaseCondition>();
        foreach (BaseCondition condition in conditions)
        {
            condition.duration -= 1;
            if (condition.duration == 0)
            {
                effectsToEnd.Add(condition);
            }
        }

        foreach(BaseCondition ended in effectsToEnd)
        {
            conditions.Remove(ended);
            ended.End();
            Destroy(ended.gameObject);
        }
    }
}
