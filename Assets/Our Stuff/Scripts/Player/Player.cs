using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData playerData;

    public void SetPlayerData(string pName, int gold, int weaponID, float x, float y, float z)
    {
        playerData.playerName = pName;
        playerData.gold = gold;
        playerData.currentWeaponID = weaponID;
        playerData.position = new Vector3(x, y, z);
    }
}
