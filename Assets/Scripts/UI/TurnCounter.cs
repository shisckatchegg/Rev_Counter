using UnityEngine;
using UnityEngine.UI;

public class TurnCounter : MonoBehaviour {

	private Text _turDisplay;
	private GameControl _gameControl;
	// Use this for initialization
	void Start ()
	{
		_gameControl = FindObjectOfType<GameControl>();
		_turDisplay = GetComponent<Text>();
	}
	
	//TODO: massively inefficient
	// Update is called once per frame
	void Update () {
		_turDisplay.text = "Turn: " + _gameControl.TurnNumber.ToString();
	}
}
