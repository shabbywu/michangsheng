using System;
using System.Collections.Generic;
using GUIPackage;
using UnityEngine;

namespace YSGame.TuJian
{
	// Token: 0x02000DD5 RID: 3541
	public static class TuJianDB
	{
		// Token: 0x06005545 RID: 21829 RVA: 0x00237AF4 File Offset: 0x00235CF4
		public static void InitDB()
		{
			if (TuJianDB._IsInited)
			{
				return;
			}
			foreach (Sprite sprite in Resources.LoadAll<Sprite>("NewUI/TuJian/TuJianUI"))
			{
				TuJianDB.TuJianUISprite.TryAdd(sprite.name, sprite, "");
				TuJianDB.TuJianRichTextSprite.TryAdd(sprite.name, sprite, "");
			}
			TuJianDB.InitStrText();
			jsonData.instance.init("Effect/json/d_TuJian.py.zhonglei", out TuJianDB._zhonglei);
			jsonData.instance.init("Effect/json/d_TuJian.py.chunwenben", out TuJianDB._chunwenben);
			jsonData.instance.init("Effect/json/d_TuJian.py.tupianwenzi", out TuJianDB._tupianwenzi);
			jsonData.instance.init("Effect/json/d_TuJian.py.zixiangtupianwenzi", out TuJianDB._zixiangtupianwenzi);
			jsonData.instance.init("Effect/json/d_TuJian.py.yaoshou", out TuJianDB._yaoshou);
			for (int j = 1; j <= 8; j++)
			{
				TuJianDB.ItemTuJianFilterData.TryAdd(j, new List<Dictionary<int, string>>(), "");
			}
			TuJianDB.InitItemTuJianData();
			TuJianDB.InitLQWuWeiType();
			TuJianDB.InitLQShuXingType();
			TuJianDB.InitMapName();
			TuJianDB.InitYaoShouData();
			TuJianDB.InitRuleTuJianData();
			TuJianDB.InitShenTongMiShu();
			TuJianDB.InitGongFa();
			TuJianDB.InitDanYao();
			TuJianDB._IsInited = true;
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x00237C14 File Offset: 0x00235E14
		public static Sprite GetRichTextSprite(string name)
		{
			if (TuJianDB.TuJianRichTextSprite.ContainsKey(name))
			{
				return TuJianDB.TuJianRichTextSprite[name];
			}
			Sprite sprite = ResManager.inst.LoadSprite("NewUI/TuJian/Image/" + name);
			if (sprite != null)
			{
				TuJianDB.TuJianRichTextSprite.TryAdd(sprite.name, sprite, "");
				return sprite;
			}
			return null;
		}

		// Token: 0x06005547 RID: 21831 RVA: 0x0003CF88 File Offset: 0x0003B188
		public static string GetLQWuWeiTypeName(int wuWeiType)
		{
			return TuJianDB._LQWuWeiTypeName[wuWeiType];
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x0003CF95 File Offset: 0x0003B195
		public static string GetLQShuXingTypeName(int shuXingType)
		{
			return TuJianDB._LQShuXingTypeName[shuXingType];
		}

		// Token: 0x06005549 RID: 21833 RVA: 0x00237C74 File Offset: 0x00235E74
		private static void InitItemTuJianData()
		{
			foreach (JSONObject jsonobject in jsonData.instance._ItemJsonData.list)
			{
				if (jsonobject.HasField("TuJianType"))
				{
					int i = jsonobject["TuJianType"].I;
					if (i <= 4 && i > 0)
					{
						TuJianDB.ItemTuJianFilterData[i].Add(new Dictionary<int, string>
						{
							{
								jsonobject["id"].I,
								jsonobject["name"].Str
							}
						});
					}
				}
			}
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x00237D2C File Offset: 0x00235F2C
		private static void InitRuleTuJianData()
		{
			foreach (JSONObject jsonobject in TuJianDB._zhonglei.list)
			{
				int i = jsonobject["id"].I;
				if (jsonobject["type1"].I == 2)
				{
					TuJianDB.RuleTuJianTypeNameData.Add(new Dictionary<int, string>
					{
						{
							i,
							jsonobject["name"].Str
						}
					});
					TuJianDB.RuleTuJianFilterData.TryAdd(i, new List<Dictionary<int, string>>(), "");
					if (jsonobject["type2"].I == 1)
					{
						TuJianDB.RuleTuJianTypeDoubleSVData.TryAdd(i, true, "");
						if (i != 101)
						{
							TuJianDB.RuleDoubleIndexData.TryAdd(i, new List<int>(), "");
						}
					}
					else if (jsonobject["type2"].I == 2)
					{
						TuJianDB.RuleTuJianTypeDoubleSVData.TryAdd(i, false, "");
						TuJianDB.RuleTuJianTypeHasChildData.TryAdd(i, false, "");
					}
					else if (jsonobject["type2"].I == 3)
					{
						TuJianDB.RuleTuJianTypeDoubleSVData.TryAdd(i, false, "");
						TuJianDB.RuleTuJianTypeHasChildData.TryAdd(i, true, "");
					}
				}
			}
			TuJianDB.RuleTuJianFilterData[101].Add(new Dictionary<int, string>
			{
				{
					1,
					"增益状态"
				}
			});
			TuJianDB.RuleTuJianFilterData[101].Add(new Dictionary<int, string>
			{
				{
					2,
					"负面状态"
				}
			});
			TuJianDB.RuleTuJianFilterData[101].Add(new Dictionary<int, string>
			{
				{
					3,
					"特殊状态"
				}
			});
			TuJianDB.RuleTuJianFilterData[101].Add(new Dictionary<int, string>
			{
				{
					4,
					"指示物"
				}
			});
			TuJianDB.RuleTuJianFilterData[101].Add(new Dictionary<int, string>
			{
				{
					5,
					"词缀"
				}
			});
			for (int j = 1; j <= 5; j++)
			{
				TuJianDB.RuleCiZhuiIndexData.TryAdd(j, new List<int>(), "");
			}
			foreach (JSONObject jsonobject2 in TuJianDB._chunwenben.list)
			{
				int i2 = jsonobject2["typenum"].I;
				int i3 = jsonobject2["type"].I;
				int i4 = jsonobject2["id"].I;
				TuJianDB.RuleDoubleData.TryAdd(i4, new DoubleItem(jsonobject2["name2"].Str, jsonobject2["descr"].Str), "");
				if (i2 == 101)
				{
					TuJianDB.RuleCiZhuiIndexData[i3].Add(i4);
				}
				else if (TuJianDB.RuleDoubleIndexData.ContainsKey(i2))
				{
					TuJianDB.RuleDoubleIndexData[i2].Add(i4);
				}
				else
				{
					Debug.LogError(string.Format("规则图鉴初始化出错，没有大类{0}，请检查规则图鉴配表", i2));
				}
			}
			foreach (JSONObject jsonobject3 in TuJianDB._tupianwenzi.list)
			{
				int i5 = jsonobject3["typenum"].I;
				TuJianDB.RuleTuJianTypeDescData.TryAdd(i5, jsonobject3["descr"].Str, "");
			}
			foreach (JSONObject jsonobject4 in TuJianDB._zixiangtupianwenzi.list)
			{
				int i6 = jsonobject4["typenum"].I;
				if (!TuJianDB.RuleTuJianFilterData.ContainsKey(i6))
				{
					TuJianDB.RuleTuJianFilterData.Add(i6, new List<Dictionary<int, string>>());
				}
				int i7 = jsonobject4["subtypenum"].I;
				string str = jsonobject4["subname"].Str;
				TuJianDB.RuleTuJianFilterData[i6].Add(new Dictionary<int, string>
				{
					{
						i7,
						str
					}
				});
				if (!TuJianDB.RuleTuJianTypeChildDescData.ContainsKey(i6))
				{
					TuJianDB.RuleTuJianTypeChildDescData.Add(i6, new Dictionary<int, string>());
				}
				TuJianDB.RuleTuJianTypeChildDescData[i6].Add(i7, jsonobject4["descr"].Str);
			}
		}

		// Token: 0x0600554B RID: 21835 RVA: 0x0023821C File Offset: 0x0023641C
		private static void InitStrText()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StrTextJsonData.list)
			{
				TuJianDB.strTextData.TryAdd(jsonobject["StrID"].str, jsonobject["ChinaText"].Str, "");
			}
		}

		// Token: 0x0600554C RID: 21836 RVA: 0x002382A4 File Offset: 0x002364A4
		private static void InitLQWuWeiType()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianQiWuWeiBiao.list)
			{
				TuJianDB._LQWuWeiTypeName.TryAdd(jsonobject["id"].I, jsonobject["desc"].Str, "");
			}
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x0023832C File Offset: 0x0023652C
		private static void InitLQShuXingType()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianQiShuXinLeiBie.list)
			{
				TuJianDB._LQShuXingTypeName.TryAdd(jsonobject["id"].I, jsonobject["desc"].Str, "");
			}
		}

		// Token: 0x0600554E RID: 21838 RVA: 0x0003CFA2 File Offset: 0x0003B1A2
		public static Sprite GetItemQualitySprite(int id)
		{
			if (!TuJianDB._ItemQualitySpriteIndexDict.ContainsKey(id))
			{
				TuJianDB.LoadItemSprite(id);
			}
			return TuJianDB._ItemQualitySpriteList[TuJianDB._ItemQualitySpriteIndexDict[id]];
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x0003CFCC File Offset: 0x0003B1CC
		public static Sprite GetItemQualityUpSprite(int id)
		{
			if (!TuJianDB._ItemQualityUpSpriteIndexDict.ContainsKey(id))
			{
				TuJianDB.LoadItemSprite(id);
			}
			return TuJianDB._ItemQualityUpSpriteList[TuJianDB._ItemQualityUpSpriteIndexDict[id]];
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x0003CFF6 File Offset: 0x0003B1F6
		public static Sprite GetItemIconSprite(int id)
		{
			if (!TuJianDB._ItemIconSpriteIndexDict.ContainsKey(id))
			{
				TuJianDB.LoadItemSprite(id);
			}
			return TuJianDB._ItemIconSpriteList[TuJianDB._ItemIconSpriteIndexDict[id]];
		}

		// Token: 0x06005551 RID: 21841 RVA: 0x002383B4 File Offset: 0x002365B4
		private static void LoadItemSprite(int id)
		{
			item temp = new item(id);
			if (!TuJianDB._ItemIconTexList.Contains(temp.itemIcon))
			{
				TuJianDB._ItemIconTexList.Add(temp.itemIcon);
				Sprite item = Sprite.Create(temp.itemIcon, new Rect(0f, 0f, (float)temp.itemIcon.width, (float)temp.itemIcon.height), new Vector2(0.5f, 0.5f));
				TuJianDB._ItemIconSpriteList.Add(item);
				TuJianDB._ItemIconSpriteIndexDict.TryAdd(id, TuJianDB._ItemIconSpriteList.Count - 1, "");
			}
			else
			{
				int value = TuJianDB._ItemIconTexList.FindIndex((Texture2D t) => t == temp.itemIcon);
				TuJianDB._ItemIconSpriteIndexDict.TryAdd(id, value, "");
			}
			if (!TuJianDB._ItemQualityTexList.Contains(temp.itemPingZhi))
			{
				TuJianDB._ItemQualityTexList.Add(temp.itemPingZhi);
				Sprite item2 = Sprite.Create(temp.itemPingZhi, new Rect(0f, 0f, (float)temp.itemPingZhi.width, (float)temp.itemPingZhi.height), new Vector2(0.5f, 0.5f));
				TuJianDB._ItemQualitySpriteList.Add(item2);
				TuJianDB._ItemQualitySpriteIndexDict.TryAdd(id, TuJianDB._ItemQualitySpriteList.Count - 1, "");
			}
			else
			{
				int value2 = TuJianDB._ItemQualityTexList.FindIndex((Texture2D t) => t == temp.itemPingZhi);
				TuJianDB._ItemQualitySpriteIndexDict.TryAdd(id, value2, "");
			}
			if (!TuJianDB._ItemQualityUpSpriteList.Contains(temp.itemPingZhiUP))
			{
				TuJianDB._ItemQualityUpSpriteList.Add(temp.itemPingZhiUP);
				TuJianDB._ItemQualityUpSpriteIndexDict.TryAdd(id, TuJianDB._ItemQualityUpSpriteList.Count - 1, "");
				return;
			}
			int value3 = TuJianDB._ItemQualityUpSpriteList.FindIndex((Sprite s) => s == temp.itemPingZhiUP);
			TuJianDB._ItemQualityUpSpriteIndexDict.TryAdd(id, value3, "");
		}

		// Token: 0x06005552 RID: 21842 RVA: 0x002385F0 File Offset: 0x002367F0
		private static void InitMapName()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SceneNameJsonData.list)
			{
				TuJianDB._MapIDNameDict.TryAdd(jsonobject["id"].str, jsonobject["EventName"].Str, "");
				TuJianDB._MapIDHighlightDict.TryAdd(jsonobject["id"].str, jsonobject["HighlightID"].I, "");
			}
		}

		// Token: 0x06005553 RID: 21843 RVA: 0x0003D020 File Offset: 0x0003B220
		public static string GetMapNameByID(string mapID)
		{
			if (TuJianManager.IsDebugMode)
			{
				return TuJianDB._MapIDNameDict[mapID] + "-" + mapID;
			}
			return TuJianDB._MapIDNameDict[mapID];
		}

		// Token: 0x06005554 RID: 21844 RVA: 0x0003D04B File Offset: 0x0003B24B
		public static int GetMapHighlightIDByMapID(string mapID)
		{
			return TuJianDB._MapIDHighlightDict[mapID];
		}

		// Token: 0x06005555 RID: 21845 RVA: 0x002386A8 File Offset: 0x002368A8
		public static Sprite GetYaoShouFace(int id)
		{
			Sprite sprite = null;
			if (!TuJianDB.YaoShouFaceSpriteData.ContainsKey(id))
			{
				if (TuJianDB.YaoShouFacePathData.ContainsKey(id))
				{
					sprite = ResManager.inst.LoadSprite(TuJianDB.YaoShouFacePathData[id]);
					TuJianDB.YaoShouFaceSpriteData.TryAdd(id, sprite, "");
				}
				else
				{
					Debug.LogError("[图鉴]加载妖兽形象出错，没有找到形象");
				}
			}
			else
			{
				sprite = TuJianDB.YaoShouFaceSpriteData[id];
			}
			return sprite;
		}

		// Token: 0x06005556 RID: 21846 RVA: 0x00238714 File Offset: 0x00236914
		private static void InitYaoShouData()
		{
			JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
			foreach (JSONObject jsonobject in TuJianDB._yaoshou.list)
			{
				int i = jsonobject["avatarid"].I;
				JSONObject jsonobject2 = avatarJsonData[i.ToString()];
				int i2 = jsonobject2["fightFace"].I;
				string str = jsonobject2["Name"].Str;
				string str2 = jsonobject["descr"].Str;
				string value = TuJianDB.LevelNames[(jsonobject2["Level"].I - 1) / 3];
				string str3 = jsonobject["chanchu"].str;
				string str4 = jsonobject["qixidi"].str;
				TuJianDB.ItemTuJianFilterData[5].Add(new Dictionary<int, string>
				{
					{
						i,
						str
					}
				});
				TuJianDB.YaoShouNameData.TryAdd(i, str, "");
				TuJianDB.YaoShouLevelNameData.TryAdd(i, value, "");
				TuJianDB.YaoShouQiXiMapData.TryAdd(i, str4, "");
				string[] array = str3.Split(new char[]
				{
					','
				});
				for (int j = 0; j < array.Length; j++)
				{
					int num = int.Parse(array[j]);
					if (!TuJianDB.YaoShouCaiLiaoChanChuData.ContainsKey(num))
					{
						TuJianDB.YaoShouCaiLiaoChanChuData.TryAdd(num, new List<int>(), "");
					}
					if (!TuJianDB.YaoShouChanChuData.ContainsKey(i))
					{
						TuJianDB.YaoShouChanChuData.TryAdd(i, new List<int>(), "");
					}
					TuJianDB.YaoShouCaiLiaoChanChuData[num].Add(i);
					TuJianDB.YaoShouChanChuData[i].Add(num);
				}
				TuJianDB.YaoShouDescData.TryAdd(i, str2, "");
				TuJianDB.YaoShouFacePathData.TryAdd(i, string.Format("Effect/Prefab/gameEntity/Avater/Avater{0}/{1}", i2, i2), "");
			}
		}

		// Token: 0x06005557 RID: 21847 RVA: 0x0023894C File Offset: 0x00236B4C
		private static void InitShenTongMiShu()
		{
			foreach (JSONObject jsonobject in jsonData.instance._ItemJsonData.list)
			{
				if (jsonobject["id"].I < 100000 && jsonobject["type"].I == 3)
				{
					int key = (int)float.Parse(jsonobject["desc"].str);
					string str = jsonobject["desc2"].Str;
					TuJianDB.ShenTongMiShuDesc1Data[key] = str;
				}
			}
			JSONObject skillJsonData = jsonData.instance._skillJsonData;
			for (int i = 0; i < skillJsonData.list.Count; i++)
			{
				JSONObject jsonobject2 = skillJsonData.list[i];
				int i2 = jsonobject2["TuJianType"].I;
				if (i2 == 6 || i2 == 8)
				{
					int i3 = jsonobject2["Skill_ID"].I;
					int i4 = jsonobject2["Skill_LV"].I;
					int i5 = jsonobject2["typePinJie"].I;
					string str2 = TuJianDB.strTextData[string.Format("pingjie{0}", i4)];
					string str3 = TuJianDB.strTextData[string.Format("shangzhongxia{0}", i5)];
					TuJianDB.ShenTongMiShuPinJiData[i3] = str2 + str3;
					TuJianDB.ShenTongMiShuQualityData[i3] = i4;
					if (!TuJianDB.ShenTongMiShuNameData.ContainsKey(i3))
					{
						string value = jsonobject2["name"].Str.RemoveNumber();
						TuJianDB.ItemTuJianFilterData[i2].Add(new Dictionary<int, string>
						{
							{
								i3,
								value
							}
						});
						TuJianDB.ShenTongMiShuNameData[i3] = value;
					}
					if (!TuJianDB.ShenTongMiShuShuXingData.ContainsKey(i3))
					{
						string text = "";
						foreach (JSONObject jsonobject3 in jsonobject2["AttackType"].list)
						{
							text += TuJianDB.strTextData[string.Format("xibie{0}", jsonobject3.I)];
						}
						TuJianDB.ShenTongMiShuShuXingData[i3] = text;
					}
					if (i2 == 6)
					{
						if (!TuJianDB.ShenTongDesc2Data.ContainsKey(i3))
						{
							List<string> list = new List<string>();
							for (int j = 0; j < 5; j++)
							{
								list.Add("无");
							}
							TuJianDB.ShenTongDesc2Data[i3] = list;
						}
						string text2 = jsonobject2["TuJiandescr"].Str;
						text2 = text2.Replace("（attack）", jsonobject2["HP"].I.ToString());
						int i6 = jsonobject2["Skill_Lv"].I;
						TuJianDB.ShenTongDesc2Data[i3][i6 - 1] = text2;
					}
					else
					{
						TuJianDB.MiShuDesc2Data[i3] = jsonobject2["TuJiandescr"].Str;
					}
					List<int> list2 = new List<int>();
					foreach (JSONObject jsonobject4 in jsonobject2["skill_CastType"].list)
					{
						list2.Add(jsonobject4.I);
					}
					List<int> list3 = new List<int>();
					foreach (JSONObject jsonobject5 in jsonobject2["skill_Cast"].list)
					{
						list3.Add(jsonobject5.I);
					}
					List<int> list4 = new List<int>();
					foreach (JSONObject jsonobject6 in jsonobject2["skill_SameCastNum"].list)
					{
						list4.Add(jsonobject6.I);
					}
					List<int> list5 = new List<int>();
					for (int k = 0; k < list3.Count; k++)
					{
						for (int l = 0; l < list3[k]; l++)
						{
							list5.Add(list2[k]);
						}
						list5.Add(9);
					}
					for (int m = 0; m < list4.Count; m++)
					{
						for (int n = 0; n < list4[m]; n++)
						{
							list5.Add(8);
						}
						list5.Add(9);
					}
					if (list5.Count > 0 && list5[list5.Count - 1] == 9)
					{
						list5.RemoveAt(list5.Count - 1);
					}
					TuJianDB.ShenTongMiShuCastData[i3] = list5;
					if (!TuJianDB.ShenTongMiShuDesc1Data.ContainsKey(i3))
					{
						Debug.Log(string.Format("[图鉴]不存在神通秘术{0}的描述1，需反馈策划", i3));
					}
				}
			}
		}

		// Token: 0x06005558 RID: 21848 RVA: 0x00238EA4 File Offset: 0x002370A4
		private static void InitGongFa()
		{
			foreach (JSONObject jsonobject in jsonData.instance._ItemJsonData.list)
			{
				if (jsonobject["id"].I < 100000 && jsonobject["type"].I == 4)
				{
					int key = (int)float.Parse(jsonobject["desc"].str);
					string str = jsonobject["desc2"].Str;
					TuJianDB.GongFaDesc1Data.TryAdd(key, str, "[TuJianDB.InitGongFa]添加功法的描述1时，尝试对已有的SkillID进行添加，SkillID:{0}，已有Value:{1}，新加Value:{2}");
				}
			}
			JSONObject staticSkillJsonData = jsonData.instance.StaticSkillJsonData;
			for (int i = 0; i < staticSkillJsonData.list.Count; i++)
			{
				JSONObject jsonobject2 = staticSkillJsonData.list[i];
				int i2 = jsonobject2["TuJianType"].I;
				if (i2 == 7)
				{
					int i3 = jsonobject2["Skill_ID"].I;
					int i4 = jsonobject2["Skill_LV"].I;
					int i5 = jsonobject2["typePinJie"].I;
					string str2 = TuJianDB.strTextData[string.Format("pingjie{0}", i4)];
					string str3 = TuJianDB.strTextData[string.Format("shangzhongxia{0}", i5)];
					TuJianDB.GongFaQualityData.TryAdd(i3, i4, "");
					TuJianDB.GongFaPinJiData.TryAdd(i3, str2 + str3, "");
					string value = jsonobject2["name"].Str.RemoveNumber();
					TuJianDB.ItemTuJianFilterData[i2].Add(new Dictionary<int, string>
					{
						{
							i3,
							value
						}
					});
					TuJianDB.GongFaNameData.TryAdd(i3, value, "");
					int i6 = jsonobject2["AttackType"].I;
					string value2 = TuJianDB.strTextData[string.Format("gongfaleibie{0}", i6)];
					TuJianDB.GongFaShuXingData.TryAdd(i3, value2, "");
					TuJianDB.GongFaSpeedData.TryAdd(i3, jsonobject2["Skill_Speed"].I, "");
					List<string> list = new List<string>();
					for (int j = 0; j < 5; j++)
					{
						string str4 = staticSkillJsonData.list[i + j]["TuJiandescr"].Str;
						list.Add(str4);
					}
					TuJianDB.GongFaDesc2Data.TryAdd(i3, list, "");
					if (!TuJianDB.GongFaDesc1Data.ContainsKey(i3))
					{
						Debug.Log(string.Format("[图鉴]不存在功法{0}的描述1，需反馈策划", i3));
					}
					i += 4;
				}
			}
		}

		// Token: 0x06005559 RID: 21849 RVA: 0x00239188 File Offset: 0x00237388
		public static Sprite GetShenTongMiShuSprite(int id)
		{
			if (!TuJianDB._ShenTongMiShuSpriteData.ContainsKey(1))
			{
				TuJianDB._ShenTongMiShuSpriteData.TryAdd(1, ResManager.inst.LoadSprite("Skill Icon/1"), "");
			}
			if (!TuJianDB._ShenTongMiShuSpriteData.ContainsKey(id))
			{
				Sprite sprite = ResManager.inst.LoadSprite("Skill Icon/" + id);
				if (sprite)
				{
					TuJianDB._ShenTongMiShuSpriteData.TryAdd(id, sprite, "");
				}
				else
				{
					TuJianDB._ShenTongMiShuSpriteData.TryAdd(id, TuJianDB._ShenTongMiShuSpriteData[1], "");
				}
			}
			return TuJianDB._ShenTongMiShuSpriteData[id];
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x00239230 File Offset: 0x00237430
		public static Sprite GetGongFaSprite(int id)
		{
			if (!TuJianDB._GongFaSpriteData.ContainsKey(1))
			{
				TuJianDB._GongFaSpriteData.TryAdd(1, ResManager.inst.LoadSprite("StaticSkill Icon/1"), "");
			}
			if (!TuJianDB._GongFaSpriteData.ContainsKey(id))
			{
				Sprite sprite = ResManager.inst.LoadSprite("StaticSkill Icon/" + id);
				if (sprite)
				{
					TuJianDB._GongFaSpriteData.TryAdd(id, sprite, "");
				}
				else
				{
					TuJianDB._GongFaSpriteData.TryAdd(id, TuJianDB._GongFaSpriteData[1], "");
				}
			}
			return TuJianDB._GongFaSpriteData[id];
		}

		// Token: 0x0600555B RID: 21851 RVA: 0x002392D8 File Offset: 0x002374D8
		public static Sprite GetSkillQualitySprite(int quality)
		{
			if (!TuJianDB._SkillQualitySpriteData.ContainsKey(quality))
			{
				TuJianDB._SkillQualitySpriteData.TryAdd(quality, ResManager.inst.LoadSprite("Ui Icon/tab/skill" + quality), "");
			}
			return TuJianDB._SkillQualitySpriteData[quality];
		}

		// Token: 0x0600555C RID: 21852 RVA: 0x00239328 File Offset: 0x00237528
		private static void InitDanYao()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianDanDanFangBiao.list)
			{
				DanFangData danFangData = new DanFangData();
				danFangData.ItemID = jsonobject["ItemID"].I;
				danFangData.YaoYinID = jsonobject["value1"].I;
				danFangData.ZhuYao1ID = jsonobject["value2"].I;
				danFangData.ZhuYao2ID = jsonobject["value3"].I;
				danFangData.FuYao1ID = jsonobject["value4"].I;
				danFangData.FuYao2ID = jsonobject["value5"].I;
				danFangData.YaoYinCount = jsonobject["num1"].I;
				danFangData.ZhuYao1Count = jsonobject["num2"].I;
				danFangData.ZhuYao2Count = jsonobject["num3"].I;
				danFangData.FuYao1Count = jsonobject["num4"].I;
				danFangData.FuYao2Count = jsonobject["num5"].I;
				danFangData.CastTime = jsonobject["castTime"].I;
				danFangData.CalcYaoCaiTypeCount();
				TuJianDB.DanFangDataDict.TryAdd(danFangData.ItemID, danFangData, "");
			}
			foreach (JSONObject jsonobject2 in jsonData.instance.LianDanItemLeiXin.list)
			{
				TuJianDB.YaoCaoTypeData.TryAdd(jsonobject2["id"].I, jsonobject2["name"].Str, "");
			}
		}

		// Token: 0x040054F9 RID: 21753
		public static bool _IsInited;

		// Token: 0x040054FA RID: 21754
		public static Dictionary<string, Sprite> TuJianUISprite = new Dictionary<string, Sprite>();

		// Token: 0x040054FB RID: 21755
		public static Dictionary<string, Sprite> TuJianRichTextSprite = new Dictionary<string, Sprite>();

		// Token: 0x040054FC RID: 21756
		public static Dictionary<int, List<Dictionary<int, string>>> ItemTuJianFilterData = new Dictionary<int, List<Dictionary<int, string>>>();

		// Token: 0x040054FD RID: 21757
		public static List<Dictionary<int, string>> RuleTuJianTypeNameData = new List<Dictionary<int, string>>();

		// Token: 0x040054FE RID: 21758
		public static Dictionary<int, string> RuleTuJianTypeDescData = new Dictionary<int, string>();

		// Token: 0x040054FF RID: 21759
		public static Dictionary<int, Dictionary<int, string>> RuleTuJianTypeChildDescData = new Dictionary<int, Dictionary<int, string>>();

		// Token: 0x04005500 RID: 21760
		public static Dictionary<int, bool> RuleTuJianTypeDoubleSVData = new Dictionary<int, bool>();

		// Token: 0x04005501 RID: 21761
		public static Dictionary<int, bool> RuleTuJianTypeHasChildData = new Dictionary<int, bool>();

		// Token: 0x04005502 RID: 21762
		public static Dictionary<int, List<Dictionary<int, string>>> RuleTuJianFilterData = new Dictionary<int, List<Dictionary<int, string>>>();

		// Token: 0x04005503 RID: 21763
		private static Dictionary<int, string> _LQWuWeiTypeName = new Dictionary<int, string>();

		// Token: 0x04005504 RID: 21764
		private static Dictionary<int, string> _LQShuXingTypeName = new Dictionary<int, string>();

		// Token: 0x04005505 RID: 21765
		private static Dictionary<string, string> _MapIDNameDict = new Dictionary<string, string>();

		// Token: 0x04005506 RID: 21766
		private static Dictionary<string, int> _MapIDHighlightDict = new Dictionary<string, int>();

		// Token: 0x04005507 RID: 21767
		public static Dictionary<int, DoubleItem> RuleDoubleData = new Dictionary<int, DoubleItem>();

		// Token: 0x04005508 RID: 21768
		public static Dictionary<int, List<int>> RuleCiZhuiIndexData = new Dictionary<int, List<int>>();

		// Token: 0x04005509 RID: 21769
		public static Dictionary<int, List<int>> RuleDoubleIndexData = new Dictionary<int, List<int>>();

		// Token: 0x0400550A RID: 21770
		private static Dictionary<string, string> strTextData = new Dictionary<string, string>();

		// Token: 0x0400550B RID: 21771
		private static JSONObject _zhonglei;

		// Token: 0x0400550C RID: 21772
		private static JSONObject _chunwenben;

		// Token: 0x0400550D RID: 21773
		private static JSONObject _tupianwenzi;

		// Token: 0x0400550E RID: 21774
		private static JSONObject _zixiangtupianwenzi;

		// Token: 0x0400550F RID: 21775
		private static JSONObject _yaoshou;

		// Token: 0x04005510 RID: 21776
		private static List<Sprite> _ItemIconSpriteList = new List<Sprite>();

		// Token: 0x04005511 RID: 21777
		private static List<Texture2D> _ItemIconTexList = new List<Texture2D>();

		// Token: 0x04005512 RID: 21778
		private static List<Sprite> _ItemQualitySpriteList = new List<Sprite>();

		// Token: 0x04005513 RID: 21779
		private static List<Texture2D> _ItemQualityTexList = new List<Texture2D>();

		// Token: 0x04005514 RID: 21780
		private static List<Sprite> _ItemQualityUpSpriteList = new List<Sprite>();

		// Token: 0x04005515 RID: 21781
		private static Dictionary<int, int> _ItemIconSpriteIndexDict = new Dictionary<int, int>();

		// Token: 0x04005516 RID: 21782
		private static Dictionary<int, int> _ItemQualitySpriteIndexDict = new Dictionary<int, int>();

		// Token: 0x04005517 RID: 21783
		private static Dictionary<int, int> _ItemQualityUpSpriteIndexDict = new Dictionary<int, int>();

		// Token: 0x04005518 RID: 21784
		public static Dictionary<int, List<int>> YaoShouCaiLiaoChanChuData = new Dictionary<int, List<int>>();

		// Token: 0x04005519 RID: 21785
		public static Dictionary<int, List<int>> YaoShouChanChuData = new Dictionary<int, List<int>>();

		// Token: 0x0400551A RID: 21786
		public static string[] LevelNames = new string[]
		{
			"炼气期",
			"筑基期",
			"金丹期",
			"元婴期",
			"化神期"
		};

		// Token: 0x0400551B RID: 21787
		public static Dictionary<int, string> YaoShouLevelNameData = new Dictionary<int, string>();

		// Token: 0x0400551C RID: 21788
		public static Dictionary<int, string> YaoShouQiXiMapData = new Dictionary<int, string>();

		// Token: 0x0400551D RID: 21789
		public static Dictionary<int, string> YaoShouDescData = new Dictionary<int, string>();

		// Token: 0x0400551E RID: 21790
		public static Dictionary<int, string> YaoShouNameData = new Dictionary<int, string>();

		// Token: 0x0400551F RID: 21791
		private static Dictionary<int, string> YaoShouFacePathData = new Dictionary<int, string>();

		// Token: 0x04005520 RID: 21792
		private static Dictionary<int, Sprite> YaoShouFaceSpriteData = new Dictionary<int, Sprite>();

		// Token: 0x04005521 RID: 21793
		public static Dictionary<int, string> ShenTongMiShuNameData = new Dictionary<int, string>();

		// Token: 0x04005522 RID: 21794
		public static Dictionary<int, string> ShenTongMiShuShuXingData = new Dictionary<int, string>();

		// Token: 0x04005523 RID: 21795
		public static Dictionary<int, string> ShenTongMiShuDesc1Data = new Dictionary<int, string>();

		// Token: 0x04005524 RID: 21796
		public static Dictionary<int, int> ShenTongMiShuQualityData = new Dictionary<int, int>();

		// Token: 0x04005525 RID: 21797
		public static Dictionary<int, List<string>> ShenTongDesc2Data = new Dictionary<int, List<string>>();

		// Token: 0x04005526 RID: 21798
		public static Dictionary<int, string> MiShuDesc2Data = new Dictionary<int, string>();

		// Token: 0x04005527 RID: 21799
		public static Dictionary<int, List<int>> ShenTongMiShuCastData = new Dictionary<int, List<int>>();

		// Token: 0x04005528 RID: 21800
		public static Dictionary<int, string> ShenTongMiShuPinJiData = new Dictionary<int, string>();

		// Token: 0x04005529 RID: 21801
		public static Dictionary<int, string> GongFaNameData = new Dictionary<int, string>();

		// Token: 0x0400552A RID: 21802
		public static Dictionary<int, string> GongFaPinJiData = new Dictionary<int, string>();

		// Token: 0x0400552B RID: 21803
		public static Dictionary<int, string> GongFaShuXingData = new Dictionary<int, string>();

		// Token: 0x0400552C RID: 21804
		public static Dictionary<int, int> GongFaSpeedData = new Dictionary<int, int>();

		// Token: 0x0400552D RID: 21805
		public static Dictionary<int, string> GongFaDesc1Data = new Dictionary<int, string>();

		// Token: 0x0400552E RID: 21806
		public static Dictionary<int, List<string>> GongFaDesc2Data = new Dictionary<int, List<string>>();

		// Token: 0x0400552F RID: 21807
		public static Dictionary<int, int> GongFaQualityData = new Dictionary<int, int>();

		// Token: 0x04005530 RID: 21808
		private static Dictionary<int, Sprite> _ShenTongMiShuSpriteData = new Dictionary<int, Sprite>();

		// Token: 0x04005531 RID: 21809
		private static Dictionary<int, Sprite> _GongFaSpriteData = new Dictionary<int, Sprite>();

		// Token: 0x04005532 RID: 21810
		private static Dictionary<int, Sprite> _SkillQualitySpriteData = new Dictionary<int, Sprite>();

		// Token: 0x04005533 RID: 21811
		public static Dictionary<int, DanFangData> DanFangDataDict = new Dictionary<int, DanFangData>();

		// Token: 0x04005534 RID: 21812
		public static Dictionary<int, string> YaoCaoTypeData = new Dictionary<int, string>();
	}
}
