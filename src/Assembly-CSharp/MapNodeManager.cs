using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000191 RID: 401
public class MapNodeManager : MonoBehaviour
{
	// Token: 0x0600112D RID: 4397 RVA: 0x0006758F File Offset: 0x0006578F
	private void Awake()
	{
		this.MapComponentList = new List<MapComponent>();
		MapNodeManager.inst = this;
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x000675A4 File Offset: 0x000657A4
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

	// Token: 0x0600112F RID: 4399 RVA: 0x0006762D File Offset: 0x0006582D
	private void OnDestroy()
	{
		MapNodeManager.inst = null;
	}

	// Token: 0x04000C49 RID: 3145
	public List<MapComponent> MapComponentList = new List<MapComponent>();

	// Token: 0x04000C4A RID: 3146
	public static MapNodeManager inst;

	// Token: 0x04000C4B RID: 3147
	public GameObject MapNodeParent;
}
