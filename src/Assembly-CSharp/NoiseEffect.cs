using System;
using UnityEngine;

// Token: 0x02000137 RID: 311
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Noise")]
public class NoiseEffect : MonoBehaviour
{
	// Token: 0x06000BBB RID: 3003 RVA: 0x00093644 File Offset: 0x00091844
	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (this.shaderRGB == null || this.shaderYUV == null)
		{
			Debug.Log("Noise shaders are not set up! Disabling noise effect.");
			base.enabled = false;
			return;
		}
		if (!this.shaderRGB.isSupported)
		{
			base.enabled = false;
			return;
		}
		if (!this.shaderYUV.isSupported)
		{
			this.rgbFallback = true;
		}
	}

	// Token: 0x170001EA RID: 490
	// (get) Token: 0x06000BBC RID: 3004 RVA: 0x000936B8 File Offset: 0x000918B8
	protected Material material
	{
		get
		{
			if (this.m_MaterialRGB == null)
			{
				this.m_MaterialRGB = new Material(this.shaderRGB);
				this.m_MaterialRGB.hideFlags = 61;
			}
			if (this.m_MaterialYUV == null && !this.rgbFallback)
			{
				this.m_MaterialYUV = new Material(this.shaderYUV);
				this.m_MaterialYUV.hideFlags = 61;
			}
			if (this.rgbFallback || this.monochrome)
			{
				return this.m_MaterialRGB;
			}
			return this.m_MaterialYUV;
		}
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x0000DD73 File Offset: 0x0000BF73
	protected void OnDisable()
	{
		if (this.m_MaterialRGB)
		{
			Object.DestroyImmediate(this.m_MaterialRGB);
		}
		if (this.m_MaterialYUV)
		{
			Object.DestroyImmediate(this.m_MaterialYUV);
		}
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x00093744 File Offset: 0x00091944
	private void SanitizeParameters()
	{
		this.grainIntensityMin = Mathf.Clamp(this.grainIntensityMin, 0f, 5f);
		this.grainIntensityMax = Mathf.Clamp(this.grainIntensityMax, 0f, 5f);
		this.scratchIntensityMin = Mathf.Clamp(this.scratchIntensityMin, 0f, 5f);
		this.scratchIntensityMax = Mathf.Clamp(this.scratchIntensityMax, 0f, 5f);
		this.scratchFPS = Mathf.Clamp(this.scratchFPS, 1f, 30f);
		this.scratchJitter = Mathf.Clamp(this.scratchJitter, 0f, 1f);
		this.grainSize = Mathf.Clamp(this.grainSize, 0.1f, 50f);
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x00093810 File Offset: 0x00091A10
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.SanitizeParameters();
		if (this.scratchTimeLeft <= 0f)
		{
			this.scratchTimeLeft = Random.value * 2f / this.scratchFPS;
			this.scratchX = Random.value;
			this.scratchY = Random.value;
		}
		this.scratchTimeLeft -= Time.deltaTime;
		Material material = this.material;
		material.SetTexture("_GrainTex", this.grainTexture);
		material.SetTexture("_ScratchTex", this.scratchTexture);
		float num = 1f / this.grainSize;
		material.SetVector("_GrainOffsetScale", new Vector4(Random.value, Random.value, (float)Screen.width / (float)this.grainTexture.width * num, (float)Screen.height / (float)this.grainTexture.height * num));
		material.SetVector("_ScratchOffsetScale", new Vector4(this.scratchX + Random.value * this.scratchJitter, this.scratchY + Random.value * this.scratchJitter, (float)Screen.width / (float)this.scratchTexture.width, (float)Screen.height / (float)this.scratchTexture.height));
		material.SetVector("_Intensity", new Vector4(Random.Range(this.grainIntensityMin, this.grainIntensityMax), Random.Range(this.scratchIntensityMin, this.scratchIntensityMax), 0f, 0f));
		Graphics.Blit(source, destination, material);
	}

	// Token: 0x0400087D RID: 2173
	public bool monochrome = true;

	// Token: 0x0400087E RID: 2174
	private bool rgbFallback;

	// Token: 0x0400087F RID: 2175
	public float grainIntensityMin = 0.1f;

	// Token: 0x04000880 RID: 2176
	public float grainIntensityMax = 0.2f;

	// Token: 0x04000881 RID: 2177
	public float grainSize = 2f;

	// Token: 0x04000882 RID: 2178
	public float scratchIntensityMin = 0.05f;

	// Token: 0x04000883 RID: 2179
	public float scratchIntensityMax = 0.25f;

	// Token: 0x04000884 RID: 2180
	public float scratchFPS = 10f;

	// Token: 0x04000885 RID: 2181
	public float scratchJitter = 0.01f;

	// Token: 0x04000886 RID: 2182
	public Texture grainTexture;

	// Token: 0x04000887 RID: 2183
	public Texture scratchTexture;

	// Token: 0x04000888 RID: 2184
	public Shader shaderRGB;

	// Token: 0x04000889 RID: 2185
	public Shader shaderYUV;

	// Token: 0x0400088A RID: 2186
	private Material m_MaterialRGB;

	// Token: 0x0400088B RID: 2187
	private Material m_MaterialYUV;

	// Token: 0x0400088C RID: 2188
	private float scratchTimeLeft;

	// Token: 0x0400088D RID: 2189
	private float scratchX;

	// Token: 0x0400088E RID: 2190
	private float scratchY;
}
