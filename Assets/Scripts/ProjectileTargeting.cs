using System.Net.Sockets;
using UnityEngine;

/// <summary>
/// a projectile that aims toward where an opponent is.
/// </summary>
public class ProjectileTargeting : Abstr_Projectile
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 15f;
    private float timer = 2;
    public RotatingObject Target;
    private Vector3 targetPos;
    protected override void Start()
    {
        base.Start();
        if (this.CompareTag("Enemy"))
        {
            Target = FriendliesManager.Instance.getActive();
        }
        else
        {
            Target = EnemiesManager.Instance.getActive();
        }


        if (Target == null) { targetPos = Vector3.up *10; } //error check
        else
        {
            targetPos = Target.transform.position;
        }
        
    }

    // Update is called once per frames
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, targetPos * 1.5f, speed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Damage();
        }

        
    }

    /// <summary>
    /// destroy the projectile on impact.
    /// </summary>
    public override void Damage()
    {
        Destroy(gameObject);
    }

    
}
