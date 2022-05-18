using System;
using UnityEngine;

namespace UltimateSurvival.StandardAssets
{
	// Token: 0x02000912 RID: 2322
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Ultimate Survival/Add/Image Effects/Antialiasing")]
	public class AA : ImageEffectBase
	{
		// Token: 0x06003B45 RID: 15173 RVA: 0x001ABD6C File Offset: 0x001A9F6C
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

		// Token: 0x06003B46 RID: 15174 RVA: 0x001ABDE8 File Offset: 0x001A9FE8
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

		// Token: 0x06003B47 RID: 15175 RVA: 0x001ABEC4 File Offset: 0x001AA0C4
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

		// Token: 0x0400358D RID: 13709
		public AAMode mode = AAMode.FXAA3Console;

		// Token: 0x0400358E RID: 13710
		public bool showGeneratedNormals;

		// Token: 0x0400358F RID: 13711
		public float offsetScale = 0.2f;

		// Token: 0x04003590 RID: 13712
		public float blurRadius = 18f;

		// Token: 0x04003591 RID: 13713
		public float edgeThresholdMin = 0.05f;

		// Token: 0x04003592 RID: 13714
		public float edgeThreshold = 0.2f;

		// Token: 0x04003593 RID: 13715
		public float edgeSharpness = 4f;

		// Token: 0x04003594 RID: 13716
		public bool dlaaSharp;

		// Token: 0x04003595 RID: 13717
		public Shader ssaaShader;

		// Token: 0x04003596 RID: 13718
		private Material ssaa;

		// Token: 0x04003597 RID: 13719
		public Shader dlaaShader;

		// Token: 0x04003598 RID: 13720
		private Material dlaa;

		// Token: 0x04003599 RID: 13721
		public Shader nfaaShader;

		// Token: 0x0400359A RID: 13722
		private Material nfaa;

		// Token: 0x0400359B RID: 13723
		public Shader shaderFXAAPreset2;

		// Token: 0x0400359C RID: 13724
		private Material materialFXAAPreset2;

		// Token: 0x0400359D RID: 13725
		public Shader shaderFXAAPreset3;

		// Token: 0x0400359E RID: 13726
		private Material materialFXAAPreset3;

		// Token: 0x0400359F RID: 13727
		public Shader shaderFXAAII;

		// Token: 0x040035A0 RID: 13728
		private Material materialFXAAII;

		// Token: 0x040035A1 RID: 13729
		public Shader shaderFXAAIII;

		// Token: 0x040035A2 RID: 13730
		private Material materialFXAAIII;
	}
}
