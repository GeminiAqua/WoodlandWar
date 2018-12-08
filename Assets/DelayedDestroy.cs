using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour {

    public float delayTime = 0.5f;

	void Start () {
		Destroy(gameObject, delayTime);
	}
}
