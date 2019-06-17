using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour
{
    private Vector3 manPosition;
    public Animator anim;
    public bool stop;

    // Update is called once per frame
    private void Start()
    {
        manPosition = new Vector3(5.98f,-0.34f,-7.92f);
    }
    void Update()
    {
        if(stop)
        {
            anim.SetTrigger("isWalking");
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 5);
            Debug.Log("Man" + manPosition);
            if (manPosition.x.Equals(gameObject.transform.position.x) && manPosition.y.Equals(gameObject.transform.position.y) && manPosition.z.Equals(gameObject.transform.position.z))
            {
                Debug.Log("Hellooooo");
            }
        }
    }
}
