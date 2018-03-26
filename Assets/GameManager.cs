using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private const int NUMBER_OF_CARDS = 36;
    private const float SECONDS_PER_GAME = 180;
    private static GameManager _instance;

    private static readonly char[] CARD_VALUES = new char[] {
        'A', 'B', 'C', 'D', 'E', 'F',
        'G', 'H', 'I', 'J', 'K', 'L',
        'M', 'N', 'O', 'P', 'Q', 'R',
        'S', 'T', 'U', 'V', 'W', 'X',
        'Y', 'Z'
    };

    [SerializeField]
    private Image timeBar;

    [SerializeField]
    private Text timeRemaining;

    [SerializeField]
    private Card prefab;

    [SerializeField]
    private Transform cardParent;

    [SerializeField]
    private Results results;

    [NonSerialized]
    public Card Selected;

    [NonSerialized]
    public int AttemptCount;

    [NonSerialized]
    public int CorrectCount;

    private float time;
    private bool isCleared;

    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    public bool IsCleared {
        get {
            return CorrectCount == NUMBER_OF_CARDS / 2;
        }
    }

    public void Stop() {
        isCleared = true;
    }

    public void Start() {
        isCleared = false;
        CARD_VALUES.Shuffle();
        List<char> chosenValues = new List<char>();
        // pick 18 chars to use
        for (int i = 0; i < NUMBER_OF_CARDS / 2; i++) {
            chosenValues.Add(CARD_VALUES[i]);
        }

        List<Card> cards = new List<Card>();
        // add char pairs
        foreach (char chosen in chosenValues) {
            Card card1 = Instantiate<Card>(prefab);
            card1.Init(chosen);
            Card card2 = Instantiate<Card>(prefab);
            card2.Init(chosen);
            cards.Add(card1);
            cards.Add(card2);
        }
        cards.Shuffle();

        foreach (Card card in cards) {
            card.transform.SetParent(cardParent);
        }

        StartCoroutine(Run());
    }

    private IEnumerator Run() {
        while (!isCleared && time < SECONDS_PER_GAME) {
            time += Time.deltaTime;
            timeRemaining.text = Mathf.CeilToInt(SECONDS_PER_GAME - time).ToString();
            timeBar.transform.localScale = new Vector3(
                Mathf.Lerp(1, 0, time / SECONDS_PER_GAME),
                1,
                1);
            yield return null;
        }
        // do game end, save stats in object
        results.AttemptCount = AttemptCount;
        results.TimeRemaining = Mathf.CeilToInt(SECONDS_PER_GAME - time);
        results.ClearedCount = CorrectCount;
        results.IsCleared = IsCleared;
        SceneManager.LoadScene(2);
    }
}

public static class ExtensionMethods {
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list) {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}