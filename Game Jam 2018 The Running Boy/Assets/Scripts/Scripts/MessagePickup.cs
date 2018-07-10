using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePickup : MonoBehaviour {

    public GameObject MessagePoint;

    public void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MessagePoint.GetComponent<BoxCollider>().enabled = true;
            MessagePoint.GetComponent<DeliveryLocation>().InventoryIcon.SetActive(true);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
