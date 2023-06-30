using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public DataBaseManager dataBaseManager;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }



}
