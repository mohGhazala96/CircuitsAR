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
        RemoveFromCircuit();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if (!GetComponent<Collider>().enabled && !isRemoved)
            {
                RemoveFromCircuit();
                isRemoved = true;
            }
            else
            {
                isRemoved = false;
            }
        }
    }

    private void RemoveFromCircuit()
    {
        if (Circuit.Instance != null)
        {
            Circuit.Instance.totalCurrent = 0;
            Circuit.Instance.UpdateComponents();
        }
            
    }

}
