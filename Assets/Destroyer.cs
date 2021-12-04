using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Ground")
        Destroy(collision.gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag != "Ground")
            Destroy(collision.gameObject);
    }
}
