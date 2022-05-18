using System;
using UnityEngine;

// Token: 0x02000132 RID: 306
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Glow")]
public class GlowEffect : MonoBehaviour
{
	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0000DB93 File Offset: 0x0000BD93
	protected Material compositeMaterial
	{
		get
		{
			if (this.m_CompositeMaterial == null)
			{
				this.m_CompositeMaterial = new Material(this.compositeShader);
				this.m_CompositeMaterial.hideFlags = 61;
			}
			return this.m_CompositeMaterial;
		}
	}

	// Token: 0x170001E7 RID: 487
	// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0000DBC7 File Offset: 0x0000BDC7
	protected Material blurMaterial
	{
		get
		{
			if (this.m_BlurMaterial == null)
			{
				this.m_BlurMaterial = new Material(this.blurShader);
				this.m_BlurMaterial.hideFlags = 61;
			}
			return this.m_BlurMaterial;
		}
	}

	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0000DBFB File Offset: 0x0000BDFB
	protected Material downsampleMaterial
	{
		get
		{
			if (this.m_DownsampleMaterial == null)
			{
				this.m_DownsampleMaterial = new Material(this.downsampleShader);
				this.m_DownsampleMaterial.hideFlags = 61;
			}
			return this.m_DownsampleMaterial;
		}
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x00093150 File Offset: 0x00091350
	protected void OnDisable()
	{
		if (this.m_CompositeMaterial)
		{
			Object.DestroyImmediate(this.m_CompositeMaterial);
		}
		if (this.m_BlurMaterial)
		{
			Object.DestroyImmediate(this.m_BlurMaterial);
		}
		if (this.m_DownsampleMaterial)
		{
			Object.DestroyImmediate(this.m_DownsampleMaterial);
		}
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x000931A8 File Offset: 0x000913A8
	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (this.downsampleShader == null)
		{
			Debug.Log("No downsample shader assigned! Disabling glow.");
			base.enabled = false;
			return;
		}
		if (!this.blurMaterial.shader.isSupported)
		{
			base.enabled = false;
		}
		if (!this.compositeMaterial.shader.isSupported)
		{
			base.enabled = false;
		}
		if (!this.downsampleMaterial.shader.isSupported)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x00093230 File Offset: 0x00091430
	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = 0.5f + (float)iteration * this.blurSpread;
		Graphics.BlitMultiTap(source, dest, this.blurMaterial, new Vector2[]
		{
			new Vector2(num, num),
			new Vector2(-num, num),
			new Vector2(num, -num),
			new Vector2(-num, -num)
		});
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x0009329C File Offset: 0x0009149C
	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		this.downsampleMaterial.color = new Color(this.glowTint.r, this.glowTint.g, this.glowTint.b, this.glowTint.a / 4f);
		Graphics.Blit(source, dest, this.downsampleMaterial);
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x000932F8 File Offset: 0x000914F8
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.glowIntensity = Mathf.Clamp(this.glowIntensity, 0f, 10f);
		this.blurIterations = Mathf.Clamp(this.blurIterations, 0, 30);
		this.blurSpread = Mathf.Clamp(this.blurSpread, 0.5f, 1f);
		RenderTexture temporary = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		this.DownSample4x(source, temporary);
		float num = Mathf.Clamp01((this.glowIntensity - 1f) / 4f);
		this.blurMaterial.color = new Color(1f, 1f, 1f, 0.25f + num);
		bool flag = true;
		for (int i = 0; i < this.blurIterations; i++)
		{
			if (flag)
			{
				this.FourTapCone(temporary, temporary2, i);
			}
			else
			{
				this.FourTapCone(temporary2, temporary, i);
			}
			flag = !flag;
		}
		Graphics.Blit(source, destination);
		if (flag)
		{
			this.BlitGlow(temporary, destination);
		}
		else
		{
			this.BlitGlow(temporary2, destination);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x0000DC2F File Offset: 0x0000BE2F
	public void BlitGlow(RenderTexture source, RenderTexture dest)
	{
		this.compositeMaterial.color = new Color(1f, 1f, 1f, Mathf.Clamp01(this.glowIntensity));
		Graphics.Blit(source, dest, this.compositeMaterial);
	}

	// Token: 0x0400086C RID: 2156
	public float glowIntensity = 1.5f;

	// Token: 0x0400086D RID: 2157
	public int blurIterations = 3;

	// Token: 0x0400086E RID: 2158
	public float blurSpread = 0.7f;

	// Token: 0x0400086F RID: 2159
	public Color glowTint = new Color(1f, 1f, 1f, 0f);

	// Token: 0x04000870 RID: 2160
	public Shader compositeShader;

	// Token: 0x04000871 RID: 2161
	private Material m_CompositeMaterial;

	// Token: 0x04000872 RID: 2162
	public Shader blurShader;

	// Token: 0x04000873 RID: 2163
	private Material m_BlurMaterial;

	// Token: 0x04000874 RID: 2164
	public Shader downsampleShader;

	// Token: 0x04000875 RID: 2165
	private Material m_DownsampleMaterial;
}
