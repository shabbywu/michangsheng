using System.Collections.Generic;
using UnityEngine;

public class MapNodeManager : MonoBehaviour
{
	public List<MapComponent> MapComponentList = new List<MapComponent>();

	public static MapNodeManager inst;

	public GameObject MapNodeParent;

	private void Awake()
	{
		MapComponentList = new List<MapComponent>();
		inst = this;
	}

	public void UpdateAllNode()
	{
		int childCount = MapNodeParent.transform.childCount;
		if (MapComponentList.Count == childCount)
		{
			for (int i = 0; i < MapComponentList.Count; i++)
			{
				MapComponentList[i].updateSedNode();
			}
			return;
		}
		for (int j = 0; j < childCount; j++)
		{
			MapComponent component = ((Component)MapNodeParent.transform.GetChild(j)).GetComponent<MapComponent>();
			MapComponentList.Add(component);
			component.updateSedNode();
		}
	}

	private void OnDestroy()
	{
		inst = null;
	}
}
