using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLauncher : MonoBehaviour {

	public Globals.FactionNames PlayerFaction; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LaunchGame()
	{
		SceneManager.LoadScene("Scenes/main");
	}
}
