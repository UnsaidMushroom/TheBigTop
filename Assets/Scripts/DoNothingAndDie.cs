using UnityEngine;

public class DoNothingAndDie : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Did nothing and died -- invalid attack");
        Destroy(gameObject);
    }

}
