using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag("Buzzer") && selection.gameObject.GetComponent<Buzzer>()!=null )
            {
                Buzzer buzzer = selection.gameObject.GetComponent<Buzzer>();
                // retrive the information
            }
            else if (selection.CompareTag("Led") && selection.gameObject.GetComponent<Led>() != null)
            {
                Led led = selection.gameObject.GetComponent<Led>();
                // retrive the information
            }
            else if (selection.CompareTag("Resistor") && selection.gameObject.GetComponent<Resistor>() != null)
            {
                Resistor resistor = selection.gameObject.GetComponent<Resistor>();
                // retrive the information
            }
            else if (selection.CompareTag("Battery") && selection.gameObject.GetComponent<Battery>() != null)
            {
                Battery battery = selection.gameObject.GetComponent<Battery>();
                // retrive the information
            }
        }
    }
}
