using UnityEngine;

//stats that can be modified by upgrades
public class PlayerStats : MonoBehaviour
{
    [Header("Player")]
    public int maxHealth;
    public int moveSpeed;

    [Header("Flashlight")]
    public int damage;
    public int flashlightRange;
    public int flashlightAngle;

}
