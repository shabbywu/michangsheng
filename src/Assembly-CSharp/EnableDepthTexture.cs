using System;
using UnityEngine;

// Token: 0x0200002E RID: 46
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class EnableDepthTexture : MonoBehaviour
{
	// Token: 0x060003EE RID: 1006 RVA: 0x00015D38 File Offset: 0x00013F38
	private void OnEnable()
	{
		base.GetComponent<Camera>().depthTextureMode = 1;
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x00015D46 File Offset: 0x00013F46
	private void OnDisable()
	{
		base.GetComponent<Camera>().depthTextureMode = 0;
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000227 RID: 551
	public bool EnableInEditor = true;
}
