using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002AC RID: 684
public class ReplaceDuJieAnimSprite : MonoBehaviour
{
	// Token: 0x0600182D RID: 6189 RVA: 0x000A8C28 File Offset: 0x000A6E28
	public void SetSprite(string leiJie)
	{
		if (this.SR == null)
		{
			this.SR = base.GetComponent<SpriteRenderer>();
		}
		if (TianJieManager.LeiJieNames.ContainsKey(leiJie))
		{
			string text = TianJieManager.LeiJieNames[leiJie];
			string text2 = string.Concat(new string[]
			{
				"NewUI/DuJie/",
				text,
				"/",
				base.name,
				"_",
				text
			});
			if (!ReplaceDuJieAnimSprite.sprites.ContainsKey(text2))
			{
				Sprite value = ResManager.inst.LoadSprite(text2);
				ReplaceDuJieAnimSprite.sprites[text2] = value;
			}
			this.SR.sprite = ReplaceDuJieAnimSprite.sprites[text2];
		}
	}

	// Token: 0x0400133C RID: 4924
	[HideInInspector]
	public SpriteRenderer SR;

	// Token: 0x0400133D RID: 4925
	private static Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
}
