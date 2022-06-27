using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class LevelSystem
{
    public int exp;
    public int currentLevel;
    public Action OnLevelUp;

    public int MAX_EXP;
    public int MAX_LEVEL = 99;
    public LevelSystem(int level, Action OnLevUp)
    {
        MAX_EXP = GetXPforLevel(MAX_LEVEL);
        currentLevel = level;
        exp = GetXPforLevel(level);
        OnLevelUp = OnLevUp;

    }

    public int GetXPforLevel(int level)
    {
        if (level > MAX_LEVEL)
            return 0;

        int firstPass = 0;
        int secondPass = 0;
        for (int levelCycle = 1; levelCycle < level; levelCycle++)
        {
            firstPass += (int)Math.Floor(levelCycle + (300.0f * Math.Pow(2.0f, levelCycle / 7.0f)));
            secondPass = firstPass / 4;
        }

        if (secondPass > MAX_EXP && MAX_EXP != 0)
            return MAX_EXP;

        if (secondPass < 0)
            return MAX_EXP;

        return secondPass;
    }

    public int GetLevelForXP(int exp)
    {
        if (exp > MAX_EXP)
            return MAX_EXP;

        int firstPass = 0;
        int secondPass = 0;
        for (int levelCycle = 1; levelCycle <= MAX_LEVEL; levelCycle++)
        {
            firstPass += (int)Math.Floor(levelCycle + (300.0f * Math.Pow(2.0f, levelCycle / 7.0f)));
            secondPass = firstPass / 4;
            if (secondPass > exp)
                return levelCycle;
        }
        if (exp > MAX_LEVEL)
            return MAX_LEVEL;

        return 0;
    }

    public bool AddExp(int amount)
    {
        if (amount + exp < 0 || exp > MAX_EXP)
        {
            if (exp > MAX_EXP)
                exp = MAX_LEVEL;
            return false;
        }
        int oldLevel = GetLevelForXP(exp);
        exp += amount;
        if (oldLevel < GetLevelForXP(exp))
        {
            if (currentLevel < GetLevelForXP(exp))
            {
                currentLevel = GetLevelForXP(exp);
                if (OnLevelUp != null)
                    OnLevelUp.Invoke();
                return true;
            }
        }
        return false;
    }
}