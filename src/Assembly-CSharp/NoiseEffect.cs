using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Noise")]
public class NoiseEffect : MonoBehaviour
{
	// Token: 0x06000AD8 RID: 2776 RVA: 0x00041640 File Offset: 0x0003F840
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

	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x000416B4 File Offset: 0x0003F8B4
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

	// Token: 0x06000ADA RID: 2778 RVA: 0x0004173E File Offset: 0x0003F93E
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

	// Token: 0x06000ADB RID: 2779 RVA: 0x00041770 File Offset: 0x0003F970
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

	// Token: 0x06000ADC RID: 2780 RVA: 0x0004183C File Offset: 0x0003FA3C
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

	// Token: 0x040006D6 RID: 1750
	public bool monochrome = true;

	// Token: 0x040006D7 RID: 1751
	private bool rgbFallback;

	// Token: 0x040006D8 RID: 1752
	public float grainIntensityMin = 0.1f;

	// Token: 0x040006D9 RID: 1753
	public float grainIntensityMax = 0.2f;

	// Token: 0x040006DA RID: 1754
	public float grainSize = 2f;

	// Token: 0x040006DB RID: 1755
	public float scratchIntensityMin = 0.05f;

	// Token: 0x040006DC RID: 1756
	public float scratchIntensityMax = 0.25f;

	// Token: 0x040006DD RID: 1757
	public float scratchFPS = 10f;

	// Token: 0x040006DE RID: 1758
	public float scratchJitter = 0.01f;

	// Token: 0x040006DF RID: 1759
	public Texture grainTexture;

	// Token: 0x040006E0 RID: 1760
	public Texture scratchTexture;

	// Token: 0x040006E1 RID: 1761
	public Shader shaderRGB;

	// Token: 0x040006E2 RID: 1762
	public Shader shaderYUV;

	// Token: 0x040006E3 RID: 1763
	private Material m_MaterialRGB;

	// Token: 0x040006E4 RID: 1764
	private Material m_MaterialYUV;

	// Token: 0x040006E5 RID: 1765
	private float scratchTimeLeft;

	// Token: 0x040006E6 RID: 1766
	private float scratchX;

	// Token: 0x040006E7 RID: 1767
	private float scratchY;
}
