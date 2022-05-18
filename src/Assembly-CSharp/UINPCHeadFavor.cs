using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000388 RID: 904
public class UINPCHeadFavor : MonoBehaviour
{
	// Token: 0x0600194C RID: 6476 RVA: 0x000E2164 File Offset: 0x000E0364
	public void SetFavor(int favor)
	{
		int favorIndex = UINPCHeadFavor.GetFavorIndex(favor);
		this.FavorText.text = UINPCHeadFavor.favorStrList[favorIndex];
		this.FavorImage.sprite = this.FavorSpriteList[favorIndex];
	}

	// Token: 0x0600194D RID: 6477 RVA: 0x000E21A8 File Offset: 0x000E03A8
	private static void InitFavor()
	{
		foreach (JSONObject jsonobject in jsonData.instance.NpcHaoGanDuData.list)
		{
			UINPCHeadFavor.favorQuJianList.Add(jsonobject["QuJian"].list[0].I);
			UINPCHeadFavor.favorStrList.Add(jsonobject["HaoGanDu"].Str);
		}
	}

	// Token: 0x0600194E RID: 6478 RVA: 0x00015AC4 File Offset: 0x00013CC4
	public static int GetFavorLevel(int favor)
	{
		return UINPCHeadFavor.GetFavorIndex(favor) + 1;
	}

	// Token: 0x0600194F RID: 6479 RVA: 0x000E223C File Offset: 0x000E043C
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

	// Token: 0x04001462 RID: 5218
	public Image FavorImage;

	// Token: 0x04001463 RID: 5219
	public Text FavorText;

	// Token: 0x04001464 RID: 5220
	private static List<string> favorStrList = new List<string>();

	// Token: 0x04001465 RID: 5221
	private static List<int> favorQuJianList = new List<int>();

	// Token: 0x04001466 RID: 5222
	public List<Sprite> FavorSpriteList = new List<Sprite>();
}
