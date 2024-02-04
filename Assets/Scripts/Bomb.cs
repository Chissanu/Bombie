using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") 
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
        } else if (collision.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
