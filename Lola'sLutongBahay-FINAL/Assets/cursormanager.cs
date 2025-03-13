using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursormanager : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite handCursor;
    public Sprite normalCursor;

    public GameObject clickEffect;
    public float timeBtwSpawn = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;

        if(Input.GetMouseButtonDown(0)){
            Instantiate(clickEffect, transform.position, Quaternion.identity);
        } else if(Input.GetMouseButtonUp(0)){
            rend.sprite = normalCursor;
        }
        if(timeBtwSpawn <= 0){
            
        } else {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
