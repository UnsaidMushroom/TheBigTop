using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Rigidbody2D myBody;
    public CircleCollider2D myCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }
        if (myCollider == null) { myCollider = gameObject.GetComponent<CircleCollider2D>(); }
    }

    // Update is called once per frame
    void Update()
    {
        //change so if either arrow keys or scroll wheel is being pressed in a direction rotate based on that
        transform.Rotate(Time.deltaTime, 0, 0);
    }
}
