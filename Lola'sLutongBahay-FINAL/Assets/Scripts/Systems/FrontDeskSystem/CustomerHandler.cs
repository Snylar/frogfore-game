using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LLB
{ 
    public class CustomerHandler : MonoBehaviour
{

    public Customer[] customers;
    public Customer currentCustomer = null;
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void GetCustomer()
    {
        // Randomly pick customer from customer list, the sprite doesnt matter anyways
        if (currentCustomer != null)
        {
            Debug.LogWarning("Customer should be null here");
            return;
        }

        var random = Random.Range(0, customers.Count());
        currentCustomer = customers[random];
    }

    public void RemoveCustomer()
    {
        // Should blow a notif here for those concerned
        currentCustomer = null;
    }
}
}


