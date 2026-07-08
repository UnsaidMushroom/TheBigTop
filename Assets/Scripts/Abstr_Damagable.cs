using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Abstr_Damagable is to be inherited by anything that deals or takes damage, mostly projectiles and rotating objects (recruits).
/// </summary>
public abstract class Abstr_Damagable : MonoBehaviour
{
    

    /// <summary>
    /// the set of friendly tags
    /// </summary>
    public static HashSet<string> friendliesTags;
    /// <summary>
    /// the set of enemy tags
    /// </summary>
    public static HashSet<string> enemiesTags;

    /// <summary>
    /// the tag belonging to this object
    /// </summary>
    protected string myTag = null;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        myTag = gameObject.tag;
        if (friendliesTags == null || enemiesTags == null)
        {
            initializeSets();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Applies damage when a value is not needed
    /// </summary>
    public abstract void Damage();

    /// <summary>
    /// applies damage with a value amount
    /// </summary>
    /// <param name="dmg"></param> the amount of damage dealt to this damagable
    public abstract void Damage(int dmg);

    /// <summary>
    /// used when two damagables collide
    /// </summary>
    /// <param name="collision"></param> the other damageable in the collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (myTag == null) { myTag = gameObject.tag; }

        string otherTag = collision.gameObject.tag;
        Debug.Log("collision! me: " + myTag + "; other: " + otherTag);

        Abstr_Projectile ap = collision.gameObject.GetComponent<Abstr_Projectile>(); //we should have null checking here

        if (!friendliesTags.Contains(myTag) && friendliesTags.Contains(otherTag))
        {
            if (ap != null)
            {
                Damage(ap.damage);
            }
            else { Damage(); }

        }
        if (!enemiesTags.Contains(myTag) && enemiesTags.Contains(otherTag))
        {
            if (ap != null)
            {
                Damage(ap.damage);
            }
            else { Damage(); }
        }
    }

    /// <summary>
    /// initializes the sets of tags that identify friendly or enemy damageables, only needs called once at the start.
    /// </summary>
    public static void initializeSets()
    {
        friendliesTags = new HashSet<string>();
        friendliesTags.Add("Friendly");

        enemiesTags = new HashSet<string>();
        enemiesTags.Add("Enemy");


    }
}

