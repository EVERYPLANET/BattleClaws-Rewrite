using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TomatoBehaviour : MonoBehaviour
{
    private float tomatoSpeed = 1f;
    public GameObject SplatParticles;

   
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody>().AddForce(transform.forward * tomatoSpeed, ForceMode.Impulse);
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            GetComponent<Rigidbody>().AddForce(direction * tomatoSpeed, ForceMode.Impulse);
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        Instantiate(SplatParticles, transform.position, transform.rotation); 
    }


   
}
