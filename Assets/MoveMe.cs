using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMe : MonoBehaviour
{
    private float spd;

    private void Start()
    {
    }

    void Update()
    {
        spd = GameHandler.Instance.moveObjectSpd;
        transform.Translate(new Vector3((-1 * spd) * Time.deltaTime, 0));
    }
}
