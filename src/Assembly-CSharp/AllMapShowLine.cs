using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200017E RID: 382
[ExecuteInEditMode]
public class AllMapShowLine : MonoBehaviour
{
	// Token: 0x06001046 RID: 4166 RVA: 0x0005FAD8 File Offset: 0x0005DCD8
	private void Start()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			this.maps.Add(base.transform.GetChild(i).GetComponent<MapComponent>());
		}
	}

	// Token: 0x06001047 RID: 4167 RVA: 0x0005FB18 File Offset: 0x0005DD18
	private void Update()
	{
		foreach (MapComponent mapComponent in this.maps)
		{
			mapComponent.showDebugLine();
		}
	}

	// Token: 0x04000BD4 RID: 3028
	private List<MapComponent> maps = new List<MapComponent>();
}
