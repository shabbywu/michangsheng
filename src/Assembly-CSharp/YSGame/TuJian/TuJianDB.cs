using System;
using System.Collections.Generic;
using GUIPackage;
using UnityEngine;

namespace YSGame.TuJian
{
	// Token: 0x02000A9B RID: 2715
	public static class TuJianDB
	{
		// Token: 0x06004C05 RID: 19461 RVA: 0x00206530 File Offset: 0x00204730
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

		// Token: 0x06004C06 RID: 19462 RVA: 0x00206650 File Offset: 0x00204850
		public static Sprite GetRichTextSprite(string name)
		{
			if (TuJianDB.TuJianRichTextSprite.ContainsKey(name))
			{
				return TuJianDB.TuJianRichTextSprite[name];
			}
			Sprite sprite = ModResources.LoadSprite("NewUI/TuJian/Image/" + name);
			if (sprite != null)
			{
				TuJianDB.TuJianRichTextSprite.TryAdd(name, sprite, "");
				return sprite;
			}
			return null;
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x002066A5 File Offset: 0x002048A5
		public static string GetLQWuWeiTypeName(int wuWeiType)
		{
			return TuJianDB._LQWuWeiTypeName[wuWeiType];
		}

		// Token: 0x06004C08 RID: 19464 RVA: 0x002066B2 File Offset: 0x002048B2
		public static string GetLQShuXingTypeName(int shuXingType)
		{
			return TuJianDB._LQShuXingTypeName[shuXingType];
		}

		// Token: 0x06004C09 RID: 19465 RVA: 0x002066C0 File Offset: 0x002048C0
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

		// Token: 0x06004C0A RID: 19466 RVA: 0x00206778 File Offset: 0x00204978
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

		// Token: 0x06004C0B RID: 19467 RVA: 0x00206C68 File Offset: 0x00204E68
		private static void InitStrText()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StrTextJsonData.list)
			{
				TuJianDB.strTextData.TryAdd(jsonobject["StrID"].str, jsonobject["ChinaText"].Str, "");
			}
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x00206CF0 File Offset: 0x00204EF0
		private static void InitLQWuWeiType()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianQiWuWeiBiao.list)
			{
				TuJianDB._LQWuWeiTypeName.TryAdd(jsonobject["id"].I, jsonobject["desc"].Str, "");
			}
		}

		// Token: 0x06004C0D RID: 19469 RVA: 0x00206D78 File Offset: 0x00204F78
		private static void InitLQShuXingType()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianQiShuXinLeiBie.list)
			{
				TuJianDB._LQShuXingTypeName.TryAdd(jsonobject["id"].I, jsonobject["desc"].Str, "");
			}
		}

		// Token: 0x06004C0E RID: 19470 RVA: 0x00206E00 File Offset: 0x00205000
		public static Sprite GetItemQualitySprite(int id)
		{
			if (!TuJianDB._ItemQualitySpriteIndexDict.ContainsKey(id))
			{
				TuJianDB.LoadItemSprite(id);
			}
			return TuJianDB._ItemQualitySpriteList[TuJianDB._ItemQualitySpriteIndexDict[id]];
		}

		// Token: 0x06004C0F RID: 19471 RVA: 0x00206E2A File Offset: 0x0020502A
		public static Sprite GetItemQualityUpSprite(int id)
		{
			if (!TuJianDB._ItemQualityUpSpriteIndexDict.ContainsKey(id))
			{
				TuJianDB.LoadItemSprite(id);
			}
			return TuJianDB._ItemQualityUpSpriteList[TuJianDB._ItemQualityUpSpriteIndexDict[id]];
		}

		// Token: 0x06004C10 RID: 19472 RVA: 0x00206E54 File Offset: 0x00205054
		public static Sprite GetItemIconSprite(int id)
		{
			if (!TuJianDB._ItemIconSpriteIndexDict.ContainsKey(id))
			{
				TuJianDB.LoadItemSprite(id);
			}
			return TuJianDB._ItemIconSpriteList[TuJianDB._ItemIconSpriteIndexDict[id]];
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x00206E80 File Offset: 0x00205080
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

		// Token: 0x06004C12 RID: 19474 RVA: 0x002070BC File Offset: 0x002052BC
		private static void InitMapName()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SceneNameJsonData.list)
			{
				TuJianDB._MapIDNameDict.TryAdd(jsonobject["id"].str, jsonobject["EventName"].Str, "");
				TuJianDB._MapIDHighlightDict.TryAdd(jsonobject["id"].str, jsonobject["HighlightID"].I, "");
			}
		}

		// Token: 0x06004C13 RID: 19475 RVA: 0x00207174 File Offset: 0x00205374
		public static string GetMapNameByID(string mapID)
		{
			if (TuJianManager.IsDebugMode)
			{
				return TuJianDB._MapIDNameDict[mapID] + "-" + mapID;
			}
			return TuJianDB._MapIDNameDict[mapID];
		}

		// Token: 0x06004C14 RID: 19476 RVA: 0x0020719F File Offset: 0x0020539F
		public static int GetMapHighlightIDByMapID(string mapID)
		{
			return TuJianDB._MapIDHighlightDict[mapID];
		}

		// Token: 0x06004C15 RID: 19477 RVA: 0x002071AC File Offset: 0x002053AC
		public static Sprite GetYaoShouFace(int id)
		{
			Sprite sprite = null;
			if (!TuJianDB.YaoShouFaceSpriteData.ContainsKey(id))
			{
				if (TuJianDB.YaoShouFacePathData.ContainsKey(id))
				{
					sprite = ModResources.LoadSprite(TuJianDB.YaoShouFacePathData[id]);
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

		// Token: 0x06004C16 RID: 19478 RVA: 0x00207214 File Offset: 0x00205414
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

		// Token: 0x06004C17 RID: 19479 RVA: 0x0020744C File Offset: 0x0020564C
		private static void InitShenTongMiShu()
		{
			foreach (JSONObject jsonobject in jsonData.instance._ItemJsonData.list)
			{
				if (jsonobject["id"].I < jsonData.QingJiaoItemIDSegment && jsonobject["type"].I == 3)
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

		// Token: 0x06004C18 RID: 19480 RVA: 0x002079A4 File Offset: 0x00205BA4
		private static void InitGongFa()
		{
			foreach (JSONObject jsonobject in jsonData.instance._ItemJsonData.list)
			{
				if (jsonobject["id"].I < jsonData.QingJiaoItemIDSegment && jsonobject["type"].I == 4)
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

		// Token: 0x06004C19 RID: 19481 RVA: 0x00207C88 File Offset: 0x00205E88
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

		// Token: 0x06004C1A RID: 19482 RVA: 0x00207D30 File Offset: 0x00205F30
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

		// Token: 0x06004C1B RID: 19483 RVA: 0x00207DD8 File Offset: 0x00205FD8
		public static Sprite GetSkillQualitySprite(int quality)
		{
			if (!TuJianDB._SkillQualitySpriteData.ContainsKey(quality))
			{
				TuJianDB._SkillQualitySpriteData.TryAdd(quality, ResManager.inst.LoadSprite("Ui Icon/tab/skill" + quality), "");
			}
			return TuJianDB._SkillQualitySpriteData[quality];
		}

		// Token: 0x06004C1C RID: 19484 RVA: 0x00207E28 File Offset: 0x00206028
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

		// Token: 0x04004B21 RID: 19233
		public static bool _IsInited;

		// Token: 0x04004B22 RID: 19234
		public static Dictionary<string, Sprite> TuJianUISprite = new Dictionary<string, Sprite>();

		// Token: 0x04004B23 RID: 19235
		public static Dictionary<string, Sprite> TuJianRichTextSprite = new Dictionary<string, Sprite>();

		// Token: 0x04004B24 RID: 19236
		public static Dictionary<int, List<Dictionary<int, string>>> ItemTuJianFilterData = new Dictionary<int, List<Dictionary<int, string>>>();

		// Token: 0x04004B25 RID: 19237
		public static List<Dictionary<int, string>> RuleTuJianTypeNameData = new List<Dictionary<int, string>>();

		// Token: 0x04004B26 RID: 19238
		public static Dictionary<int, string> RuleTuJianTypeDescData = new Dictionary<int, string>();

		// Token: 0x04004B27 RID: 19239
		public static Dictionary<int, Dictionary<int, string>> RuleTuJianTypeChildDescData = new Dictionary<int, Dictionary<int, string>>();

		// Token: 0x04004B28 RID: 19240
		public static Dictionary<int, bool> RuleTuJianTypeDoubleSVData = new Dictionary<int, bool>();

		// Token: 0x04004B29 RID: 19241
		public static Dictionary<int, bool> RuleTuJianTypeHasChildData = new Dictionary<int, bool>();

		// Token: 0x04004B2A RID: 19242
		public static Dictionary<int, List<Dictionary<int, string>>> RuleTuJianFilterData = new Dictionary<int, List<Dictionary<int, string>>>();

		// Token: 0x04004B2B RID: 19243
		private static Dictionary<int, string> _LQWuWeiTypeName = new Dictionary<int, string>();

		// Token: 0x04004B2C RID: 19244
		private static Dictionary<int, string> _LQShuXingTypeName = new Dictionary<int, string>();

		// Token: 0x04004B2D RID: 19245
		private static Dictionary<string, string> _MapIDNameDict = new Dictionary<string, string>();

		// Token: 0x04004B2E RID: 19246
		private static Dictionary<string, int> _MapIDHighlightDict = new Dictionary<string, int>();

		// Token: 0x04004B2F RID: 19247
		public static Dictionary<int, DoubleItem> RuleDoubleData = new Dictionary<int, DoubleItem>();

		// Token: 0x04004B30 RID: 19248
		public static Dictionary<int, List<int>> RuleCiZhuiIndexData = new Dictionary<int, List<int>>();

		// Token: 0x04004B31 RID: 19249
		public static Dictionary<int, List<int>> RuleDoubleIndexData = new Dictionary<int, List<int>>();

		// Token: 0x04004B32 RID: 19250
		private static Dictionary<string, string> strTextData = new Dictionary<string, string>();

		// Token: 0x04004B33 RID: 19251
		private static JSONObject _zhonglei;

		// Token: 0x04004B34 RID: 19252
		private static JSONObject _chunwenben;

		// Token: 0x04004B35 RID: 19253
		private static JSONObject _tupianwenzi;

		// Token: 0x04004B36 RID: 19254
		private static JSONObject _zixiangtupianwenzi;

		// Token: 0x04004B37 RID: 19255
		private static JSONObject _yaoshou;

		// Token: 0x04004B38 RID: 19256
		private static List<Sprite> _ItemIconSpriteList = new List<Sprite>();

		// Token: 0x04004B39 RID: 19257
		private static List<Texture2D> _ItemIconTexList = new List<Texture2D>();

		// Token: 0x04004B3A RID: 19258
		private static List<Sprite> _ItemQualitySpriteList = new List<Sprite>();

		// Token: 0x04004B3B RID: 19259
		private static List<Texture2D> _ItemQualityTexList = new List<Texture2D>();

		// Token: 0x04004B3C RID: 19260
		private static List<Sprite> _ItemQualityUpSpriteList = new List<Sprite>();

		// Token: 0x04004B3D RID: 19261
		private static Dictionary<int, int> _ItemIconSpriteIndexDict = new Dictionary<int, int>();

		// Token: 0x04004B3E RID: 19262
		private static Dictionary<int, int> _ItemQualitySpriteIndexDict = new Dictionary<int, int>();

		// Token: 0x04004B3F RID: 19263
		private static Dictionary<int, int> _ItemQualityUpSpriteIndexDict = new Dictionary<int, int>();

		// Token: 0x04004B40 RID: 19264
		public static Dictionary<int, List<int>> YaoShouCaiLiaoChanChuData = new Dictionary<int, List<int>>();

		// Token: 0x04004B41 RID: 19265
		public static Dictionary<int, List<int>> YaoShouChanChuData = new Dictionary<int, List<int>>();

		// Token: 0x04004B42 RID: 19266
		public static string[] LevelNames = new string[]
		{
			"炼气期",
			"筑基期",
			"金丹期",
			"元婴期",
			"化神期"
		};

		// Token: 0x04004B43 RID: 19267
		public static Dictionary<int, string> YaoShouLevelNameData = new Dictionary<int, string>();

		// Token: 0x04004B44 RID: 19268
		public static Dictionary<int, string> YaoShouQiXiMapData = new Dictionary<int, string>();

		// Token: 0x04004B45 RID: 19269
		public static Dictionary<int, string> YaoShouDescData = new Dictionary<int, string>();

		// Token: 0x04004B46 RID: 19270
		public static Dictionary<int, string> YaoShouNameData = new Dictionary<int, string>();

		// Token: 0x04004B47 RID: 19271
		private static Dictionary<int, string> YaoShouFacePathData = new Dictionary<int, string>();

		// Token: 0x04004B48 RID: 19272
		private static Dictionary<int, Sprite> YaoShouFaceSpriteData = new Dictionary<int, Sprite>();

		// Token: 0x04004B49 RID: 19273
		public static Dictionary<int, string> ShenTongMiShuNameData = new Dictionary<int, string>();

		// Token: 0x04004B4A RID: 19274
		public static Dictionary<int, string> ShenTongMiShuShuXingData = new Dictionary<int, string>();

		// Token: 0x04004B4B RID: 19275
		public static Dictionary<int, string> ShenTongMiShuDesc1Data = new Dictionary<int, string>();

		// Token: 0x04004B4C RID: 19276
		public static Dictionary<int, int> ShenTongMiShuQualityData = new Dictionary<int, int>();

		// Token: 0x04004B4D RID: 19277
		public static Dictionary<int, List<string>> ShenTongDesc2Data = new Dictionary<int, List<string>>();

		// Token: 0x04004B4E RID: 19278
		public static Dictionary<int, string> MiShuDesc2Data = new Dictionary<int, string>();

		// Token: 0x04004B4F RID: 19279
		public static Dictionary<int, List<int>> ShenTongMiShuCastData = new Dictionary<int, List<int>>();

		// Token: 0x04004B50 RID: 19280
		public static Dictionary<int, string> ShenTongMiShuPinJiData = new Dictionary<int, string>();

		// Token: 0x04004B51 RID: 19281
		public static Dictionary<int, string> GongFaNameData = new Dictionary<int, string>();

		// Token: 0x04004B52 RID: 19282
		public static Dictionary<int, string> GongFaPinJiData = new Dictionary<int, string>();

		// Token: 0x04004B53 RID: 19283
		public static Dictionary<int, string> GongFaShuXingData = new Dictionary<int, string>();

		// Token: 0x04004B54 RID: 19284
		public static Dictionary<int, int> GongFaSpeedData = new Dictionary<int, int>();

		// Token: 0x04004B55 RID: 19285
		public static Dictionary<int, string> GongFaDesc1Data = new Dictionary<int, string>();

		// Token: 0x04004B56 RID: 19286
		public static Dictionary<int, List<string>> GongFaDesc2Data = new Dictionary<int, List<string>>();

		// Token: 0x04004B57 RID: 19287
		public static Dictionary<int, int> GongFaQualityData = new Dictionary<int, int>();

		// Token: 0x04004B58 RID: 19288
		private static Dictionary<int, Sprite> _ShenTongMiShuSpriteData = new Dictionary<int, Sprite>();

		// Token: 0x04004B59 RID: 19289
		private static Dictionary<int, Sprite> _GongFaSpriteData = new Dictionary<int, Sprite>();

		// Token: 0x04004B5A RID: 19290
		private static Dictionary<int, Sprite> _SkillQualitySpriteData = new Dictionary<int, Sprite>();

		// Token: 0x04004B5B RID: 19291
		public static Dictionary<int, DanFangData> DanFangDataDict = new Dictionary<int, DanFangData>();

		// Token: 0x04004B5C RID: 19292
		public static Dictionary<int, string> YaoCaoTypeData = new Dictionary<int, string>();
	}
}
