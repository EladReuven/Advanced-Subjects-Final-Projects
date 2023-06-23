using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    //private string username;
    private string password;

    private string name;
    private int gold;
    private int currentWeaponID;
    private float x;
    private float y;
    private float z;

    //public string Username { get => username;}
    public string Name { get => name;}
    public int Gold { get => gold;}
    public int CurrentWeaponID { get => currentWeaponID;}
    public float X { get => x;}
    public float Y { get => y;}
    public float Z { get => z;}
    public string Password { get => password;}

    public User(string password, PlayerData playerData)
    {
        this.password = password;
        this.name = playerData.playerName;
        this.gold = playerData.gold;
        this.currentWeaponID = playerData.currentWeaponID;
        this.x = playerData.position.x;
        this.y = playerData.position.y;
        this.z = playerData.position.z;
    }
}
