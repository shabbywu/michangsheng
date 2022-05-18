using System;
using UnityEngine;

// Token: 0x02000225 RID: 549
[RequireComponent(typeof(GUIText))]
public class GUIText_Color : MonoBehaviour
{
	// Token: 0x06001107 RID: 4359 RVA: 0x000109A7 File Offset: 0x0000EBA7
	private void Awake()
	{
		base.GetComponent<GUIText>().material.color = this.labelColor;
	}

	// Token: 0x04000DB2 RID: 3506
	public Color labelColor;
}
