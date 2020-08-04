using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody enemy;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        enemy = new Rigidbody();
    }

    // Update is called once per frame
    void Update()
    {
 
      
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
            Debug.Log("Enemy got hit");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }
}
