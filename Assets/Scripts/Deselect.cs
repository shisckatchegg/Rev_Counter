using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deselect : MonoBehaviour
{
    private Text _selectionText;

    // Use this for initialization
    void Start () {
        GameObject.Find("Recruitment").transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update () {
  
    }


    public void OnMouseDown()
    {        
        _selectionText = GameObject.Find("SelectedPopulationNodeText").GetComponent<Text>();

        if (_selectionText.text != "")
        {
            _selectionText.text = "";

            GameObject.Find("Recruitment").transform.localScale = new Vector3(0, 0, 0);
        }

    }
}
