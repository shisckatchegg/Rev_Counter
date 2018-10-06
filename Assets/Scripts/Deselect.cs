using UnityEngine;
using UnityEngine.UI;

public class Deselect : MonoBehaviour
{
    private Text _selectionText;
	private Text _amountOfSpiesPresent;

	private GameObject _spyRecruitmentButton;
	private GameObject _assasinateButton;
	private GameObject _propagandaButton;
	private GameObject _spySelectionGameObject;

	private GameObject _soldierRecruitmentButton;
	private GameObject _soldierSelectionGameObject;
	private GameObject _repressButton;

	private GameObject _leftPanel;
	private GameObject _factionRelations;

	private void Awake()
	{
		_spyRecruitmentButton = GameObject.Find("Recruitment");

		_selectionText = GameObject.Find("SelectedPopulationNodeText").GetComponent<Text>();

		_assasinateButton = GameObject.Find("AssassinateButton");
		_propagandaButton = GameObject.Find("PropagandaButton");

		_spySelectionGameObject = GameObject.Find("SpyCounter");
		_amountOfSpiesPresent = GameObject.Find("AmountOfSpiesPresent").GetComponent<Text>();

		_soldierRecruitmentButton = GameObject.Find("SoldierRecruitment");
		_soldierSelectionGameObject = GameObject.Find("SoldierCounter");
		_repressButton = GameObject.Find("RepressButton");

		_leftPanel = GameObject.Find("LeftPanel");
		_factionRelations = GameObject.Find("FactionRelations");
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
		_spyRecruitmentButton.SetActive(false);
		_assasinateButton.SetActive(false);

		_soldierRecruitmentButton.SetActive(false);
		_soldierSelectionGameObject.SetActive(false);
		_repressButton.SetActive(false);

		_leftPanel.SetActive(false);
		_factionRelations.SetActive(false);
	}
}
