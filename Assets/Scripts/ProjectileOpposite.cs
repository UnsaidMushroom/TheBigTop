using System.Net.Sockets;
using UnityEngine;

public class ProjectileOpposite : Abstr_Damagable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D myBody;
    public float speed = 15f;
    private float timer = 2;
    public GameObject Target;
    void Start()
    {
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }
        transform.position = Vector2.MoveTowards(this.transform.position, Target.transform.position, Speed * Time.deltaTime);
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
