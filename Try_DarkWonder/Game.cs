using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    [System.NonSerialized]
    public bool gameOver = false;
    private static Game instance;
	// Use this for initialization
    public static Game GetInstance()
    {
        return instance;
    }

	void Awake () {
        instance = this;
	}

    public void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        gameOver = true;
    }
}
