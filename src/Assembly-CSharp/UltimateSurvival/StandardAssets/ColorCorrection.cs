using System;
using UnityEngine;

namespace UltimateSurvival.StandardAssets
{
	// Token: 0x0200062C RID: 1580
	[ExecuteInEditMode]
	[AddComponentMenu("Ultimate Survival/Add/Image Effects/Color Correction (Curves, Saturation)")]
	public class ColorCorrection : ImageEffectBase
	{
		// Token: 0x06003217 RID: 12823 RVA: 0x00163303 File Offset: 0x00161503
		private new void Start()
		{
			base.Start();
			this.updateTexturesOnStartup = true;
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x00004095 File Offset: 0x00002295
		private void Awake()
		{
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x00163314 File Offset: 0x00161514
		public override bool CheckResources()
		{
			base.CheckSupport(this.mode == ColorCorrection.ColorCorrectionMode.Advanced);
			this.ccMaterial = base.CheckShaderAndCreateMaterial(this.simpleColorCorrectionCurvesShader, this.ccMaterial);
			this.ccDepthMaterial = base.CheckShaderAndCreateMaterial(this.colorCorrectionCurvesShader, this.ccDepthMaterial);
			this.selectiveCcMaterial = base.CheckShaderAndCreateMaterial(this.colorCorrectionSelectiveShader, this.selectiveCcMaterial);
			if (!this.rgbChannelTex)
			{
				this.rgbChannelTex = new Texture2D(256, 4, 5, false, true);
			}
			if (!this.rgbDepthChannelTex)
			{
				this.rgbDepthChannelTex = new Texture2D(256, 4, 5, false, true);
			}
			if (!this.zCurveTex)
			{
				this.zCurveTex = new Texture2D(256, 1, 5, false, true);
			}
			this.rgbChannelTex.hideFlags = 52;
			this.rgbDepthChannelTex.hideFlags = 52;
			this.zCurveTex.hideFlags = 52;
			this.rgbChannelTex.wrapMode = 1;
			this.rgbDepthChannelTex.wrapMode = 1;
			this.zCurveTex.wrapMode = 1;
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x0016343C File Offset: 0x0016163C
		public void UpdateParameters()
		{
			this.CheckResources();
			if (this.redChannel != null && this.greenChannel != null && this.blueChannel != null)
			{
				for (float num = 0f; num <= 1f; num += 0.003921569f)
				{
					float num2 = Mathf.Clamp(this.redChannel.Evaluate(num), 0f, 1f);
					float num3 = Mathf.Clamp(this.greenChannel.Evaluate(num), 0f, 1f);
					float num4 = Mathf.Clamp(this.blueChannel.Evaluate(num), 0f, 1f);
					this.rgbChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 0, new Color(num2, num2, num2));
					this.rgbChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 1, new Color(num3, num3, num3));
					this.rgbChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 2, new Color(num4, num4, num4));
					float num5 = Mathf.Clamp(this.zCurve.Evaluate(num), 0f, 1f);
					this.zCurveTex.SetPixel((int)Mathf.Floor(num * 255f), 0, new Color(num5, num5, num5));
					num2 = Mathf.Clamp(this.depthRedChannel.Evaluate(num), 0f, 1f);
					num3 = Mathf.Clamp(this.depthGreenChannel.Evaluate(num), 0f, 1f);
					num4 = Mathf.Clamp(this.depthBlueChannel.Evaluate(num), 0f, 1f);
					this.rgbDepthChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 0, new Color(num2, num2, num2));
					this.rgbDepthChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 1, new Color(num3, num3, num3));
					this.rgbDepthChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 2, new Color(num4, num4, num4));
				}
				this.rgbChannelTex.Apply();
				this.rgbDepthChannelTex.Apply();
				this.zCurveTex.Apply();
			}
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x0016365F File Offset: 0x0016185F
		private void UpdateTextures()
		{
			this.UpdateParameters();
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x00163668 File Offset: 0x00161868
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.updateTexturesOnStartup)
			{
				this.UpdateParameters();
				this.updateTexturesOnStartup = false;
			}
			if (this.useDepthCorrection)
			{
				base.GetComponent<Camera>().depthTextureMode |= 1;
			}
			RenderTexture renderTexture = destination;
			if (this.selectiveCc)
			{
				renderTexture = RenderTexture.GetTemporary(source.width, source.height);
			}
			if (this.useDepthCorrection)
			{
				this.ccDepthMaterial.SetTexture("_RgbTex", this.rgbChannelTex);
				this.ccDepthMaterial.SetTexture("_ZCurve", this.zCurveTex);
				this.ccDepthMaterial.SetTexture("_RgbDepthTex", this.rgbDepthChannelTex);
				this.ccDepthMaterial.SetFloat("_Saturation", this.saturation);
				Graphics.Blit(source, renderTexture, this.ccDepthMaterial);
			}
			else
			{
				this.ccMaterial.SetTexture("_RgbTex", this.rgbChannelTex);
				this.ccMaterial.SetFloat("_Saturation", this.saturation);
				Graphics.Blit(source, renderTexture, this.ccMaterial);
			}
			if (this.selectiveCc)
			{
				this.selectiveCcMaterial.SetColor("selColor", this.selectiveFromColor);
				this.selectiveCcMaterial.SetColor("targetColor", this.selectiveToColor);
				Graphics.Blit(renderTexture, destination, this.selectiveCcMaterial);
				RenderTexture.ReleaseTemporary(renderTexture);
			}
		}

		// Token: 0x04002C93 RID: 11411
		public AnimationCurve redChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04002C94 RID: 11412
		public AnimationCurve greenChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04002C95 RID: 11413
		public AnimationCurve blueChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04002C96 RID: 11414
		public bool useDepthCorrection;

		// Token: 0x04002C97 RID: 11415
		public AnimationCurve zCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04002C98 RID: 11416
		public AnimationCurve depthRedChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04002C99 RID: 11417
		public AnimationCurve depthGreenChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04002C9A RID: 11418
		public AnimationCurve depthBlueChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04002C9B RID: 11419
		private Material ccMaterial;

		// Token: 0x04002C9C RID: 11420
		private Material ccDepthMaterial;

		// Token: 0x04002C9D RID: 11421
		private Material selectiveCcMaterial;

		// Token: 0x04002C9E RID: 11422
		private Texture2D rgbChannelTex;

		// Token: 0x04002C9F RID: 11423
		private Texture2D rgbDepthChannelTex;

		// Token: 0x04002CA0 RID: 11424
		private Texture2D zCurveTex;

		// Token: 0x04002CA1 RID: 11425
		public float saturation = 1f;

		// Token: 0x04002CA2 RID: 11426
		public bool selectiveCc;

		// Token: 0x04002CA3 RID: 11427
		public Color selectiveFromColor = Color.white;

		// Token: 0x04002CA4 RID: 11428
		public Color selectiveToColor = Color.white;

		// Token: 0x04002CA5 RID: 11429
		public ColorCorrection.ColorCorrectionMode mode;

		// Token: 0x04002CA6 RID: 11430
		public bool updateTextures = true;

		// Token: 0x04002CA7 RID: 11431
		public Shader colorCorrectionCurvesShader;

		// Token: 0x04002CA8 RID: 11432
		public Shader simpleColorCorrectionCurvesShader;

		// Token: 0x04002CA9 RID: 11433
		public Shader colorCorrectionSelectiveShader;

		// Token: 0x04002CAA RID: 11434
		private bool updateTexturesOnStartup = true;

		// Token: 0x020014D7 RID: 5335
		public enum ColorCorrectionMode
		{
			// Token: 0x04006D82 RID: 28034
			Simple,
			// Token: 0x04006D83 RID: 28035
			Advanced
		}
	}
}
