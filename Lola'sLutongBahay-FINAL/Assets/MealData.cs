using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMeal", menuName = "Meal Order")]
public class MealData : ScriptableObject
{
    public string riceName;
    public string mealName;
    public string sideName;
}

