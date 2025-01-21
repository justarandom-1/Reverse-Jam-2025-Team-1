using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{   
    public static Toolbar instance;
    // Start is called before the first frame update
    private Item currentItem;
    void Start()
    {
        instance = this;
        currentItem = Item.None;
    }

    public void DepleteItem(Item item){
        if(item != Item.None)
            transform.GetChild((int)item - 1).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.LoadAll<Sprite>("Toolbar/Buttons")[3];
            // transform.GetChild((int)item - 1).gameObject.GetComponent<Button>().interactable = false;
        if(item == currentItem)
            currentItem = Item.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
            SceneChanger.Restart();
        if (Input.GetKeyDown("h"))
            SceneChanger.Home();

        for(int i = 1; i <= 5; i++)
        {
            if (Input.GetKeyDown("" + i))
            {
                if(PlayerController.instance.Equip((Item)i)){
                    transform.GetChild(i-1).transform.GetComponent<UnityEngine.UI.Image>().sprite = Resources.LoadAll<Sprite>("Toolbar/Buttons")[1];
                    return;
                }
            }
        }

        if(currentItem != PlayerController.instance.GetCurrentItem()){
            if(currentItem != Item.None)
                transform.GetChild((int)currentItem - 1).transform.GetComponent<UnityEngine.UI.Image>().sprite = Resources.LoadAll<Sprite>("Toolbar/Buttons")[2];
            currentItem = PlayerController.instance.GetCurrentItem();
        }
    }
}
