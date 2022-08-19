using System;
using UnityEngine;

// Token: 0x020000BE RID: 190
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Blur")]
public class BlurEffect : MonoBehaviour
{
	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0004096D File Offset: 0x0003EB6D
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

	// Token: 0x06000AAB RID: 2731 RVA: 0x0004099D File Offset: 0x0003EB9D
	protected void OnDisable()
	{
		if (BlurEffect.m_Material)
		{
			Object.DestroyImmediate(BlurEffect.m_Material);
		}
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x000409B5 File Offset: 0x0003EBB5
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

	// Token: 0x06000AAD RID: 2733 RVA: 0x000409F0 File Offset: 0x0003EBF0
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

	// Token: 0x06000AAE RID: 2734 RVA: 0x00040A5C File Offset: 0x0003EC5C
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

	// Token: 0x06000AAF RID: 2735 RVA: 0x00040AC0 File Offset: 0x0003ECC0
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

	// Token: 0x040006B2 RID: 1714
	public int iterations = 3;

	// Token: 0x040006B3 RID: 1715
	public float blurSpread = 0.6f;

	// Token: 0x040006B4 RID: 1716
	public Shader blurShader;

	// Token: 0x040006B5 RID: 1717
	private static Material m_Material;
}
