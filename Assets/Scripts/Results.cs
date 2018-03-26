using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Results : MonoBehaviour {
    public int AttemptCount;
    public int ClearedCount;
    public float TimeRemaining;
    public bool IsCleared;

    private void Start() {
        DontDestroyOnLoad(this.gameObject);
    }
}