using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject uiElement;
    public GameObject canvas;
    static bool isBatteryMenuOpen;
    static bool isResistorMenuOpen;
    static bool isBuzzerMenuOpen;
    static bool isLedMenuOpen;
    static public bool instantiate;
    public Text currentRequired;
    public Text timeCounter;
    static public int mode = 0;
    public bool closed;
    public LevelManager levelManager;
    public GameObject levelFinishedPanel;
    public Text levelTime;
    public Text resistors;
    public Text buzzers;
    public Text leds;

    private void Start()
    {
        closed = false;
        isBatteryMenuOpen = false;
        isResistorMenuOpen = false;
        isBuzzerMenuOpen = false;
        isLedMenuOpen = false;
        SoundManager.Instance.Stop();

        if (levelManager != null)
        {
            levelManager.mode = mode;
            levelManager.Init();

            if (!levelManager.isFreePlay)
            {
                currentRequired.gameObject.SetActive(true);
                timeCounter.gameObject.SetActive(true);
                resistors.gameObject.SetActive(true);
                leds.gameObject.SetActive(true);
                buzzers.gameObject.SetActive(true);
                currentRequired.text = "Required Current:" + levelManager.requiredCurrent;
            }
            else
            {
                currentRequired.gameObject.SetActive(false);
                timeCounter.gameObject.SetActive(false);
                resistors.gameObject.SetActive(false);
                leds.gameObject.SetActive(false);
                buzzers.gameObject.SetActive(false);

            }
        }

     }
    // Update is called once per frame
    void Update()
    {
        if (!levelManager.isFreePlay)
        {
            resistors.text = "Current Resistors: " + Circuit.Instance.resistorCount.ToString();
            buzzers.text = "Current Buzzers: " + Circuit.Instance.buzzerCount.ToString();
            leds.text = "Current Leds: "+Circuit.Instance.ledCount.ToString();

        }
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

    public void setMode(int m)
    {
        mode = m;
        SceneManager.LoadScene("AR Scene");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void goToMainMenu()
    {
        SoundManager.Instance.Stop();
        SceneManager.LoadScene("MainMenu");
    }
    public void EndLevel()
    {
        levelTime.text = "Time Taken: "+levelManager.time.ToString("F2");
        levelFinishedPanel.SetActive(true);
        SoundManager.Instance.Stop();

    }
}
