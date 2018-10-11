using Events;

public class DiplomaticActions
{
	public void DeclareWarToFaction(Globals.FactionNames factionId)
	{
		EventManager.TriggerEvent<DiplomaticStatusData>(EventNames.DiplomaticStatusChange, new DiplomaticStatusData() { Source = Globals.PlayerFaction, Target = factionId, DiplomaticStatus = RelationStatus.War });
	}

	public void OfferPeaceToFaction(Globals.FactionNames factionId)
	{
		EventManager.TriggerEvent<DiplomaticStatusData>(EventNames.DiplomaticStatusChange, new DiplomaticStatusData() { Source = Globals.PlayerFaction, Target = factionId, DiplomaticStatus = RelationStatus.Peace });
	}

	public void OfferAllianceToFaction(Globals.FactionNames factionId)
	{
		EventManager.TriggerEvent<DiplomaticStatusData>(EventNames.DiplomaticStatusChange, new DiplomaticStatusData() { Source = Globals.PlayerFaction, Target = factionId, DiplomaticStatus = RelationStatus.Ally });
	}

	public void AskToJoinFederation(Globals.FactionNames factionId)
	{
		EventManager.TriggerEvent<DiplomaticStatusData>(EventNames.DiplomaticStatusChange, new DiplomaticStatusData() { Source = Globals.PlayerFaction, Target = factionId, DiplomaticStatus = RelationStatus.Federation });
	}
}
