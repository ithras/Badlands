using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class UIButtonLevelLoad : MonoBehaviour {

	[Scene] [SerializeField] private string LevelToLoad = string.Empty;
	
	public void loadLevel() {
		//Load the level from LevelToLoad
		SceneManager.LoadScene(LevelToLoad);
	}
}
