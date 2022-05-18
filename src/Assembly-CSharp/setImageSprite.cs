using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000246 RID: 582
public class setImageSprite : MonoBehaviour
{
	// Token: 0x060011E5 RID: 4581 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x000112C7 File Offset: 0x0000F4C7
	private void Update()
	{
		if (this.image.sprite != this.sprite.sprite)
		{
			this.image.sprite = this.sprite.sprite;
		}
	}

	// Token: 0x04000E73 RID: 3699
	public Image image;

	// Token: 0x04000E74 RID: 3700
	public SpriteRenderer sprite;
}
