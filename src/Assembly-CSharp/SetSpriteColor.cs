using System;
using UnityEngine;

// Token: 0x02000425 RID: 1061
public class SetSpriteColor : MonoBehaviour
{
	// Token: 0x060021F0 RID: 8688 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060021F1 RID: 8689 RVA: 0x000E9EAA File Offset: 0x000E80AA
	protected virtual void OnMouseEnter()
	{
		this.sprite.color = this.onHover;
	}

	// Token: 0x060021F2 RID: 8690 RVA: 0x000E9EBD File Offset: 0x000E80BD
	protected virtual void OnMouseExit()
	{
		this.sprite.color = this.Namel;
	}

	// Token: 0x04001B5A RID: 7002
	public Color Namel;

	// Token: 0x04001B5B RID: 7003
	public Color onHover;

	// Token: 0x04001B5C RID: 7004
	public SpriteRenderer sprite;
}
