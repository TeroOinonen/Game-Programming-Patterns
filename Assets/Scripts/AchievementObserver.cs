using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementObserver : MonoBehaviour
{
    private int coinsCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        CoinPickup.OnCoinCollected += AddCollectedCount;
    }

    private void AddCollectedCount(CoinPickup obj)
    {
        this.coinsCollected += obj.CoinValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
