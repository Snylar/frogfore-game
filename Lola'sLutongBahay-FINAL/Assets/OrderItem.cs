using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatePart { Rice, Meal, Side }

public class OrderItem : MonoBehaviour
{
    public string itemName;
    public PlatePart partType;
}
