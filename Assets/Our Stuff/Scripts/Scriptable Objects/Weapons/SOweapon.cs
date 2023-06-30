using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/Weapon")]
public class SOweapon : ScriptableObject
{
    public string Name;
    public string Lore;
    public string Animation;
    public float Damage;
    public float AttackSpeed;
    public GameObject WeaponPrefab;
}