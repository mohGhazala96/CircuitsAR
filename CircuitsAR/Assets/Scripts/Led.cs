using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Led : MonoBehaviour
{
    public float resistance;
    public float current;
    public float voltage;
    public float idealVoltage; // current at which it will operate optimally
    public float idealCurrent; // current at which it will operate optimally
    public string description = "led";
    public Material lampMaterial;
    public int index; // incase of multiple components

}
