using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private RawImage buttonImage;
    private Button btn;
    private int _itemId;
    private Sprite _buttonTexture;

     public int ItemId
    {
        set => _itemId = value;
    }
    public Sprite ButtonTexture 
    {
        set
        {
            _buttonTexture = value;
            buttonImage.texture = _buttonTexture.texture;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SelectObject);
    }
    

    void SelectObject(){
        DataHandler.Instance.SetFurniture(_itemId);
        DataHandler.Instance.SetFurnitureId(_itemId);

    }
}
