using System;
using UnityEngine;

// Token: 0x020005D9 RID: 1497
public class SetSpriteColor : MonoBehaviour
{
	// Token: 0x060025AA RID: 9642 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060025AB RID: 9643 RVA: 0x0001E2A9 File Offset: 0x0001C4A9
	protected virtual void OnMouseEnter()
	{
		this.sprite.color = this.onHover;
	}

	// Token: 0x060025AC RID: 9644 RVA: 0x0001E2BC File Offset: 0x0001C4BC
	protected virtual void OnMouseExit()
	{
		this.sprite.color = this.Namel;
	}

	// Token: 0x04002020 RID: 8224
	public Color Namel;

	// Token: 0x04002021 RID: 8225
	public Color onHover;

	// Token: 0x04002022 RID: 8226
	public SpriteRenderer sprite;
}
