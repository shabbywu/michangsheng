using System;
using UnityEngine;

// Token: 0x0200014F RID: 335
[RequireComponent(typeof(GUIText))]
public class GUIText_Color : MonoBehaviour
{
	// Token: 0x06000EE1 RID: 3809 RVA: 0x0005A6C8 File Offset: 0x000588C8
	private void Awake()
	{
		base.GetComponent<GUIText>().material.color = this.labelColor;
	}

	// Token: 0x04000B14 RID: 2836
	public Color labelColor;
}
