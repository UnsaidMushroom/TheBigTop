using System.Drawing;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RotatingObject : MonoBehaviour
{
    public InputActionAsset actions;

    public float rotateSpeed = 2f;

    public Vector3 centerPos;
    private Vector3 destPos;

    private float yRad;
    private float xRad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        centerPos.x = -1;
        centerPos.y = -5;
        xRad = transform.position.x - centerPos.x;
        yRad = transform.position.y - centerPos.y;
    }

    // Update is called once per frame
    void Update()
    {
        destPos.y = Mathf.Sin(Time.deltaTime) * xRad + centerPos.y;
        destPos.x = Mathf.Cos(Time.deltaTime) * yRad + centerPos.x;
        transform.position = Vector2.MoveTowards(this.transform.position, destPos, rotateSpeed * Time.deltaTime);
    }

}
