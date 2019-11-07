using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circuit : MonoBehaviour
{
    float current;

    private void OnTriggerEnter(Collider other)
    {
        UpdateCircuit(other.gameObject,true);

    }
    private void OnTriggerExit(Collider other)
    {
        UpdateCircuit(other.gameObject,false);
    }

    public void UpdateCircuit(GameObject currentElement, bool didEnter)
    {
        if (currentElement.tag.Equals("Buzzer") && (currentElement.gameObject.GetComponent<Buzzer>() != null))
        {
            Buzzer buzzer = currentElement.gameObject.GetComponent<Buzzer>();

        }
        else if (currentElement.tag.Equals("Led") && (currentElement.gameObject.GetComponent<Led>() != null))
        {
            Led led = currentElement.gameObject.GetComponent<Led>();

        }
        else if (currentElement.tag.Equals("Resistor") && (currentElement.gameObject.GetComponent<Resistor>() != null))
        {
            Resistor led = currentElement.gameObject.GetComponent<Resistor>();
        }
        else if (currentElement.tag.Equals("Battery") && (currentElement.gameObject.GetComponent<Battery>() != null))
        {
            Battery led = currentElement.gameObject.GetComponent<Battery>();
        }
    }
}
