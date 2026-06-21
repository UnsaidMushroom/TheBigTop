using System.Net.Sockets;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RotatingObject : MonoBehaviour
{
    public InputActionAsset actions;

    public float rotateSpeed = 2f;

    public Vector3 centerPosition;
    public Vector3 rightPosition;
    public Vector3 leftPosition;

    private bool moving;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        centerPosition.x = -1;
        centerPosition.y = -2.85f;
        rightPosition.x = 3.5f;
        rightPosition.y = -4.25f;
        leftPosition.x = -5.5f;
        leftPosition.y = -4.25f;
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        //on right press, left to center, center to right, right to left
        //if right button was pressed this frame
            //if currently left position, set dest to center
            //if currently center position, set dest to right
            //if currently right position, set dest to left AND remove hitbox rule
            //set moving to true
        //on left press, right to center, center to left, left to right
            //if right button was pressed this frame
            //if currently left position, set dest to center
            //if currently center position, set dest to right
            //if currently right position, set dest to left AND remove hitbox rule
            //set moving to true
        
        transform.position = Vector2.MoveTowards(this.transform.position, rightPosition, rotateSpeed * Time.deltaTime);
    }

    //Hi, for the rotation i was imagining several degrees of rotation, such that you can swivel in live time and react to attacks, not switching between a few set locations. something probably involving sin/cos, at least in my vision
}
