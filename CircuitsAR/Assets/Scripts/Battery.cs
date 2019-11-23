using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public float voltage;
    public float current;
    public string description= "battery";
    bool isRemoved;
    private void Start()
    {
        isRemoved = false;
    }
    private void OnDisable()
    {
        if (Circuit.Instance != null)
        {
            Circuit.Instance.totalCurrent = 0;
            Circuit.Instance.UpdateComponents();
        }
    }

}
