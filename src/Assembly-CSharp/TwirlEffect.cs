using System;
using UnityEngine;

// Token: 0x0200013B RID: 315
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Twirl")]
public class TwirlEffect : ImageEffectBase
{
	// Token: 0x06000BCB RID: 3019 RVA: 0x0000DE03 File Offset: 0x0000C003
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		ImageEffects.RenderDistortion(base.material, source, destination, this.angle, this.center, this.radius);
	}

	// Token: 0x0400089F RID: 2207
	public Vector2 radius = new Vector2(0.3f, 0.3f);

	// Token: 0x040008A0 RID: 2208
	public float angle = 50f;

	// Token: 0x040008A1 RID: 2209
	public Vector2 center = new Vector2(0.5f, 0.5f);
}
