using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour {

    [SerializeField]
    private Text title;

    [SerializeField]
    private Text corrects;

    [SerializeField]
    private Text attempts;

    [SerializeField]
    private Text timeRemaining;

    public void Start() {
        Results results = FindObjectOfType<Results>();
        title.text = results.IsCleared ? "Cleared" : "Time Out";
        corrects.text = string.Format("{0}\n{1}", "Successful Matches:", results.ClearedCount);
        attempts.text = string.Format("{0}\n{1}", "Attempted Matches:", results.AttemptCount);
        timeRemaining.text = string.Format("{0}\n{1}", "Time Remaining:", results.TimeRemaining);
        Destroy(results.gameObject);
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Escape) && Input.GetKey(KeyCode.LeftShift)) {
            SceneManager.LoadScene(0);
        }
    }
}