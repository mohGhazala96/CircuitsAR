using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Level
{
    first,
    second,
    third
}
public class LevelManager : MonoBehaviour
{
    public bool isFreePlay;
    public bool isFinished;
    public float time = 0;
    public float requiredCurrent=0;
    public int mode;
    public UIManager uIManager;
    public Level level;
    // Start is called before the first frame update
    public void Init()
    {
        SoundManager.Instance.Stop();
        Circuit.Instance.totalCurrent = 0;
        Circuit.Instance.totalResistance = 0;
        Circuit.Instance.battery = null;
        time = 0;
        if (mode == 0)
        {
            isFreePlay = true;
        }
        else if (mode == 1)
        {
            level = Level.first;
            requiredCurrent = 6;
        }
        else if (mode == 2)
        {
            level = Level.second;
            requiredCurrent = 4;
        }
        else if (mode == 3)
        {
            level = Level.third;
            requiredCurrent = 3;
        }
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
