using UnityEditor;
using UnityEngine;

namespace recruits
{
    public class Recruit
    {
        public static string spritesPath = "Assets/Sprites";

        //stores data for a given recruit
        public string name;
        public Sprite sprite;
        public Rarity rarity;
        public int damage;
        public string attackDescription;
        public int maxHP;
        public int remainingHP;


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


        }

        public Recruit getCopy()
        {
            return new Recruit(name, rarity, damage, attackDescription, maxHP);
        }



    }
}
