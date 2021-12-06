using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMe : MonoBehaviour
{
    private float spd;


    public bool isBg;

    public float backgroundLeftEdge;
    public float backgroundRightEdge;
    private Vector3 backgroundDistanceBtwEdges;
    private void Start()
    {
        if (isBg)
        {
            CalculateBackgroundEdges();
            backgroundDistanceBtwEdges = new Vector3(backgroundRightEdge - backgroundLeftEdge, 0f, 0f);

            transform.GetChild(0).transform.position = new Vector3(GetComponent<Renderer>().bounds.extents.x*2 - transform.GetChild(0).transform.position.x , transform.position.y, transform.position.z);
            transform.GetChild(1).transform.position = new Vector3(-GetComponent<Renderer>().bounds.extents.x*2 +  transform.GetChild(1).transform.position.x, transform.position.y, transform.position.z);
        }

    }

    void Update()
    {
        spd = GameHandler.Instance.moveObjectSpd;
        if (isBg)
        {
           Move();
        }
        else
        {
            transform.position += spd * Vector3.right * Time.deltaTime;
        }
    }


    private void CalculateBackgroundEdges()
    {
        backgroundRightEdge = transform.position.x + GetComponent<Renderer>().bounds.extents.x;
        backgroundLeftEdge = transform.position.x - GetComponent<Renderer>().bounds.extents.x;
    }

    //void CalculateBackgroundEdges()
    //{
    //    Vector3 position = transform.position;

    //    float distance = transform.position.z - Camera.main.transform.position.z;

    //   backgroundLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x;
    //   backgroundRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x;
    //}

    private bool PassedEdge()
    {
        return GameHandler.Instance.moveObjectSpd > 0 && transform.position.x > backgroundRightEdge || GameHandler.Instance.moveObjectSpd < 0 && transform.position.x < backgroundRightEdge;
    }


    private void Move()
    {
       transform.position += spd * Vector3.right * Time.deltaTime;
        if (PassedEdge())
        {
            MoveRightObjToOppositeEdge();
        }

    }

    private void MoveRightObjToOppositeEdge()
    {
        if (GameHandler.Instance.moveObjectSpd > 0)
            transform.position -= backgroundDistanceBtwEdges;
        else
            transform.position += backgroundDistanceBtwEdges;



        Debug.Log("1");
    }


    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawCube(new Vector3(backgroundRightEdge, 0f, 0f), new Vector3(0.1f, 2f, 0.1f));
            Gizmos.DrawCube(new Vector3(backgroundLeftEdge, 0f, 0f), new Vector3(0.1f, 2f, 0.1f));
        }
    }
}
