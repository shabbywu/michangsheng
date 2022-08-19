using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003CA RID: 970
public class LoadingScreenImages : MonoBehaviour
{
	// Token: 0x06001FA9 RID: 8105 RVA: 0x000DF49B File Offset: 0x000DD69B
	private void Awake()
	{
		this.LoadSprites();
		this.LS = base.GetComponent<LoadingScreen>();
		this.LS.LoadingScreenImages = this.sprites.ToArray();
	}

	// Token: 0x06001FAA RID: 8106 RVA: 0x000DF4C8 File Offset: 0x000DD6C8
	public void LoadSprites()
	{
		foreach (string bgName in this.imageNames)
		{
			Sprite sprite = BackGroundImage.GetSprite(bgName);
			if (sprite != null)
			{
				this.sprites.Add(sprite);
			}
		}
	}

	// Token: 0x040019BA RID: 6586
	public List<string> imageNames = new List<string>();

	// Token: 0x040019BB RID: 6587
	[HideInInspector]
	public List<Sprite> sprites = new List<Sprite>();

	// Token: 0x040019BC RID: 6588
	private LoadingScreen LS;
}
