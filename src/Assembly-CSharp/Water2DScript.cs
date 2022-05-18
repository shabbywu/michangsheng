using System;
using UnityEngine;

// Token: 0x02000249 RID: 585
public class Water2DScript : MonoBehaviour
{
	// Token: 0x060011EC RID: 4588 RVA: 0x0001130B File Offset: 0x0000F50B
	private void Awake()
	{
		this.rend = base.GetComponent<Renderer>();
		this.rend.enabled = true;
		this.mat = this.rend.material;
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x000ACF6C File Offset: 0x000AB16C
	private void LateUpdate()
	{
		Vector2 vector = Time.deltaTime * this.speed;
		this.mat.mainTextureOffset += vector;
	}

	// Token: 0x04000E76 RID: 3702
	public Vector2 speed = new Vector2(0.01f, 0f);

	// Token: 0x04000E77 RID: 3703
	private Renderer rend;

	// Token: 0x04000E78 RID: 3704
	private Material mat;
}
