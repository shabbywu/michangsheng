using System;
using UnityEngine;

// Token: 0x0200012E RID: 302
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Blur")]
public class BlurEffect : MonoBehaviour
{
	// Token: 0x170001E1 RID: 481
	// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0000D990 File Offset: 0x0000BB90
	protected Material material
	{
		get
		{
			if (BlurEffect.m_Material == null)
			{
				BlurEffect.m_Material = new Material(this.blurShader);
				BlurEffect.m_Material.hideFlags = 52;
			}
			return BlurEffect.m_Material;
		}
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
	protected void OnDisable()
	{
		if (BlurEffect.m_Material)
		{
			Object.DestroyImmediate(BlurEffect.m_Material);
		}
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x0000D9D8 File Offset: 0x0000BBD8
	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (!this.blurShader || !this.material.shader.isSupported)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x00092D54 File Offset: 0x00090F54
	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = 0.5f + (float)iteration * this.blurSpread;
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x00092DC0 File Offset: 0x00090FC0
	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		float num = 1f;
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x00092E24 File Offset: 0x00091024
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		RenderTexture temporary = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		this.DownSample4x(source, temporary);
		bool flag = true;
		for (int i = 0; i < this.iterations; i++)
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
		if (flag)
		{
			Graphics.Blit(temporary, destination);
		}
		else
		{
			Graphics.Blit(temporary2, destination);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	// Token: 0x04000859 RID: 2137
	public int iterations = 3;

	// Token: 0x0400085A RID: 2138
	public float blurSpread = 0.6f;

	// Token: 0x0400085B RID: 2139
	public Shader blurShader;

	// Token: 0x0400085C RID: 2140
	private static Material m_Material;
}
