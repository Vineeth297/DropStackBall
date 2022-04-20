using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float forceOnTile;
    public float angleOfForceOnTile;

    private void Awake()
    {
        #region Singleton
        
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
        
        #endregion
    }
}
