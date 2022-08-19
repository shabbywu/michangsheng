using System;
using UnityEngine;

// Token: 0x020000CA RID: 202
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Twirl")]
public class TwirlEffect : ImageEffectBase
{
	// Token: 0x06000AE8 RID: 2792 RVA: 0x00041EA3 File Offset: 0x000400A3
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		ImageEffects.RenderDistortion(base.material, source, destination, this.angle, this.center, this.radius);
	}

	// Token: 0x040006F4 RID: 1780
	public Vector2 radius = new Vector2(0.3f, 0.3f);

	// Token: 0x040006F5 RID: 1781
	public float angle = 50f;

	// Token: 0x040006F6 RID: 1782
	public Vector2 center = new Vector2(0.5f, 0.5f);
}
