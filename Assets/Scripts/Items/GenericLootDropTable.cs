using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericLootDropTable<T, U> where T : GenericLootDropItem<U>
{
    [SerializeField] public List<T> lootDropItems;
    float probabilityTotalWeight;

    public void ValidateTable()
    {
        if (lootDropItems != null && lootDropItems.Count > 0)
        {
            float currentProbabilityWeightMaximum = 0f;
            
            foreach (T lootDropItem in lootDropItems)
            {
                if (lootDropItem.Weight < 0f)
                {
                    lootDropItem.Weight = 0f;
                }
                else
                {
                    lootDropItem.probabilityRangeFrom = currentProbabilityWeightMaximum;
                    currentProbabilityWeightMaximum += lootDropItem.Weight;
                    lootDropItem.probabilityRangeTo = currentProbabilityWeightMaximum;
                }
            }
            probabilityTotalWeight = currentProbabilityWeightMaximum;
            
            foreach (T lootDropItem in lootDropItems)
            {
                lootDropItem.Chance = ((lootDropItem.Weight) / probabilityTotalWeight) * 100;
            }
        }
    }

    public T PickLootDropItem()
    {
        float pickedNumber = Random.Range(0, probabilityTotalWeight);
        Debug.Log("total weight is: " + probabilityTotalWeight);

        foreach (T lootDropItem in lootDropItems)
        {
            if (pickedNumber > lootDropItem.probabilityRangeFrom && pickedNumber < lootDropItem.probabilityRangeTo)
            {
                Debug.Log("Range from " + lootDropItem.probabilityRangeFrom + " to " + lootDropItem.probabilityRangeTo);
                return lootDropItem;
            }
        }
        Debug.Log("pickin my nose hahaha");
        return lootDropItems[0];
    }
}
