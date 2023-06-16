using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
