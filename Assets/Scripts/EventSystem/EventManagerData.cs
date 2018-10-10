namespace Events
{
	public struct PopulationNodeSelectedEventData
	{
		public int Control;
	}

	public struct DiplomaticStatusData
	{
		public Globals.FactionNames Source;
		public Globals.FactionNames Target;

		public RelationStatus DiplomaticStatus;
	}
}