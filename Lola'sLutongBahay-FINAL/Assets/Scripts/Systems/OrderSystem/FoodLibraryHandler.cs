using System;
using System.Collections.Generic;
using UnityEngine;


namespace LLB
{
    public class FoodLibraryHandler : MonoBehaviour
{
    public Food[] foods;

    public Dictionary<string, Food> currentOrder = null;

    void Start()
    {
        currentOrder = null;
        DontDestroyOnLoad(this);
    }

    Food GetFoodWhichIsA(string foodType)
    {
        List<Food> validFoods = new List<Food>();

        foreach (Food food in foods)
        {
            if (food.dishType.ToString() == foodType)
            {
                validFoods.Add(food);
            }
        }

        if (validFoods.Count > 0)
        {
            return validFoods[UnityEngine.Random.Range(0, validFoods.Count)];
        }

        return null;
    }

    public void GenerateRandomOrder()
    {
        Dictionary<string, Food> order = new Dictionary<string, Food>();

        foreach (DishType dish in Enum.GetValues(typeof(DishType)))
        {
            order.Add(dish.ToString(), GetFoodWhichIsA(dish.ToString()));
        }

        currentOrder = order;
    }

}
}

