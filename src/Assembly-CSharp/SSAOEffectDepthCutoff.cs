using System;
using UnityEngine;

// Token: 0x020000C9 RID: 201
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Screen Space Ambient Occlusion")]
public class SSAOEffectDepthCutoff : MonoBehaviour
{
	// Token: 0x06000AE0 RID: 2784 RVA: 0x00041A2E File Offset: 0x0003FC2E
	private static Material CreateMaterial(Shader shader)
	{
		if (!shader)
		{
			return null;
		}
		return new Material(shader)
		{
			hideFlags = 61
		};
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x00041A48 File Offset: 0x0003FC48
	private static void DestroyMaterial(Material mat)
	{
		if (mat)
		{
			Object.DestroyImmediate(mat);
			mat = null;
		}
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x00041A5B File Offset: 0x0003FC5B
	private void OnDisable()
	{
		SSAOEffectDepthCutoff.DestroyMaterial(this.m_SSAOMaterial);
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x00041A68 File Offset: 0x0003FC68
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects || !SystemInfo.SupportsRenderTextureFormat(1))
		{
			this.m_Supported = false;
			base.enabled = false;
			return;
		}
		this.CreateMaterials();
		if (!this.m_SSAOMaterial || this.m_SSAOMaterial.passCount != 5)
		{
			this.m_Supported = false;
			base.enabled = false;
			return;
		}
		this.m_Supported = true;
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x00041ACA File Offset: 0x0003FCCA
	private void OnEnable()
	{
		base.GetComponent<Camera>().depthTextureMode |= 2;
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x00041AE0 File Offset: 0x0003FCE0
	private void CreateMaterials()
	{
		if (!this.m_SSAOMaterial && this.m_SSAOShader.isSupported)
		{
			this.m_SSAOMaterial = SSAOEffectDepthCutoff.CreateMaterial(this.m_SSAOShader);
			this.m_SSAOMaterial.SetTexture("_RandomTexture", this.m_RandomTexture);
		}
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x00041B30 File Offset: 0x0003FD30
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.m_Supported || !this.m_SSAOShader.isSupported)
		{
			base.enabled = false;
			return;
		}
		this.CreateMaterials();
		this.m_Downsampling = Mathf.Clamp(this.m_Downsampling, 1, 6);
		this.m_Radius = Mathf.Clamp(this.m_Radius, 0.05f, 1f);
		this.m_MinZ = Mathf.Clamp(this.m_MinZ, 1E-05f, 0.5f);
		this.m_OcclusionIntensity = Mathf.Clamp(this.m_OcclusionIntensity, 0.5f, 4f);
		this.m_OcclusionAttenuation = Mathf.Clamp(this.m_OcclusionAttenuation, 0.2f, 2f);
		this.m_Blur = Mathf.Clamp(this.m_Blur, 0, 4);
		RenderTexture renderTexture = RenderTexture.GetTemporary(source.width / this.m_Downsampling, source.height / this.m_Downsampling, 0);
		float fieldOfView = base.GetComponent<Camera>().fieldOfView;
		float farClipPlane = base.GetComponent<Camera>().farClipPlane;
		float num = Mathf.Tan(fieldOfView * 0.017453292f * 0.5f) * farClipPlane;
		float num2 = num * base.GetComponent<Camera>().aspect;
		this.m_SSAOMaterial.SetVector("_FarCorner", new Vector3(num2, num, farClipPlane));
		int num3;
		int num4;
		if (this.m_RandomTexture)
		{
			num3 = this.m_RandomTexture.width;
			num4 = this.m_RandomTexture.height;
		}
		else
		{
			num3 = 1;
			num4 = 1;
		}
		this.m_SSAOMaterial.SetVector("_NoiseScale", new Vector3((float)renderTexture.width / (float)num3, (float)renderTexture.height / (float)num4, 0f));
		this.m_SSAOMaterial.SetVector("_Params", new Vector4(this.m_Radius, this.m_MinZ, 1f / this.m_OcclusionAttenuation, this.m_OcclusionIntensity));
		this.m_SSAOMaterial.SetFloat("_DepthCutoff", this.m_DepthCutoff);
		bool flag = this.m_Blur > 0;
		Graphics.Blit(flag ? null : source, renderTexture, this.m_SSAOMaterial, (int)this.m_SampleCount);
		if (flag)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0);
			this.m_SSAOMaterial.SetVector("_TexelOffsetScale", new Vector4((float)this.m_Blur / (float)source.width, 0f, 0f, 0f));
			this.m_SSAOMaterial.SetTexture("_SSAO", renderTexture);
			Graphics.Blit(null, temporary, this.m_SSAOMaterial, 3);
			RenderTexture.ReleaseTemporary(renderTexture);
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width, source.height, 0);
			this.m_SSAOMaterial.SetVector("_TexelOffsetScale", new Vector4(0f, (float)this.m_Blur / (float)source.height, 0f, 0f));
			this.m_SSAOMaterial.SetTexture("_SSAO", temporary);
			Graphics.Blit(source, temporary2, this.m_SSAOMaterial, 3);
			RenderTexture.ReleaseTemporary(temporary);
			renderTexture = temporary2;
		}
		this.m_SSAOMaterial.SetTexture("_SSAO", renderTexture);
		Graphics.Blit(source, destination, this.m_SSAOMaterial, 4);
		RenderTexture.ReleaseTemporary(renderTexture);
	}

	// Token: 0x040006E8 RID: 1768
	public float m_Radius = 0.4f;

	// Token: 0x040006E9 RID: 1769
	public SSAOEffectDepthCutoff.SSAOSamples m_SampleCount = SSAOEffectDepthCutoff.SSAOSamples.Medium;

	// Token: 0x040006EA RID: 1770
	public float m_OcclusionIntensity = 1.5f;

	// Token: 0x040006EB RID: 1771
	public int m_Blur = 2;

	// Token: 0x040006EC RID: 1772
	public int m_Downsampling = 2;

	// Token: 0x040006ED RID: 1773
	public float m_OcclusionAttenuation = 1f;

	// Token: 0x040006EE RID: 1774
	public float m_MinZ = 0.01f;

	// Token: 0x040006EF RID: 1775
	public float m_DepthCutoff = 50f;

	// Token: 0x040006F0 RID: 1776
	public Shader m_SSAOShader;

	// Token: 0x040006F1 RID: 1777
	private Material m_SSAOMaterial;

	// Token: 0x040006F2 RID: 1778
	public Texture2D m_RandomTexture;

	// Token: 0x040006F3 RID: 1779
	private bool m_Supported;

	// Token: 0x02001234 RID: 4660
	public enum SSAOSamples
	{
		// Token: 0x040064F7 RID: 25847
		Low,
		// Token: 0x040064F8 RID: 25848
		Medium,
		// Token: 0x040064F9 RID: 25849
		High
	}
}
