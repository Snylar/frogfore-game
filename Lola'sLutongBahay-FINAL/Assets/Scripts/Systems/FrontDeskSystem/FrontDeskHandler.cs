using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDeskHandler : MonoBehaviour
{

    CustomerHandler customerHandler;
    GameObject customerHolder;
    void Start()
    {
        GameObject manager = GameObject.FindWithTag("CustomerManager");
        customerHandler = manager.GetComponent<CustomerHandler>();
        customerHolder = transform.Find("CustomerHolder")?.gameObject;


        // reset stae of customerHolder
        customerHolder.GetComponent<SpriteRenderer>().sprite = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnterCustomer();
            Debug.Log("Space key was pressed!");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            RemoveCustomerToDesk();
            Debug.Log("Space A was pressed!");
        }
    }

    void EnterCustomer()
    {
        customerHandler.GetCustomer();

        if (customerHandler.currentCustomer)
        {
            LoadCustomerToDesk();
        }
    }

    void LoadCustomerToDesk()
    {
        customerHolder.GetComponent<SpriteRenderer>().sprite = customerHandler.currentCustomer.sprite;
    }

    void RemoveCustomerToDesk()
    {
        customerHolder.GetComponent<SpriteRenderer>().sprite = null;

        //For debug only
        customerHandler.RemoveCustomer();
    }
}
