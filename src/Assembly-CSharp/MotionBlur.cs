using System;
using UnityEngine;

// Token: 0x020000C6 RID: 198
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Motion Blur (Color Accumulation)")]
[RequireComponent(typeof(Camera))]
public class MotionBlur : ImageEffectBase
{
	// Token: 0x06000AD4 RID: 2772 RVA: 0x000414DE File Offset: 0x0003F6DE
	protected override void Start()
	{
		if (!SystemInfo.supportsRenderTextures)
		{
			base.enabled = false;
			return;
		}
		base.Start();
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x000414F5 File Offset: 0x0003F6F5
	protected override void OnDisable()
	{
		base.OnDisable();
		Object.DestroyImmediate(this.accumTexture);
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x00041508 File Offset: 0x0003F708
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.accumTexture == null || this.accumTexture.width != source.width || this.accumTexture.height != source.height)
		{
			Object.DestroyImmediate(this.accumTexture);
			this.accumTexture = new RenderTexture(source.width, source.height, 0);
			this.accumTexture.hideFlags = 61;
			Graphics.Blit(source, this.accumTexture);
		}
		if (this.extraBlur)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
			Graphics.Blit(this.accumTexture, temporary);
			Graphics.Blit(temporary, this.accumTexture);
			RenderTexture.ReleaseTemporary(temporary);
		}
		this.blurAmount = Mathf.Clamp(this.blurAmount, 0f, 0.92f);
		base.material.SetTexture("_MainTex", this.accumTexture);
		base.material.SetFloat("_AccumOrig", 1f - this.blurAmount);
		Graphics.Blit(source, this.accumTexture, base.material);
		Graphics.Blit(this.accumTexture, destination);
	}

	// Token: 0x040006D3 RID: 1747
	public float blurAmount = 0.8f;

	// Token: 0x040006D4 RID: 1748
	public bool extraBlur;

	// Token: 0x040006D5 RID: 1749
	private RenderTexture accumTexture;
}
