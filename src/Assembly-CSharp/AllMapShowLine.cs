using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200025B RID: 603
[ExecuteInEditMode]
public class AllMapShowLine : MonoBehaviour
{
	// Token: 0x06001298 RID: 4760 RVA: 0x000AEAD4 File Offset: 0x000ACCD4
	private void Start()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			this.maps.Add(base.transform.GetChild(i).GetComponent<MapComponent>());
		}
	}

	// Token: 0x06001299 RID: 4761 RVA: 0x000AEB14 File Offset: 0x000ACD14
	private void Update()
	{
		foreach (MapComponent mapComponent in this.maps)
		{
			mapComponent.showDebugLine();
		}
	}

	// Token: 0x04000EA6 RID: 3750
	private List<MapComponent> maps = new List<MapComponent>();
}
