using UnityEngine;

public abstract class Abstr_Projectile : Abstr_Damagable
{
    public Rigidbody2D myBody;

    public int damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public override void Damage()
    {
        Destroy(gameObject);
    }
    public override void Damage(int dmg)
    {
        Damage();
    }

    public void setDamage(int amt)
    {
        damage = amt;
    }
    public void setTag(string tag)
    {
        gameObject.tag = tag;
    }

}
