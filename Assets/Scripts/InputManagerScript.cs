using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class InputManagerScript : ARBaseGestureInteractable
{
    [SerializeField] private Camera arCam;
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private Canvas FurnitureScreen;
    [SerializeField] private Button buy_btn;
    private String furnitureID;

    
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private Touch touch;
    private Pose pose;
   

    // Start is called before the first frame update
    void Start()
    {
    buy_btn.gameObject.SetActive(false);
    }
    protected override bool CanStartManipulationForGesture(TapGesture gesture){
        if (FurnitureScreen.enabled){
        if(gesture.targetObject == null)
            return true;
        }
        return false;
    }

    protected override void OnEndManipulation(TapGesture gesture){
        if (FurnitureScreen.enabled){
        if(gesture.isCanceled)
            return;
        if(gesture.targetObject!=null && IsPointerOverUI(gesture)){
            return;
        }
        if (GestureTransformationUtility.Raycast(gesture.startPosition, _hits, TrackableType.PlaneWithinPolygon)){
            GameObject placedObj = Instantiate(DataHandler.Instance.GetFurniture(), pose.position, pose.rotation);

            furnitureID = DataHandler.Instance.GetFurnitureId();
            buy_btn.gameObject.SetActive(true);
            buy_btn.onClick.AddListener(OpenBuyWebsite);


            var anchorobject = new GameObject("PlacementAnchor");
            anchorobject.transform.position = pose.position;
            anchorobject.transform.rotation = pose.rotation;
            placedObj.transform.parent = anchorobject.transform;   
        }
    }
        return;
    }

    void FixedUpdate() {
        CrosshairCalculation();
    }

    void OpenBuyWebsite() {
    string url = "https://main.divhf1lph4xju.amplifyapp.com/product/view-product/" + furnitureID;
    Application.OpenURL(url);
    }


    bool IsPointerOverUI(TapGesture touch)
    {
        if (FurnitureScreen.enabled)
        {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.startPosition.x, touch.startPosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
        }
        return false;
    }

    private RaycastHit hit;
    void CrosshairCalculation()
    {
        if(FurnitureScreen.enabled){
        if (crosshair != null && crosshair.TryGetComponent<Renderer>(out Renderer objectRenderer))
        {
            // Enable the Renderer component to make the object visible
            objectRenderer.enabled = true;
        }
        Vector3 origin = arCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
        
        if (GestureTransformationUtility.Raycast(origin, _hits, TrackableType.PlaneWithinPolygon))
        {
            pose = _hits[0].pose;
            crosshair.transform.position = pose.position;
            crosshair.transform.eulerAngles = new Vector3(90,0,0);
        }
    }
    else{
        if (crosshair != null && crosshair.TryGetComponent<Renderer>(out Renderer objectRenderer))
        {
            // Disable the Renderer component to make the object invisible
            objectRenderer.enabled = false;
        }
    }
}
}