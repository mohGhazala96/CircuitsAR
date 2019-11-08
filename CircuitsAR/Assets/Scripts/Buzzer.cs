using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buzzer : MonoBehaviour
{
    public float resistance;
    public float current;
    public float voltage;
    public float idealVoltage; // current at which it will operate optimally
    public float idealCurrent; // current at which it will operate optimally
    public string description = "buzzer";
    public int index; // incase of multiple components
}
