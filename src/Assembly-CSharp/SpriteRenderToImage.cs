using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000357 RID: 855
public class SpriteRenderToImage : MonoBehaviour
{
	// Token: 0x06001CEA RID: 7402 RVA: 0x000CE318 File Offset: 0x000CC518
	private void Awake()
	{
		this.image = base.GetComponent<Image>();
		this.sr = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06001CEB RID: 7403 RVA: 0x000CE334 File Offset: 0x000CC534
	private void Update()
	{
		if (this.sr.sprite != null && this.image.sprite != this.sr.sprite)
		{
			this.image.sprite = this.sr.sprite;
		}
	}

	// Token: 0x04001772 RID: 6002
	private Image image;

	// Token: 0x04001773 RID: 6003
	private SpriteRenderer sr;
}
