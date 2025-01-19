using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    // Start is called before the first frame update
    private Item currentItem;
    void Start()
    {
        currentItem = Item.None;
    }

    // Update is called once per frame
    void Update()
    {
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
