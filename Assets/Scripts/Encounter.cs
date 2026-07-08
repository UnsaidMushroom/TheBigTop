using UnityEngine;
using recruits;
using System.Collections.Generic;

/// <summary>
/// a pattern which an enemy follows
/// </summary>
public struct AttackPattern
{
    //a pattern is like "LeftAttack", "slowMoveLeft", RightAttack", "fastMoveLeft"
    //it does one after the other, looping
    public List<string> procedure;
    

    /// <summary>
    /// initializes an attackpattern based on a list of strings
    /// </summary>
    /// <param name="procedure"></param> the pattern to set
    public AttackPattern(List<string> procedure)
    {
        this.procedure = procedure;
    }

    /// <summary>
    /// initializes an attackpattern based on a series of strings (varargs)
    /// </summary>
    /// <param name="instructions"></param> the pattern to set
    public AttackPattern(params string[] instructions)
    {
        procedure = new List<string>();
        foreach(string s in instructions)
        {
            this.procedure.Add(s);
        }
    }

}

/// <summary>
/// describes an opponent to be battled
/// </summary>
public class Encounter
{
    public string name; //name is currently unused, but in the future may be used to load the character sprite
    //public Sprite character; //the character that you are fighting
    public int level = 1;
    public List<string> encounterRecs;
    public AttackPattern strategy;
    public int stratIndex = -1;
    
    /// <summary>
    /// initializes an encounter
    /// </summary>
    /// <param name="name"></param> the name of the encounter 
    /// <param name="level"></param> the level the encounter sets its recruits to 
    /// <param name="recruits"></param> the recruits you fight in this encounter
    /// <param name="strategy"></param> the attack pattern that this opponent follows
    public Encounter(string name, int level, List<string> recruits, AttackPattern strategy)
    {
        this.name = name;
        this.level = level;
        encounterRecs = recruits;
        this.strategy = strategy;
        stratIndex = -1;
    }

    /// <summary>
    /// gets the next move of the attack pattern
    /// </summary>
    /// <returns></returns> a string representing the next move in sequence.
    public string getNextMove()
    {
        stratIndex++;
        stratIndex %= strategy.procedure.Count;
        return strategy.procedure[stratIndex];
    }

    

    


}
