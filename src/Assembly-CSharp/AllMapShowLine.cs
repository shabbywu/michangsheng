using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AllMapShowLine : MonoBehaviour
{
	private List<MapComponent> maps = new List<MapComponent>();

	private void Start()
	{
		for (int i = 0; i < ((Component)this).transform.childCount; i++)
		{
			maps.Add(((Component)((Component)this).transform.GetChild(i)).GetComponent<MapComponent>());
		}
	}

	private void Update()
	{
		foreach (MapComponent map in maps)
		{
			map.showDebugLine();
		}
	}
}
