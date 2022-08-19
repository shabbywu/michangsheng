using System;
using UnityEngine;

// Token: 0x020000BF RID: 191
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Correction (Ramp)")]
public class ColorCorrectionEffect : ImageEffectBase
{
	// Token: 0x06000AB2 RID: 2738 RVA: 0x00040B6B File Offset: 0x0003ED6B
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040006B6 RID: 1718
	public Texture textureRamp;
}
