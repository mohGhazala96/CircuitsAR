using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviour
{
    public GameObject element;
    public Text voltage;
    public Text current;
    public Text resitance;
    public Text description;
    public Slider slider;
    public Button closeButton;
    int oldValue;// used to update components
    bool setSlider;
    void Start()
    {
        closeButton.onClick.AddListener(Close);
        setSlider = false;

    }
    public void Close()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (element.CompareTag("Buzzer") && element.gameObject.GetComponent<Buzzer>() != null)
        {
            Buzzer buzzer = element.gameObject.GetComponent<Buzzer>();
            voltage.text = "Voltage: " + buzzer.voltage;
            current.text = "Current: " + buzzer.current;
            resitance.text = "Resitance: " + buzzer.resistance;
            description.text = "Description: " + buzzer.description;
            slider.enabled = false;
        }
        else if (element.CompareTag("Led") && element.gameObject.GetComponent<Led>() != null)
        {
            Led led = element.gameObject.GetComponent<Led>();
            voltage.text = "Voltage: " + led.voltage;
            current.text = "Current: " + led.current;
            resitance.text = "Resitance: " + led.resistance;
            description.text = "Description: " + led.description;
            slider.enabled = false;
        }
        else if (element.CompareTag("Resistor") && element.gameObject.GetComponent<Resistor>() != null)
        {
            Resistor resistor = element.gameObject.GetComponent<Resistor>();
            oldValue = (int)resistor.resistance;

            voltage.text = "Voltage: " + resistor.voltage;
            current.text = "Current: " + resistor.current;
            resitance.text = "Resitance: " + (int)resistor.resistance;
            description.text = "Description: " + resistor.description;
            slider.maxValue = 100;
            slider.minValue = 0;
            if (!setSlider)
            {
                slider.value = resistor.resistance;
            }
            resistor.resistance = slider.value;
            if(oldValue!= resistor.resistance)
            {
                Circuit circuit = FindObjectOfType<Circuit>();
                circuit.UpdateComponents();
            }
            setSlider = true;


        }
        else if (element.CompareTag("Battery") && element.gameObject.GetComponent<Battery>() != null)
        {
            Battery battery = element.gameObject.GetComponent<Battery>();
            oldValue= (int)battery.voltage;
            voltage.text = "Voltage: " + (int)battery.voltage;
            current.text = "Current: " + battery.current;
            resitance.enabled = false;
            description.text = "Description: " + battery.description;
            slider.maxValue = 20;
            slider.minValue = 0;
            if (!setSlider)
            {
                slider.value = battery.voltage;
            }
            battery.voltage = slider.value;
            if (oldValue != battery.voltage)
            {
                Circuit circuit = FindObjectOfType<Circuit>();
                circuit.UpdateComponents();
                print("updating");

            }
            setSlider = true;
        }

        gameObject.transform.position = new Vector3(element.transform.position.x, gameObject.transform.position.y, element.transform.position.z) ;
    }
}
