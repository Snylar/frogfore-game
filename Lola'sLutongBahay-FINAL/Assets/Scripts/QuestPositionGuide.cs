using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPositionGuide : MonoBehaviour
{
    public GameObject startcook;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            startcook.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            startcook.SetActive(false);
        }
    }
}
