using System;
using UnityEngine;

// Token: 0x020000BC RID: 188
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Desaturate")]
public class DesaturateEffect : ImageEffectBase
{
	// Token: 0x06000AA6 RID: 2726 RVA: 0x000408B4 File Offset: 0x0003EAB4
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		base.material.SetFloat("_Desat", this.desaturateAmount);
		base.material.SetVector("_RampOffset", new Vector4(this.rampOffsetR, this.rampOffsetG, this.rampOffsetB, 0f));
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040006AC RID: 1708
	public float desaturateAmount;

	// Token: 0x040006AD RID: 1709
	public Texture textureRamp;

	// Token: 0x040006AE RID: 1710
	public float rampOffsetR;

	// Token: 0x040006AF RID: 1711
	public float rampOffsetG;

	// Token: 0x040006B0 RID: 1712
	public float rampOffsetB;
}
