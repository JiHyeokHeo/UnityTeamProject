using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat

    [Serializable]
    public class Stat
    {
        public string name;
        public int level;
        public int hp;
        public int mp;
        public int attack;
        public int attackRange;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> tempDict = new Dictionary<int, Stat>();
            foreach (Stat stat in stats)
                tempDict.Add(stat.level, stat);
            return tempDict;
        }
    }
    #endregion
}