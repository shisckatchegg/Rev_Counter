using UnityEditor;

[CustomEditor(typeof(PopulationNodeStats))]
public class PopulationNodeStatsEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
	}
}
