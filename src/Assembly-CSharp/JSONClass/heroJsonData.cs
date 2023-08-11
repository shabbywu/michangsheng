using System;
using System.Collections.Generic;

namespace JSONClass;

public class heroJsonData : IJSONClass
{
	public static Dictionary<int, heroJsonData> DataDict = new Dictionary<int, heroJsonData>();

	public static List<heroJsonData> DataList = new List<heroJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int heroid;

	public int dexterity;

	public int mp_max;

	public int money;

	public int energy;

	public int sex;

	public int spaceUType;

	public int defense;

	public int anger;

	public int speed;

	public int intellect;

	public int modelID;

	public int strength;

	public int constitution;

	public int magic_damage;

	public int stamina;

	public int potential;

	public int role;

	public int mp;

	public int dodge;

	public int modelScale;

	public int hp;

	public int anger_max;

	public int moveSpeed;

	public int spawnYaw;

	public int damage;

	public int hp_max;

	public int level;

	public int energy_max;

	public int hitval;

	public int race;

	public int magic_defense;

	public int exp;

	public string heroType;

	public List<int> skills = new List<int>();

	public List<int> spawnPos = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.heroJsonData.list)
		{
			try
			{
				heroJsonData heroJsonData2 = new heroJsonData();
				heroJsonData2.id = item["id"].I;
				heroJsonData2.heroid = item["heroid"].I;
				heroJsonData2.dexterity = item["dexterity"].I;
				heroJsonData2.mp_max = item["mp_max"].I;
				heroJsonData2.money = item["money"].I;
				heroJsonData2.energy = item["energy"].I;
				heroJsonData2.sex = item["sex"].I;
				heroJsonData2.spaceUType = item["spaceUType"].I;
				heroJsonData2.defense = item["defense"].I;
				heroJsonData2.anger = item["anger"].I;
				heroJsonData2.speed = item["speed"].I;
				heroJsonData2.intellect = item["intellect"].I;
				heroJsonData2.modelID = item["modelID"].I;
				heroJsonData2.strength = item["strength"].I;
				heroJsonData2.constitution = item["constitution"].I;
				heroJsonData2.magic_damage = item["magic_damage"].I;
				heroJsonData2.stamina = item["stamina"].I;
				heroJsonData2.potential = item["potential"].I;
				heroJsonData2.role = item["role"].I;
				heroJsonData2.mp = item["mp"].I;
				heroJsonData2.dodge = item["dodge"].I;
				heroJsonData2.modelScale = item["modelScale"].I;
				heroJsonData2.hp = item["hp"].I;
				heroJsonData2.anger_max = item["anger_max"].I;
				heroJsonData2.moveSpeed = item["moveSpeed"].I;
				heroJsonData2.spawnYaw = item["spawnYaw"].I;
				heroJsonData2.damage = item["damage"].I;
				heroJsonData2.hp_max = item["hp_max"].I;
				heroJsonData2.level = item["level"].I;
				heroJsonData2.energy_max = item["energy_max"].I;
				heroJsonData2.hitval = item["hitval"].I;
				heroJsonData2.race = item["race"].I;
				heroJsonData2.magic_defense = item["magic_defense"].I;
				heroJsonData2.exp = item["exp"].I;
				heroJsonData2.heroType = item["heroType"].Str;
				heroJsonData2.skills = item["skills"].ToList();
				heroJsonData2.spawnPos = item["spawnPos"].ToList();
				if (DataDict.ContainsKey(heroJsonData2.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典heroJsonData.DataDict添加数据时出现重复的键，Key:{heroJsonData2.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(heroJsonData2.id, heroJsonData2);
				DataList.Add(heroJsonData2);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典heroJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
				PreloadManager.LogException($"异常信息:\n{arg}");
				PreloadManager.LogException($"数据序列化:\n{item}");
			}
		}
		if (OnInitFinishAction != null)
		{
			OnInitFinishAction();
		}
	}

	private static void OnInitFinish()
	{
	}
}
