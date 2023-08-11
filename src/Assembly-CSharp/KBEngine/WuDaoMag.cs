using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KBEngine;

public class WuDaoMag
{
	public Avatar entity;

	public Dictionary<int, Dictionary<int, int>> WuDaoSkillSeidFlag = new Dictionary<int, Dictionary<int, int>>();

	public List<SkillItem> equipStaticSkillList = new List<SkillItem>();

	public WuDaoMag(Entity avater)
	{
		entity = (Avatar)avater;
	}

	public JSONObject getWuDaoTypeJson(int WuDaoType)
	{
		return getWuDaoTypeJson(WuDaoType.ToString());
	}

	public int getWuDaoLevelByType(int WuDaoType)
	{
		JSONObject wuDaoTypeJson = getWuDaoTypeJson(WuDaoType);
		foreach (JSONObject item in jsonData.instance.WuDaoJinJieJson.list)
		{
			if ((int)item["Max"].n > wuDaoTypeJson["ex"].I)
			{
				return item["id"].I;
			}
		}
		return jsonData.instance.WuDaoJinJieJson.Count;
	}

	public JSONObject getWuDaoExTypeJson(int WuDaoType)
	{
		JSONObject wuDaoTypeJson = getWuDaoTypeJson(WuDaoType);
		foreach (JSONObject item in jsonData.instance.WuDaoJinJieJson.list)
		{
			if ((int)item["Max"].n > wuDaoTypeJson["ex"].I)
			{
				return item;
			}
		}
		return jsonData.instance.WuDaoJinJieJson[jsonData.instance.WuDaoJinJieJson.Count.ToString()];
	}

	public JSONObject getWuDaoTypeJson(string WuDaoType)
	{
		if (!entity.WuDaoJson.HasField(WuDaoType))
		{
			CreateWuDaoJson(WuDaoType);
		}
		return entity.WuDaoJson[WuDaoType];
	}

	public void CreateWuDaoJson(string WuDaoType)
	{
		entity.WuDaoJson.AddField(WuDaoType, new JSONObject(JSONObject.Type.OBJECT));
		entity.WuDaoJson[WuDaoType].AddField("ex", 0);
		entity.WuDaoJson[WuDaoType].AddField("study", new JSONObject(JSONObject.Type.ARRAY));
	}

	public JSONObject getWuDaoStudy(int WuDaoType)
	{
		return getWuDaoTypeJson(WuDaoType)["study"];
	}

	public JSONObject getWuDaoEx(int WuDaoType)
	{
		return getWuDaoTypeJson(WuDaoType)["ex"];
	}

