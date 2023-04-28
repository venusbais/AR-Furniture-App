using System.Collections;   
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class LineManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    [SerializeField] public ARPlacementInteractable placementInteractable;
    [SerializeField] public GameObject pointPrefab;
    [SerializeField] public Canvas MeasuremnetScreen;
    [SerializeField] public Button ClrBtn;
    public TextMeshPro mText;

    private List<TextMeshPro> textMeshPros = new List<TextMeshPro>(); // Store the TextMeshPro objects

    void Start()
    {
        placementInteractable.objectPlaced.AddListener(DrawLine);
         ClrBtn.onClick.AddListener(ClearObject);
    }

    void Update()
    {
    }

    void DrawLine(ARObjectPlacementEventArgs args)
    { 
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, args.placementObject.transform.position);
        if (lineRenderer.positionCount > 1)
        {
            Vector3 pointA = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
            Vector3 pointB = lineRenderer.GetPosition(lineRenderer.positionCount - 2);
            float dist = Vector3.Distance(pointA, pointB);

            TextMeshPro distText = Instantiate(mText); 
            distText.text = "" + dist;
            textMeshPros.Add(distText); // Add the TextMeshPro object to the list

            Vector3 directionvector = (pointB - pointA);
            Vector3 normal = args.placementObject.transform.up;

            Vector3 upd = Vector3.Cross(directionvector, normal).normalized;
            Quaternion rotation = Quaternion.LookRotation(normal, upd);

            distText.transform.rotation = Quaternion.identity;
            distText.transform.position = (pointA + directionvector * 0.5f) + upd * 0.2f;
        }
    }

    public void ClearObject()
    {
        // Find all game objects with the "Point" tag and destroy them
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("Point"); 
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }

        // Reset the position count of the LineRenderer component to 0
        lineRenderer.positionCount = 0;

        // Destroy all the TextMeshPro objects that were created
        foreach (TextMeshPro textMeshPro in textMeshPros)
        {
            Destroy(textMeshPro.gameObject);
        }

        // Clear the list of TextMeshPro objects
        textMeshPros.Clear();
    }
}
