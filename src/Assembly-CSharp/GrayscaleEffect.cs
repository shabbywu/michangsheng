using System;
using UnityEngine;

// Token: 0x02000133 RID: 307
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Grayscale")]
public class GrayscaleEffect : ImageEffectBase
{
	// Token: 0x06000BAD RID: 2989 RVA: 0x0000DC68 File Offset: 0x0000BE68
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		base.material.SetFloat("_RampOffset", this.rampOffset);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000876 RID: 2166
	public Texture textureRamp;

	// Token: 0x04000877 RID: 2167
	public float rampOffset;
}
