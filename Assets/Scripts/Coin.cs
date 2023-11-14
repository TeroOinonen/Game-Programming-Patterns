using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	public static event Action OnCoinCollected;

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
        OnCoinCollected?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.EulerRotation(0, 5 * Time.deltaTime, 0);
    }
}
