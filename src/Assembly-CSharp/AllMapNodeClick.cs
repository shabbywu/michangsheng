using System;
using UnityEngine;

// Token: 0x0200025A RID: 602
public class AllMapNodeClick : MonoBehaviour
{
	// Token: 0x06001291 RID: 4753 RVA: 0x00011A8F File Offset: 0x0000FC8F
	protected virtual void OnMouseDown()
	{
		if (base.gameObject.GetComponent<MapComponent>().CanClick())
		{
			base.transform.localScale = new Vector3(0.42f, 0.42f, 0.42f);
		}
	}

	// Token: 0x06001292 RID: 4754 RVA: 0x00011AC2 File Offset: 0x0000FCC2
	protected virtual void OnMouseUp()
	{
		base.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
	}

	// Token: 0x06001293 RID: 4755 RVA: 0x00011AE3 File Offset: 0x0000FCE3
	protected virtual void OnMouseEnter()
	{
		if (this.OnHoverSprite != null)
		{
			this.sprite.sprite = this.OnHoverSprite;
		}
	}

	// Token: 0x06001294 RID: 4756 RVA: 0x00011B04 File Offset: 0x0000FD04
	protected virtual void OnMouseExit()
	{
		if (this.StartSprite != null)
		{
			this.sprite.sprite = this.StartSprite;
		}
	}

	// Token: 0x06001295 RID: 4757 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06001296 RID: 4758 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04000EA3 RID: 3747
	public SpriteRenderer sprite;

	// Token: 0x04000EA4 RID: 3748
	public Sprite StartSprite;

	// Token: 0x04000EA5 RID: 3749
	public Sprite OnHoverSprite;
}
