using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallArrow : MonoBehaviour {

    public int force;

    Rigidbody rb;

    public GameObject col;

    float losingVelocity;

    public bool shoot;

    public bool startVel;

    public GameObject splash;

    public GameObject splash1;

    public GameObject splash2;

    public bool spl1;

    public int sp1;

    public bool spl2;

    public int sp2;

    public Collider myCol;

    public Collider mySecCol;

    public bool align;

    public List <GameObject> miniGrapes;

    // Use this for initialization
    void Start () 
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(0,0,force);
        //losingVelocity = Game.losingVelocity;
    }

    void Update () 
    {
        if (shoot && rb != null)
        {
            startVel = true;
            shoot = false;
            //rb.AddRelativeForce(0,0,force); 
        }

        if (transform.position.y < -55)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (spl1)
        {
            sp1++;
            if (sp1 > 15)
            {
                Destroy (splash1);
                spl1 = false;
            }
        }

        if (spl2)
        {
            if (sp2 == 0)
            {
                //splash2 = Instantiate(splash, help.transform.position, Quaternion.LookRotation(rb.velocity));
            }
            sp2++;
            if (sp2 > 15)
            {
                //Destroy (splash2);
                spl2 = false;
            }
        }

        if (align)
        {
            AlignArrow();
        }

        if (rb.velocity != Vector3.zero)
        {
            rb.rotation = Quaternion.LookRotation(rb.velocity);  
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "fruit" || collider.gameObject.tag == "dummy" || collider.gameObject.tag == "banana" || collider.gameObject.tag == "miniGrapes")
        {
            if (collider.gameObject.tag == "miniGrapes")
            {
                foreach (GameObject mini in miniGrapes)
                {
                    if (mini == collider.gameObject)
                    {
                        return;
                    }
                }
            }

            if (col.GetComponent<ArrowCollider>().myFruit == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit))
                {
                    if (collider.gameObject.GetComponent<Fruit>().particle != null)
                    {
                        splash1 = Instantiate(collider.gameObject.GetComponent<Fruit>().particle, hit.point, Quaternion.LookRotation(-rb.velocity));
                        spl1 = true;
                    }
                }

                if (collider.gameObject.tag == "fruit" || collider.gameObject.tag == "miniGrapes")
                {
                    StabFruit(collider, col);
                    return;
                }
                else if (collider.gameObject.tag == "banana")
                {
                    StabFruit(collider, col);
                    return;
                }
                else if (collider.gameObject.tag == "dummy")
                {
                    StabDummy(collider);
                    return;
                }
            }

        }

        else if (collider.gameObject.tag == "bomb")
        {
            Game.gameOver = true;
        }

        else if (collider.gameObject.tag == "arrowBomb")
        {
            collider.GetComponent<ArrowBomb>().explode = true;
        }

        else if (collider.gameObject.tag == "grapes")
        {
            collider.gameObject.GetComponent<Grapes>().split = true;
            collider.gameObject.GetComponent<Grapes>().arCol = mySecCol;
            collider.gameObject.GetComponent<Grapes>().arrow = gameObject;
        }

    }

    void StabFruit(Collider collider, GameObject col)
    {
        AudioManager._sound.clip = AudioManager._hitFruit;
        AudioManager._sound.Play();

        if (collider.gameObject.tag == "banana")
        {
            collider.gameObject.GetComponent<Banana>().bananaAudio.Stop();
            Destroy(collider.gameObject.GetComponent<Banana>());

            for (int i = 0; i < collider.gameObject.GetComponent<Banana>().cols.Length; i++)
            {
                collider.gameObject.GetComponent<Banana>().cols[i].enabled = false;
            }
        }

        col.GetComponent<ArrowCollider>().myFruit = collider.gameObject;
        col.GetComponent<ArrowCollider>().copy = Instantiate(collider.gameObject);
        col.GetComponent<ArrowCollider>().align = true;
        Destroy(collider.transform.GetChild(0).gameObject);
        Destroy(col.GetComponent<ArrowCollider>().copy.GetComponent<Rigidbody>());
        Destroy(col.GetComponent<ArrowCollider>().copy.GetComponent<Collider>());
        Destroy(collider.gameObject.GetComponent<Rigidbody>());
        collider.transform.rotation = transform.rotation;
        collider.transform.parent = transform;
        Destroy(collider);

    }


    void StabDummy(Collider collider)
    {     
        AudioManager._sound.clip = AudioManager._hitDummy;
        AudioManager._sound.Play();
        align = true;
        collider.gameObject.GetComponent<Rigidbody>().velocity = rb.velocity / 5;
        col.transform.localPosition = new Vector3 (0,0,1);
        col.transform.parent = collider.transform;
        transform.parent = col.transform;
        myCol.enabled = false;
        Destroy(rb);
        AlignArrow();
    }

    void AlignArrow ()
    {
        Vector3 pos = col.transform.localPosition;
        float x = 0;
        float y = 0;
        float z = 0;

        if (Mathf.Abs(pos.x) > 0.9f || Mathf.Abs(pos.y) > 0.9f || Mathf.Abs(pos.z) > 0.9f)
        {
            if (Mathf.Abs(pos.x) > 0.9f)
            {
                if (pos.x > 0)
                {
                    x = -0.1f;
                }
                else
                {
                    x = 0.1f;
                }

            }

            if (Mathf.Abs(pos.y) > 0.9f)
            {
                if (pos.y > 0)
                {
                    y = -0.1f;
                }
                else
                {
                    y = 0.1f;
                }

            }

            if (Mathf.Abs(pos.z) > 0.9f)
            {
                if (pos.z > 0)
                {
                    z = -0.1f;
                }
                else
                {
                    z = 0.1f;
                }

            }

            col.transform.localPosition = new Vector3 (col.transform.localPosition.x + x, col.transform.localPosition.y + y, col.transform.localPosition.z + z);
        }

        else
        {
            align = false;
        }

    }

}