	public float getWuDaoExPercent(int WuDaoType)
	{
		int wuDaoLevelByType = getWuDaoLevelByType(WuDaoType);
		int i = getWuDaoEx(WuDaoType).I;
		float num = 0f;
		num += (float)(20 * wuDaoLevelByType);
		float num2 = jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType]["Max"].I - ((wuDaoLevelByType != 0) ? jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType - 1]["Max"].I : 0);
		float num3 = i - ((wuDaoLevelByType != 0) ? jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType - 1]["Max"].I : 0);
		float num4 = 20f * (num3 / num2);
		num += num4;
		if (num > 100f)
		{
			num = 100f;
		}
		return num;
	}

	public int getNowTypePercent(int WuDaoType)
	{
		int wuDaoLevelByType = getWuDaoLevelByType(WuDaoType);
		int i = getWuDaoEx(WuDaoType).I;
		float num = jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType]["Max"].I - ((wuDaoLevelByType != 0) ? jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType - 1]["Max"].I : 0);
		float num2 = i - ((wuDaoLevelByType != 0) ? jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType - 1]["Max"].I : 0);
		return (int)(100f * (num2 / num));
	}

	public int getNowTypeExMax(int WuDaoType)
	{
		int wuDaoLevelByType = getWuDaoLevelByType(WuDaoType);
		return jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType]["Max"].I;
	}

	public int GetAllWuDaoDian()
	{
		int num = 0;
		foreach (JSONObject item in jsonData.instance.LevelUpDataJsonData.list)
		{
			if (item["level"].I < entity.level)
			{
				num += item["wudaodian"].I;
			}
		}
		num += entity._WuDaoDian;
		foreach (JSONObject item2 in jsonData.instance.WuDaoAllTypeJson.list)
		{
			if (getWuDaoLevelByType(item2["id"].I) >= 3)
			{
				num++;
			}
		}
		return num;
	}

	public int getStudyCast()
	{
		List<SkillItem> allWuDaoSkills = GetAllWuDaoSkills();
		int num = 0;
		foreach (SkillItem item in allWuDaoSkills)
		{
			num += jsonData.instance.WuDaoJson[item.itemId.ToString()]["Cast"].I;
		}
		return num;
	}

	public int GetNowWuDaoDian()
	{
		return GetAllWuDaoDian() - getStudyCast();
	}

	public bool addWuDaoSkill(int WuDaoType, int type)
	{
		JSONObject wuDaoStudy = getWuDaoStudy(WuDaoType);
		if (!wuDaoStudy.HasItem(type))
		{
			wuDaoStudy.Add(type);
			WuDaoStaticSkill.resetWuDaoSeid(entity);
			PlayTutorial.CheckGanWuTianDi3();
			return true;
		}
		return false;
	}

	public void addWuDaoEx(int WuDaoType, int exNum)
	{
		JSONObject wuDaoTypeJson = getWuDaoTypeJson(WuDaoType);
		int i = wuDaoTypeJson["ex"].I;
		wuDaoTypeJson.SetField("ex", i + exNum);
		PlayTutorial.CheckGanWuTianDi2();
		PlayTutorial.CheckGanWuTianDi4();
		PlayTutorial.CheckLianQi2();
	}

	public void AddLingGuang(string name, int type, int studyTime, int guoqiTime, string desc, int quality, bool isLunDao = false)
	{
		JSONObject jSONObject = new JSONObject(JSONObject.Type.ARRAY);
		jSONObject.AddField("uuid", Tools.getUUID());
		jSONObject.AddField("name", name);
		jSONObject.AddField("type", type);
		jSONObject.AddField("studyTime", studyTime);
		jSONObject.AddField("guoqiTime", guoqiTime);
		jSONObject.AddField("desc", desc);
		jSONObject.AddField("quality", quality);
		jSONObject.AddField("startTime", entity.worldTimeMag.getNowTime().ToString());
		jSONObject.AddField("isLunDao", isLunDao);
		entity.LingGuang.Add(jSONObject);
	}

	public void AutoReomveLingGuang()
	{
		JArray val = JArray.Parse(entity.LingGuang.ToString());
		for (int i = 0; i < ((JContainer)val).Count; i++)
		{
			if (IsLingGuangGuoQi((string)val[i][(object)"startTime"], (int)val[i][(object)"guoqiTime"]))
			{
				val.RemoveAt(i);
				i--;
			}
		}
		entity.LingGuang = new JSONObject(((object)val).ToString());
	}

	public int CalcGanWuTime(JSONObject json)
	{
		int wuXin = (int)Tools.instance.getPlayer().wuXin;
		float num = (8f * (float)Math.Pow(10.0, -6.0) * (float)Math.Pow(wuXin, 3.0) - 0.0042f * (float)Math.Pow(wuXin, 2.0) + 0.7922f * (float)wuXin - 0.3381f) / 100f + 1f;
		return (int)(json["studyTime"].n / num);
	}

	public void StudyLingGuang(string uuid)
	{
		JSONObject jSONObject = entity.LingGuang.list.Find((JSONObject aa) => aa["uuid"].str == uuid);
		if (jSONObject != null)
		{
			item.AddWuDao(jSONObject["studyTime"].I, jsonData.instance.WuDaoExBeiLuJson["1"]["lingguang" + jSONObject["quality"].I].n, new List<int> { jSONObject["type"].I }, 1);
			int addday = CalcGanWuTime(jSONObject);
			if (jSONObject.HasField("isLunDao") && jSONObject["isLunDao"].b)
			{
				PlayTutorial.CheckGanWuTianDi1();
			}
			removeLingGuang(uuid);
			entity.AddTime(addday);
		}
	}

	public void StudyLingGuang(string uuid, int day, float precent)
	{
		JSONObject jSONObject = entity.LingGuang.list.Find((JSONObject aa) => aa["uuid"].str == uuid);
		if (jSONObject != null)
		{
			item.AddWuDao((int)((float)(jSONObject["studyTime"].I * jsonData.instance.WuDaoExBeiLuJson["1"]["lingguang" + jSONObject["quality"].I].I) * precent), new List<int> { jSONObject["type"].I }, 1);
			if (jSONObject.HasField("isLunDao") && jSONObject["isLunDao"].b)
			{
				PlayTutorial.CheckGanWuTianDi1();
			}
			removeLingGuang(uuid);
			entity.AddTime(day);
		}
	}

	public void removeLingGuang(string uuid)
	{
		JArray val = JArray.Parse(entity.LingGuang.ToString());
		for (int i = 0; i < ((JContainer)val).Count; i++)
		{
			if ((string)val[i][(object)"uuid"] == uuid)
			{
				val.Remove(val[i]);
				break;
			}
		}
		entity.LingGuang = new JSONObject(((object)val).ToString());
	}

	public bool IsLingGuangGuoQi(string _startTime, int GuoqiTaime)
	{
		DateTime startTime = DateTime.Parse(_startTime);
		DateTime endTime = startTime.AddDays(GuoqiTaime);
		if (Tools.instance.IsInTime(entity.worldTimeMag.getNowTime(), startTime, endTime))
		{
			return false;
		}
		return true;
	}

	public void autoAddBiGuanLingGuang(int month)
	{
		entity.BiGuanLingGuangTime += month;
		int num = entity.BiGuanLingGuangTime / 12;
		bool flag = false;
		JToken val = null;
		foreach (KeyValuePair<string, JToken> item in jsonData.instance.BiGuanWuDao)
		{
			if (num >= (int)item.Value[(object)"biguanTime"])
			{
				flag = true;
				val = item.Value;
			}
			else if (flag)
			{
				JToken val2 = val;
				entity.BiGuanLingGuangTime -= (int)val2[(object)"biguanTime"];
				int zhuXiuSkill = entity.getZhuXiuSkill();
				JSONObject jSONObject = jsonData.instance.StaticSkillJsonData[zhuXiuSkill.ToString()];
				JSONObject skillWuDaoType = GetSkillWuDaoType(jSONObject["Skill_ID"].I);
				string text = Tools.Code64(jsonData.instance.WuDaoAllTypeJson[skillWuDaoType[0].I.ToString()]["name"].str);
				string desc = ((string)val2[(object)"desc"]).Replace("{skill}", Tools.instance.getStaticSkillName(zhuXiuSkill)).Replace("{type}", text ?? "");
				string name = ((string)val2[(object)"name"]).Replace("{skill}", Tools.instance.getStaticSkillName(zhuXiuSkill)).Replace("{type}", text ?? "");
				AddLingGuang(name, skillWuDaoType[0].I, (int)val2[(object)"studyTime"], (int)val2[(object)"guoqiTime"], desc, (int)val2[(object)"quality"]);
				break;
			}
		}
	}

	public JSONObject GetSkillWuDaoType(int _skillId)
	{
		foreach (KeyValuePair<string, JSONObject> itemJsonDatum in jsonData.instance.ItemJsonData)
		{
			if (itemJsonDatum.Value["type"].I == 4)
			{
				float result = 0f;
				if (float.TryParse(itemJsonDatum.Value["desc"].str, out result) && (int)result == _skillId)
				{
					return itemJsonDatum.Value["wuDao"];
				}
			}
		}
		return null;
	}

	public void AddLingGuangByJsonID(int ID)
	{
		JSONObject jSONObject = jsonData.instance.LingGuangJson[ID.ToString()];
		AddLingGuang(jSONObject["name"].str, jSONObject["type"].I, jSONObject["studyTime"].I, jSONObject["guoqiTime"].I, jSONObject["desc"].str, jSONObject["quality"].I);
	}

	public void SetWuDaoEx(int WuDaoType, int exNum)
	{
		getWuDaoTypeJson(WuDaoType).SetField("ex", exNum);
	}

	public bool IsStudy(int id)
	{
		int wuDaoType = WuDaoJson.DataDict[id].Type[0];
		if (getWuDaoStudy(wuDaoType).ToList().Contains(id))
		{
			return true;
		}
		return false;
	}

	public List<SkillItem> GetHasWuDaoSkillItems(int WuDaoType)
	{
		JSONObject wuDaoStudy = getWuDaoStudy(WuDaoType);
		List<SkillItem> list = new List<SkillItem>();
		foreach (JSONObject item in wuDaoStudy.list)
		{
			SkillItem skillItem = new SkillItem();
			skillItem.itemId = item.I;
			list.Add(skillItem);
		}
		return list;
	}

	public List<SkillItem> GetAllWuDaoSkills()
	{
		List<SkillItem> AllWuDaoSkillList = new List<SkillItem>();
		foreach (string key in entity.WuDaoJson.keys)
		{
			int result = -1;
			if (!int.TryParse(key, out result))
			{
				continue;
			}
			GetHasWuDaoSkillItems(result).ForEach(delegate(SkillItem aa)
			{
				if (AllWuDaoSkillList.Find((SkillItem cc) => cc.itemId == aa.itemId) == null)
				{
					AllWuDaoSkillList.Add(aa);
				}
			});
		}
		return AllWuDaoSkillList;
	}

	public JSONObject getMonstarWuDaoJson(int MonstarID)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[MonstarID.ToString()];
		int monstarWuDao = jSONObject["wudaoType"].I;
		int monstarlv = jSONObject["Level"].I;
		return jsonData.instance.NPCWuDaoJson.list.Find((JSONObject _cc) => monstarlv == _cc["lv"].I && _cc["Type"].I == monstarWuDao);
	}

	public void MonstarAddWuDaoList(int MonstarID)
	{
		JSONObject monstarWuDaoJson = getMonstarWuDaoJson(MonstarID);
		if (monstarWuDaoJson == null)
		{
			return;
		}
		foreach (JSONObject item in monstarWuDaoJson["wudaoID"].list)
		{
			try
			{
				addWuDaoSkill(1, item.I);
				new WuDaoStaticSkill(item.I, 0, 5).putingStudySkill(entity, entity);
			}
			catch (Exception)
			{
				Debug.LogError((object)("悟道特性报错武将ID" + MonstarID + "特性" + item.I));
			}
		}
	}

	public List<SkillItem> MonstarGetAllWuDaoSkills(int MonstarID)
	{
		List<SkillItem> list = new List<SkillItem>();
		JSONObject monstarWuDaoJson = getMonstarWuDaoJson(MonstarID);
		if (monstarWuDaoJson != null)
		{
			foreach (JSONObject item in monstarWuDaoJson["wudaoID"].list)
			{
				SkillItem skillItem = new SkillItem();
				skillItem.itemId = item.I;
				list.Add(skillItem);
			}
		}
		return list;
	}
}
