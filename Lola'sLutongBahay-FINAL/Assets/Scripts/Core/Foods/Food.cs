using UnityEngine;

public enum DishType
{
    Side,
    Ulam,
    Rice
}

[CreateAssetMenu(fileName = "New Food", menuName = "Food")]

public class Food : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public DishType dishType;
}
