using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004D4 RID: 1236
public class SpriteRenderToImage : MonoBehaviour
{
	// Token: 0x06002054 RID: 8276 RVA: 0x0001A8D3 File Offset: 0x00018AD3
	private void Awake()
	{
		this.image = base.GetComponent<Image>();
		this.sr = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06002055 RID: 8277 RVA: 0x00113118 File Offset: 0x00111318
	private void Update()
	{
		if (this.sr.sprite != null && this.image.sprite != this.sr.sprite)
		{
			this.image.sprite = this.sr.sprite;
		}
	}

	// Token: 0x04001BCA RID: 7114
	private Image image;

	// Token: 0x04001BCB RID: 7115
	private SpriteRenderer sr;
}
