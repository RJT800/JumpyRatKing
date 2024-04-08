using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    private static GameManager Instance { get => _instance; }

    private void Awake()
    {
        // this is the singleton 
        //if _instance is not null, destroy this GameObject
        if (_instance != null)
            Destroy(gameObject);
        //else, set _instance to this
        else
            _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
