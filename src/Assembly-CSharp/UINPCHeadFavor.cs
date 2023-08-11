using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINPCHeadFavor : MonoBehaviour
{
	public Image FavorImage;

	public Text FavorText;

	private static List<string> favorStrList = new List<string>();

	private static List<int> favorQuJianList = new List<int>();

	public List<Sprite> FavorSpriteList = new List<Sprite>();

	public void SetFavor(int favor)
	{
		int favorIndex = GetFavorIndex(favor);
		FavorText.text = favorStrList[favorIndex];
		FavorImage.sprite = FavorSpriteList[favorIndex];
	}

	private static void InitFavor()
	{
		foreach (JSONObject item in jsonData.instance.NpcHaoGanDuData.list)
		{
			favorQuJianList.Add(item["QuJian"].list[0].I);
			favorStrList.Add(item["HaoGanDu"].Str);
		}
	}

	public static int GetFavorLevel(int favor)
	{
		return GetFavorIndex(favor) + 1;
	}

	public static int GetFavorIndex(int favor)
	{
		if (favorStrList.Count == 0)
		{
			InitFavor();
		}
		int result = 0;
		for (int i = 1; i < favorQuJianList.Count && favor >= favorQuJianList[i]; i++)
		{
			result = i;
		}
		return result;
	}
}
