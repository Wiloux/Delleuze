using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public float moveObjectSpd;
    public static GameHandler Instance;

    public List<Material> EspacesMat = new List<Material>();
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
     //foreach(Material mat in EspacesMat)
     //   {
     //       if(mat != null)
     //       {
     //           mat.SetFloat("_Speed", -1.8f -moveObjectSpd);
     //       }
     //   }   
    }
}
