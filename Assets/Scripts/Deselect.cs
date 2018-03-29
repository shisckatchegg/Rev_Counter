using UnityEngine;
using UnityEngine.UI;

public class Deselect : MonoBehaviour
{
    private Text _selectionText;
	private Text _amountOfSpiesPresent;

	private GameObject _recruitmentButton;
	private GameObject _assasinateButton;
	private GameObject _propagandaButton;
	private GameObject _spySelectionGameObject;

	private void Awake()
	{
		_recruitmentButton = GameObject.Find("Recruitment");

		_selectionText = GameObject.Find("SelectedPopulationNodeText").GetComponent<Text>();

		_assasinateButton = GameObject.Find("AssassinateButton");
		_propagandaButton = GameObject.Find("PropagandaButton");

		_spySelectionGameObject = GameObject.Find("SpyCounter");
		_amountOfSpiesPresent = GameObject.Find("AmountOfSpiesPresent").GetComponent<Text>();
	}

	// Use this for initialization
	void Start () {
        
    }

    // Update is called once per frame
    void Update () {
  
    }


	public void OnMouseDown()
	{
		_selectionText.text = "";
		_amountOfSpiesPresent.text = "";

		_spySelectionGameObject.SetActive(false);
		_propagandaButton.SetActive(false);
		_recruitmentButton.SetActive(false);
		_assasinateButton.SetActive(false);
	}
}
