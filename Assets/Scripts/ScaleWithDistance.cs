using UnityEngine;

public class ScaleWithDistance : MonoBehaviour
{
    public Vector3 baseScale = Vector3.one;
    public static float scalingIntensity = (1/10f); //for every y unit vertically, scale by this much 
    public float scrollSensitivity = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = baseScale * calcScale(); ;
    }

    public float calcScale()
    {
        return 1 + scalingIntensity * -1 * transform.position.y;
    }
}
