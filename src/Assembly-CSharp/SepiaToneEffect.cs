using System;
using UnityEngine;

// Token: 0x020000C8 RID: 200
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Sepia Tone")]
public class SepiaToneEffect : ImageEffectBase
{
	// Token: 0x06000ADE RID: 2782 RVA: 0x00041A1F File Offset: 0x0003FC1F
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, base.material);
	}
}
