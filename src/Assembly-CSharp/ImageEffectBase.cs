using System;
using UnityEngine;

// Token: 0x020000C4 RID: 196
[RequireComponent(typeof(Camera))]
[AddComponentMenu("")]
public class ImageEffectBase : MonoBehaviour
{
	// Token: 0x06000ACC RID: 2764 RVA: 0x0004139E File Offset: 0x0003F59E
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

	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x06000ACD RID: 2765 RVA: 0x000413D0 File Offset: 0x0003F5D0
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

	// Token: 0x06000ACE RID: 2766 RVA: 0x00041404 File Offset: 0x0003F604
	protected virtual void OnDisable()
	{
		if (this.m_Material)
		{
			Object.DestroyImmediate(this.m_Material);
		}
	}

	// Token: 0x040006D1 RID: 1745
	public Shader shader;

	// Token: 0x040006D2 RID: 1746
	private Material m_Material;
}
