using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReciver : MonoBehaviour
{
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRayHit()
    {

        Debug.Log("Raycast hit:" + hit.transform.name);
    }
}
