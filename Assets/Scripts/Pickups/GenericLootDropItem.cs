using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GenericLootDropItem<T>
{
    public T item;

    public float Weight;

    public float Chance;

    [HideInInspector] public float probabilityRangeFrom;
    [HideInInspector] public float probabilityRangeTo;
}
