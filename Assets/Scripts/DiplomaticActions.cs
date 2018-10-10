using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Events;

public class DiplomaticActions
{
	private Button _declareWarButton;
	private Button _offerPeaceButton;
	private Button _offerAllianceButton;
	private Button _askToJoinFederation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void DeclareWarToFaction(Globals.FactionNames factionId)
	{
		EventManager.TriggerEvent<DiplomaticStatusData>(EventNames.DiplomaticStatusChange, new DiplomaticStatusData() { Source = Globals.PlayerFaction, Target = factionId, DiplomaticStatus = RelationStatus.War });
	}

	private void OfferPeaceToFaction(Globals.FactionNames factionId)
	{
		EventManager.TriggerEvent<DiplomaticStatusData>(EventNames.DiplomaticStatusChange, new DiplomaticStatusData() { Source = Globals.PlayerFaction, Target = factionId, DiplomaticStatus = RelationStatus.Peace });
	}

	private void OfferAllianceToFaction(Globals.FactionNames factionId)
	{
		EventManager.TriggerEvent<DiplomaticStatusData>(EventNames.DiplomaticStatusChange, new DiplomaticStatusData() { Source = Globals.PlayerFaction, Target = factionId, DiplomaticStatus = RelationStatus.Ally });
	}

	private void AskToJoinFederation(Globals.FactionNames factionId)
	{
		EventManager.TriggerEvent<DiplomaticStatusData>(EventNames.DiplomaticStatusChange, new DiplomaticStatusData() { Source = Globals.PlayerFaction, Target = factionId, DiplomaticStatus = RelationStatus.Federation });
	}
}
