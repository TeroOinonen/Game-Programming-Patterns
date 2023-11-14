using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public static event Action OnEnemyKilled;

	// Start is called before the first frame update
	void Start()
	{

	}

	private void OnDestroy()
	{
		OnEnemyKilled?.Invoke();
	}
}
