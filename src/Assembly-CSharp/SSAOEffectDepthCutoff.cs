using System;
using UnityEngine;

// Token: 0x02000139 RID: 313
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Screen Space Ambient Occlusion")]
public class SSAOEffectDepthCutoff : MonoBehaviour
{
	// Token: 0x06000BC3 RID: 3011 RVA: 0x0000DDB4 File Offset: 0x0000BFB4
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

	// Token: 0x06000BC4 RID: 3012 RVA: 0x0000DDCE File Offset: 0x0000BFCE
	private static void DestroyMaterial(Material mat)
	{
		if (mat)
		{
			Object.DestroyImmediate(mat);
			mat = null;
		}
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x0000DDE1 File Offset: 0x0000BFE1
	private void OnDisable()
	{
		SSAOEffectDepthCutoff.DestroyMaterial(this.m_SSAOMaterial);
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x000939F4 File Offset: 0x00091BF4
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

	// Token: 0x06000BC7 RID: 3015 RVA: 0x0000DDEE File Offset: 0x0000BFEE
	private void OnEnable()
	{
		base.GetComponent<Camera>().depthTextureMode |= 2;
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x00093A58 File Offset: 0x00091C58
	private void CreateMaterials()
	{
		if (!this.m_SSAOMaterial && this.m_SSAOShader.isSupported)
		{
			this.m_SSAOMaterial = SSAOEffectDepthCutoff.CreateMaterial(this.m_SSAOShader);
			this.m_SSAOMaterial.SetTexture("_RandomTexture", this.m_RandomTexture);
		}
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x00093AA8 File Offset: 0x00091CA8
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

	// Token: 0x0400088F RID: 2191
	public float m_Radius = 0.4f;

	// Token: 0x04000890 RID: 2192
	public SSAOEffectDepthCutoff.SSAOSamples m_SampleCount = SSAOEffectDepthCutoff.SSAOSamples.Medium;

	// Token: 0x04000891 RID: 2193
	public float m_OcclusionIntensity = 1.5f;

	// Token: 0x04000892 RID: 2194
	public int m_Blur = 2;

	// Token: 0x04000893 RID: 2195
	public int m_Downsampling = 2;

	// Token: 0x04000894 RID: 2196
	public float m_OcclusionAttenuation = 1f;

	// Token: 0x04000895 RID: 2197
	public float m_MinZ = 0.01f;

	// Token: 0x04000896 RID: 2198
	public float m_DepthCutoff = 50f;

	// Token: 0x04000897 RID: 2199
	public Shader m_SSAOShader;

	// Token: 0x04000898 RID: 2200
	private Material m_SSAOMaterial;

	// Token: 0x04000899 RID: 2201
	public Texture2D m_RandomTexture;

	// Token: 0x0400089A RID: 2202
	private bool m_Supported;

	// Token: 0x0200013A RID: 314
	public enum SSAOSamples
	{
		// Token: 0x0400089C RID: 2204
		Low,
		// Token: 0x0400089D RID: 2205
		Medium,
		// Token: 0x0400089E RID: 2206
		High
	}
}
