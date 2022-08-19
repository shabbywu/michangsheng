using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200016A RID: 362
public class setImageSprite : MonoBehaviour
{
	// Token: 0x06000F87 RID: 3975 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x0005D3E6 File Offset: 0x0005B5E6
	private void Update()
	{
		if (this.image.sprite != this.sprite.sprite)
		{
			this.image.sprite = this.sprite.sprite;
		}
	}

	// Token: 0x04000BA3 RID: 2979
	public Image image;

	// Token: 0x04000BA4 RID: 2980
	public SpriteRenderer sprite;
}
