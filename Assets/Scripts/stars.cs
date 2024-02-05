using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stars : MonoBehaviour
{
    public GameObject star1;
    public GameObject star2;
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
        if (collision.gameObject.tag == "bomb")
        {
            collision.gameObject.GetComponent<Renderer>().enabled = false;
            this.gameObject.GetComponent<Renderer>().enabled = false;

            StartCoroutine(SpawnStarCoroutine());
        } else if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().GameOver();
        }
    }

    private IEnumerator SpawnStarCoroutine()
    {
        yield return new WaitForSeconds(2f);
        int val = Random.Range(0, 2);
        if (val == 1)
        {
            Instantiate(star1, new Vector3(Random.Range(-17, 0), Random.Range(-2, 0), -13), Quaternion.Euler(0, 90, 90));
        }
        else
        {
            Instantiate(star2, new Vector3(Random.Range(-17, 0), Random.Range(-2, 0), -13), Quaternion.Euler(0, 90, 90));
        }
        Destroy(this.gameObject);
    }
}
