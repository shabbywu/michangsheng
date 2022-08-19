using System;
using UnityEngine;

// Token: 0x020001F2 RID: 498
[RequireComponent(typeof(SpriteRenderer))]
public class ModSpriteRenderer : MonoBehaviour
{
	// Token: 0x06001483 RID: 5251 RVA: 0x00083992 File Offset: 0x00081B92
	private void Awake()
	{
		this.sr = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06001484 RID: 5252 RVA: 0x000839A0 File Offset: 0x00081BA0
	public void Refresh()
	{
		this.sr.sprite = ModResources.LoadSprite(this.SpritePath);
	}

	// Token: 0x06001485 RID: 5253 RVA: 0x000839B8 File Offset: 0x00081BB8
	private void Start()
	{
		this.Refresh();
	}

	// Token: 0x04000F3E RID: 3902
	private SpriteRenderer sr;

	// Token: 0x04000F3F RID: 3903
	public string SpritePath;
}
