using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    //private string username;
    [SerializeField] private string password;

    [SerializeField] private string name;
    [SerializeField] private int gold;
    [SerializeField] private int currentWeaponID;
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z;

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
