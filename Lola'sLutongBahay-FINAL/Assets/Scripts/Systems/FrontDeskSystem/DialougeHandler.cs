using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LLB
{
    public class DialougeHandler : MonoBehaviour
    {

        public string GenerateOrderDialouge(Dictionary<string, Food> currentOrder)
        {
            List<string> foodNames = new List<string>();

            foreach (KeyValuePair<string, Food> pair in currentOrder)
            {
                foodNames.Add(pair.Value.name);
            }

            string sentence = string.Join(", ", foodNames);

            // Replace last comma with " at " if more than one item
            int lastComma = sentence.LastIndexOf(", ");
            if (lastComma != -1)
            {
                sentence = sentence.Remove(lastComma, 2).Insert(lastComma, " at ");
            }

            return $"Pabili nga po ng {sentence}";
        }
    }

}
