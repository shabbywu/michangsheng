using System;
using System.Collections.Generic;
using System.Text;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace YSGame.Test;

[Serializable]
public class AvatarFightStatus
{
	[NonSerialized]
	public Avatar Avatar;

	public string Name;

	public string HP;

	public string LingGen;

	public string LingQi;

	[Multiline]
	public string Buff;

	public AvatarFightStatus(Avatar avatar)
	{
		Avatar = avatar;
	}

	public void RefreshData()
	{
		Name = Avatar.name;
		HP = $"{Avatar.HP}/{Avatar.HP_Max}";
		LingGen = GetLingGenDesc();
		LingQi = GetLingQiDesc();
		Buff = GetBuffDesc();
	}

	public string GetLingGenDesc()
	{
		string text = "基础灵根";
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < 6; i++)
		{
			dictionary.Add(i, 0);
		}
		int num = 0;
		foreach (int item in Avatar.GetLingGeng)
		{
			if (num > 5)
			{
				break;
			}
			dictionary[num] += item;
			text += $"{num.ToLingQiName()}{item} ";
			num++;
		}
		foreach (KeyValuePair<int, int> item2 in Avatar.DrawWeight)
		{
			dictionary[item2.Key] += item2.Value;
		}
		if (Avatar.SkillSeidFlag.ContainsKey(13))
		{
			foreach (KeyValuePair<int, int> item3 in Avatar.SkillSeidFlag[13])
			{
				dictionary[item3.Key] += item3.Value;
			}
		}
		text += "战时灵根";
		for (int j = 0; j < dictionary.Count; j++)
		{
			text += $"{j.ToLingQiName()}{dictionary[j]} ";
		}
		return text;
	}

	public string GetLingQiDesc()
	{
		string text = "";
		List<int> list = Avatar.cardMag.ToListInt32();
		for (int i = 0; i < list.Count; i++)
		{
			text += $"{i.ToLingQiName()}{list[i]} ";
		}
		return text;
	}

	public string GetBuffDesc()
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (List<int> item in Avatar.bufflist)
		{
			_BuffJsonData buffJsonData = _BuffJsonData.DataDict[item[2]];
			stringBuilder.AppendLine($"{buffJsonData.name} ID:{item[2]} ROUND:{item[1]} NUM:{item[0]}");
		}
		return stringBuilder.ToString();
	}
}
