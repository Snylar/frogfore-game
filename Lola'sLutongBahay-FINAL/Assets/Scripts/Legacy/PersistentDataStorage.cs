using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataStorage : MonoBehaviour
{
    public static PersistentDataStorage gameSave;
    public List<ScriptableObject> objects = new List<ScriptableObject>();
    public void Awake() 
    {   
        if(gameSave  == null)
        {
            gameSave = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
