using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resistor : MonoBehaviour
{
    public float resistance;
    public float current;
    public float voltage;
    public string description = "resistor";
    public int index; // incase of multiple components
    bool isRemoved;
    private void Start()
    {
        isRemoved = false;
    }
    private void OnDisable()
    {

        Circuit.Instance.RemoveComponent(gameObject, index);
    }

}
