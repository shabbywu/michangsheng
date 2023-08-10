using System.Collections.Generic;
using UnityEngine;

public class ReplaceDuJieAnimSprite : MonoBehaviour
{
	[HideInInspector]
	public SpriteRenderer SR;

	private static Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

	public void SetSprite(string leiJie)
	{
		if ((Object)(object)SR == (Object)null)
		{
			SR = ((Component)this).GetComponent<SpriteRenderer>();
		}
		if (TianJieManager.LeiJieNames.ContainsKey(leiJie))
		{
			string text = TianJieManager.LeiJieNames[leiJie];
			string text2 = "NewUI/DuJie/" + text + "/" + ((Object)this).name + "_" + text;
			if (!sprites.ContainsKey(text2))
			{
				Sprite value = ResManager.inst.LoadSprite(text2);
				sprites[text2] = value;
			}
			SR.sprite = sprites[text2];
		}
	}
}
