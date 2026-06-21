using System;
using UnityEditor;
using UnityEngine;

namespace recruits
{
    public class Recruit : IComparable<Recruit>
    {
        public static string spritesPath = "Assets/Sprites";
        public static string sortMode = "none";

        //stores data for a given recruit
        public string name;
        public Sprite sprite;
        public Rarity rarity;
        public int damage;
        public string attackDescription;
        public int maxHP;
        public int remainingHP;

        public float timeLastModified;



        public Recruit(string name, Rarity rarity, int damage, string description, int maxHP)
        {
            this.name = name;
            this.rarity = rarity;
            this.damage = damage;
            this.attackDescription = description;
            this.maxHP = maxHP;
            this.remainingHP = maxHP;



            sprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritesPath + name + ".png", typeof(Sprite));
            if (sprite == null)
            {
                sprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritesPath + "missing_sprite" + ".png", typeof(Sprite));
            }

            timeLastModified = Time.time ;


        }

        public Recruit getCopy()
        {
            return new Recruit(name, rarity, damage, attackDescription, maxHP);
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
                return name.CompareTo(other.name);
                
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

    }
}
