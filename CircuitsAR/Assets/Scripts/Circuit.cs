using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circuit : MonoBehaviour
{
    float current;
    float totalResistance;
    float mainVoltage;
    List<GameObject> components;
    int resistorIndex = 0;
    int buzzerIndex = 0;
    int ledIndex = 0;

    private void Start()
    {
        components = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        UpdateCircuit(other.gameObject, true, false);

    }
    private void OnTriggerExit(Collider other)
    {
        UpdateCircuit(other.gameObject, false, false);
    }

    public void UpdateCircuit(GameObject currentElement, bool didEnter, bool onlyUpdate)
    {
        if (!onlyUpdate)
        {
            if (currentElement.tag.Equals("Buzzer") && (currentElement.gameObject.GetComponent<Buzzer>() != null))
            {
                Buzzer buzzer = currentElement.gameObject.GetComponent<Buzzer>();

                if (didEnter)
                {
                    totalResistance += buzzer.resistance;
                    buzzer.index = buzzerIndex;
                    buzzerIndex++;
                }
                else
                {
                    totalResistance -= buzzer.resistance;
                    RemoveComponent(currentElement, buzzer.index);
                }

            }
            else if (currentElement.tag.Equals("Led") && (currentElement.gameObject.GetComponent<Led>() != null))
            {
                Led led = currentElement.gameObject.GetComponent<Led>();
                if (didEnter)
                {
                    totalResistance += led.resistance;
                    led.index = ledIndex;
                    ledIndex++;
                }
                else
                {
                    totalResistance -= led.resistance;
                    RemoveComponent(currentElement, led.index);
                }

            }
            else if (currentElement.tag.Equals("Resistor") && (currentElement.gameObject.GetComponent<Resistor>() != null))
            {
                Resistor resistor = currentElement.gameObject.GetComponent<Resistor>();
                if (didEnter)
                {
                    totalResistance += resistor.resistance;
                    resistor.index = resistorIndex;
                    resistorIndex++;
                }
                else
                {
                    totalResistance -= resistor.resistance;
                    RemoveComponent(currentElement, resistor.index);
                }
            }
            else if (currentElement.tag.Equals("Battery") && (currentElement.gameObject.GetComponent<Battery>() != null))
            {
                Battery battery = currentElement.gameObject.GetComponent<Battery>();
                if (didEnter)
                    mainVoltage = battery.voltage;
                else
                    mainVoltage = 0;
            }
        }

        if (mainVoltage != 0)
        {
            current = mainVoltage / totalResistance;
        }
        else
        {   // turn off all components
            current = 0;
            DisableComponents();
        }
    }
    void DisableComponents()
    {
        foreach (GameObject component in components)
        {

            if (component.GetComponent<Resistor>() != null)
            {
                component.GetComponent<Resistor>().current = 0;
                component.GetComponent<Resistor>().voltage = 0;

            }
            else if (component.GetComponent<Led>() != null)
            {
                component.GetComponent<Led>().current = 0;
                component.GetComponent<Led>().voltage = 0;
                // turn off light
            }
            else if (component.GetComponent<Buzzer>() != null)
            {
                component.GetComponent<Buzzer>().current = 0;
                component.GetComponent<Buzzer>().voltage = 0;
                // turn off sound
            }

        }
    }
    void RemoveComponent(GameObject component, int index)
    {
        foreach (GameObject currentComponent in components)
        {
            if (component.tag.Equals(currentComponent.tag))
            {
                if (component.GetComponent<Resistor>() != null && component.GetComponent<Resistor>().index == index)
                {
                    components.Remove(component);
                }
                else if (component.GetComponent<Led>() != null && component.GetComponent<Led>().index == index)
                {
                    components.Remove(component);
                }
                else if (component.GetComponent<Buzzer>() != null && component.GetComponent<Buzzer>().index == index)
                {
                    components.Remove(component);
                }
                return;
            }
        }

    }
}
