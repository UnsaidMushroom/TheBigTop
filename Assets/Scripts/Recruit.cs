using System;
using UnityEditor;
using UnityEngine;


namespace recruits
{
    /// <summary>
    /// handles data for a recruit. not a monobehaviour
    /// </summary>
    public class Recruit : IComparable<Recruit>
    {
        public static string spritesPath = "Sprites/";
        public static string attacksPath = "Prefabs/";
        public static string sortMode = "none";

        public const float levelUpCoefficient = 1.05f;

        //stores data for a given recruit
        public string name;
        public Sprite sprite;
        public Rarity rarity;
        public int damage;
        public string attackDescription;
        public int maxHP;
        public int remainingHP;
        public int level;
        public string attackType;
        private GameObject myAttack;

        public float timeLastModified;


        /// <summary>
        /// instantiates a recruit based on parameters.
        /// </summary>
        /// <param name="name"></param> the name of the recruit
        /// <param name="rarity"></param> the rarity of the recruit
        /// <param name="damage"></param> the damage the recruit's attack will deal
        /// <param name="description"></param> the description of the recruit
        /// <param name="maxHP"></param> the max hp of the recruit
        /// <param name="type"></param> the name of the attack this recruit uses
        /// <param name="level"></param> the level this recruit is at
        public Recruit(string name, Rarity rarity, int damage, string description, int maxHP, string type = "default", int level = 1)
        {
            this.name = name;
            this.rarity = rarity;
            this.damage = damage;
            this.attackDescription = description;
            this.maxHP = maxHP;
            this.level = level;
            this.remainingHP = maxHP;
            this.attackType = type;



            sprite = Resources.Load<Sprite>(spritesPath + name /*+ ".png"*/);
            if (sprite == null)
            {
                sprite = Resources.Load<Sprite>(spritesPath + "missing_sprite" /*+ ".png"*/);
            }



            myAttack = Resources.Load<GameObject>(attacksPath + attackType /*+ ".prefab"*/);
            if (myAttack == null)
            {
                myAttack = Resources.Load<GameObject>(attacksPath + "DoNothingAndDie" /*+ ".prefab"*/);
            }

            timeLastModified = Time.time;


        }

        /// <summary>
        /// makes a copy of a recuit
        /// </summary>
        /// <param name="toCopy"></param> the recruit this copies
        private Recruit(Recruit toCopy)
        {
            this.name = toCopy.name;
            this.rarity = toCopy.rarity;
            this.damage = toCopy.damage;
            this.attackDescription= toCopy.attackDescription;
            this.maxHP = toCopy.maxHP;
            this.level = toCopy.level;
            this.remainingHP = maxHP;
            this.attackType= toCopy.attackType;

            this.sprite = toCopy.sprite;
            this.myAttack = toCopy.myAttack;

            this.timeLastModified = Time.time;
        }



        /// <summary>
        /// returns a copy of this recruit
        /// </summary>
        /// <returns></returns> a copy of this recruit
        public Recruit getCopy()
        {
            //return new Recruit(name, rarity, damage, attackDescription, maxHP, attackType, level);
            return new Recruit(this);
        }

        /// <summary>
        /// compares two recruits by rarity.
        /// if same rarity, by level.
        /// used in sorting.
        /// </summary>
        /// <param name="other"></param> the recruit to compare against
        /// <returns></returns> 1 if this is rarer, -1 if less rare, 0 if samesies.
        public int compareByRarity(Recruit other)
        {
            //returns -1 if this is less rare, 1 if more rare
            // if just as rare, then goes by  name
            //this is used to sort so that rarer ones appear first (the list may need reversed)

            if (other == null)
            {
                return 1;
            }
            else if (rarity > other.rarity) //making use of the fact that c# treats structs as ints, common = 0, legendary = 3
            {
                return 1;
            }
            else if (rarity < other.rarity)
            {
                return -1;
            }
            else
            {
                //sort by level
                return compareByLevel(other);
                
            }

        }

        /// <summary>
        /// compares two recruits by time.
        /// used in sorting
        /// </summary>
        /// <param name="other"></param> the recruit to compare against
        /// <returns></returns> 1 if this was more recently modified, -1 is less recently modified.
        public int compareByTime(Recruit other)
        {
            //returns -1 if this was modified less recently, 1 if more recent
            if (other == null)
            {
                return 1;
            }
            else if (timeLastModified > other.timeLastModified)
            {
                return 1;
            }
            else if (timeLastModified < other.timeLastModified)
            {
                return -1;
            }
            else
            { // unlikely to duplicate time exactly, 
                return name.CompareTo(other.name);
            } 
        }

        /// <summary>
        /// compares two recruits by level.
        /// if same level, goes by name
        /// used in sorting
        /// </summary>
        /// <param name="other"></param> the recruit to compare against
        /// <returns></returns> 1 if this is higher level, -1 if lower level, 0 if samesies
        public int compareByLevel(Recruit other)
        {
            //returns -1 if this was modified less recently, 1 if more recent
            if (other == null)
            {
                return 1;
            }
            else if (level > other.level)
            {
                return 1;
            }
            else if (level < other.level)
            {
                return -1;
            }
            else
            { // unlikely to duplicate time exactly, 
                return name.CompareTo(other.name);
            }
        }

        /// <summary>
        /// copmares two recruits.
        /// used to sort a list.
        /// before calling list.sort, be sure to assign the appropriate sortmode.
        /// </summary>
        /// <param name="other"></param> the recruit to compare against
        /// <returns></returns> 1 if greater, -1 if lesser, 0 if samesies or err
        public int CompareTo(Recruit other)
        {
            if (sortMode == "rarity")
            {
                return compareByRarity(other);
            }
            else if (sortMode == "time")
            {
                return compareByTime(other);
            }
            else if (sortMode == "level")
            {
                return compareByLevel(other);
            }
            else if (sortMode == "none")
            {
                return 0;
            }
            else
            {
                Debug.Log("invalid sort mode for recruits! \n valid options 'rarity', 'time', 'none'");
                return 0;
            }
        }

        /// <summary>
        /// levels up this recruit
        /// </summary>
        /// <param name="n"></param> the amount of levels to level up by
        public void levelUp(int n)
        {
            level += n;
            for (int i = 0; i < n; i++)
            {
                maxHP = Mathf.CeilToInt(maxHP * levelUpCoefficient);
                damage = Mathf.CeilToInt(damage *  levelUpCoefficient);

            }
            remainingHP = maxHP;

        }

        /// <summary>
        /// the amount this recruit is worth if sold.
        /// does not itself sell the recruit, just gets the value.
        /// </summary>
        /// <returns></returns> the amount this recruit is worth
        public int sellValue()
        {
            return 2 + (2 * (int) rarity) + level;
        }

        /// <summary>
        /// gets the attack  prefab this recruit uses.
        /// </summary>
        /// <returns></returns> the attack this recruit uses
        public GameObject getAttack()
        {
            return myAttack;
        }


    }
}
