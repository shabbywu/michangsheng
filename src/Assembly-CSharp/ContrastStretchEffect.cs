using System;
using UnityEngine;

// Token: 0x02000130 RID: 304
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Contrast Stretch")]
public class ContrastStretchEffect : MonoBehaviour
{
	// Token: 0x170001E2 RID: 482
	// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0000DA4F File Offset: 0x0000BC4F
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

	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0000DA83 File Offset: 0x0000BC83
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

	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0000DAB7 File Offset: 0x0000BCB7
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

	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0000DAEB File Offset: 0x0000BCEB
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

	// Token: 0x06000B9B RID: 2971 RVA: 0x00092EB8 File Offset: 0x000910B8
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

	// Token: 0x06000B9C RID: 2972 RVA: 0x00092F10 File Offset: 0x00091110
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

	// Token: 0x06000B9D RID: 2973 RVA: 0x00092F58 File Offset: 0x00091158
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

	// Token: 0x06000B9E RID: 2974 RVA: 0x00092FE8 File Offset: 0x000911E8
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

	// Token: 0x06000B9F RID: 2975 RVA: 0x000930A0 File Offset: 0x000912A0
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

	// Token: 0x0400085E RID: 2142
	public float adaptationSpeed = 0.02f;

	// Token: 0x0400085F RID: 2143
	public float limitMinimum = 0.2f;

	// Token: 0x04000860 RID: 2144
	public float limitMaximum = 0.6f;

	// Token: 0x04000861 RID: 2145
	private RenderTexture[] adaptRenderTex = new RenderTexture[2];

	// Token: 0x04000862 RID: 2146
	private int curAdaptIndex;

	// Token: 0x04000863 RID: 2147
	public Shader shaderLum;

	// Token: 0x04000864 RID: 2148
	private Material m_materialLum;

	// Token: 0x04000865 RID: 2149
	public Shader shaderReduce;

	// Token: 0x04000866 RID: 2150
	private Material m_materialReduce;

	// Token: 0x04000867 RID: 2151
	public Shader shaderAdapt;

	// Token: 0x04000868 RID: 2152
	private Material m_materialAdapt;

	// Token: 0x04000869 RID: 2153
	public Shader shaderApply;

	// Token: 0x0400086A RID: 2154
	private Material m_materialApply;
}
