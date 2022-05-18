using System;
using UnityEngine;

namespace UltimateSurvival.StandardAssets
{
	// Token: 0x0200091B RID: 2331
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Ultimate Survival/Add/Image Effects/DOF")]
	public class DOF : ImageEffectBase
	{
		// Token: 0x06003B58 RID: 15192 RVA: 0x001AD25C File Offset: 0x001AB45C
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

		// Token: 0x06003B59 RID: 15193 RVA: 0x001AD2E0 File Offset: 0x001AB4E0
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

		// Token: 0x06003B5A RID: 15194 RVA: 0x0002AF3B File Offset: 0x0002913B
		private void OnDisable()
		{
			MyQuads.Cleanup();
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x0002AF42 File Offset: 0x00029142
		private void OnEnable()
		{
			this._camera = base.GetComponent<Camera>();
			this._camera.depthTextureMode |= 1;
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x001AD380 File Offset: 0x001AB580
		private float FocalDistance01(float worldDist)
		{
			return this._camera.WorldToViewportPoint((worldDist - this._camera.nearClipPlane) * this._camera.transform.forward + this._camera.transform.position).z / (this._camera.farClipPlane - this._camera.nearClipPlane);
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x001AD3EC File Offset: 0x001AB5EC
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

		// Token: 0x06003B5E RID: 15198 RVA: 0x001AD414 File Offset: 0x001AB614
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

		// Token: 0x06003B5F RID: 15199 RVA: 0x001AD440 File Offset: 0x001AB640
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

		// Token: 0x06003B60 RID: 15200 RVA: 0x001ADA00 File Offset: 0x001ABC00
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

		// Token: 0x06003B61 RID: 15201 RVA: 0x001ADB40 File Offset: 0x001ABD40
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

		// Token: 0x06003B62 RID: 15202 RVA: 0x001ADC94 File Offset: 0x001ABE94
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

		// Token: 0x06003B63 RID: 15203 RVA: 0x001ADDB0 File Offset: 0x001ABFB0
		private void Downsample(RenderTexture from, RenderTexture to)
		{
			this.dofMaterial.SetVector("_InvRenderTargetSize", new Vector4(1f / (1f * (float)to.width), 1f / (1f * (float)to.height), 0f, 0f));
			Graphics.Blit(from, to, this.dofMaterial, DOF.SMOOTH_DOWNSAMPLE_PASS);
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x001ADE14 File Offset: 0x001AC014
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

		// Token: 0x06003B65 RID: 15205 RVA: 0x001ADF78 File Offset: 0x001AC178
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

		// Token: 0x06003B66 RID: 15206 RVA: 0x001AE018 File Offset: 0x001AC218
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

		// Token: 0x040035ED RID: 13805
		private static int SMOOTH_DOWNSAMPLE_PASS = 6;

		// Token: 0x040035EE RID: 13806
		private static float BOKEH_EXTRA_BLUR = 2f;

		// Token: 0x040035EF RID: 13807
		public DOF.Dof34QualitySetting quality = DOF.Dof34QualitySetting.OnlyBackground;

		// Token: 0x040035F0 RID: 13808
		public DOF.DofResolution resolution = DOF.DofResolution.Low;

		// Token: 0x040035F1 RID: 13809
		public bool simpleTweakMode = true;

		// Token: 0x040035F2 RID: 13810
		public float focalPoint = 1f;

		// Token: 0x040035F3 RID: 13811
		public float smoothness = 0.5f;

		// Token: 0x040035F4 RID: 13812
		public float focalZDistance;

		// Token: 0x040035F5 RID: 13813
		public float focalZStartCurve = 1f;

		// Token: 0x040035F6 RID: 13814
		public float focalZEndCurve = 1f;

		// Token: 0x040035F7 RID: 13815
		private float focalStartCurve = 2f;

		// Token: 0x040035F8 RID: 13816
		private float focalEndCurve = 2f;

		// Token: 0x040035F9 RID: 13817
		private float focalDistance01 = 0.1f;

		// Token: 0x040035FA RID: 13818
		public Transform objectFocus;

		// Token: 0x040035FB RID: 13819
		public float focalSize;

		// Token: 0x040035FC RID: 13820
		public DOF.DofBlurriness bluriness = DOF.DofBlurriness.High;

		// Token: 0x040035FD RID: 13821
		public float maxBlurSpread = 1.75f;

		// Token: 0x040035FE RID: 13822
		public float foregroundBlurExtrude = 1.15f;

		// Token: 0x040035FF RID: 13823
		public Shader dofBlurShader;

		// Token: 0x04003600 RID: 13824
		private Material dofBlurMaterial;

		// Token: 0x04003601 RID: 13825
		public Shader dofShader;

		// Token: 0x04003602 RID: 13826
		private Material dofMaterial;

		// Token: 0x04003603 RID: 13827
		public bool visualize;

		// Token: 0x04003604 RID: 13828
		public DOF.BokehDestination bokehDestination = DOF.BokehDestination.Background;

		// Token: 0x04003605 RID: 13829
		private float widthOverHeight = 1.25f;

		// Token: 0x04003606 RID: 13830
		private float oneOverBaseSize = 0.001953125f;

		// Token: 0x04003607 RID: 13831
		public bool bokeh;

		// Token: 0x04003608 RID: 13832
		public bool bokehSupport = true;

		// Token: 0x04003609 RID: 13833
		public Shader bokehShader;

		// Token: 0x0400360A RID: 13834
		public Texture2D bokehTexture;

		// Token: 0x0400360B RID: 13835
		public float bokehScale = 2.4f;

		// Token: 0x0400360C RID: 13836
		public float bokehIntensity = 0.15f;

		// Token: 0x0400360D RID: 13837
		public float bokehThresholdContrast = 0.1f;

		// Token: 0x0400360E RID: 13838
		public float bokehThresholdLuminance = 0.55f;

		// Token: 0x0400360F RID: 13839
		public int bokehDownsample = 1;

		// Token: 0x04003610 RID: 13840
		private Material bokehMaterial;

		// Token: 0x04003611 RID: 13841
		private Camera _camera;

		// Token: 0x04003612 RID: 13842
		private RenderTexture foregroundTexture;

		// Token: 0x04003613 RID: 13843
		private RenderTexture mediumRezWorkTexture;

		// Token: 0x04003614 RID: 13844
		private RenderTexture finalDefocus;

		// Token: 0x04003615 RID: 13845
		private RenderTexture lowRezWorkTexture;

		// Token: 0x04003616 RID: 13846
		private RenderTexture bokehSource;

		// Token: 0x04003617 RID: 13847
		private RenderTexture bokehSource2;

		// Token: 0x0200091C RID: 2332
		public enum Dof34QualitySetting
		{
			// Token: 0x04003619 RID: 13849
			OnlyBackground = 1,
			// Token: 0x0400361A RID: 13850
			BackgroundAndForeground
		}

		// Token: 0x0200091D RID: 2333
		public enum DofResolution
		{
			// Token: 0x0400361C RID: 13852
			High = 2,
			// Token: 0x0400361D RID: 13853
			Medium,
			// Token: 0x0400361E RID: 13854
			Low
		}

		// Token: 0x0200091E RID: 2334
		public enum DofBlurriness
		{
			// Token: 0x04003620 RID: 13856
			Low = 1,
			// Token: 0x04003621 RID: 13857
			High,
			// Token: 0x04003622 RID: 13858
			VeryHigh = 4
		}

		// Token: 0x0200091F RID: 2335
		public enum BokehDestination
		{
			// Token: 0x04003624 RID: 13860
			Background = 1,
			// Token: 0x04003625 RID: 13861
			Foreground,
			// Token: 0x04003626 RID: 13862
			BackgroundAndForeground
		}
	}
}
