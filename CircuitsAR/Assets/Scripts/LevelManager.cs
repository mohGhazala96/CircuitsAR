using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool isFirstLevel;
    public bool isFreePlay;
    public bool isFinished;
    public float time = 0;
    public float requiredCurrent=0;
    public int mode;
    public UIManager uIManager;
    // Start is called before the first frame update
    public void Init()
    {
        time = 0;
        if (mode == 0)
        {
            isFreePlay = true;
        }
        else if (mode == 1)
        {
            ChooseLevel1();
            requiredCurrent = 6;
        }
        else if (mode == 2)
        {
            ChooseLevel2();
            requiredCurrent = 4;
        }
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
        {
            time += Time.deltaTime;
            uIManager.timeCounter.text = "Time: "+time.ToString("F2");
        }
    }
}
