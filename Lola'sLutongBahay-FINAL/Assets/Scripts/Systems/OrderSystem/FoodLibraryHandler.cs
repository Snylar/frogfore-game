using UnityEngine;

public class FoodLibraryHandler : MonoBehaviour
{
    public Food[] foods;

    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
