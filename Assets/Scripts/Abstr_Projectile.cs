using System.Collections;
using UnityEngine;

/// <summary>
/// Abstr_Projectile is to be inherited by all projectiles
/// </summary>
public abstract class Abstr_Projectile : Abstr_Damagable
{
    /// <summary>
    /// the rigidbody attached to this projectile
    /// </summary>
    public Rigidbody2D myBody;

    /// <summary>
    /// the damage this projectile deals on contact
    /// </summary>
    public int damage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }
        StartCoroutine(WaitABitToContact());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    /// <summary>
    /// When this projectile hits anything it is destroyed
    /// </summary>
    public override void Damage()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Any amount of damage is enough to destroy a projectile
    /// </summary>
    /// <param name="dmg"></param> the amount of damage taken
    public override void Damage(int dmg)
    {
        Damage();
    }

    /// <summary>
    /// initializes this projectile's damage, called by whatever instantiated this projectile
    /// </summary>
    /// <param name="amt"></param> the damage this projectile should deal
    public void setDamage(int amt)
    {
        damage = amt;
    }

    /// <summary>
    /// initializes this projectile's tag, called by whatever instantiated this projectile
    /// </summary>
    /// <param name="tag"></param> the tag this projectile should have
    public void setTag(string tag)
    {
        gameObject.tag = tag;
    }

    /// <summary>
    /// called shortly after creation, prevents this from immediately colliding with its creator
    /// </summary>
    /// <returns></returns>
    public IEnumerator WaitABitToContact()
    {
        Collider2D Col = gameObject.GetComponent<Collider2D>();
        Col.enabled = false;
        yield return new WaitForSeconds(.1f);
        Col.enabled = true;

    }


}
