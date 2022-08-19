using System;
using UnityEngine;

// Token: 0x020000C0 RID: 192
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Contrast Stretch")]
public class ContrastStretchEffect : MonoBehaviour
{
	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00040B90 File Offset: 0x0003ED90
	protected Material materialLum
	{
		get
		{
			if (this.m_materialLum == null)
			{
				this.m_materialLum = new Material(this.shaderLum);
				this.m_materialLum.hideFlags = 61;
			}
			return this.m_materialLum;
		}
	}

	// Token: 0x170001CA RID: 458
	// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00040BC4 File Offset: 0x0003EDC4
	protected Material materialReduce
	{
		get
		{
			if (this.m_materialReduce == null)
			{
				this.m_materialReduce = new Material(this.shaderReduce);
				this.m_materialReduce.hideFlags = 61;
			}
			return this.m_materialReduce;
		}
	}

	// Token: 0x170001CB RID: 459
	// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00040BF8 File Offset: 0x0003EDF8
	protected Material materialAdapt
	{
		get
		{
			if (this.m_materialAdapt == null)
			{
				this.m_materialAdapt = new Material(this.shaderAdapt);
				this.m_materialAdapt.hideFlags = 61;
			}
			return this.m_materialAdapt;
		}
	}

	// Token: 0x170001CC RID: 460
	// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x00040C2C File Offset: 0x0003EE2C
	protected Material materialApply
	{
		get
		{
			if (this.m_materialApply == null)
			{
				this.m_materialApply = new Material(this.shaderApply);
				this.m_materialApply.hideFlags = 61;
			}
			return this.m_materialApply;
		}
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x00040C60 File Offset: 0x0003EE60
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (!this.shaderAdapt.isSupported || !this.shaderApply.isSupported || !this.shaderLum.isSupported || !this.shaderReduce.isSupported)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x00040CB8 File Offset: 0x0003EEB8
	private void OnEnable()
	{
		for (int i = 0; i < 2; i++)
		{
			if (!this.adaptRenderTex[i])
			{
				this.adaptRenderTex[i] = new RenderTexture(1, 1, 32);
				this.adaptRenderTex[i].hideFlags = 61;
			}
		}
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x00040D00 File Offset: 0x0003EF00
	private void OnDisable()
	{
		for (int i = 0; i < 2; i++)
		{
			Object.DestroyImmediate(this.adaptRenderTex[i]);
			this.adaptRenderTex[i] = null;
		}
		if (this.m_materialLum)
		{
			Object.DestroyImmediate(this.m_materialLum);
		}
		if (this.m_materialReduce)
		{
			Object.DestroyImmediate(this.m_materialReduce);
		}
		if (this.m_materialAdapt)
		{
			Object.DestroyImmediate(this.m_materialAdapt);
		}
		if (this.m_materialApply)
		{
			Object.DestroyImmediate(this.m_materialApply);
		}
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x00040D90 File Offset: 0x0003EF90
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		RenderTexture renderTexture = RenderTexture.GetTemporary(source.width / 1, source.height / 1);
		Graphics.Blit(source, renderTexture, this.materialLum);
		while (renderTexture.width > 1 || renderTexture.height > 1)
		{
			int num = renderTexture.width / 2;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = renderTexture.height / 2;
			if (num2 < 1)
			{
				num2 = 1;
			}
			RenderTexture temporary = RenderTexture.GetTemporary(num, num2);
			Graphics.Blit(renderTexture, temporary, this.materialReduce);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
		}
		this.CalculateAdaptation(renderTexture);
		this.materialApply.SetTexture("_AdaptTex", this.adaptRenderTex[this.curAdaptIndex]);
		Graphics.Blit(source, destination, this.materialApply);
		RenderTexture.ReleaseTemporary(renderTexture);
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x00040E48 File Offset: 0x0003F048
	private void CalculateAdaptation(Texture curTexture)
	{
		int num = this.curAdaptIndex;
		this.curAdaptIndex = (this.curAdaptIndex + 1) % 2;
		float num2 = 1f - Mathf.Pow(1f - this.adaptationSpeed, 30f * Time.deltaTime);
		num2 = Mathf.Clamp(num2, 0.01f, 1f);
		this.materialAdapt.SetTexture("_CurTex", curTexture);
		this.materialAdapt.SetVector("_AdaptParams", new Vector4(num2, this.limitMinimum, this.limitMaximum, 0f));
		Graphics.Blit(this.adaptRenderTex[num], this.adaptRenderTex[this.curAdaptIndex], this.materialAdapt);
	}

	// Token: 0x040006B7 RID: 1719
	public float adaptationSpeed = 0.02f;

	// Token: 0x040006B8 RID: 1720
	public float limitMinimum = 0.2f;

	// Token: 0x040006B9 RID: 1721
	public float limitMaximum = 0.6f;

	// Token: 0x040006BA RID: 1722
	private RenderTexture[] adaptRenderTex = new RenderTexture[2];

	// Token: 0x040006BB RID: 1723
	private int curAdaptIndex;

	// Token: 0x040006BC RID: 1724
	public Shader shaderLum;

	// Token: 0x040006BD RID: 1725
	private Material m_materialLum;

	// Token: 0x040006BE RID: 1726
	public Shader shaderReduce;

	// Token: 0x040006BF RID: 1727
	private Material m_materialReduce;

	// Token: 0x040006C0 RID: 1728
	public Shader shaderAdapt;

	// Token: 0x040006C1 RID: 1729
	private Material m_materialAdapt;

	// Token: 0x040006C2 RID: 1730
	public Shader shaderApply;

	// Token: 0x040006C3 RID: 1731
	private Material m_materialApply;
}
