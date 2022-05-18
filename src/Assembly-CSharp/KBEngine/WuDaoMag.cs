using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02001032 RID: 4146
	public class WuDaoMag
	{
		// Token: 0x06006330 RID: 25392 RVA: 0x000447C2 File Offset: 0x000429C2
		public WuDaoMag(Entity avater)
		{
			this.entity = (Avatar)avater;
		}

		// Token: 0x06006331 RID: 25393 RVA: 0x000447EC File Offset: 0x000429EC
		public JSONObject getWuDaoTypeJson(int WuDaoType)
		{
			return this.getWuDaoTypeJson(WuDaoType.ToString());
		}

		// Token: 0x06006332 RID: 25394 RVA: 0x0027A5D4 File Offset: 0x002787D4
		public int getWuDaoLevelByType(int WuDaoType)
		{
			JSONObject wuDaoTypeJson = this.getWuDaoTypeJson(WuDaoType);
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoJinJieJson.list)
			{
				if ((int)jsonobject["Max"].n > wuDaoTypeJson["ex"].I)
				{
					return (int)jsonobject["id"].n;
				}
			}
			return jsonData.instance.WuDaoJinJieJson.Count;
		}

		// Token: 0x06006333 RID: 25395 RVA: 0x0027A67C File Offset: 0x0027887C
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

		// Token: 0x06006334 RID: 25396 RVA: 0x000447FB File Offset: 0x000429FB
		public JSONObject getWuDaoTypeJson(string WuDaoType)
		{
			if (!this.entity.WuDaoJson.HasField(WuDaoType))
			{
				this.CreateWuDaoJson(WuDaoType);
			}
			return this.entity.WuDaoJson[WuDaoType];
		}

		// Token: 0x06006335 RID: 25397 RVA: 0x0027A72C File Offset: 0x0027892C
		public void CreateWuDaoJson(string WuDaoType)
		{
			this.entity.WuDaoJson.AddField(WuDaoType, new JSONObject(JSONObject.Type.OBJECT));
			this.entity.WuDaoJson[WuDaoType].AddField("ex", 0);
			this.entity.WuDaoJson[WuDaoType].AddField("study", new JSONObject(JSONObject.Type.ARRAY));
		}

		// Token: 0x06006336 RID: 25398 RVA: 0x00044828 File Offset: 0x00042A28
		public JSONObject getWuDaoStudy(int WuDaoType)
		{
			return this.getWuDaoTypeJson(WuDaoType)["study"];
		}

		// Token: 0x06006337 RID: 25399 RVA: 0x0004483B File Offset: 0x00042A3B
		public JSONObject getWuDaoEx(int WuDaoType)
		{
			return this.getWuDaoTypeJson(WuDaoType)["ex"];
		}

		// Token: 0x06006338 RID: 25400 RVA: 0x0027A790 File Offset: 0x00278990
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

		// Token: 0x06006339 RID: 25401 RVA: 0x0027A854 File Offset: 0x00278A54
		public int getNowTypePercent(int WuDaoType)
		{
			int wuDaoLevelByType = this.getWuDaoLevelByType(WuDaoType);
			float i = (float)this.getWuDaoEx(WuDaoType).I;
			float num = (float)(jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType]["Max"].I - ((wuDaoLevelByType == 0) ? 0 : jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType - 1]["Max"].I));
			float num2 = i - (float)((wuDaoLevelByType == 0) ? 0 : jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType - 1]["Max"].I);
			return (int)(100f * (num2 / num));
		}

		// Token: 0x0600633A RID: 25402 RVA: 0x0027A8F4 File Offset: 0x00278AF4
		public int getNowTypeExMax(int WuDaoType)
		{
			int wuDaoLevelByType = this.getWuDaoLevelByType(WuDaoType);
			return jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType]["Max"].I;
		}

		// Token: 0x0600633B RID: 25403 RVA: 0x0027A928 File Offset: 0x00278B28
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

		// Token: 0x0600633C RID: 25404 RVA: 0x0027AA20 File Offset: 0x00278C20
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

		// Token: 0x0600633D RID: 25405 RVA: 0x0004484E File Offset: 0x00042A4E
		public int GetNowWuDaoDian()
		{
			return this.GetAllWuDaoDian() - this.getStudyCast();
		}

		// Token: 0x0600633E RID: 25406 RVA: 0x0027AA9C File Offset: 0x00278C9C
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

		// Token: 0x0600633F RID: 25407 RVA: 0x0027AAD4 File Offset: 0x00278CD4
		public void addWuDaoEx(int WuDaoType, int exNum)
		{
			JSONObject wuDaoTypeJson = this.getWuDaoTypeJson(WuDaoType);
			int i = wuDaoTypeJson["ex"].I;
			wuDaoTypeJson.SetField("ex", i + exNum);
			PlayTutorial.CheckGanWuTianDi2();
			PlayTutorial.CheckGanWuTianDi4();
			PlayTutorial.CheckLianQi2();
		}

		// Token: 0x06006340 RID: 25408 RVA: 0x0027AB18 File Offset: 0x00278D18
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

		// Token: 0x06006341 RID: 25409 RVA: 0x0027ABC8 File Offset: 0x00278DC8
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

		// Token: 0x06006342 RID: 25410 RVA: 0x0027AC58 File Offset: 0x00278E58
		public int CalcGanWuTime(JSONObject json)
		{
			int wuXin = (int)Tools.instance.getPlayer().wuXin;
			float num = (8f * (float)Math.Pow(10.0, -6.0) * (float)Math.Pow((double)wuXin, 3.0) - 0.0042f * (float)Math.Pow((double)wuXin, 2.0) + 0.7922f * (float)wuXin - 0.3381f) / 100f + 1f;
			return (int)(json["studyTime"].n / num);
		}

		// Token: 0x06006343 RID: 25411 RVA: 0x0027ACEC File Offset: 0x00278EEC
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

		// Token: 0x06006344 RID: 25412 RVA: 0x0027ADE4 File Offset: 0x00278FE4
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

		// Token: 0x06006345 RID: 25413 RVA: 0x0027AED8 File Offset: 0x002790D8
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

		// Token: 0x06006346 RID: 25414 RVA: 0x0027AF54 File Offset: 0x00279154
		public bool IsLingGuangGuoQi(string _startTime, int GuoqiTaime)
		{
			DateTime startTime = DateTime.Parse(_startTime);
			DateTime endTime = startTime.AddDays((double)GuoqiTaime);
			return !Tools.instance.IsInTime(this.entity.worldTimeMag.getNowTime(), startTime, endTime, 0);
		}

		// Token: 0x06006347 RID: 25415 RVA: 0x0027AF94 File Offset: 0x00279194
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

		// Token: 0x06006348 RID: 25416 RVA: 0x0027B1B0 File Offset: 0x002793B0
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

		// Token: 0x06006349 RID: 25417 RVA: 0x0027B25C File Offset: 0x0027945C
		public void AddLingGuangByJsonID(int ID)
		{
			JSONObject jsonobject = jsonData.instance.LingGuangJson[ID.ToString()];
			this.AddLingGuang(jsonobject["name"].str, jsonobject["type"].I, jsonobject["studyTime"].I, jsonobject["guoqiTime"].I, jsonobject["desc"].str, jsonobject["quality"].I, false);
		}

		// Token: 0x0600634A RID: 25418 RVA: 0x0004485D File Offset: 0x00042A5D
		public void SetWuDaoEx(int WuDaoType, int exNum)
		{
			this.getWuDaoTypeJson(WuDaoType).SetField("ex", exNum);
		}

		// Token: 0x0600634B RID: 25419 RVA: 0x0027B2E8 File Offset: 0x002794E8
		public bool IsStudy(int id)
		{
			int wuDaoType = WuDaoJson.DataDict[id].Type[0];
			return this.getWuDaoStudy(wuDaoType).ToList().Contains(id);
		}

		// Token: 0x0600634C RID: 25420 RVA: 0x0027B324 File Offset: 0x00279524
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

		// Token: 0x0600634D RID: 25421 RVA: 0x0027B398 File Offset: 0x00279598
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

		// Token: 0x0600634E RID: 25422 RVA: 0x0027B43C File Offset: 0x0027963C
		public JSONObject getMonstarWuDaoJson(int MonstarID)
		{
			JSONObject jsonobject = jsonData.instance.AvatarJsonData[MonstarID.ToString()];
			int monstarWuDao = jsonobject["wudaoType"].I;
			int monstarlv = jsonobject["Level"].I;
			return jsonData.instance.NPCWuDaoJson.list.Find((JSONObject _cc) => monstarlv == _cc["lv"].I && _cc["Type"].I == monstarWuDao);
		}

		// Token: 0x0600634F RID: 25423 RVA: 0x0027B4B4 File Offset: 0x002796B4
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

		// Token: 0x06006350 RID: 25424 RVA: 0x0027B590 File Offset: 0x00279790
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

		// Token: 0x04005D16 RID: 23830
		public Avatar entity;

		// Token: 0x04005D17 RID: 23831
		public Dictionary<int, Dictionary<int, int>> WuDaoSkillSeidFlag = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x04005D18 RID: 23832
		public List<SkillItem> equipStaticSkillList = new List<SkillItem>();
	}
}
