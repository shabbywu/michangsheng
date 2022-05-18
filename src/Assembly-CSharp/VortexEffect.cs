using System;
using UnityEngine;

// Token: 0x0200013C RID: 316
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Vortex")]
public class VortexEffect : ImageEffectBase
{
	// Token: 0x06000BCD RID: 3021 RVA: 0x0000DE61 File Offset: 0x0000C061
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		ImageEffects.RenderDistortion(base.material, source, destination, this.angle, this.center, this.radius);
	}

	// Token: 0x040008A2 RID: 2210
	public Vector2 radius = new Vector2(0.4f, 0.4f);

	// Token: 0x040008A3 RID: 2211
	public float angle = 50f;

	// Token: 0x040008A4 RID: 2212
	public Vector2 center = new Vector2(0.5f, 0.5f);
}
