using UnityEngine;

[CreateAssetMenu(fileName="New Customer", menuName="Customer")]
public class Customer : ScriptableObject
{
    public string customerName;
    public Sprite sprite;
}