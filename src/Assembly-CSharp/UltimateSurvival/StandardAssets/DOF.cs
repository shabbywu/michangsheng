using System;
using UnityEngine;

namespace UltimateSurvival.StandardAssets
{
	// Token: 0x0200062D RID: 1581
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Ultimate Survival/Add/Image Effects/DOF")]
	public class DOF : ImageEffectBase
	{
		// Token: 0x0600321E RID: 12830 RVA: 0x001639B0 File Offset: 0x00161BB0
		private void CreateMaterials()
		{
			this.dofBlurMaterial = base.CheckShaderAndCreateMaterial(this.dofBlurShader, this.dofBlurMaterial);
			this.dofMaterial = base.CheckShaderAndCreateMaterial(this.dofShader, this.dofMaterial);
			this.bokehSupport = this.bokehShader.isSupported;
			if (this.bokeh && this.bokehSupport && this.bokehShader)
			{
				this.bokehMaterial = base.CheckShaderAndCreateMaterial(this.bokehShader, this.bokehMaterial);
			}
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x00163A34 File Offset: 0x00161C34
		public override bool CheckResources()
		{
			base.CheckSupport(true);
			this.dofBlurMaterial = base.CheckShaderAndCreateMaterial(this.dofBlurShader, this.dofBlurMaterial);
			this.dofMaterial = base.CheckShaderAndCreateMaterial(this.dofShader, this.dofMaterial);
			this.bokehSupport = this.bokehShader.isSupported;
			if (this.bokeh && this.bokehSupport && this.bokehShader)
			{
				this.bokehMaterial = base.CheckShaderAndCreateMaterial(this.bokehShader, this.bokehMaterial);
			}
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x00163AD3 File Offset: 0x00161CD3
		private void OnDisable()
		{
			MyQuads.Cleanup();
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x00163ADA File Offset: 0x00161CDA
		private void OnEnable()
		{
			this._camera = base.GetComponent<Camera>();
			this._camera.depthTextureMode |= 1;
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x00163AFC File Offset: 0x00161CFC
		private float FocalDistance01(float worldDist)
		{
			return this._camera.WorldToViewportPoint((worldDist - this._camera.nearClipPlane) * this._camera.transform.forward + this._camera.transform.position).z / (this._camera.farClipPlane - this._camera.nearClipPlane);
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x00163B68 File Offset: 0x00161D68
		private int GetDividerBasedOnQuality()
		{
			int result = 1;
			if (this.resolution == DOF.DofResolution.Medium)
			{
				result = 2;
			}
			else if (this.resolution == DOF.DofResolution.Low)
			{
				result = 2;
			}
			return result;
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x00163B90 File Offset: 0x00161D90
		private int GetLowResolutionDividerBasedOnQuality(int baseDivider)
		{
			int num = baseDivider;
			if (this.resolution == DOF.DofResolution.High)
			{
				num *= 2;
			}
			if (this.resolution == DOF.DofResolution.Low)
			{
				num *= 2;
			}
			return num;
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x00163BBC File Offset: 0x00161DBC
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.smoothness < 0.1f)
			{
				this.smoothness = 0.1f;
			}
			this.bokeh = (this.bokeh && this.bokehSupport);
			float num = this.bokeh ? DOF.BOKEH_EXTRA_BLUR : 1f;
			bool flag = this.quality > DOF.Dof34QualitySetting.OnlyBackground;
			float num2 = this.focalSize / (this._camera.farClipPlane - this._camera.nearClipPlane);
			if (this.simpleTweakMode)
			{
				this.focalDistance01 = (this.objectFocus ? (this._camera.WorldToViewportPoint(this.objectFocus.position).z / this._camera.farClipPlane) : this.FocalDistance01(this.focalPoint));
				this.focalStartCurve = this.focalDistance01 * this.smoothness;
				this.focalEndCurve = this.focalStartCurve;
				flag = (flag && this.focalPoint > this._camera.nearClipPlane + Mathf.Epsilon);
			}
			else
			{
				if (this.objectFocus)
				{
					Vector3 vector = this._camera.WorldToViewportPoint(this.objectFocus.position);
					vector.z /= this._camera.farClipPlane;
					this.focalDistance01 = vector.z;
				}
				else
				{
					this.focalDistance01 = this.FocalDistance01(this.focalZDistance);
				}
				this.focalStartCurve = this.focalZStartCurve;
				this.focalEndCurve = this.focalZEndCurve;
				flag = (flag && this.focalPoint > this._camera.nearClipPlane + Mathf.Epsilon);
			}
			this.widthOverHeight = 1f * (float)source.width / (1f * (float)source.height);
			this.oneOverBaseSize = 0.001953125f;
			this.dofMaterial.SetFloat("_ForegroundBlurExtrude", this.foregroundBlurExtrude);
			this.dofMaterial.SetVector("_CurveParams", new Vector4(this.simpleTweakMode ? (1f / this.focalStartCurve) : this.focalStartCurve, this.simpleTweakMode ? (1f / this.focalEndCurve) : this.focalEndCurve, num2 * 0.5f, this.focalDistance01));
			this.dofMaterial.SetVector("_InvRenderTargetSize", new Vector4(1f / (1f * (float)source.width), 1f / (1f * (float)source.height), 0f, 0f));
			int dividerBasedOnQuality = this.GetDividerBasedOnQuality();
			int lowResolutionDividerBasedOnQuality = this.GetLowResolutionDividerBasedOnQuality(dividerBasedOnQuality);
			this.AllocateTextures(flag, source, dividerBasedOnQuality, lowResolutionDividerBasedOnQuality);
			Graphics.Blit(source, source, this.dofMaterial, 3);
			this.Downsample(source, this.mediumRezWorkTexture);
			this.Blur(this.mediumRezWorkTexture, this.mediumRezWorkTexture, DOF.DofBlurriness.Low, 4, this.maxBlurSpread);
			if (this.bokeh && (DOF.BokehDestination.Foreground & this.bokehDestination) != (DOF.BokehDestination)0)
			{
				this.dofMaterial.SetVector("_Threshhold", new Vector4(this.bokehThresholdContrast, this.bokehThresholdLuminance, 0.95f, 0f));
				Graphics.Blit(this.mediumRezWorkTexture, this.bokehSource2, this.dofMaterial, 11);
				Graphics.Blit(this.mediumRezWorkTexture, this.lowRezWorkTexture);
				this.Blur(this.lowRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 0, this.maxBlurSpread * num);
			}
			else
			{
				this.Downsample(this.mediumRezWorkTexture, this.lowRezWorkTexture);
				this.Blur(this.lowRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 0, this.maxBlurSpread);
			}
			this.dofBlurMaterial.SetTexture("_TapLow", this.lowRezWorkTexture);
			this.dofBlurMaterial.SetTexture("_TapMedium", this.mediumRezWorkTexture);
			Graphics.Blit(null, this.finalDefocus, this.dofBlurMaterial, 3);
			if (this.bokeh && (DOF.BokehDestination.Foreground & this.bokehDestination) != (DOF.BokehDestination)0)
			{
				this.AddBokeh(this.bokehSource2, this.bokehSource, this.finalDefocus);
			}
			this.dofMaterial.SetTexture("_TapLowBackground", this.finalDefocus);
			this.dofMaterial.SetTexture("_TapMedium", this.mediumRezWorkTexture);
			Graphics.Blit(source, flag ? this.foregroundTexture : destination, this.dofMaterial, this.visualize ? 2 : 0);
			if (flag)
			{
				Graphics.Blit(this.foregroundTexture, source, this.dofMaterial, 5);
				this.Downsample(source, this.mediumRezWorkTexture);
				this.BlurFg(this.mediumRezWorkTexture, this.mediumRezWorkTexture, DOF.DofBlurriness.Low, 2, this.maxBlurSpread);
				if (this.bokeh && (DOF.BokehDestination.Foreground & this.bokehDestination) != (DOF.BokehDestination)0)
				{
					this.dofMaterial.SetVector("_Threshhold", new Vector4(this.bokehThresholdContrast * 0.5f, this.bokehThresholdLuminance, 0f, 0f));
					Graphics.Blit(this.mediumRezWorkTexture, this.bokehSource2, this.dofMaterial, 11);
					Graphics.Blit(this.mediumRezWorkTexture, this.lowRezWorkTexture);
					this.BlurFg(this.lowRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 1, this.maxBlurSpread * num);
				}
				else
				{
					this.BlurFg(this.mediumRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 1, this.maxBlurSpread);
				}
				Graphics.Blit(this.lowRezWorkTexture, this.finalDefocus);
				this.dofMaterial.SetTexture("_TapLowForeground", this.finalDefocus);
				Graphics.Blit(source, destination, this.dofMaterial, this.visualize ? 1 : 4);
				if (this.bokeh && (DOF.BokehDestination.Foreground & this.bokehDestination) != (DOF.BokehDestination)0)
				{
					this.AddBokeh(this.bokehSource2, this.bokehSource, destination);
				}
			}
			this.ReleaseTextures();
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x0016417C File Offset: 0x0016237C
		private void Blur(RenderTexture from, RenderTexture to, DOF.DofBlurriness iterations, int blurPass, float spread)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(to.width, to.height);
			if (iterations > DOF.DofBlurriness.Low)
			{
				this.BlurHex(from, to, blurPass, spread, temporary);
				if (iterations > DOF.DofBlurriness.High)
				{
					this.dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * this.oneOverBaseSize, 0f, 0f));
					Graphics.Blit(to, temporary, this.dofBlurMaterial, blurPass);
					this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0f, 0f, 0f));
					Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
				}
			}
			else
			{
				this.dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * this.oneOverBaseSize, 0f, 0f));
				Graphics.Blit(from, temporary, this.dofBlurMaterial, blurPass);
				this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0f, 0f, 0f));
				Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
			}
			RenderTexture.ReleaseTemporary(temporary);
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x001642BC File Offset: 0x001624BC
		private void BlurFg(RenderTexture from, RenderTexture to, DOF.DofBlurriness iterations, int blurPass, float spread)
		{
			this.dofBlurMaterial.SetTexture("_TapHigh", from);
			RenderTexture temporary = RenderTexture.GetTemporary(to.width, to.height);
			if (iterations > DOF.DofBlurriness.Low)
			{
				this.BlurHex(from, to, blurPass, spread, temporary);
				if (iterations > DOF.DofBlurriness.High)
				{
					this.dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * this.oneOverBaseSize, 0f, 0f));
					Graphics.Blit(to, temporary, this.dofBlurMaterial, blurPass);
					this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0f, 0f, 0f));
					Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
				}
			}
			else
			{
				this.dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * this.oneOverBaseSize, 0f, 0f));
				Graphics.Blit(from, temporary, this.dofBlurMaterial, blurPass);
				this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0f, 0f, 0f));
				Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
			}
			RenderTexture.ReleaseTemporary(temporary);
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x00164410 File Offset: 0x00162610
		private void BlurHex(RenderTexture from, RenderTexture to, int blurPass, float spread, RenderTexture tmp)
		{
			this.dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * this.oneOverBaseSize, 0f, 0f));
			Graphics.Blit(from, tmp, this.dofBlurMaterial, blurPass);
			this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0f, 0f, 0f));
			Graphics.Blit(tmp, to, this.dofBlurMaterial, blurPass);
			this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, spread * this.oneOverBaseSize, 0f, 0f));
			Graphics.Blit(to, tmp, this.dofBlurMaterial, blurPass);
			this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, -spread * this.oneOverBaseSize, 0f, 0f));
			Graphics.Blit(tmp, to, this.dofBlurMaterial, blurPass);
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x0016452C File Offset: 0x0016272C
		private void Downsample(RenderTexture from, RenderTexture to)
		{
			this.dofMaterial.SetVector("_InvRenderTargetSize", new Vector4(1f / (1f * (float)to.width), 1f / (1f * (float)to.height), 0f, 0f));
			Graphics.Blit(from, to, this.dofMaterial, DOF.SMOOTH_DOWNSAMPLE_PASS);
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x00164590 File Offset: 0x00162790
		private void AddBokeh(RenderTexture bokehInfo, RenderTexture tempTex, RenderTexture finalTarget)
		{
			if (this.bokehMaterial)
			{
				Mesh[] meshes = MyQuads.GetMeshes(tempTex.width, tempTex.height);
				RenderTexture.active = tempTex;
				GL.Clear(false, true, new Color(0f, 0f, 0f, 0f));
				GL.PushMatrix();
				GL.LoadIdentity();
				bokehInfo.filterMode = 0;
				float num = (float)bokehInfo.width * 1f / ((float)bokehInfo.height * 1f);
				float num2 = 2f / (1f * (float)bokehInfo.width);
				num2 += this.bokehScale * this.maxBlurSpread * DOF.BOKEH_EXTRA_BLUR * this.oneOverBaseSize;
				this.bokehMaterial.SetTexture("_Source", bokehInfo);
				this.bokehMaterial.SetTexture("_MainTex", this.bokehTexture);
				this.bokehMaterial.SetVector("_ArScale", new Vector4(num2, num2 * num, 0.5f, 0.5f * num));
				this.bokehMaterial.SetFloat("_Intensity", this.bokehIntensity);
				this.bokehMaterial.SetPass(0);
				foreach (Mesh mesh in meshes)
				{
					if (mesh)
					{
						Graphics.DrawMeshNow(mesh, Matrix4x4.identity);
					}
				}
				GL.PopMatrix();
				Graphics.Blit(tempTex, finalTarget, this.dofMaterial, 8);
				bokehInfo.filterMode = 1;
			}
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x001646F4 File Offset: 0x001628F4
		private void ReleaseTextures()
		{
			if (this.foregroundTexture)
			{
				RenderTexture.ReleaseTemporary(this.foregroundTexture);
			}
			if (this.finalDefocus)
			{
				RenderTexture.ReleaseTemporary(this.finalDefocus);
			}
			if (this.mediumRezWorkTexture)
			{
				RenderTexture.ReleaseTemporary(this.mediumRezWorkTexture);
			}
			if (this.lowRezWorkTexture)
			{
				RenderTexture.ReleaseTemporary(this.lowRezWorkTexture);
			}
			if (this.bokehSource)
			{
				RenderTexture.ReleaseTemporary(this.bokehSource);
			}
			if (this.bokehSource2)
			{
				RenderTexture.ReleaseTemporary(this.bokehSource2);
			}
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x00164794 File Offset: 0x00162994
		private void AllocateTextures(bool blurForeground, RenderTexture source, int divider, int lowTexDivider)
		{
			this.foregroundTexture = null;
			if (blurForeground)
			{
				this.foregroundTexture = RenderTexture.GetTemporary(source.width, source.height, 0);
			}
			this.mediumRezWorkTexture = RenderTexture.GetTemporary(source.width / divider, source.height / divider, 0);
			this.finalDefocus = RenderTexture.GetTemporary(source.width / divider, source.height / divider, 0);
			this.lowRezWorkTexture = RenderTexture.GetTemporary(source.width / lowTexDivider, source.height / lowTexDivider, 0);
			this.bokehSource = null;
			this.bokehSource2 = null;
			if (this.bokeh)
			{
				this.bokehSource = RenderTexture.GetTemporary(source.width / (lowTexDivider * this.bokehDownsample), source.height / (lowTexDivider * this.bokehDownsample), 0, 2);
				this.bokehSource2 = RenderTexture.GetTemporary(source.width / (lowTexDivider * this.bokehDownsample), source.height / (lowTexDivider * this.bokehDownsample), 0, 2);
				this.bokehSource.filterMode = 1;
				this.bokehSource2.filterMode = 1;
				RenderTexture.active = this.bokehSource2;
				GL.Clear(false, true, new Color(0f, 0f, 0f, 0f));
			}
			source.filterMode = 1;
			this.finalDefocus.filterMode = 1;
			this.mediumRezWorkTexture.filterMode = 1;
			this.lowRezWorkTexture.filterMode = 1;
			if (this.foregroundTexture)
			{
				this.foregroundTexture.filterMode = 1;
			}
		}

		// Token: 0x04002CAB RID: 11435
		private static int SMOOTH_DOWNSAMPLE_PASS = 6;

		// Token: 0x04002CAC RID: 11436
		private static float BOKEH_EXTRA_BLUR = 2f;

		// Token: 0x04002CAD RID: 11437
		public DOF.Dof34QualitySetting quality = DOF.Dof34QualitySetting.OnlyBackground;

		// Token: 0x04002CAE RID: 11438
		public DOF.DofResolution resolution = DOF.DofResolution.Low;

		// Token: 0x04002CAF RID: 11439
		public bool simpleTweakMode = true;

		// Token: 0x04002CB0 RID: 11440
		public float focalPoint = 1f;

		// Token: 0x04002CB1 RID: 11441
		public float smoothness = 0.5f;

		// Token: 0x04002CB2 RID: 11442
		public float focalZDistance;

		// Token: 0x04002CB3 RID: 11443
		public float focalZStartCurve = 1f;

		// Token: 0x04002CB4 RID: 11444
		public float focalZEndCurve = 1f;

		// Token: 0x04002CB5 RID: 11445
		private float focalStartCurve = 2f;

		// Token: 0x04002CB6 RID: 11446
		private float focalEndCurve = 2f;

		// Token: 0x04002CB7 RID: 11447
		private float focalDistance01 = 0.1f;

		// Token: 0x04002CB8 RID: 11448
		public Transform objectFocus;

		// Token: 0x04002CB9 RID: 11449
		public float focalSize;

		// Token: 0x04002CBA RID: 11450
		public DOF.DofBlurriness bluriness = DOF.DofBlurriness.High;

		// Token: 0x04002CBB RID: 11451
		public float maxBlurSpread = 1.75f;

		// Token: 0x04002CBC RID: 11452
		public float foregroundBlurExtrude = 1.15f;

		// Token: 0x04002CBD RID: 11453
		public Shader dofBlurShader;

		// Token: 0x04002CBE RID: 11454
		private Material dofBlurMaterial;

		// Token: 0x04002CBF RID: 11455
		public Shader dofShader;

		// Token: 0x04002CC0 RID: 11456
		private Material dofMaterial;

		// Token: 0x04002CC1 RID: 11457
		public bool visualize;

		// Token: 0x04002CC2 RID: 11458
		public DOF.BokehDestination bokehDestination = DOF.BokehDestination.Background;

		// Token: 0x04002CC3 RID: 11459
		private float widthOverHeight = 1.25f;

		// Token: 0x04002CC4 RID: 11460
		private float oneOverBaseSize = 0.001953125f;

		// Token: 0x04002CC5 RID: 11461
		public bool bokeh;

		// Token: 0x04002CC6 RID: 11462
		public bool bokehSupport = true;

		// Token: 0x04002CC7 RID: 11463
		public Shader bokehShader;

		// Token: 0x04002CC8 RID: 11464
		public Texture2D bokehTexture;

		// Token: 0x04002CC9 RID: 11465
		public float bokehScale = 2.4f;

		// Token: 0x04002CCA RID: 11466
		public float bokehIntensity = 0.15f;

		// Token: 0x04002CCB RID: 11467
		public float bokehThresholdContrast = 0.1f;

		// Token: 0x04002CCC RID: 11468
		public float bokehThresholdLuminance = 0.55f;

		// Token: 0x04002CCD RID: 11469
		public int bokehDownsample = 1;

		// Token: 0x04002CCE RID: 11470
		private Material bokehMaterial;

		// Token: 0x04002CCF RID: 11471
		private Camera _camera;

		// Token: 0x04002CD0 RID: 11472
		private RenderTexture foregroundTexture;

		// Token: 0x04002CD1 RID: 11473
		private RenderTexture mediumRezWorkTexture;

		// Token: 0x04002CD2 RID: 11474
		private RenderTexture finalDefocus;

		// Token: 0x04002CD3 RID: 11475
		private RenderTexture lowRezWorkTexture;

		// Token: 0x04002CD4 RID: 11476
		private RenderTexture bokehSource;

		// Token: 0x04002CD5 RID: 11477
		private RenderTexture bokehSource2;

		// Token: 0x020014D8 RID: 5336
		public enum Dof34QualitySetting
		{
			// Token: 0x04006D85 RID: 28037
			OnlyBackground = 1,
			// Token: 0x04006D86 RID: 28038
			BackgroundAndForeground
		}

		// Token: 0x020014D9 RID: 5337
		public enum DofResolution
		{
			// Token: 0x04006D88 RID: 28040
			High = 2,
			// Token: 0x04006D89 RID: 28041
			Medium,
			// Token: 0x04006D8A RID: 28042
			Low
		}

		// Token: 0x020014DA RID: 5338
		public enum DofBlurriness
		{
			// Token: 0x04006D8C RID: 28044
			Low = 1,
			// Token: 0x04006D8D RID: 28045
			High,
			// Token: 0x04006D8E RID: 28046
			VeryHigh = 4
		}

		// Token: 0x020014DB RID: 5339
		public enum BokehDestination
		{
			// Token: 0x04006D90 RID: 28048
			Background = 1,
			// Token: 0x04006D91 RID: 28049
			Foreground,
			// Token: 0x04006D92 RID: 28050
			BackgroundAndForeground
		}
	}
}
