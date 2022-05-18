using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000281 RID: 641
public class MapNodeManager : MonoBehaviour
{
	// Token: 0x060013B0 RID: 5040 RVA: 0x000126B0 File Offset: 0x000108B0
	private void Awake()
	{
		this.MapComponentList = new List<MapComponent>();
		MapNodeManager.inst = this;
	}

	// Token: 0x060013B1 RID: 5041 RVA: 0x000B5BB4 File Offset: 0x000B3DB4
	public void UpdateAllNode()
	{
		int childCount = this.MapNodeParent.transform.childCount;
		if (this.MapComponentList.Count == childCount)
		{
			for (int i = 0; i < this.MapComponentList.Count; i++)
			{
				this.MapComponentList[i].updateSedNode();
			}
			return;
		}
		for (int j = 0; j < childCount; j++)
		{
			MapComponent component = this.MapNodeParent.transform.GetChild(j).GetComponent<MapComponent>();
			this.MapComponentList.Add(component);
			component.updateSedNode();
		}
	}

	// Token: 0x060013B2 RID: 5042 RVA: 0x000126C3 File Offset: 0x000108C3
	private void OnDestroy()
	{
		MapNodeManager.inst = null;
	}

	// Token: 0x04000F49 RID: 3913
	public List<MapComponent> MapComponentList = new List<MapComponent>();

	// Token: 0x04000F4A RID: 3914
	public static MapNodeManager inst;

	// Token: 0x04000F4B RID: 3915
	public GameObject MapNodeParent;
}
