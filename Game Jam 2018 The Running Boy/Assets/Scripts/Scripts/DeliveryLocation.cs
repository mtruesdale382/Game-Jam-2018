using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryLocation : MonoBehaviour {

    //LINK this gameObject in Inspector
    public GameObject InventoryIcon;
    public GameObject UIManager;

    public bool delivered = false;

    private UIManagerScript UIScript;

    private bool inventoryIconLinked = false;   

    private void Start()
    {
        UIScript = UIManager.GetComponent<UIManagerScript>();
        UIScript.totalMessages++;
    }
    public void Update()
    {
        if(InventoryIcon && !inventoryIconLinked)
        {
            InventoryIcon.GetComponent<InventoryMessage>().destination = this;
            inventoryIconLinked = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            delivered = true;
            UIScript.messagesDelivered++;

            gameObject.GetComponent<BoxCollider>().enabled = false;
        }


    }
}
