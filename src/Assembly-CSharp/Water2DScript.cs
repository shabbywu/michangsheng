using System;
using UnityEngine;

// Token: 0x0200016D RID: 365
public class Water2DScript : MonoBehaviour
{
	// Token: 0x06000F8E RID: 3982 RVA: 0x0005D42A File Offset: 0x0005B62A
	private void Awake()
	{
		this.rend = base.GetComponent<Renderer>();
		this.rend.enabled = true;
		this.mat = this.rend.material;
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x0005D458 File Offset: 0x0005B658
	private void LateUpdate()
	{
		Vector2 vector = Time.deltaTime * this.speed;
		this.mat.mainTextureOffset += vector;
	}

	// Token: 0x04000BA6 RID: 2982
	public Vector2 speed = new Vector2(0.01f, 0f);

	// Token: 0x04000BA7 RID: 2983
	private Renderer rend;

	// Token: 0x04000BA8 RID: 2984
	private Material mat;
}
