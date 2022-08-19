using System;
using UnityEngine;

// Token: 0x0200017D RID: 381
public class AllMapNodeClick : MonoBehaviour
{
	// Token: 0x0600103F RID: 4159 RVA: 0x0005FA41 File Offset: 0x0005DC41
	protected virtual void OnMouseDown()
	{
		if (base.gameObject.GetComponent<MapComponent>().CanClick())
		{
			base.transform.localScale = new Vector3(0.42f, 0.42f, 0.42f);
		}
	}

	// Token: 0x06001040 RID: 4160 RVA: 0x0005FA74 File Offset: 0x0005DC74
	protected virtual void OnMouseUp()
	{
		base.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
	}

	// Token: 0x06001041 RID: 4161 RVA: 0x0005FA95 File Offset: 0x0005DC95
	protected virtual void OnMouseEnter()
	{
		if (this.OnHoverSprite != null)
		{
			this.sprite.sprite = this.OnHoverSprite;
		}
	}

	// Token: 0x06001042 RID: 4162 RVA: 0x0005FAB6 File Offset: 0x0005DCB6
	protected virtual void OnMouseExit()
	{
		if (this.StartSprite != null)
		{
			this.sprite.sprite = this.StartSprite;
		}
	}

	// Token: 0x06001043 RID: 4163 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06001044 RID: 4164 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000BD1 RID: 3025
	public SpriteRenderer sprite;

	// Token: 0x04000BD2 RID: 3026
	public Sprite StartSprite;

	// Token: 0x04000BD3 RID: 3027
	public Sprite OnHoverSprite;
}
