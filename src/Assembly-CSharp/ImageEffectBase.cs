using System;
using UnityEngine;

// Token: 0x02000134 RID: 308
[RequireComponent(typeof(Camera))]
[AddComponentMenu("")]
public class ImageEffectBase : MonoBehaviour
{
	// Token: 0x06000BAF RID: 2991 RVA: 0x0000DCA3 File Offset: 0x0000BEA3
	protected virtual void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (!this.shader || !this.shader.isSupported)
		{
			base.enabled = false;
		}
	}

	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0000DCD5 File Offset: 0x0000BED5
	protected Material material
	{
		get
		{
			if (this.m_Material == null)
			{
				this.m_Material = new Material(this.shader);
				this.m_Material.hideFlags = 61;
			}
			return this.m_Material;
		}
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x0000DD09 File Offset: 0x0000BF09
	protected virtual void OnDisable()
	{
		if (this.m_Material)
		{
			Object.DestroyImmediate(this.m_Material);
		}
	}

	// Token: 0x04000878 RID: 2168
	public Shader shader;

	// Token: 0x04000879 RID: 2169
	private Material m_Material;
}
