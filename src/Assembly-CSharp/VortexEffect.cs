using System;
using UnityEngine;

// Token: 0x020000CB RID: 203
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Vortex")]
public class VortexEffect : ImageEffectBase
{
	// Token: 0x06000AEA RID: 2794 RVA: 0x00041F01 File Offset: 0x00040101
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		ImageEffects.RenderDistortion(base.material, source, destination, this.angle, this.center, this.radius);
	}

	// Token: 0x040006F7 RID: 1783
	public Vector2 radius = new Vector2(0.4f, 0.4f);

	// Token: 0x040006F8 RID: 1784
	public float angle = 50f;

	// Token: 0x040006F9 RID: 1785
	public Vector2 center = new Vector2(0.5f, 0.5f);
}
