using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapes : MonoBehaviour {

    public Vector3 miniGrape1;

    public Vector3 miniGrape2;

    public Vector3 miniGrape3;

    public bool split;

    public GameObject miniGra1;

    public GameObject miniGra2;

    public GameObject miniGra3;

    public GameObject pre1;

    public GameObject pre2;

    public GameObject pre3;

    public Rigidbody rb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (split)
        {
            pre1 = Instantiate(miniGra1, transform.position, Quaternion.identity);    

            pre1.gameObject.GetComponent<Rigidbody>().velocity = rb.velocity; 

            pre1.gameObject.GetComponent<Rigidbody>().AddForce(miniGrape1); 

            pre2 = Instantiate(miniGra2, transform.position, Quaternion.identity);    

            pre2.gameObject.GetComponent<Rigidbody>().velocity = rb.velocity; 

            pre2.gameObject.GetComponent<Rigidbody>().AddForce(miniGrape2); 

            pre3 = Instantiate(miniGra3, transform.position, Quaternion.identity);    

            pre3.gameObject.GetComponent<Rigidbody>().velocity = rb.velocity; 

            pre3.gameObject.GetComponent<Rigidbody>().AddForce(miniGrape3); 

            Destroy(gameObject);    

        }	
	}
}
