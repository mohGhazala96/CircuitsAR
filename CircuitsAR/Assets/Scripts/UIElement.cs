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
    float oldValue;// used to update components
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
            if (element.activeSelf)
            {
                Buzzer buzzer = element.gameObject.GetComponent<Buzzer>();
                voltage.text = "Voltage: " + buzzer.voltage.ToString("F2");
                current.text = "Current: " + buzzer.current.ToString("F2");
                resitance.text = "Resitance: " + buzzer.resistance;
                description.text = "" + buzzer.description;
                slider.gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }

        }
        else if (element.CompareTag("Led") && element.gameObject.GetComponent<Led>() != null)
        {
            if (element.activeSelf)
            {
                Led led = element.gameObject.GetComponent<Led>();
                voltage.text = "Voltage: " + led.voltage.ToString("F2"); ;
                current.text = "Current: " + led.current.ToString("F2"); ;
                resitance.text = "Resitance: " + led.resistance;
                description.text = "" + led.description;
                slider.gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (element.CompareTag("Resistor") && element.gameObject.GetComponent<Resistor>() != null)
        {
            if (element.activeSelf)
            {

                Resistor resistor = element.gameObject.GetComponent<Resistor>();
                oldValue = resistor.resistance;

                voltage.text = "Voltage: " + resistor.voltage.ToString("F2");
                current.text = "Current: " + resistor.current.ToString("F2"); 
                resitance.text = "Resitance: " + resistor.resistance.ToString("F2");
                description.text = "" + resistor.description;
                slider.maxValue = 1;
                slider.minValue = 0;
                if (!setSlider)
                {
                    slider.value = resistor.resistance;
                }
                resistor.resistance = slider.value;
         
                    Circuit circuit = FindObjectOfType<Circuit>();
                    circuit.UpdateComponents();
           
                setSlider = true;
            }
            else
            {
                Destroy(gameObject);
            }

        }
        else if (element.CompareTag("Battery") && element.gameObject.GetComponent<Battery>() != null)
        {
            if (element.activeSelf)
            {

                Battery battery = element.gameObject.GetComponent<Battery>();
                oldValue = battery.voltage;
                voltage.text = "Voltage: " + (int)battery.voltage;
                current.text = "Current: " + battery.current.ToString("F2"); ;
                resitance.enabled = false;
                description.text = "" + battery.description;
                slider.maxValue = 20;
                slider.minValue = 0;
                if (!setSlider)
                {
                    slider.value = battery.voltage;
                }
                battery.voltage = slider.value;

                    Circuit circuit = FindObjectOfType<Circuit>();
                    circuit.UpdateComponents();


                setSlider = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        gameObject.transform.position = new Vector3(element.transform.position.x, gameObject.transform.position.y, element.transform.position.z);
    }
}
