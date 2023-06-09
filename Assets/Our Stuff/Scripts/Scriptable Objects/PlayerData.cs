using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Scriptable Objects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public string playerName;
    public int gold;
    public int currentWeaponID;
    public Vector3 position;
}
