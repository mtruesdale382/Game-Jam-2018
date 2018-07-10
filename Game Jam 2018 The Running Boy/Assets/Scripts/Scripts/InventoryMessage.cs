using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMessage : MonoBehaviour {

    public Text DescriptionText;
    public string MessageText;
    //DON'T link this one in Inspector.
    public DeliveryLocation destination;

    private void Start()
    {
        Debug.Log("Inventory message: s" + gameObject.name + " starts");
    }

    private void Update()
    {
        if (destination)
        {
            if (destination.delivered)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void MouseEnter()
    {
        DescriptionText.text = MessageText;
    }

    public void MouseExit()
    {
        DescriptionText.text = "";
    }
}
