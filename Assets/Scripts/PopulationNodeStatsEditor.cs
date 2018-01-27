using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PopulationNodeStats))]
public class PopulationNodeStatsEditor : Editor
{
	//SerializedProperty stats;

	//private void OnEnable()
	//{
	//	serializedObject = new SerializedObject(target);
	//}

	public override void OnInspectorGUI()
	{
		var populationStats = target as PopulationNodeStats;
		if(populationStats != null)
		{
			populationStats.Stats.Population = EditorGUILayout.IntField("Population", populationStats.Stats.Population);
			populationStats.Stats.PopulationNodeName = EditorGUILayout.TextField("Population node name", populationStats.Stats.PopulationNodeName);
			populationStats.Stats.Control = EditorGUILayout.IntField("Faction in control", populationStats.Stats.Control);
			//TODO: adding editor support to faction support field
			//populationStats.Stats.FactionsSupport = EditorGUILayout.("Faction support", populationStats.Stats.Population);
			populationStats.Stats.Type = (CityType) EditorGUILayout.EnumPopup("Population node type", populationStats.Stats.Type);
			populationStats.Stats.RecruitmentSlots = EditorGUILayout.IntField("Population node recruitment slots", populationStats.Stats.RecruitmentSlots);
		}

		if(GUI.changed)
		{
			EditorUtility.SetDirty(populationStats);
		}

		serializedObject.Update();
		serializedObject.ApplyModifiedProperties();
	}
}
