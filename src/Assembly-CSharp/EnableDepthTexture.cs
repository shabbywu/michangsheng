using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class EnableDepthTexture : MonoBehaviour
{
	// Token: 0x06000436 RID: 1078 RVA: 0x00007BAD File Offset: 0x00005DAD
	private void OnEnable()
	{
		base.GetComponent<Camera>().depthTextureMode = 1;
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x00007BBB File Offset: 0x00005DBB
	private void OnDisable()
	{
		base.GetComponent<Camera>().depthTextureMode = 0;
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x0400026D RID: 621
	public bool EnableInEditor = true;
}
