using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000270 RID: 624
public class UINPCHeadFavor : MonoBehaviour
{
	// Token: 0x0600169A RID: 5786 RVA: 0x0009A278 File Offset: 0x00098478
	public void SetFavor(int favor)
	{
		int favorIndex = UINPCHeadFavor.GetFavorIndex(favor);
		this.FavorText.text = UINPCHeadFavor.favorStrList[favorIndex];
		this.FavorImage.sprite = this.FavorSpriteList[favorIndex];
	}

	// Token: 0x0600169B RID: 5787 RVA: 0x0009A2BC File Offset: 0x000984BC
	private static void InitFavor()
	{
		foreach (JSONObject jsonobject in jsonData.instance.NpcHaoGanDuData.list)
		{
			UINPCHeadFavor.favorQuJianList.Add(jsonobject["QuJian"].list[0].I);
			UINPCHeadFavor.favorStrList.Add(jsonobject["HaoGanDu"].Str);
		}
	}

	// Token: 0x0600169C RID: 5788 RVA: 0x0009A350 File Offset: 0x00098550
	public static int GetFavorLevel(int favor)
	{
		return UINPCHeadFavor.GetFavorIndex(favor) + 1;
	}

	// Token: 0x0600169D RID: 5789 RVA: 0x0009A35C File Offset: 0x0009855C
	public static int GetFavorIndex(int favor)
	{
		if (UINPCHeadFavor.favorStrList.Count == 0)
		{
			UINPCHeadFavor.InitFavor();
		}
		int result = 0;
		int num = 1;
		while (num < UINPCHeadFavor.favorQuJianList.Count && favor >= UINPCHeadFavor.favorQuJianList[num])
		{
			result = num;
			num++;
		}
		return result;
	}

	// Token: 0x04001112 RID: 4370
	public Image FavorImage;

	// Token: 0x04001113 RID: 4371
	public Text FavorText;

	// Token: 0x04001114 RID: 4372
	private static List<string> favorStrList = new List<string>();

	// Token: 0x04001115 RID: 4373
	private static List<int> favorQuJianList = new List<int>();

	// Token: 0x04001116 RID: 4374
	public List<Sprite> FavorSpriteList = new List<Sprite>();
}
