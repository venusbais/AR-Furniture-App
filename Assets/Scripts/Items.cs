using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "AR FURNITURE APP/Items", order = 0)]
public class Items : ScriptableObject 
{
    public float price;
    public GameObject itemPrefab;
    public Sprite itemImage;
    public String furniture_id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
