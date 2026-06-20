using UnityEngine;
using UnityEngine.InputSystem;

public class RotatingObject : MonoBehaviour
{
    public InputActionAsset actions;

    public Vector3 centerPosition;
    public Vector3 rightPosition;
    public Vector3 leftPosition;
    public Vector3 pivotPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        centerPosition.x = -1;
        centerPosition.y = -2.85f;
        rightPosition.x = 3.5f;
        rightPosition.y = -4.25f;
        leftPosition.x = -5.5f;
        leftPosition.y = -4.25f;
        pivotPoint.x = 0;
        pivotPoint.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //on right press, left to center, center to right, right to left
        //on left press, right to center, center to left, left to right
        
    }

    //Hi, for the rotation i was imagining several degrees of rotation, such that you can swivel in live time and react to attacks, not switching between a few set locations. something probably involving sin/cos, at least in my vision
}
