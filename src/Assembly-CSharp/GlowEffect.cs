using System;
using UnityEngine;

// Token: 0x020000C2 RID: 194
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Glow")]
public class GlowEffect : MonoBehaviour
{
	// Token: 0x170001CD RID: 461
	// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00040F6C File Offset: 0x0003F16C
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

	// Token: 0x170001CE RID: 462
	// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00040FA0 File Offset: 0x0003F1A0
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

	// Token: 0x170001CF RID: 463
	// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00040FD4 File Offset: 0x0003F1D4
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

	// Token: 0x06000AC3 RID: 2755 RVA: 0x00041008 File Offset: 0x0003F208
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

	// Token: 0x06000AC4 RID: 2756 RVA: 0x00041060 File Offset: 0x0003F260
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

	// Token: 0x06000AC5 RID: 2757 RVA: 0x000410E8 File Offset: 0x0003F2E8
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

	// Token: 0x06000AC6 RID: 2758 RVA: 0x00041154 File Offset: 0x0003F354
	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		this.downsampleMaterial.color = new Color(this.glowTint.r, this.glowTint.g, this.glowTint.b, this.glowTint.a / 4f);
		Graphics.Blit(source, dest, this.downsampleMaterial);
	}

	// Token: 0x06000AC7 RID: 2759 RVA: 0x000411B0 File Offset: 0x0003F3B0
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

	// Token: 0x06000AC8 RID: 2760 RVA: 0x000412D8 File Offset: 0x0003F4D8
	public void BlitGlow(RenderTexture source, RenderTexture dest)
	{
		this.compositeMaterial.color = new Color(1f, 1f, 1f, Mathf.Clamp01(this.glowIntensity));
		Graphics.Blit(source, dest, this.compositeMaterial);
	}

	// Token: 0x040006C5 RID: 1733
	public float glowIntensity = 1.5f;

	// Token: 0x040006C6 RID: 1734
	public int blurIterations = 3;

	// Token: 0x040006C7 RID: 1735
	public float blurSpread = 0.7f;

	// Token: 0x040006C8 RID: 1736
	public Color glowTint = new Color(1f, 1f, 1f, 0f);

	// Token: 0x040006C9 RID: 1737
	public Shader compositeShader;

	// Token: 0x040006CA RID: 1738
	private Material m_CompositeMaterial;

	// Token: 0x040006CB RID: 1739
	public Shader blurShader;

	// Token: 0x040006CC RID: 1740
	private Material m_BlurMaterial;

	// Token: 0x040006CD RID: 1741
	public Shader downsampleShader;

	// Token: 0x040006CE RID: 1742
	private Material m_DownsampleMaterial;
}
