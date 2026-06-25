using UnityEngine;

public class ProjectileTwisting : Abstr_Projectile
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 1f;
    public float rotSpeed = 250f;
    private float timer = 7.5f;
    private Vector3 initPos;
    public float angle;
    protected override void Start()
    {
        base.Start();
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        angle += rotSpeed * Time.deltaTime;
        angle = angle % 360;
        //destPos.y = Mathf.Sin(Time.deltaTime) * xRad + centerPos.y;
        //destPos.x = Mathf.Cos(Time.deltaTime) * yRad + centerPos.x;
        //transform.position = Vector2.MoveTowards(this.transform.position, destPos, rotateSpeed * Time.deltaTime);
        float xPos = initPos.x + 2 * Mathf.Cos(angle * Mathf.Deg2Rad);
        float yPos = transform.position.y + speed * Time.deltaTime;
        transform.position = new Vector3(xPos, yPos, 0);
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
