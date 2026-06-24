using UnityEngine;
using recruits;
using System.Collections.Generic;


public struct AttackPattern
{
    //a pattern is like "LeftAttack", "slowMoveLeft", RightAttack", "fastMoveLeft"
    //it does one after the other, looping
    public List<string> procedure;

    public AttackPattern(List<string> procedure)
    {
        this.procedure = procedure;
    }

    public AttackPattern(params string[] instructions)
    {
        procedure = new List<string>();
        foreach(string s in instructions)
        {
            this.procedure.Add(s);
        }
    }

}

public class Encounter
{
    public string name;
    //public Sprite character; //the character that you are fighting
    public int level = 1;
    public List<string> encounterRecs;
    //public enemyStrategy
    public AttackPattern strategy;
    public int stratIndex = -1;
    
    public Encounter(string name, int level, List<string> recruits, AttackPattern strategy)
    {
        this.name = name;
        this.level = level;
        encounterRecs = recruits;
        this.strategy = strategy;
        stratIndex = -1;
    }

    public string getNextMove()
    {
        stratIndex++;
        stratIndex %= strategy.procedure.Count;
        return strategy.procedure[stratIndex];
    }

    

    


}
