using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string name;
    public int gold;
    public int currentWeaponID;
    public float x;
    public float y;
    public float z;

    public string Name { get => name;}
    public int Gold { get => gold;}
    public int CurrentWeaponID { get => currentWeaponID;}
    public float X { get => x;}
    public float Y { get => y;}
    public float Z { get => z;}

    public User(PlayerData playerData)
    {
        this.name = playerData.playerName;
        this.gold = playerData.gold;
        this.currentWeaponID = playerData.currentWeaponID;
        this.x = playerData.position.x;
        this.y = playerData.position.y;
        this.z = playerData.position.z;
    }
}
