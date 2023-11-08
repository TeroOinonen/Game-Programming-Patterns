using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public static event Action<CoinPickup> OnCoinCollected;

    public int CoinValue { get; private set; }

    public void SetValue(int newValue)
    {
        CoinValue = newValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDestroy()
    {
        OnCoinCollected?.Invoke(this);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.EulerRotation(0, 5 * Time.deltaTime, 0);
    }
}
