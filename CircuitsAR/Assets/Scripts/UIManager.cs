using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject uiElement;
    public GameObject canvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                if (!selection.CompareTag("Circuit"))
                {
                    GameObject currrentUiElement = Instantiate(uiElement, canvas.transform);
                    UIElement uielement = currrentUiElement.GetComponent<UIElement>();
                    uielement.element = selection.gameObject;
                }
                
            }
        }
    }
}
