using UnityEngine;

public class ProjectileBasic : Abstr_Projectile
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D myBody;
    public float speed = 15f;
    private float timer = 2;
    void Start()
    {
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }
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
