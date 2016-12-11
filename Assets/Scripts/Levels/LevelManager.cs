using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager Obj { get; private set; }

    public Level[] levels;

    private int levelIndex = 0;

	// Use this for initialization
	void Awake() {
		if (Obj != null) {
            Debug.Log("Two level managers!  Destroying second");
            Destroy(this);
            return;
        }

        Obj = this;

        DontDestroyOnLoad(this.gameObject);
        LoadNextLevel();
	}
	
	public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Instantiate(levels[levelIndex]);
        levelIndex++;
    }
}
