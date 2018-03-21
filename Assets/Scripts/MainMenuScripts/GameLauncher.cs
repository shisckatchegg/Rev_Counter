using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLauncher : MonoBehaviour {

	public Globals.FactionNames ButtonFactionId; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LaunchGame()
	{
		Globals.PlayerFaction = ButtonFactionId;
		SceneManager.LoadScene("Scenes/main");
	}
}
