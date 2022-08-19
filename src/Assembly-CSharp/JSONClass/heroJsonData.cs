using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000849 RID: 2121
	public class heroJsonData : IJSONClass
	{
		// Token: 0x06003F36 RID: 16182 RVA: 0x001AFE04 File Offset: 0x001AE004
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

		// Token: 0x06003F37 RID: 16183 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B48 RID: 15176
		public static Dictionary<int, heroJsonData> DataDict = new Dictionary<int, heroJsonData>();

		// Token: 0x04003B49 RID: 15177
		public static List<heroJsonData> DataList = new List<heroJsonData>();

		// Token: 0x04003B4A RID: 15178
		public static Action OnInitFinishAction = new Action(heroJsonData.OnInitFinish);

		// Token: 0x04003B4B RID: 15179
		public int id;

		// Token: 0x04003B4C RID: 15180
		public int heroid;

		// Token: 0x04003B4D RID: 15181
		public int dexterity;

		// Token: 0x04003B4E RID: 15182
		public int mp_max;

		// Token: 0x04003B4F RID: 15183
		public int money;

		// Token: 0x04003B50 RID: 15184
		public int energy;

		// Token: 0x04003B51 RID: 15185
		public int sex;

		// Token: 0x04003B52 RID: 15186
		public int spaceUType;

		// Token: 0x04003B53 RID: 15187
		public int defense;

		// Token: 0x04003B54 RID: 15188
		public int anger;

		// Token: 0x04003B55 RID: 15189
		public int speed;

		// Token: 0x04003B56 RID: 15190
		public int intellect;

		// Token: 0x04003B57 RID: 15191
		public int modelID;

		// Token: 0x04003B58 RID: 15192
		public int strength;

		// Token: 0x04003B59 RID: 15193
		public int constitution;

		// Token: 0x04003B5A RID: 15194
		public int magic_damage;

		// Token: 0x04003B5B RID: 15195
		public int stamina;

		// Token: 0x04003B5C RID: 15196
		public int potential;

		// Token: 0x04003B5D RID: 15197
		public int role;

		// Token: 0x04003B5E RID: 15198
		public int mp;

		// Token: 0x04003B5F RID: 15199
		public int dodge;

		// Token: 0x04003B60 RID: 15200
		public int modelScale;

		// Token: 0x04003B61 RID: 15201
		public int hp;

		// Token: 0x04003B62 RID: 15202
		public int anger_max;

		// Token: 0x04003B63 RID: 15203
		public int moveSpeed;

		// Token: 0x04003B64 RID: 15204
		public int spawnYaw;

		// Token: 0x04003B65 RID: 15205
		public int damage;

		// Token: 0x04003B66 RID: 15206
		public int hp_max;

		// Token: 0x04003B67 RID: 15207
		public int level;

		// Token: 0x04003B68 RID: 15208
		public int energy_max;

		// Token: 0x04003B69 RID: 15209
		public int hitval;

		// Token: 0x04003B6A RID: 15210
		public int race;

		// Token: 0x04003B6B RID: 15211
		public int magic_defense;

		// Token: 0x04003B6C RID: 15212
		public int exp;

		// Token: 0x04003B6D RID: 15213
		public string heroType;

		// Token: 0x04003B6E RID: 15214
		public List<int> skills = new List<int>();

		// Token: 0x04003B6F RID: 15215
		public List<int> spawnPos = new List<int>();
	}
}
