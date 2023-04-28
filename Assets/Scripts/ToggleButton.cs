using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{

   [SerializeField] public Canvas FurnitureScreen;
   [SerializeField] public Canvas MeasurementScreen;
   [SerializeField] Button btn;


private void Start() {
    btn.onClick.AddListener(ToggleScreens);
    FurnitureScreen.enabled = true;
    MeasurementScreen.enabled = false;
}

private void Update() {
    
}

public void ToggleScreens(){
    Debug.Log("ToggleScreens() called");
    if (FurnitureScreen.enabled)
    {
        FurnitureScreen.enabled = false;
        MeasurementScreen.enabled = true;
    }
    else
    {
        FurnitureScreen.enabled = true;
        MeasurementScreen.enabled = false;
    }
    }
}
