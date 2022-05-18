using System;
using UnityEngine;

// Token: 0x0200012C RID: 300
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Desaturate")]
public class DesaturateEffect : ImageEffectBase
{
	// Token: 0x06000B89 RID: 2953 RVA: 0x00092CE0 File Offset: 0x00090EE0
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		base.material.SetFloat("_Desat", this.desaturateAmount);
		base.material.SetVector("_RampOffset", new Vector4(this.rampOffsetR, this.rampOffsetG, this.rampOffsetB, 0f));
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000853 RID: 2131
	public float desaturateAmount;

	// Token: 0x04000854 RID: 2132
	public Texture textureRamp;

	// Token: 0x04000855 RID: 2133
	public float rampOffsetR;

	// Token: 0x04000856 RID: 2134
	public float rampOffsetG;

	// Token: 0x04000857 RID: 2135
	public float rampOffsetB;
}
