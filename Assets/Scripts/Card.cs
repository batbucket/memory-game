using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {
    private static GameManager game;

    [SerializeField]
    private Text text;

    [SerializeField]
    private Image background;

    [SerializeField]
    private Button button;

    private char value;
    private CardState state;

    public void Select() {
        if (game.Selected == null) {
            game.Selected = this;
            SetState(CardState.SELECTED);
        } else {
            game.AttemptCount++;
            if (this.Equals(game.Selected)) {
                this.SetState(CardState.SOLVED);
                game.Selected.SetState(CardState.SOLVED);
                game.CorrectCount++;
                // detect end state
                if (GameManager.Instance.IsCleared) {
                    GameManager.Instance.Stop();
                }
            } else {
                this.SetState(CardState.INCORRECT);
                game.Selected.SetState(CardState.INCORRECT);
            }
            game.Selected = null;
        }
    }

    private void SetState(CardState state) {
        this.state = state;
        switch (state) {
            case CardState.UNSELECTED:
                background.color = Color.white;
                button.interactable = true;
                text.color = Color.black;
                text.text = "";
                break;

            case CardState.SELECTED:
                background.color = Color.cyan;
                button.interactable = false;
                text.color = Color.black;
                text.text = value.ToString();
                break;

            case CardState.INCORRECT:
                background.color = Color.red;
                button.interactable = true;
                text.color = Color.black;
                text.text = value.ToString();
                break;

            case CardState.SOLVED:
                background.color = Color.grey;
                button.interactable = false;
                text.color = Color.black;
                text.text = value.ToString();
                break;
        }
    }

    public void Init(char value) {
        SetState(CardState.UNSELECTED);
        this.value = value;
    }

    public override bool Equals(object other) {
        Card item = other as Card;
        if (item == null) {
            return false;
        }
        return this.value.Equals(item.value);
    }

    public override int GetHashCode() {
        return value.GetHashCode();
    }

    private void Update() {
        if (state == CardState.INCORRECT && game.Selected != null) {
            this.SetState(CardState.UNSELECTED);
        }
    }

    private void Start() {
        if (game == null) {
            game = GameManager.Instance;
        }
    }
}