using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000C7F RID: 3199
	public class WuDaoMag
	{
		// Token: 0x06005891 RID: 22673 RVA: 0x0024E997 File Offset: 0x0024CB97
		public WuDaoMag(Entity avater)
		{
			this.entity = (Avatar)avater;
		}

		// Token: 0x06005892 RID: 22674 RVA: 0x0024E9C1 File Offset: 0x0024CBC1
		public JSONObject getWuDaoTypeJson(int WuDaoType)
		{
			return this.getWuDaoTypeJson(WuDaoType.ToString());
		}

		// Token: 0x06005893 RID: 22675 RVA: 0x0024E9D0 File Offset: 0x0024CBD0
		public int getWuDaoLevelByType(int WuDaoType)
		{
			JSONObject wuDaoTypeJson = this.getWuDaoTypeJson(WuDaoType);
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoJinJieJson.list)
			{
				if ((int)jsonobject["Max"].n > wuDaoTypeJson["ex"].I)
				{
					return jsonobject["id"].I;
				}
			}
			return jsonData.instance.WuDaoJinJieJson.Count;
		}

		// Token: 0x06005894 RID: 22676 RVA: 0x0024EA74 File Offset: 0x0024CC74
		public JSONObject getWuDaoExTypeJson(int WuDaoType)
		{
			JSONObject wuDaoTypeJson = this.getWuDaoTypeJson(WuDaoType);
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoJinJieJson.list)
			{
				if ((int)jsonobject["Max"].n > wuDaoTypeJson["ex"].I)
				{
					return jsonobject;
				}
			}
			return jsonData.instance.WuDaoJinJieJson[jsonData.instance.WuDaoJinJieJson.Count.ToString()];
		}

		// Token: 0x06005895 RID: 22677 RVA: 0x0024EB24 File Offset: 0x0024CD24
		public JSONObject getWuDaoTypeJson(string WuDaoType)
		{
			if (!this.entity.WuDaoJson.HasField(WuDaoType))
			{
				this.CreateWuDaoJson(WuDaoType);
			}
			return this.entity.WuDaoJson[WuDaoType];
		}

		// Token: 0x06005896 RID: 22678 RVA: 0x0024EB54 File Offset: 0x0024CD54
		public void CreateWuDaoJson(string WuDaoType)
		{
			this.entity.WuDaoJson.AddField(WuDaoType, new JSONObject(JSONObject.Type.OBJECT));
			this.entity.WuDaoJson[WuDaoType].AddField("ex", 0);
			this.entity.WuDaoJson[WuDaoType].AddField("study", new JSONObject(JSONObject.Type.ARRAY));
		}

		// Token: 0x06005897 RID: 22679 RVA: 0x0024EBB5 File Offset: 0x0024CDB5
		public JSONObject getWuDaoStudy(int WuDaoType)
		{
			return this.getWuDaoTypeJson(WuDaoType)["study"];
		}

		// Token: 0x06005898 RID: 22680 RVA: 0x0024EBC8 File Offset: 0x0024CDC8
		public JSONObject getWuDaoEx(int WuDaoType)
		{
			return this.getWuDaoTypeJson(WuDaoType)["ex"];
		}

		// Token: 0x06005899 RID: 22681 RVA: 0x0024EBDC File Offset: 0x0024CDDC
		public float getWuDaoExPercent(int WuDaoType)
		{
			int wuDaoLevelByType = this.getWuDaoLevelByType(WuDaoType);
			float i = (float)this.getWuDaoEx(WuDaoType).I;
			float num = 0f;
			num += (float)(20 * wuDaoLevelByType);
			float num2 = (float)(jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType]["Max"].I - ((wuDaoLevelByType == 0) ? 0 : jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType - 1]["Max"].I));
			float num3 = i - (float)((wuDaoLevelByType == 0) ? 0 : jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType - 1]["Max"].I);
			float num4 = 20f * (num3 / num2);
			num += num4;
			if (num > 100f)
			{
				num = 100f;
			}
			return num;
		}

		// Token: 0x0600589A RID: 22682 RVA: 0x0024ECA0 File Offset: 0x0024CEA0
		public int getNowTypePercent(int WuDaoType)
		{
			int wuDaoLevelByType = this.getWuDaoLevelByType(WuDaoType);
			float i = (float)this.getWuDaoEx(WuDaoType).I;
			float num = (float)(jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType]["Max"].I - ((wuDaoLevelByType == 0) ? 0 : jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType - 1]["Max"].I));
			float num2 = i - (float)((wuDaoLevelByType == 0) ? 0 : jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType - 1]["Max"].I);
			return (int)(100f * (num2 / num));
		}

		// Token: 0x0600589B RID: 22683 RVA: 0x0024ED40 File Offset: 0x0024CF40
		public int getNowTypeExMax(int WuDaoType)
		{
			int wuDaoLevelByType = this.getWuDaoLevelByType(WuDaoType);
			return jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType]["Max"].I;
		}

		// Token: 0x0600589C RID: 22684 RVA: 0x0024ED74 File Offset: 0x0024CF74
		public int GetAllWuDaoDian()
		{
			int num = 0;
			foreach (JSONObject jsonobject in jsonData.instance.LevelUpDataJsonData.list)
			{
				if (jsonobject["level"].I < (int)this.entity.level)
				{
					num += jsonobject["wudaodian"].I;
				}
			}
			num += this.entity._WuDaoDian;
			foreach (JSONObject jsonobject2 in jsonData.instance.WuDaoAllTypeJson.list)
			{
				if (this.getWuDaoLevelByType(jsonobject2["id"].I) >= 3)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600589D RID: 22685 RVA: 0x0024EE6C File Offset: 0x0024D06C
		public int getStudyCast()
		{
			List<SkillItem> allWuDaoSkills = this.GetAllWuDaoSkills();
			int num = 0;
			foreach (SkillItem skillItem in allWuDaoSkills)
			{
				num += jsonData.instance.WuDaoJson[skillItem.itemId.ToString()]["Cast"].I;
			}
			return num;
		}

		// Token: 0x0600589E RID: 22686 RVA: 0x0024EEE8 File Offset: 0x0024D0E8
		public int GetNowWuDaoDian()
		{
			return this.GetAllWuDaoDian() - this.getStudyCast();
		}

		// Token: 0x0600589F RID: 22687 RVA: 0x0024EEF8 File Offset: 0x0024D0F8
		public bool addWuDaoSkill(int WuDaoType, int type)
		{
			JSONObject wuDaoStudy = this.getWuDaoStudy(WuDaoType);
			if (!wuDaoStudy.HasItem(type))
			{
				wuDaoStudy.Add(type);
				WuDaoStaticSkill.resetWuDaoSeid(this.entity);
				PlayTutorial.CheckGanWuTianDi3();
				return true;
			}
			return false;
		}

		// Token: 0x060058A0 RID: 22688 RVA: 0x0024EF30 File Offset: 0x0024D130
		public void addWuDaoEx(int WuDaoType, int exNum)
		{
			JSONObject wuDaoTypeJson = this.getWuDaoTypeJson(WuDaoType);
			int i = wuDaoTypeJson["ex"].I;
			wuDaoTypeJson.SetField("ex", i + exNum);
			PlayTutorial.CheckGanWuTianDi2();
			PlayTutorial.CheckGanWuTianDi4();
			PlayTutorial.CheckLianQi2();
		}

		// Token: 0x060058A1 RID: 22689 RVA: 0x0024EF74 File Offset: 0x0024D174
		public void AddLingGuang(string name, int type, int studyTime, int guoqiTime, string desc, int quality, bool isLunDao = false)
		{
			JSONObject jsonobject = new JSONObject(JSONObject.Type.ARRAY);
			jsonobject.AddField("uuid", Tools.getUUID());
			jsonobject.AddField("name", name);
			jsonobject.AddField("type", type);
			jsonobject.AddField("studyTime", studyTime);
			jsonobject.AddField("guoqiTime", guoqiTime);
			jsonobject.AddField("desc", desc);
			jsonobject.AddField("quality", quality);
			jsonobject.AddField("startTime", this.entity.worldTimeMag.getNowTime().ToString());
			jsonobject.AddField("isLunDao", isLunDao);
			this.entity.LingGuang.Add(jsonobject);
		}

		// Token: 0x060058A2 RID: 22690 RVA: 0x0024F024 File Offset: 0x0024D224
		public void AutoReomveLingGuang()
		{
			JArray jarray = JArray.Parse(this.entity.LingGuang.ToString());
			for (int i = 0; i < jarray.Count; i++)
			{
				if (this.IsLingGuangGuoQi((string)jarray[i]["startTime"], (int)jarray[i]["guoqiTime"]))
				{
					jarray.RemoveAt(i);
					i--;
				}
			}
			this.entity.LingGuang = new JSONObject(jarray.ToString(), -2, false, false);
		}

		// Token: 0x060058A3 RID: 22691 RVA: 0x0024F0B4 File Offset: 0x0024D2B4
		public int CalcGanWuTime(JSONObject json)
		{
			int wuXin = (int)Tools.instance.getPlayer().wuXin;
			float num = (8f * (float)Math.Pow(10.0, -6.0) * (float)Math.Pow((double)wuXin, 3.0) - 0.0042f * (float)Math.Pow((double)wuXin, 2.0) + 0.7922f * (float)wuXin - 0.3381f) / 100f + 1f;
			return (int)(json["studyTime"].n / num);
		}

		// Token: 0x060058A4 RID: 22692 RVA: 0x0024F148 File Offset: 0x0024D348
		public void StudyLingGuang(string uuid)
		{
			JSONObject jsonobject = this.entity.LingGuang.list.Find((JSONObject aa) => aa["uuid"].str == uuid);
			if (jsonobject != null)
			{
				item.AddWuDao(jsonobject["studyTime"].I, jsonData.instance.WuDaoExBeiLuJson["1"]["lingguang" + jsonobject["quality"].I].n, new List<int>
				{
					jsonobject["type"].I
				}, 1);
				int addday = this.CalcGanWuTime(jsonobject);
				if (jsonobject.HasField("isLunDao") && jsonobject["isLunDao"].b)
				{
					PlayTutorial.CheckGanWuTianDi1();
				}
				this.removeLingGuang(uuid);
				this.entity.AddTime(addday, 0, 0);
			}
		}

		// Token: 0x060058A5 RID: 22693 RVA: 0x0024F240 File Offset: 0x0024D440
		public void StudyLingGuang(string uuid, int day, float precent)
		{
			JSONObject jsonobject = this.entity.LingGuang.list.Find((JSONObject aa) => aa["uuid"].str == uuid);
			if (jsonobject != null)
			{
				item.AddWuDao((int)((float)(jsonobject["studyTime"].I * jsonData.instance.WuDaoExBeiLuJson["1"]["lingguang" + jsonobject["quality"].I].I) * precent), new List<int>
				{
					jsonobject["type"].I
				}, 1);
				if (jsonobject.HasField("isLunDao") && jsonobject["isLunDao"].b)
				{
					PlayTutorial.CheckGanWuTianDi1();
				}
				this.removeLingGuang(uuid);
				this.entity.AddTime(day, 0, 0);
			}
		}

		// Token: 0x060058A6 RID: 22694 RVA: 0x0024F334 File Offset: 0x0024D534
		public void removeLingGuang(string uuid)
		{
			JArray jarray = JArray.Parse(this.entity.LingGuang.ToString());
			for (int i = 0; i < jarray.Count; i++)
			{
				if ((string)jarray[i]["uuid"] == uuid)
				{
					jarray.Remove(jarray[i]);
					break;
				}
			}
			this.entity.LingGuang = new JSONObject(jarray.ToString(), -2, false, false);
		}

		// Token: 0x060058A7 RID: 22695 RVA: 0x0024F3B0 File Offset: 0x0024D5B0
		public bool IsLingGuangGuoQi(string _startTime, int GuoqiTaime)
		{
			DateTime startTime = DateTime.Parse(_startTime);
			DateTime endTime = startTime.AddDays((double)GuoqiTaime);
			return !Tools.instance.IsInTime(this.entity.worldTimeMag.getNowTime(), startTime, endTime, 0);
		}

		// Token: 0x060058A8 RID: 22696 RVA: 0x0024F3F0 File Offset: 0x0024D5F0
		public void autoAddBiGuanLingGuang(int month)
		{
			this.entity.BiGuanLingGuangTime += month;
			int num = this.entity.BiGuanLingGuangTime / 12;
			bool flag = false;
			JToken jtoken = null;
			foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.BiGuanWuDao)
			{
				if (num >= (int)keyValuePair.Value["biguanTime"])
				{
					flag = true;
					jtoken = keyValuePair.Value;
				}
				else if (flag)
				{
					JToken jtoken2 = jtoken;
					this.entity.BiGuanLingGuangTime -= (int)jtoken2["biguanTime"];
					int zhuXiuSkill = this.entity.getZhuXiuSkill();
					JSONObject jsonobject = jsonData.instance.StaticSkillJsonData[zhuXiuSkill.ToString()];
					JSONObject skillWuDaoType = this.GetSkillWuDaoType(jsonobject["Skill_ID"].I);
					string text = Tools.Code64(jsonData.instance.WuDaoAllTypeJson[skillWuDaoType[0].I.ToString()]["name"].str);
					string desc = ((string)jtoken2["desc"]).Replace("{skill}", Tools.instance.getStaticSkillName(zhuXiuSkill, false)).Replace("{type}", text ?? "");
					string name = ((string)jtoken2["name"]).Replace("{skill}", Tools.instance.getStaticSkillName(zhuXiuSkill, false)).Replace("{type}", text ?? "");
					this.AddLingGuang(name, skillWuDaoType[0].I, (int)jtoken2["studyTime"], (int)jtoken2["guoqiTime"], desc, (int)jtoken2["quality"], false);
					break;
				}
			}
		}

		// Token: 0x060058A9 RID: 22697 RVA: 0x0024F60C File Offset: 0x0024D80C
		public JSONObject GetSkillWuDaoType(int _skillId)
		{
			foreach (KeyValuePair<string, JSONObject> keyValuePair in jsonData.instance.ItemJsonData)
			{
				if (keyValuePair.Value["type"].I == 4)
				{
					float num = 0f;
					if (float.TryParse(keyValuePair.Value["desc"].str, out num) && (int)num == _skillId)
					{
						return keyValuePair.Value["wuDao"];
					}
				}
			}
			return null;
		}

		// Token: 0x060058AA RID: 22698 RVA: 0x0024F6B8 File Offset: 0x0024D8B8
		public void AddLingGuangByJsonID(int ID)
		{
			JSONObject jsonobject = jsonData.instance.LingGuangJson[ID.ToString()];
			this.AddLingGuang(jsonobject["name"].str, jsonobject["type"].I, jsonobject["studyTime"].I, jsonobject["guoqiTime"].I, jsonobject["desc"].str, jsonobject["quality"].I, false);
		}

		// Token: 0x060058AB RID: 22699 RVA: 0x0024F743 File Offset: 0x0024D943
		public void SetWuDaoEx(int WuDaoType, int exNum)
		{
			this.getWuDaoTypeJson(WuDaoType).SetField("ex", exNum);
		}

		// Token: 0x060058AC RID: 22700 RVA: 0x0024F758 File Offset: 0x0024D958
		public bool IsStudy(int id)
		{
			int wuDaoType = WuDaoJson.DataDict[id].Type[0];
			return this.getWuDaoStudy(wuDaoType).ToList().Contains(id);
		}

		// Token: 0x060058AD RID: 22701 RVA: 0x0024F794 File Offset: 0x0024D994
		public List<SkillItem> GetHasWuDaoSkillItems(int WuDaoType)
		{
			JSONObject wuDaoStudy = this.getWuDaoStudy(WuDaoType);
			List<SkillItem> list = new List<SkillItem>();
			foreach (JSONObject jsonobject in wuDaoStudy.list)
			{
				list.Add(new SkillItem
				{
					itemId = jsonobject.I
				});
			}
			return list;
		}

		// Token: 0x060058AE RID: 22702 RVA: 0x0024F808 File Offset: 0x0024DA08
		public List<SkillItem> GetAllWuDaoSkills()
		{
			List<SkillItem> AllWuDaoSkillList = new List<SkillItem>();
			Action<SkillItem> <>9__0;
			foreach (string s in this.entity.WuDaoJson.keys)
			{
				int wuDaoType = -1;
				if (int.TryParse(s, out wuDaoType))
				{
					List<SkillItem> hasWuDaoSkillItems = this.GetHasWuDaoSkillItems(wuDaoType);
					Action<SkillItem> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(SkillItem aa)
						{
							if (AllWuDaoSkillList.Find((SkillItem cc) => cc.itemId == aa.itemId) == null)
							{
								AllWuDaoSkillList.Add(aa);
							}
						});
					}
					hasWuDaoSkillItems.ForEach(action);
				}
			}
			return AllWuDaoSkillList;
		}

		// Token: 0x060058AF RID: 22703 RVA: 0x0024F8AC File Offset: 0x0024DAAC
		public JSONObject getMonstarWuDaoJson(int MonstarID)
		{
			JSONObject jsonobject = jsonData.instance.AvatarJsonData[MonstarID.ToString()];
			int monstarWuDao = jsonobject["wudaoType"].I;
			int monstarlv = jsonobject["Level"].I;
			return jsonData.instance.NPCWuDaoJson.list.Find((JSONObject _cc) => monstarlv == _cc["lv"].I && _cc["Type"].I == monstarWuDao);
		}

		// Token: 0x060058B0 RID: 22704 RVA: 0x0024F924 File Offset: 0x0024DB24
		public void MonstarAddWuDaoList(int MonstarID)
		{
			JSONObject monstarWuDaoJson = this.getMonstarWuDaoJson(MonstarID);
			if (monstarWuDaoJson != null)
			{
				foreach (JSONObject jsonobject in monstarWuDaoJson["wudaoID"].list)
				{
					try
					{
						this.addWuDaoSkill(1, jsonobject.I);
						new WuDaoStaticSkill(jsonobject.I, 0, 5).putingStudySkill(this.entity, this.entity, 0);
					}
					catch (Exception)
					{
						Debug.LogError(string.Concat(new object[]
						{
							"悟道特性报错武将ID",
							MonstarID,
							"特性",
							jsonobject.I
						}));
					}
				}
			}
		}

		// Token: 0x060058B1 RID: 22705 RVA: 0x0024FA00 File Offset: 0x0024DC00
		public List<SkillItem> MonstarGetAllWuDaoSkills(int MonstarID)
		{
			List<SkillItem> list = new List<SkillItem>();
			JSONObject monstarWuDaoJson = this.getMonstarWuDaoJson(MonstarID);
			if (monstarWuDaoJson != null)
			{
				foreach (JSONObject jsonobject in monstarWuDaoJson["wudaoID"].list)
				{
					list.Add(new SkillItem
					{
						itemId = jsonobject.I
					});
				}
			}
			return list;
		}

		// Token: 0x04005207 RID: 20999
		public Avatar entity;

		// Token: 0x04005208 RID: 21000
		public Dictionary<int, Dictionary<int, int>> WuDaoSkillSeidFlag = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x04005209 RID: 21001
		public List<SkillItem> equipStaticSkillList = new List<SkillItem>();
	}
}
