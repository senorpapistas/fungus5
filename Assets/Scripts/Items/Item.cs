using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum Effects
{
    None, Heal, MaxHealth, MoveSpeed, TurnSpeed
}

[System.Serializable]
public struct EffectWrapper
{
    public Effects effect;
    public int amount;
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/Item")]

public class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite sprite;
    public List<EffectWrapper> effects;

    public void Use(GameObject player)
    {
        foreach (EffectWrapper effectWrapper in effects)
        {
            switch(effectWrapper.effect)
            {
                case Effects.None:
                    break;
                case Effects.Heal:
                    player.GetComponent<PlayerHealth>().GainHealth(effectWrapper.amount);
                    break;
                case Effects.MaxHealth:
                    player.GetComponent<PlayerHealth>().ChangeMaxHealth(effectWrapper.amount);
                    break;
                case Effects.MoveSpeed:
                    player.GetComponent<BasicMovement>().moveSpeed += effectWrapper.amount;
                    break;
                case Effects.TurnSpeed:
                    player.GetComponent<BasicMovement>().turnSpeed += effectWrapper.amount;
                    break;
            }
        }
    }
}
