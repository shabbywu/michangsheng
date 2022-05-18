using System;
using UnityEngine;

namespace UltimateSurvival.StandardAssets
{
	// Token: 0x02000913 RID: 2323
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Ultimate Survival/Add/Image Effects/Bloom")]
	public class BloomEffect : ImageEffectBase
	{
		// Token: 0x06003B49 RID: 15177 RVA: 0x001AC108 File Offset: 0x001AA308
		public override bool CheckResources()
		{
			base.CheckSupport(false);
			this.screenBlend = base.CheckShaderAndCreateMaterial(this.screenBlendShader, this.screenBlend);
			this.lensFlareMaterial = base.CheckShaderAndCreateMaterial(this.lensFlareShader, this.lensFlareMaterial);
			this.blurAndFlaresMaterial = base.CheckShaderAndCreateMaterial(this.blurAndFlaresShader, this.blurAndFlaresMaterial);
			this.brightPassFilterMaterial = base.CheckShaderAndCreateMaterial(this.brightPassFilterShader, this.brightPassFilterMaterial);
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x001AC194 File Offset: 0x001AA394
		public void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			this.doHdr = false;
			if (this.hdr == BloomEffect.HDRBloomMode.Auto)
			{
				this.doHdr = (source.format == 2 && base.GetComponent<Camera>().allowHDR);
			}
			else
			{
				this.doHdr = (this.hdr == BloomEffect.HDRBloomMode.On);
			}
			this.doHdr = (this.doHdr && this.supportHDRTextures);
			BloomEffect.BloomScreenBlendMode bloomScreenBlendMode = this.screenBlendMode;
			if (this.doHdr)
			{
				bloomScreenBlendMode = BloomEffect.BloomScreenBlendMode.Add;
			}
			RenderTextureFormat renderTextureFormat = this.doHdr ? 2 : 7;
			int num = source.width / 2;
			int num2 = source.height / 2;
			int num3 = source.width / 4;
			int num4 = source.height / 4;
			float num5 = 1f * (float)source.width / (1f * (float)source.height);
			float num6 = 0.001953125f;
			RenderTexture temporary = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
			RenderTexture temporary2 = RenderTexture.GetTemporary(num, num2, 0, renderTextureFormat);
			if (this.quality > BloomEffect.BloomQuality.Cheap)
			{
				Graphics.Blit(source, temporary2, this.screenBlend, 2);
				RenderTexture temporary3 = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
				Graphics.Blit(temporary2, temporary3, this.screenBlend, 2);
				Graphics.Blit(temporary3, temporary, this.screenBlend, 6);
				RenderTexture.ReleaseTemporary(temporary3);
			}
			else
			{
				Graphics.Blit(source, temporary2);
				Graphics.Blit(temporary2, temporary, this.screenBlend, 6);
			}
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture renderTexture = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
			this.BrightFilter(this.bloomThreshold * this.bloomThresholdColor, temporary, renderTexture);
			if (this.bloomBlurIterations < 1)
			{
				this.bloomBlurIterations = 1;
			}
			else if (this.bloomBlurIterations > 10)
			{
				this.bloomBlurIterations = 10;
			}
			for (int i = 0; i < this.bloomBlurIterations; i++)
			{
				float num7 = (1f + (float)i * 0.25f) * this.sepBlurSpread;
				RenderTexture temporary4 = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
				this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(0f, num7 * num6, 0f, 0f));
				Graphics.Blit(renderTexture, temporary4, this.blurAndFlaresMaterial, 4);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary4;
				temporary4 = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
				this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num7 / num5 * num6, 0f, 0f, 0f));
				Graphics.Blit(renderTexture, temporary4, this.blurAndFlaresMaterial, 4);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary4;
				if (this.quality > BloomEffect.BloomQuality.Cheap)
				{
					if (i == 0)
					{
						Graphics.SetRenderTarget(temporary);
						GL.Clear(false, true, Color.black);
						Graphics.Blit(renderTexture, temporary);
					}
					else
					{
						temporary.MarkRestoreExpected();
						Graphics.Blit(renderTexture, temporary, this.screenBlend, 10);
					}
				}
			}
			if (this.quality > BloomEffect.BloomQuality.Cheap)
			{
				Graphics.SetRenderTarget(renderTexture);
				GL.Clear(false, true, Color.black);
				Graphics.Blit(temporary, renderTexture, this.screenBlend, 6);
			}
			if (this.lensflareIntensity > Mathf.Epsilon)
			{
				RenderTexture temporary5 = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
				if (this.lensflareMode == BloomEffect.LensFlareStyle.Ghosting)
				{
					this.BrightFilter(this.lensflareThreshold, renderTexture, temporary5);
					if (this.quality > BloomEffect.BloomQuality.Cheap)
					{
						this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(0f, 1.5f / (1f * (float)temporary.height), 0f, 0f));
						Graphics.SetRenderTarget(temporary);
						GL.Clear(false, true, Color.black);
						Graphics.Blit(temporary5, temporary, this.blurAndFlaresMaterial, 4);
						this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(1.5f / (1f * (float)temporary.width), 0f, 0f, 0f));
						Graphics.SetRenderTarget(temporary5);
						GL.Clear(false, true, Color.black);
						Graphics.Blit(temporary, temporary5, this.blurAndFlaresMaterial, 4);
					}
					this.Vignette(0.975f, temporary5, temporary5);
					this.BlendFlares(temporary5, renderTexture);
				}
				else
				{
					float num8 = 1f * Mathf.Cos(this.flareRotation);
					float num9 = 1f * Mathf.Sin(this.flareRotation);
					float num10 = this.hollyStretchWidth * 1f / num5 * num6;
					this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num8, num9, 0f, 0f));
					this.blurAndFlaresMaterial.SetVector("_Threshhold", new Vector4(this.lensflareThreshold, 1f, 0f, 0f));
					this.blurAndFlaresMaterial.SetVector("_TintColor", new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) * this.flareColorA.a * this.lensflareIntensity);
					this.blurAndFlaresMaterial.SetFloat("_Saturation", this.lensFlareSaturation);
					temporary.DiscardContents();
					Graphics.Blit(temporary5, temporary, this.blurAndFlaresMaterial, 2);
					temporary5.DiscardContents();
					Graphics.Blit(temporary, temporary5, this.blurAndFlaresMaterial, 3);
					this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num8 * num10, num9 * num10, 0f, 0f));
					this.blurAndFlaresMaterial.SetFloat("_StretchWidth", this.hollyStretchWidth);
					temporary.DiscardContents();
					Graphics.Blit(temporary5, temporary, this.blurAndFlaresMaterial, 1);
					this.blurAndFlaresMaterial.SetFloat("_StretchWidth", this.hollyStretchWidth * 2f);
					temporary5.DiscardContents();
					Graphics.Blit(temporary, temporary5, this.blurAndFlaresMaterial, 1);
					this.blurAndFlaresMaterial.SetFloat("_StretchWidth", this.hollyStretchWidth * 4f);
					temporary.DiscardContents();
					Graphics.Blit(temporary5, temporary, this.blurAndFlaresMaterial, 1);
					for (int j = 0; j < this.hollywoodFlareBlurIterations; j++)
					{
						num10 = this.hollyStretchWidth * 2f / num5 * num6;
						this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num10 * num8, num10 * num9, 0f, 0f));
						temporary5.DiscardContents();
						Graphics.Blit(temporary, temporary5, this.blurAndFlaresMaterial, 4);
						this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num10 * num8, num10 * num9, 0f, 0f));
						temporary.DiscardContents();
						Graphics.Blit(temporary5, temporary, this.blurAndFlaresMaterial, 4);
					}
					if (this.lensflareMode == BloomEffect.LensFlareStyle.Anamorphic)
					{
						this.AddTo(1f, temporary, renderTexture);
					}
					else
					{
						this.Vignette(1f, temporary, temporary5);
						this.BlendFlares(temporary5, temporary);
						this.AddTo(1f, temporary, renderTexture);
					}
				}
				RenderTexture.ReleaseTemporary(temporary5);
			}
			int num11 = (int)bloomScreenBlendMode;
			this.screenBlend.SetFloat("_Intensity", this.bloomIntensity);
			this.screenBlend.SetTexture("_ColorBuffer", source);
			if (this.quality > BloomEffect.BloomQuality.Cheap)
			{
				RenderTexture temporary6 = RenderTexture.GetTemporary(num, num2, 0, renderTextureFormat);
				Graphics.Blit(renderTexture, temporary6);
				Graphics.Blit(temporary6, destination, this.screenBlend, num11);
				RenderTexture.ReleaseTemporary(temporary6);
			}
			else
			{
				Graphics.Blit(renderTexture, destination, this.screenBlend, num11);
			}
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(renderTexture);
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x0002AEAD File Offset: 0x000290AD
		private void AddTo(float intensity_, RenderTexture from, RenderTexture to)
		{
			this.screenBlend.SetFloat("_Intensity", intensity_);
			to.MarkRestoreExpected();
			Graphics.Blit(from, to, this.screenBlend, 9);
		}

		// Token: 0x06003B4C RID: 15180 RVA: 0x001AC8F8 File Offset: 0x001AAAF8
		private void BlendFlares(RenderTexture from, RenderTexture to)
		{
			this.lensFlareMaterial.SetVector("colorA", new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) * this.lensflareIntensity);
			this.lensFlareMaterial.SetVector("colorB", new Vector4(this.flareColorB.r, this.flareColorB.g, this.flareColorB.b, this.flareColorB.a) * this.lensflareIntensity);
			this.lensFlareMaterial.SetVector("colorC", new Vector4(this.flareColorC.r, this.flareColorC.g, this.flareColorC.b, this.flareColorC.a) * this.lensflareIntensity);
			this.lensFlareMaterial.SetVector("colorD", new Vector4(this.flareColorD.r, this.flareColorD.g, this.flareColorD.b, this.flareColorD.a) * this.lensflareIntensity);
			to.MarkRestoreExpected();
			Graphics.Blit(from, to, this.lensFlareMaterial);
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x0002AED5 File Offset: 0x000290D5
		private void BrightFilter(float thresh, RenderTexture from, RenderTexture to)
		{
			this.brightPassFilterMaterial.SetVector("_Threshhold", new Vector4(thresh, thresh, thresh, thresh));
			Graphics.Blit(from, to, this.brightPassFilterMaterial, 0);
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x0002AEFE File Offset: 0x000290FE
		private void BrightFilter(Color threshColor, RenderTexture from, RenderTexture to)
		{
			this.brightPassFilterMaterial.SetVector("_Threshhold", threshColor);
			Graphics.Blit(from, to, this.brightPassFilterMaterial, 1);
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x001ACA48 File Offset: 0x001AAC48
		private void Vignette(float amount, RenderTexture from, RenderTexture to)
		{
			if (this.lensFlareVignetteMask)
			{
				this.screenBlend.SetTexture("_ColorBuffer", this.lensFlareVignetteMask);
				to.MarkRestoreExpected();
				Graphics.Blit((from == to) ? null : from, to, this.screenBlend, (from == to) ? 7 : 3);
				return;
			}
			if (from != to)
			{
				Graphics.SetRenderTarget(to);
				GL.Clear(false, true, Color.black);
				Graphics.Blit(from, to);
			}
		}

		// Token: 0x040035A3 RID: 13731
		public BloomEffect.TweakMode tweakMode;

		// Token: 0x040035A4 RID: 13732
		public BloomEffect.BloomScreenBlendMode screenBlendMode = BloomEffect.BloomScreenBlendMode.Add;

		// Token: 0x040035A5 RID: 13733
		public BloomEffect.HDRBloomMode hdr;

		// Token: 0x040035A6 RID: 13734
		private bool doHdr;

		// Token: 0x040035A7 RID: 13735
		public float sepBlurSpread = 2.5f;

		// Token: 0x040035A8 RID: 13736
		public BloomEffect.BloomQuality quality = BloomEffect.BloomQuality.High;

		// Token: 0x040035A9 RID: 13737
		public float bloomIntensity = 0.5f;

		// Token: 0x040035AA RID: 13738
		public float bloomThreshold = 0.5f;

		// Token: 0x040035AB RID: 13739
		public Color bloomThresholdColor = Color.white;

		// Token: 0x040035AC RID: 13740
		public int bloomBlurIterations = 2;

		// Token: 0x040035AD RID: 13741
		public int hollywoodFlareBlurIterations = 2;

		// Token: 0x040035AE RID: 13742
		public float flareRotation;

		// Token: 0x040035AF RID: 13743
		public BloomEffect.LensFlareStyle lensflareMode = BloomEffect.LensFlareStyle.Anamorphic;

		// Token: 0x040035B0 RID: 13744
		public float hollyStretchWidth = 2.5f;

		// Token: 0x040035B1 RID: 13745
		public float lensflareIntensity;

		// Token: 0x040035B2 RID: 13746
		public float lensflareThreshold = 0.3f;

		// Token: 0x040035B3 RID: 13747
		public float lensFlareSaturation = 0.75f;

		// Token: 0x040035B4 RID: 13748
		public Color flareColorA = new Color(0.4f, 0.4f, 0.8f, 0.75f);

		// Token: 0x040035B5 RID: 13749
		public Color flareColorB = new Color(0.4f, 0.8f, 0.8f, 0.75f);

		// Token: 0x040035B6 RID: 13750
		public Color flareColorC = new Color(0.8f, 0.4f, 0.8f, 0.75f);

		// Token: 0x040035B7 RID: 13751
		public Color flareColorD = new Color(0.8f, 0.4f, 0f, 0.75f);

		// Token: 0x040035B8 RID: 13752
		public Texture2D lensFlareVignetteMask;

		// Token: 0x040035B9 RID: 13753
		public Shader lensFlareShader;

		// Token: 0x040035BA RID: 13754
		private Material lensFlareMaterial;

		// Token: 0x040035BB RID: 13755
		public Shader screenBlendShader;

		// Token: 0x040035BC RID: 13756
		private Material screenBlend;

		// Token: 0x040035BD RID: 13757
		public Shader blurAndFlaresShader;

		// Token: 0x040035BE RID: 13758
		private Material blurAndFlaresMaterial;

		// Token: 0x040035BF RID: 13759
		public Shader brightPassFilterShader;

		// Token: 0x040035C0 RID: 13760
		private Material brightPassFilterMaterial;

		// Token: 0x02000914 RID: 2324
		public enum LensFlareStyle
		{
			// Token: 0x040035C2 RID: 13762
			Ghosting,
			// Token: 0x040035C3 RID: 13763
			Anamorphic,
			// Token: 0x040035C4 RID: 13764
			Combined
		}

		// Token: 0x02000915 RID: 2325
		public enum TweakMode
		{
			// Token: 0x040035C6 RID: 13766
			Basic,
			// Token: 0x040035C7 RID: 13767
			Complex
		}

		// Token: 0x02000916 RID: 2326
		public enum HDRBloomMode
		{
			// Token: 0x040035C9 RID: 13769
			Auto,
			// Token: 0x040035CA RID: 13770
			On,
			// Token: 0x040035CB RID: 13771
			Off
		}

		// Token: 0x02000917 RID: 2327
		public enum BloomScreenBlendMode
		{
			// Token: 0x040035CD RID: 13773
			Screen,
			// Token: 0x040035CE RID: 13774
			Add
		}

		// Token: 0x02000918 RID: 2328
		public enum BloomQuality
		{
			// Token: 0x040035D0 RID: 13776
			Cheap,
			// Token: 0x040035D1 RID: 13777
			High
		}
	}
}
