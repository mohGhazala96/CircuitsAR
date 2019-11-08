using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circuit : MonoBehaviour
{
    public float totalCurrent;
    public float totalResistance;
    public float mainVoltage;
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
        UpdateCircuit(other.gameObject, true);

    }

    private void OnTriggerExit(Collider other)
    {
        UpdateCircuit(other.gameObject, false);
    }

    void UpdateCircuit(GameObject currentElement, bool didEnter)
    {

        if (currentElement.tag.Equals("Buzzer") && (currentElement.gameObject.GetComponent<Buzzer>() != null))
        {
            Buzzer buzzer = currentElement.gameObject.GetComponent<Buzzer>();

            if (didEnter)
            {
                buzzer.index = buzzerIndex;
                buzzerIndex++;
                components.Add(currentElement);
            }
            else
            {
                RemoveComponent(currentElement, buzzer.index);
            }

        }
        else if (currentElement.tag.Equals("Led") && (currentElement.gameObject.GetComponent<Led>() != null))
        {
            Led led = currentElement.gameObject.GetComponent<Led>();
            if (didEnter)
            {
                led.index = ledIndex;
                ledIndex++;
                components.Add(currentElement);

            }
            else
            {
                RemoveComponent(currentElement, led.index);
            }

        }
        else if (currentElement.tag.Equals("Resistor") && (currentElement.gameObject.GetComponent<Resistor>() != null))
        {
            Resistor resistor = currentElement.gameObject.GetComponent<Resistor>();
            if (didEnter)
            {
                resistor.index = resistorIndex;
                resistorIndex++;
                components.Add(currentElement);
            }
            else
            {
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


        if (mainVoltage != 0) //upadting the resistance
        {
            UpdateResistance();
            UpdateComponents();
        }
        else
        {   // turn off all components
            totalCurrent = 0;
            DisableComponents();
        }
    }

    void UpdateResistance()
    {
        totalResistance = 0;
        foreach (GameObject component in components)
        {
            if (component.GetComponent<Resistor>() != null)
            {
                totalResistance += component.GetComponent<Resistor>().resistance;
            }
            else if (component.GetComponent<Led>() != null)
            {
                totalResistance += component.GetComponent<Led>().resistance;
            }
            else if (component.GetComponent<Buzzer>() != null)
            {
                totalResistance += component.GetComponent<Buzzer>().resistance;
            }

        }
        totalCurrent = mainVoltage / totalResistance;
    }

    public void UpdateComponents()
    {
        foreach (GameObject component in components)
        {

            if (component.GetComponent<Resistor>() != null)
            {
                Resistor resistor = component.GetComponent<Resistor>();
                resistor.current = totalCurrent;
                resistor.voltage = resistor.resistance* resistor.current;
            }
            else if (component.GetComponent<Led>() != null)
            {
                Led led = component.GetComponent<Led>();
                led.current = totalCurrent;
                led.voltage = led.resistance * led.current;
                // turn on led if it passes ideal values
            }
            else if (component.GetComponent<Buzzer>() != null)
            {
                Buzzer buzzer = component.GetComponent<Buzzer>();
                buzzer.current = totalCurrent;
                buzzer.voltage = buzzer.resistance * buzzer.current;
                SoundManager.Instance.PlayMusic(SoundManager.Instance.buzzerSound,true);
            }

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
                SoundManager.Instance.Stop();
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
                    component.GetComponent<Resistor>().current = 0;
                    component.GetComponent<Resistor>().voltage = 0;
                }
                else if (component.GetComponent<Led>() != null && component.GetComponent<Led>().index == index)
                {
                    components.Remove(component);
                    component.GetComponent<Led>().current = 0;
                    component.GetComponent<Led>().voltage = 0;
                }
                else if (component.GetComponent<Buzzer>() != null && component.GetComponent<Buzzer>().index == index)
                {
                    components.Remove(component);
                    component.GetComponent<Buzzer>().current = 0;
                    component.GetComponent<Buzzer>().voltage = 0;
                    SoundManager.Instance.Stop();
                }
                return;
            }
        }
        UpdateComponents();
    }
}
