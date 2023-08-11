using System;
using System.Collections.Generic;
using UnityEngine;

namespace script.Steam;

public class ModInfo
{
	public ulong Id;

	public List<ulong> DependencyList;

	public string Author;

	public string Name;

	public string Desc;

	public string Tags;

	public string ImgUrl;

	public ulong Subscriptions;

	public bool IsUp;

	public bool IsDown;

	public int UpNum;

	public int DownNum;

	private Sprite sprite;

	public void ShowImg(Action<Sprite> callBack)
	{
		if ((Object)(object)sprite == (Object)null)
		{
			WorkShopMag.Inst.downUtils.DownSpriteByUrl(ImgUrl, delegate(Sprite _)
			{
				sprite = _;
				callBack(sprite);
			});
		}
		else
		{
			callBack(sprite);
		}
	}

	public void SetTags(string tags)
	{
		if (string.IsNullOrEmpty(tags))
		{
			Tags = "无";
			return;
		}
		foreach (string key in WorkShopMag.TagsDict.Keys)
		{
			if (tags.Contains(WorkShopMag.TagsDict[key]))
			{
				tags = tags.Replace(WorkShopMag.TagsDict[key], key);
			}
		}
		Tags = tags;
	}

	public void SetAuthor(string author)
	{
		if (string.IsNullOrEmpty(author))
		{
			Author = "未知";
		}
		else
		{
			Author = author;
		}
	}

	public string GetLv()
	{
		if (UpNum + DownNum <= 10)
		{
			return "暂无";
		}
		int num = (int)((float)UpNum / (float)(UpNum + DownNum) * 100f);
		return $"{num}%";
	}
}
