using System;
using UnityEditor;
using UnityEngine;

namespace recruits
{
    public class Recruit : IComparable<Recruit>
    {
        public static string spritesPath = "Assets/Sprites/";
        public static string attacksPath = "Assets/Prefabs/";
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



            sprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritesPath + name + ".png", typeof(Sprite));
            if (sprite == null)
            {
                sprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritesPath + "missing_sprite" + ".png", typeof(Sprite));
            }



            myAttack = (GameObject)AssetDatabase.LoadAssetAtPath(attacksPath + attackType + ".prefab", typeof(GameObject));
            if (myAttack == null)
            {
                myAttack = (GameObject)AssetDatabase.LoadAssetAtPath(attacksPath + "DoNothingAndDie" + ".prefab", typeof(GameObject));
            }

            timeLastModified = Time.time;


        }




        public Recruit getCopy()
        {
            return new Recruit(name, rarity, damage, attackDescription, maxHP, attackType, level);
        }

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
                //sort by name
                return compareByLevel(other);
                
            }

        }

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

        public void levelUp(int n)
        {
            level += n;
            for (int i = 0; i < n; i++)
            {
                maxHP = Mathf.CeilToInt(maxHP * levelUpCoefficient);
                damage = Mathf.CeilToInt(damage *  levelUpCoefficient);

            }

        }

        public int sellValue()
        {
            return 2 + (2 * (int) rarity) + level;
        }

        public GameObject getAttack()
        {
            return myAttack;
        }


    }
}
