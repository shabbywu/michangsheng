using System;
using UnityEngine;

namespace UltimateSurvival.StandardAssets
{
	// Token: 0x02000919 RID: 2329
	[ExecuteInEditMode]
	[AddComponentMenu("Ultimate Survival/Add/Image Effects/Color Correction (Curves, Saturation)")]
	public class ColorCorrection : ImageEffectBase
	{
		// Token: 0x06003B51 RID: 15185 RVA: 0x0002AF24 File Offset: 0x00029124
		private new void Start()
		{
			base.Start();
			this.updateTexturesOnStartup = true;
		}

		// Token: 0x06003B52 RID: 15186 RVA: 0x000042DD File Offset: 0x000024DD
		private void Awake()
		{
		}

		// Token: 0x06003B53 RID: 15187 RVA: 0x001ACBC8 File Offset: 0x001AADC8
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

		// Token: 0x06003B54 RID: 15188 RVA: 0x001ACCF0 File Offset: 0x001AAEF0
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

		// Token: 0x06003B55 RID: 15189 RVA: 0x0002AF33 File Offset: 0x00029133
		private void UpdateTextures()
		{
			this.UpdateParameters();
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x001ACF14 File Offset: 0x001AB114
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

		// Token: 0x040035D2 RID: 13778
		public AnimationCurve redChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x040035D3 RID: 13779
		public AnimationCurve greenChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x040035D4 RID: 13780
		public AnimationCurve blueChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x040035D5 RID: 13781
		public bool useDepthCorrection;

		// Token: 0x040035D6 RID: 13782
		public AnimationCurve zCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x040035D7 RID: 13783
		public AnimationCurve depthRedChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x040035D8 RID: 13784
		public AnimationCurve depthGreenChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x040035D9 RID: 13785
		public AnimationCurve depthBlueChannel = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x040035DA RID: 13786
		private Material ccMaterial;

		// Token: 0x040035DB RID: 13787
		private Material ccDepthMaterial;

		// Token: 0x040035DC RID: 13788
		private Material selectiveCcMaterial;

		// Token: 0x040035DD RID: 13789
		private Texture2D rgbChannelTex;

		// Token: 0x040035DE RID: 13790
		private Texture2D rgbDepthChannelTex;

		// Token: 0x040035DF RID: 13791
		private Texture2D zCurveTex;

		// Token: 0x040035E0 RID: 13792
		public float saturation = 1f;

		// Token: 0x040035E1 RID: 13793
		public bool selectiveCc;

		// Token: 0x040035E2 RID: 13794
		public Color selectiveFromColor = Color.white;

		// Token: 0x040035E3 RID: 13795
		public Color selectiveToColor = Color.white;

		// Token: 0x040035E4 RID: 13796
		public ColorCorrection.ColorCorrectionMode mode;

		// Token: 0x040035E5 RID: 13797
		public bool updateTextures = true;

		// Token: 0x040035E6 RID: 13798
		public Shader colorCorrectionCurvesShader;

		// Token: 0x040035E7 RID: 13799
		public Shader simpleColorCorrectionCurvesShader;

		// Token: 0x040035E8 RID: 13800
		public Shader colorCorrectionSelectiveShader;

		// Token: 0x040035E9 RID: 13801
		private bool updateTexturesOnStartup = true;

		// Token: 0x0200091A RID: 2330
		public enum ColorCorrectionMode
		{
			// Token: 0x040035EB RID: 13803
			Simple,
			// Token: 0x040035EC RID: 13804
			Advanced
		}
	}
}
