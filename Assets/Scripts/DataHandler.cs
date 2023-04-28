using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DataHandler : MonoBehaviour
{
    [SerializeField] private ButtonManager buttonPrefab;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private List<Items> items;
    [SerializeField] private String label; 

    private int current_id = 0;

    private GameObject furniture;
    private String furnid;
    private static DataHandler instance;
    public static DataHandler Instance {
        get{
        if(instance==null){
            instance = FindObjectOfType<DataHandler>();
        }
        return instance;
    }}

    private async void Start() {
        await Get(label);
        CreateButton();
    }

    void CreateButton(){
        foreach (Items i in items)
        {
            ButtonManager b = Instantiate(buttonPrefab, buttonContainer.transform);
            b.ItemId = current_id;
            b.ButtonTexture = i.itemImage;
            current_id++;
        }
    }

    public void SetFurniture(int id)
    {
        furniture = items[id].itemPrefab;
    }

    public void SetFurnitureId(int id)
    {
        furnid = items[id].furniture_id;
    }

    public String GetFurnitureId(){
        return furnid;
    }


    public GameObject GetFurniture(){
        return furniture;
    }

    public async Task Get(String label){
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;
        foreach (var location in locations){
            var obj = await Addressables.LoadAssetAsync<Items>(location).Task;
            items.Add(obj);
        }
    }
}
