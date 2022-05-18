using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BD9 RID: 3033
	public class heroJsonData : IJSONClass
	{
		// Token: 0x06004ACC RID: 19148 RVA: 0x001FA298 File Offset: 0x001F8498
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.heroJsonData.list)
			{
				try
				{
					heroJsonData heroJsonData = new heroJsonData();
					heroJsonData.id = jsonobject["id"].I;
					heroJsonData.heroid = jsonobject["heroid"].I;
					heroJsonData.dexterity = jsonobject["dexterity"].I;
					heroJsonData.mp_max = jsonobject["mp_max"].I;
					heroJsonData.money = jsonobject["money"].I;
					heroJsonData.energy = jsonobject["energy"].I;
					heroJsonData.sex = jsonobject["sex"].I;
					heroJsonData.spaceUType = jsonobject["spaceUType"].I;
					heroJsonData.defense = jsonobject["defense"].I;
					heroJsonData.anger = jsonobject["anger"].I;
					heroJsonData.speed = jsonobject["speed"].I;
					heroJsonData.intellect = jsonobject["intellect"].I;
					heroJsonData.modelID = jsonobject["modelID"].I;
					heroJsonData.strength = jsonobject["strength"].I;
					heroJsonData.constitution = jsonobject["constitution"].I;
					heroJsonData.magic_damage = jsonobject["magic_damage"].I;
					heroJsonData.stamina = jsonobject["stamina"].I;
					heroJsonData.potential = jsonobject["potential"].I;
					heroJsonData.role = jsonobject["role"].I;
					heroJsonData.mp = jsonobject["mp"].I;
					heroJsonData.dodge = jsonobject["dodge"].I;
					heroJsonData.modelScale = jsonobject["modelScale"].I;
					heroJsonData.hp = jsonobject["hp"].I;
					heroJsonData.anger_max = jsonobject["anger_max"].I;
					heroJsonData.moveSpeed = jsonobject["moveSpeed"].I;
					heroJsonData.spawnYaw = jsonobject["spawnYaw"].I;
					heroJsonData.damage = jsonobject["damage"].I;
					heroJsonData.hp_max = jsonobject["hp_max"].I;
					heroJsonData.level = jsonobject["level"].I;
					heroJsonData.energy_max = jsonobject["energy_max"].I;
					heroJsonData.hitval = jsonobject["hitval"].I;
					heroJsonData.race = jsonobject["race"].I;
					heroJsonData.magic_defense = jsonobject["magic_defense"].I;
					heroJsonData.exp = jsonobject["exp"].I;
					heroJsonData.heroType = jsonobject["heroType"].Str;
					heroJsonData.skills = jsonobject["skills"].ToList();
					heroJsonData.spawnPos = jsonobject["spawnPos"].ToList();
					if (heroJsonData.DataDict.ContainsKey(heroJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典heroJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", heroJsonData.id));
					}
					else
					{
						heroJsonData.DataDict.Add(heroJsonData.id, heroJsonData);
						heroJsonData.DataList.Add(heroJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典heroJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (heroJsonData.OnInitFinishAction != null)
			{
				heroJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040046AB RID: 18091
		public static Dictionary<int, heroJsonData> DataDict = new Dictionary<int, heroJsonData>();

		// Token: 0x040046AC RID: 18092
		public static List<heroJsonData> DataList = new List<heroJsonData>();

		// Token: 0x040046AD RID: 18093
		public static Action OnInitFinishAction = new Action(heroJsonData.OnInitFinish);

		// Token: 0x040046AE RID: 18094
		public int id;

		// Token: 0x040046AF RID: 18095
		public int heroid;

		// Token: 0x040046B0 RID: 18096
		public int dexterity;

		// Token: 0x040046B1 RID: 18097
		public int mp_max;

		// Token: 0x040046B2 RID: 18098
		public int money;

		// Token: 0x040046B3 RID: 18099
		public int energy;

		// Token: 0x040046B4 RID: 18100
		public int sex;

		// Token: 0x040046B5 RID: 18101
		public int spaceUType;

		// Token: 0x040046B6 RID: 18102
		public int defense;

		// Token: 0x040046B7 RID: 18103
		public int anger;

		// Token: 0x040046B8 RID: 18104
		public int speed;

		// Token: 0x040046B9 RID: 18105
		public int intellect;

		// Token: 0x040046BA RID: 18106
		public int modelID;

		// Token: 0x040046BB RID: 18107
		public int strength;

		// Token: 0x040046BC RID: 18108
		public int constitution;

		// Token: 0x040046BD RID: 18109
		public int magic_damage;

		// Token: 0x040046BE RID: 18110
		public int stamina;

		// Token: 0x040046BF RID: 18111
		public int potential;

		// Token: 0x040046C0 RID: 18112
		public int role;

		// Token: 0x040046C1 RID: 18113
		public int mp;

		// Token: 0x040046C2 RID: 18114
		public int dodge;

		// Token: 0x040046C3 RID: 18115
		public int modelScale;

		// Token: 0x040046C4 RID: 18116
		public int hp;

		// Token: 0x040046C5 RID: 18117
		public int anger_max;

		// Token: 0x040046C6 RID: 18118
		public int moveSpeed;

		// Token: 0x040046C7 RID: 18119
		public int spawnYaw;

		// Token: 0x040046C8 RID: 18120
		public int damage;

		// Token: 0x040046C9 RID: 18121
		public int hp_max;

		// Token: 0x040046CA RID: 18122
		public int level;

		// Token: 0x040046CB RID: 18123
		public int energy_max;

		// Token: 0x040046CC RID: 18124
		public int hitval;

		// Token: 0x040046CD RID: 18125
		public int race;

		// Token: 0x040046CE RID: 18126
		public int magic_defense;

		// Token: 0x040046CF RID: 18127
		public int exp;

		// Token: 0x040046D0 RID: 18128
		public string heroType;

		// Token: 0x040046D1 RID: 18129
		public List<int> skills = new List<int>();

		// Token: 0x040046D2 RID: 18130
		public List<int> spawnPos = new List<int>();
	}
}
