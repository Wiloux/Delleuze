using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockingwiresanim : MonoBehaviour
{

    public float alpha;
    public Material mat;

    private void Start()
    {
        alpha = 1;
        mat.SetFloat("WireAlpha", 1);
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetFloat("WireAlpha", alpha);
    }
}
