using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Spawner : MonoBehaviour
{

    private int currentStage = 1;

    private List<GameObject> obstacles;
    private List<GameObject> backgroundObjects;
    private List<GameObject> Parallaxbg;


    [Header("Stage1")]
    public List<GameObject> obstacles1;
    public List<GameObject> backgroundObjects1;
    public List<GameObject> Parallaxbg1;
    public VolumeProfile Volume1;

    [Header("Stage2")]
    public List<GameObject> obstacles2;
    public List<GameObject> backgroundObjects2;
    public List<GameObject> Parallaxbg2;
    public VolumeProfile Volume2;

    [Header("Stage3")]
    public List<GameObject> obstacles3;
    public List<GameObject> backgroundObjects3;
    public List<GameObject> Parallaxbg3;
    public VolumeProfile Volume3;


    public bool rdyToSpawnObs;
    public bool rdyToSpawnBg;

    public GameObject lastBgSpawned;
    public GameObject lastObsSpawned;



    public int currentPara;

    private float backgroundCd;
    public Vector2 backgroundCdDur;

    private float obstacleCd;
    public Vector2 obstacleCdDur;

    public Transform obstaclesPosition;
    public Transform backgroundPosition;
    public Transform parraPosition;

    private bool transitioning;
    public GameObject allObstacles;
    void Start()
    {
        changeStageSkin(currentStage);
        backgroundCd = Random.Range(backgroundCdDur.x, backgroundCdDur.y);
        obstacleCd = Random.Range(obstacleCdDur.x, obstacleCdDur.y);
        currentPara = Random.Range(0, Parallaxbg.Count);
        // Parallaxbg[currentPara].transform.position = new Vector3(11.845f, Parallaxbg[currentPara].transform.position.y, parraPosition.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            StartCoroutine(Transition());

        foreach (GameObject t in Parallaxbg)
        {
            if (t.transform.position.x <= -12.83f)
            {
                t.transform.position = new Vector3(11.8f, Parallaxbg[currentPara].transform.position.y, parraPosition.transform.position.z);
            }

        }
        //if (Parallaxbg[currentPara].transform.position.x <= -12.83f)
        //{
        //    int oldCurrentPara = currentPara;
        //    while (currentPara == oldCurrentPara)
        //    {
        //        currentPara = Random.Range(0, Parallaxbg.Count);
        //    }
        //    Parallaxbg[currentPara].transform.position = new Vector3(11.845f, Parallaxbg[currentPara].transform.position.y, parraPosition.transform.position.z);
        //}

        if (transitioning)
            return;


        if (rdyToSpawnObs)
            if (obstacleCd >= 0)
            {
                obstacleCd -= Time.deltaTime;
            }
            else
            {
                rdyToSpawnObs = false;
                obstacleCd = Random.Range(obstacleCdDur.x, obstacleCdDur.y);
                lastObsSpawned = Instantiate(obstacles[Random.Range(0, obstacles.Count)], obstaclesPosition.position, Quaternion.Euler(-90, 0, 0), allObstacles.transform);
            }


        if (rdyToSpawnBg)
            if (backgroundCd >= 0)
            {
                backgroundCd -= Time.deltaTime;
            }
            else
            {
                rdyToSpawnBg = false;
                backgroundCd = Random.Range(backgroundCdDur.x, backgroundCdDur.y);
                lastBgSpawned = Instantiate(backgroundObjects[Random.Range(0, backgroundObjects.Count)], backgroundPosition.position, Quaternion.Euler(-90,0,0));
            }
    }



    void changeStageSkin(int currentStage)
    {
            switch (currentStage)
        {
            case 1:
                obstacles = obstacles1;
                Parallaxbg = Parallaxbg1;
                backgroundObjects = backgroundObjects1;
                Camera.main.GetComponent<Volume>().profile = Volume1;
                break;
            case 2:
                obstacles = obstacles2;
                Parallaxbg = Parallaxbg2;
                backgroundObjects = backgroundObjects2;
                Camera.main.GetComponent<Volume>().profile = Volume2;
                break;
            case 3:
                obstacles = obstacles3;
                Parallaxbg = Parallaxbg3;
                backgroundObjects = backgroundObjects3;
                Camera.main.GetComponent<Volume>().profile = Volume3;
                break;
        }

        foreach (GameObject t in Parallaxbg)
        {
            t.SetActive(true);

        }

        currentPara = Random.Range(0, Parallaxbg.Count);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject == lastBgSpawned)
        {
            rdyToSpawnBg = true;
        }

        if (other.transform.gameObject == lastObsSpawned)
        {
            rdyToSpawnObs = true;
        }
    }

    public GameObject transitionObject;
    public Transform transitionObjectPos;

    IEnumerator Transition()
    {

        transitioning = true;
        currentStage++;
        while (allObstacles.transform.childCount != 0)
        {
            yield return null;
        }
        Instantiate(transitionObject, transitionObjectPos.position, Quaternion.identity, transitionObjectPos);

        yield return new WaitForSeconds(2f);
        foreach (GameObject t in Parallaxbg)
        {
            t.SetActive(false);

        }
        yield return new WaitForSeconds(0.5f);
        changeStageSkin(currentStage);
        while (transitionObjectPos.childCount != 0)
        {
            yield return null;
        }
        transitioning = false;
    }
}
