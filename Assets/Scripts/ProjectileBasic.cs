using UnityEngine;

public class ProjectileBasic : Abstr_Projectile
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 15f;
    private float timer = 2;
    protected override void Start()
    {
        base.Start();
        if(this.CompareTag("Enemy"))
        {
            speed = -speed;
        }
        myBody.linearVelocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
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
