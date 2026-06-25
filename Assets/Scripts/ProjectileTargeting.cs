using System.Net.Sockets;
using UnityEngine;

public class ProjectileTargeting : Abstr_Projectile
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 15f;
    private float timer = 2;
    public GameObject Target;
    private Vector3 targetPos;
    protected override void Start()
    {
        base.Start();
        targetPos = Target.transform.position;
        
    }

    // Update is called once per frames
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, targetPos*2, speed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Damage();
        }

        
    }

    public override void Damage()
    {
        Destroy(gameObject);
    }

    
}
