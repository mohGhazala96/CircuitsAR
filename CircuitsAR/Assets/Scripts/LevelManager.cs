using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool isFirstLevel;
    public bool isFreePlay;
    public bool isFinished;
    public float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ChooseLevel1()
    {
        isFirstLevel = true;
        isFreePlay = false;
    }
    public void ChooseLevel2()
    {
        isFirstLevel=false;
        isFreePlay = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(!isFinished&& !isFreePlay)
            time += Time.deltaTime;
    }
}
