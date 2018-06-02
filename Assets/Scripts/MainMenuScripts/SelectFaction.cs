using UnityEngine;

public class SelectFaction : MonoBehaviour
{

	private GameObject _singlePlayerGameButton;
	private GameObject _loadGameButton;
	private GameObject _quitGameButton;

	private GameObject _sovietFactionButton;
	private GameObject _tsaristFactionButton;
	private GameObject _socRevFactionButton;


	private GameObject _mapsizeDropDown;


	private void Awake()
	{
		_singlePlayerGameButton = GameObject.Find("SinglePlayerButton");
		_loadGameButton = GameObject.Find("LoadGameButton");
		_quitGameButton = GameObject.Find("QuitButton");

		_sovietFactionButton = GameObject.Find("SovietFactionButton");
		_tsaristFactionButton = GameObject.Find("TsaristFactionButton");
		_socRevFactionButton = GameObject.Find("SocialistRevolutionariesFactionButton");

		_mapsizeDropDown = GameObject.Find("Map Size Dropdown");

	}
	// Use this for initialization
	void Start()
	{
		_sovietFactionButton.SetActive(false);
		_tsaristFactionButton.SetActive(false);
		_socRevFactionButton.SetActive(false);
		_mapsizeDropDown.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OpenFactionSelectionScreen()
	{
		HideButtons();
		ShowFactions();
	}

	private void HideButtons()
	{
		_singlePlayerGameButton.SetActive(false);
		_loadGameButton.SetActive(false);
		_quitGameButton.SetActive(false);
		_socRevFactionButton.SetActive(false);
		_mapsizeDropDown.SetActive(false);
	}

	private void ShowFactions()
	{
		_sovietFactionButton.SetActive(true);
		_tsaristFactionButton.SetActive(true);
		_socRevFactionButton.SetActive(true);
		_mapsizeDropDown.SetActive(true);
	}
}
