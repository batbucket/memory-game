using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

    public void GoToGame() {
        SceneManager.LoadScene(1);
    }
}