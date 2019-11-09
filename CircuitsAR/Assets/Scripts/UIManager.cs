using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject uiElement;
    public GameObject canvas;
    static bool isBatteryMenuOpen;
    static bool isResistorMenuOpen;
    static bool isBuzzerMenuOpen;
    static bool isLedMenuOpen;
    static public bool instantiate;
    public bool closed;
    private void Start()
    {
        closed = false;
        isBatteryMenuOpen = false;
        isResistorMenuOpen = false;
        isBuzzerMenuOpen = false;
        isLedMenuOpen = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) )
        {

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                if (!selection.CompareTag("Circuit"))
                {
                    if ((selection.CompareTag("Buzzer") && !isBuzzerMenuOpen) || (selection.CompareTag("Led") && !isLedMenuOpen)
                        || (selection.CompareTag("Resistor") && !isResistorMenuOpen)|| (selection.CompareTag("Battery") && !isBatteryMenuOpen))
                    {
                        GameObject currrentUiElement = Instantiate(uiElement, canvas.transform);
                        UIElement uielement = currrentUiElement.GetComponent<UIElement>();
                        uielement.element = selection.gameObject;
                        if(selection.CompareTag("Buzzer"))
                            isBuzzerMenuOpen = true;
                        if (selection.CompareTag("Led"))
                            isLedMenuOpen = true;
                        if (selection.CompareTag("Resistor"))
                            isResistorMenuOpen = true;
                        if (selection.CompareTag("Battery"))
                            isBatteryMenuOpen = true;
                    }

                }
                
            }
        }

        GameObject[] uiElements = GameObject.FindGameObjectsWithTag("UIElement");

        isBuzzerMenuOpen = false;
        isBatteryMenuOpen = false;
        isResistorMenuOpen = false;
        isLedMenuOpen = false;

        foreach (GameObject uiElement in uiElements)
        {
            if (uiElement.GetComponent<UIElement>().element.CompareTag("Buzzer"))
                isBuzzerMenuOpen = true;
            if (uiElement.GetComponent<UIElement>().element.CompareTag("Led"))
                isLedMenuOpen = true;
            if (uiElement.GetComponent<UIElement>().element.CompareTag("Resistor"))
                isResistorMenuOpen = true;
            if (uiElement.GetComponent<UIElement>().element.CompareTag("Battery"))
                isBatteryMenuOpen = true;
        }


    }
}
