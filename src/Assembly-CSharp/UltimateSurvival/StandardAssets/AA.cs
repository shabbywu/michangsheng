using System;
using UnityEngine;

namespace UltimateSurvival.StandardAssets
{
	// Token: 0x0200062A RID: 1578
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Ultimate Survival/Add/Image Effects/Antialiasing")]
	public class AA : ImageEffectBase
	{
		// Token: 0x0600320B RID: 12811 RVA: 0x00162430 File Offset: 0x00160630
		public Material CurrentAAMaterial()
		{
			Material result;
			switch (this.mode)
			{
			case AAMode.FXAA2:
				result = this.materialFXAAII;
				break;
			case AAMode.FXAA3Console:
				result = this.materialFXAAIII;
				break;
			case AAMode.FXAA1PresetA:
				result = this.materialFXAAPreset2;
				break;
			case AAMode.FXAA1PresetB:
				result = this.materialFXAAPreset3;
				break;
			case AAMode.NFAA:
				result = this.nfaa;
				break;
			case AAMode.SSAA:
				result = this.ssaa;
				break;
			case AAMode.DLAA:
				result = this.dlaa;
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x001624AC File Offset: 0x001606AC
		public override bool CheckResources()
		{
			base.CheckSupport(false);
			this.materialFXAAPreset2 = base.CreateMaterial(this.shaderFXAAPreset2, this.materialFXAAPreset2);
			this.materialFXAAPreset3 = base.CreateMaterial(this.shaderFXAAPreset3, this.materialFXAAPreset3);
			this.materialFXAAII = base.CreateMaterial(this.shaderFXAAII, this.materialFXAAII);
			this.materialFXAAIII = base.CreateMaterial(this.shaderFXAAIII, this.materialFXAAIII);
			this.nfaa = base.CreateMaterial(this.nfaaShader, this.nfaa);
			this.ssaa = base.CreateMaterial(this.ssaaShader, this.ssaa);
			this.dlaa = base.CreateMaterial(this.dlaaShader, this.dlaa);
			if (!this.ssaaShader.isSupported)
			{
				base.NotSupported();
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x00162588 File Offset: 0x00160788
		public void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.mode == AAMode.FXAA3Console && this.materialFXAAIII != null)
			{
				this.materialFXAAIII.SetFloat("_EdgeThresholdMin", this.edgeThresholdMin);
				this.materialFXAAIII.SetFloat("_EdgeThreshold", this.edgeThreshold);
				this.materialFXAAIII.SetFloat("_EdgeSharpness", this.edgeSharpness);
				Graphics.Blit(source, destination, this.materialFXAAIII);
				return;
			}
			if (this.mode == AAMode.FXAA1PresetB && this.materialFXAAPreset3 != null)
			{
				Graphics.Blit(source, destination, this.materialFXAAPreset3);
				return;
			}
			if (this.mode == AAMode.FXAA1PresetA && this.materialFXAAPreset2 != null)
			{
				source.anisoLevel = 4;
				Graphics.Blit(source, destination, this.materialFXAAPreset2);
				source.anisoLevel = 0;
				return;
			}
			if (this.mode == AAMode.FXAA2 && this.materialFXAAII != null)
			{
				Graphics.Blit(source, destination, this.materialFXAAII);
				return;
			}
			if (this.mode == AAMode.SSAA && this.ssaa != null)
			{
				Graphics.Blit(source, destination, this.ssaa);
				return;
			}
			if (this.mode == AAMode.DLAA && this.dlaa != null)
			{
				source.anisoLevel = 0;
				RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height);
				Graphics.Blit(source, temporary, this.dlaa, 0);
				Graphics.Blit(temporary, destination, this.dlaa, this.dlaaSharp ? 2 : 1);
				RenderTexture.ReleaseTemporary(temporary);
				return;
			}
			if (this.mode == AAMode.NFAA && this.nfaa != null)
			{
				source.anisoLevel = 0;
				this.nfaa.SetFloat("_OffsetScale", this.offsetScale);
				this.nfaa.SetFloat("_BlurRadius", this.blurRadius);
				Graphics.Blit(source, destination, this.nfaa, this.showGeneratedNormals ? 1 : 0);
				return;
			}
			Graphics.Blit(source, destination);
		}

		// Token: 0x04002C5F RID: 11359
		public AAMode mode = AAMode.FXAA3Console;

		// Token: 0x04002C60 RID: 11360
		public bool showGeneratedNormals;

		// Token: 0x04002C61 RID: 11361
		public float offsetScale = 0.2f;

		// Token: 0x04002C62 RID: 11362
		public float blurRadius = 18f;

		// Token: 0x04002C63 RID: 11363
		public float edgeThresholdMin = 0.05f;

		// Token: 0x04002C64 RID: 11364
		public float edgeThreshold = 0.2f;

		// Token: 0x04002C65 RID: 11365
		public float edgeSharpness = 4f;

		// Token: 0x04002C66 RID: 11366
		public bool dlaaSharp;

		// Token: 0x04002C67 RID: 11367
		public Shader ssaaShader;

		// Token: 0x04002C68 RID: 11368
		private Material ssaa;

		// Token: 0x04002C69 RID: 11369
		public Shader dlaaShader;

		// Token: 0x04002C6A RID: 11370
		private Material dlaa;

		// Token: 0x04002C6B RID: 11371
		public Shader nfaaShader;

		// Token: 0x04002C6C RID: 11372
		private Material nfaa;

		// Token: 0x04002C6D RID: 11373
		public Shader shaderFXAAPreset2;

		// Token: 0x04002C6E RID: 11374
		private Material materialFXAAPreset2;

		// Token: 0x04002C6F RID: 11375
		public Shader shaderFXAAPreset3;

		// Token: 0x04002C70 RID: 11376
		private Material materialFXAAPreset3;

		// Token: 0x04002C71 RID: 11377
		public Shader shaderFXAAII;

		// Token: 0x04002C72 RID: 11378
		private Material materialFXAAII;

		// Token: 0x04002C73 RID: 11379
		public Shader shaderFXAAIII;

		// Token: 0x04002C74 RID: 11380
		private Material materialFXAAIII;
	}
}
