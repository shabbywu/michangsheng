using System;
using UnityEngine;

// Token: 0x020000C3 RID: 195
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Grayscale")]
public class GrayscaleEffect : ImageEffectBase
{
	// Token: 0x06000ACA RID: 2762 RVA: 0x00041363 File Offset: 0x0003F563
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		base.material.SetFloat("_RampOffset", this.rampOffset);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040006CF RID: 1743
	public Texture textureRamp;

	// Token: 0x040006D0 RID: 1744
	public float rampOffset;
}
