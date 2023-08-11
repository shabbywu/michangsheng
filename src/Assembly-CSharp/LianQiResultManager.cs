using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bag;
using JSONClass;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;
using YSGame.Fight;

public class LianQiResultManager : MonoBehaviour
{
	[SerializeField]
	private InputField inputFieldEquipName;

	[SerializeField]
	private Image equipImage;

	public bool lianQiResultPanelIsOpen;

	public void init()
	{
		((Component)this).gameObject.SetActive(false);
		lianQiResultPanelIsOpen = false;
	}

	public void openLianQiResultPanel()
	{
		int selectZhongLei = LianQiTotalManager.inst.selectTypePageManager.getSelectZhongLei();
		inputFieldEquipName.text = "灵" + ((object)jsonData.instance.LianQiEquipType[selectZhongLei.ToString()][(object)"desc"]).ToString();
		((Component)this).gameObject.SetActive(true);
		UpdateEquipImage();
		lianQiResultPanelIsOpen = true;
	}

	public void closeLianQiResultPanel()
	{
		inputFieldEquipName.text = "";
		((Component)this).gameObject.SetActive(false);
		lianQiResultPanelIsOpen = false;
	}

	public void LianQiCallBack()
	{
		closeLianQiResultPanel();
		Dictionary<int, int> needDict = LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.putCaiLiaoCell.GetNeedDict();
		Dictionary<int, BaseItem> dictionary = new Dictionary<int, BaseItem>();
		foreach (int key in needDict.Keys)
		{
			BaseItem item = LianQiTotalManager.inst.Bag.GetItem(key);
			if (item != null)
			{
				dictionary.Add(key, item);
			}
		}
		foreach (PutMaterialCell caiLiaoCell in LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.putCaiLiaoCell.caiLiaoCells)
		{
			if (dictionary.ContainsKey(caiLiaoCell.Item.Id))
			{
				int id = caiLiaoCell.Item.Id;
				dictionary[id].Count--;
				caiLiaoCell.Item.Uid = dictionary[id].Uid;
				LianQiTotalManager.inst.Bag.RemoveTempItem(dictionary[id].Uid, 1);
				if (dictionary[id].Count <= 0)
				{
					dictionary.Remove(id);
				}
			}
			else
			{
				caiLiaoCell.SetNull();
			}
		}
		LianQiTotalManager.inst.putCaiLiaoCallBack();
		LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.putCaiLiaoCell.caiLiaoCellParent.SetActive(true);
	}

	public Dictionary<int, int> GetRemoveDict(Dictionary<int, int> needDict)
	{
		Avatar player = Tools.instance.getPlayer();
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (int key in needDict.Keys)
		{
			int num = player.getItemNum(key) - needDict[key];
			if (num < 0)
			{
				num = -num;
				dictionary.Add(key, num);
			}
		}
		return dictionary;
	}

	public void setEquipNameClick()
	{
		Regex regex = new Regex("^[一-龥a-zA-Z0-9]+$");
		if (inputFieldEquipName.text != "")
		{
			if (regex.IsMatch(inputFieldEquipName.text))
			{
				createEquip();
			}
			else
			{
				UIPopTip.Inst.Pop("不允许有特殊字符");
			}
		}
		else
		{
			UIPopTip.Inst.Pop("请输入名称");
		}
	}

	private int getItemCD()
	{
		if (LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLinWenType() != 1)
		{
			return 1;
		}
		int selectLingWenID = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLingWenID();
		if (selectLingWenID > 0)
		{
			return jsonData.instance.LianQiLingWenBiao[selectLingWenID.ToString()]["value1"].I;
		}
		return 1;
	}

	private JSONObject AddItemSeid(int seid, int value1 = -9999, int value2 = -9999)
	{
		JSONObject jSONObject = new JSONObject();
		jSONObject.SetField("id", seid);
		if (value1 != -9999)
		{
			jSONObject.SetField("value1", value1);
		}
		if (value2 != -9999)
		{
			jSONObject.SetField("value2", value2);
		}
		return jSONObject;
	}

	private void GetEquipSkillSeid(JSONObject skillSeids, JSONObject itemSeid, ref int Damage, ref string seidDesc, JSONObject shuXingIdList)
	{
		skillSeids.Add(AddItemSeid(29, getItemCD()));
		Dictionary<int, int> entryDictionary = LianQiTotalManager.inst.putMaterialPageManager.showXiaoGuoManager.entryDictionary;
		foreach (int key in entryDictionary.Keys)
		{
			shuXingIdList.Add(key);
		}
		JSONObject lianQiHeCheng = jsonData.instance.LianQiHeCheng;
		int selectLinWenType = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLinWenType();
		int curSelectEquipType = LianQiTotalManager.inst.getCurSelectEquipType();
		SetLingWenSeid(skillSeids, itemSeid);
		foreach (int key2 in entryDictionary.Keys)
		{
			string descBy = LianQiTotalManager.inst.putMaterialPageManager.showXiaoGuoManager.getDescBy(key2);
			if (descBy != "")
			{
				seidDesc += descBy;
				seidDesc += "\n";
			}
			int num = entryDictionary[key2];
			if (key2 == 49)
			{
				int duoDuanIDByLingLi = LianQiTotalManager.inst.putMaterialPageManager.showXiaoGuoManager.getDuoDuanIDByLingLi(num);
				if (duoDuanIDByLingLi != 0)
				{
					JSONObject jSONObject = jsonData.instance.LianQiDuoDuanShangHaiBiao[duoDuanIDByLingLi.ToString()];
					JSONObject jSONObject2 = new JSONObject();
					jSONObject2.SetField("id", jSONObject["seid"].I);
					jSONObject2.SetField("value1", jSONObject["value1"].I);
					jSONObject2.SetField("value2", jSONObject["value2"].I);
					jSONObject2.SetField("value3", jSONObject["value3"].I);
					jSONObject2.SetField("AttackType", GetEquipAttackType());
					skillSeids.Add(jSONObject2);
				}
				continue;
			}
			if (lianQiHeCheng[key2.ToString()]["seid"].I != 0)
			{
				JSONObject jSONObject3 = new JSONObject();
				jSONObject3.SetField("id", lianQiHeCheng[key2.ToString()]["seid"].I);
				for (int i = 1; i < 3; i++)
				{
					int num2 = ((!lianQiHeCheng[key2.ToString()]["fanbei"].HasItem(i)) ? 1 : num);
					if (lianQiHeCheng[key2.ToString()].HasField("intvalue" + i) && lianQiHeCheng[key2.ToString()]["intvalue" + i].I != 0)
					{
						jSONObject3.SetField("value" + i, lianQiHeCheng[key2.ToString()]["intvalue" + i].I * num2);
					}
				}
				for (int j = 1; j < 3; j++)
				{
					if (!lianQiHeCheng[key2.ToString()].HasField("listvalue" + j) || lianQiHeCheng[key2.ToString()]["listvalue" + j].list.Count == 0)
					{
						continue;
					}
					int num3 = ((!lianQiHeCheng[key2.ToString()]["fanbei"].HasItem(j)) ? 1 : num);
					JSONObject jSONObject4 = new JSONObject(JSONObject.Type.ARRAY);
					foreach (JSONObject item in lianQiHeCheng[key2.ToString()]["listvalue" + j].list)
					{
						jSONObject4.Add(item.I * num3);
					}
					jSONObject3.SetField("value" + j, jSONObject4);
				}
				skillSeids.Add(jSONObject3);
			}
			if (lianQiHeCheng[key2.ToString()]["Itemseid"].I != 0)
			{
				JSONObject jSONObject5 = new JSONObject();
				int num4 = num;
				jSONObject5.SetField("id", lianQiHeCheng[key2.ToString()]["Itemseid"].I);
				JSONObject jSONObject6 = new JSONObject(JSONObject.Type.ARRAY);
				JSONObject jSONObject7 = new JSONObject(JSONObject.Type.ARRAY);
				for (int k = 0; k < lianQiHeCheng[key2.ToString()]["Itemintvalue1"].Count; k++)
				{
					jSONObject6.Add(lianQiHeCheng[key2.ToString()]["Itemintvalue1"][k].I);
					jSONObject7.Add(lianQiHeCheng[key2.ToString()]["Itemintvalue2"][k].I * num4);
				}
				jSONObject5.SetField("value1", jSONObject6);
				jSONObject5.SetField("value2", jSONObject7);
				itemSeid.Add(jSONObject5);
			}
			Damage += lianQiHeCheng[key2.ToString()]["HP"].I * num;
		}
		if (selectLinWenType == 2 || selectLinWenType == 3)
		{
			string text = LianQiTotalManager.inst.putMaterialPageManager.showXiaoGuoManager.getLingWenDesc();
			if (text.Contains("造成<color=#fff227>x"))
			{
				text = text.Replace("x", "");
			}
			seidDesc = seidDesc + text + "\n";
		}
		if (selectLinWenType == 4)
		{
			string text2 = LianQiTotalManager.inst.putMaterialPageManager.showXiaoGuoManager.getLingWenDesc();
			if (curSelectEquipType != 1)
			{
				text2 = text2.Replace("使用", "生效");
			}
			seidDesc = seidDesc + text2 + "\n";
		}
		if (seidDesc.Contains("<color=#ff624d>"))
		{
			seidDesc = seidDesc.Replace("<color=#ff624d>", "");
		}
		if (seidDesc.Contains("<color=#fff227>"))
		{
			seidDesc = seidDesc.Replace("<color=#fff227>", "");
		}
		if (seidDesc.Contains("<color=#ff724d>"))
		{
			seidDesc = seidDesc.Replace("<color=#ff724d>", "");
		}
		if (seidDesc.Contains("<color=#f5e929>"))
		{
			seidDesc = seidDesc.Replace("<color=#f5e929>", "");
		}
		if (seidDesc.Contains("</color>"))
		{
			seidDesc = seidDesc.Replace("</color>", "");
		}
	}

	public void createEquip()
	{
		try
		{
			Avatar player = Tools.instance.getPlayer();
			int curSelectEquipMuBanID = LianQiTotalManager.inst.getCurSelectEquipMuBanID();
			int Damage = 0;
			int num = 0;
			int num2 = 0;
			string seidDesc = "";
			JSONObject jSONObject = new JSONObject(JSONObject.Type.ARRAY);
			JSONObject jSONObject2 = new JSONObject(JSONObject.Type.ARRAY);
			JSONObject jSONObject3 = Tools.CreateItemSeid(curSelectEquipMuBanID);
			JSONObject jSONObject4 = new JSONObject(JSONObject.Type.ARRAY);
			if (!LianQiTotalManager.inst.putMaterialPageManager.showXiaoGuoManager.checkHasOneChiTiao())
			{
				return;
			}
			GetEquipSkillSeid(jSONObject, jSONObject2, ref Damage, ref seidDesc, jSONObject4);
			JToken val = LianQiTotalManager.inst.getcurEquipQualityDate();
			if (val != null)
			{
				num = (int)val[(object)"quality"];
				num2 = (int)val[(object)"shangxia"];
				jSONObject3.AddField("SkillSeids", jSONObject);
				jSONObject3.AddField("shuXingIdList", jSONObject4);
				if (jSONObject2.list.Count > 0)
				{
					jSONObject3.AddField("ItemSeids", jSONObject2);
				}
				jSONObject3.AddField("ItemID", curSelectEquipMuBanID);
				jSONObject3.AddField("Name", inputFieldEquipName.text);
				jSONObject3.AddField("SeidDesc", seidDesc);
				jSONObject3.AddField("ItemIcon", GetIconPath());
				if (Damage > 0)
				{
					jSONObject3.AddField("Damage", Damage);
				}
				jSONObject3.AddField("quality", num);
				jSONObject3.AddField("QPingZhi", num2);
				jSONObject3.AddField("qualitydesc", LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.showEquipCell.getEquipDesc());
				jSONObject3.AddField("Desc", GetEquipDesc(num, num2));
				new JSONObject(JSONObject.Type.ARRAY);
				jSONObject3.AddField("AttackType", GetEquipAttackType());
				jSONObject3.AddField("Money", getEquipMoney());
				jSONObject3.AddField("ItemFlag", GetEquipItemFlag());
				player.addItem(curSelectEquipMuBanID, 1, jSONObject3);
				removeCaiLiao();
				addLianQiWuDaoExp();
				int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
				PlayTutorial.CheckLianQi3(num, num2);
				Tools.instance.Save(@int, 0);
				UIPopTip.Inst.Pop("获得" + inputFieldEquipName.text);
				LianQiCallBack();
			}
		}
		catch (Exception ex)
		{
			Debug.Log((object)ex);
		}
	}

	public string GetImagePath()
	{
		int equipQuality = (int)LianQiTotalManager.inst.getcurEquipQualityDate()[(object)"quality"];
		int i = GetEquipAttackType()[0].I;
		return FightFaBaoShow.GetEquipFightShowPath(LianQiTotalManager.inst.selectTypePageManager.getSelectZhongLei(), i, equipQuality).Replace("FightFaBao", "LianQiImage");
	}

	public string GetIconPath()
	{
		int equipQuality = (int)LianQiTotalManager.inst.getcurEquipQualityDate()[(object)"quality"];
		int i = GetEquipAttackType()[0].I;
		return FightFaBaoShow.GetEquipFightShowPath(LianQiTotalManager.inst.selectTypePageManager.getSelectZhongLei(), i, equipQuality).Replace("FightFaBao", "LianQiIcon");
	}

	public void UpdateEquipImage()
	{
		equipImage.sprite = ResManager.inst.LoadSprite(GetImagePath());
		((Graphic)equipImage).SetNativeSize();
		((Component)equipImage).gameObject.SetActive(true);
	}

	public void SetLingWenSeid(JSONObject skillSeids, JSONObject itemSeid)
	{
		int selectLingWenID = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLingWenID();
		int curSelectEquipType = LianQiTotalManager.inst.getCurSelectEquipType();
		Debug.Log((object)$"LingWenID:{selectLingWenID}");
		Debug.Log((object)$"equipType:{curSelectEquipType}");
		if (selectLingWenID == -1)
		{
			return;
		}
		JSONObject jSONObject = jsonData.instance.LianQiLingWenBiao[selectLingWenID.ToString()];
		if (jSONObject["type"].I == 1)
		{
			return;
		}
		JSONObject jSONObject2 = new JSONObject();
		switch (curSelectEquipType)
		{
		case 1:
			jSONObject2.SetField("id", jSONObject["seid"].I);
			if (jSONObject["seid"].I == 77)
			{
				jSONObject2.SetField("value1", jSONObject["listvalue1"]);
			}
			else if (jSONObject["seid"].I == 80)
			{
				jSONObject2.SetField("value1", jSONObject["listvalue1"]);
				jSONObject2.SetField("value2", jSONObject["listvalue2"]);
			}
			else if (jSONObject["seid"].I == 145)
			{
				jSONObject2.SetField("value1", jSONObject["listvalue1"][0]);
			}
			skillSeids.Add(jSONObject2);
			break;
		case 2:
		case 3:
			jSONObject2.SetField("id", jSONObject["Itemseid"].I);
			if (jSONObject["seid"].I == 62)
			{
				jSONObject2.SetField("value1", jSONObject["Itemintvalue1"]);
			}
			else
			{
				jSONObject2.SetField("value1", jSONObject["Itemintvalue1"]);
				jSONObject2.SetField("value2", jSONObject["Itemintvalue2"]);
			}
			itemSeid.Add(jSONObject2);
			break;
		}
	}

	private JSONObject GetEquipAttackType()
	{
		JSONObject jSONObject = new JSONObject(JSONObject.Type.ARRAY);
		JSONObject jSONObject2 = new JSONObject(JSONObject.Type.ARRAY);
		List<int> list = new List<int>();
		List<PutMaterialCell> caiLiaoCells = LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.putCaiLiaoCell.caiLiaoCells;
		int num = 0;
		for (int i = 0; i < caiLiaoCells.Count; i++)
		{
			if (caiLiaoCells[i].attackType != -1 && !list.Contains(caiLiaoCells[i].attackType))
			{
				list.Add(caiLiaoCells[i].attackType);
				if (caiLiaoCells[i].attackType != 5)
				{
					num++;
					jSONObject2.Add(caiLiaoCells[i].attackType);
				}
				jSONObject.Add(caiLiaoCells[i].attackType);
			}
		}
		if (num > 0)
		{
			return jSONObject2;
		}
		return jSONObject;
	}

	private JSONObject GetEquipItemFlag()
	{
		JSONObject jSONObject = new JSONObject(JSONObject.Type.ARRAY);
		try
		{
			int num = LianQiTotalManager.inst.getCurSelectEquipType() * 100 + (int)LianQiTotalManager.inst.getcurEquipQualityDate()[(object)"quality"];
			jSONObject.Add(num);
			JSONObject equipAttackType = GetEquipAttackType();
			for (int i = 0; i < equipAttackType.Count; i++)
			{
				int val = num * 10 + equipAttackType[i].I;
				jSONObject.Add(val);
			}
			jSONObject.Add(LianQiTotalManager.inst.getCurSelectEquipType());
		}
		catch (Exception arg)
		{
			Debug.LogError((object)$"GetEquipItemFlag:{arg}");
		}
		return jSONObject;
	}

	private int getEquipMoney()
	{
		JToken val = LianQiTotalManager.inst.getcurEquipQualityDate();
		if (val != null)
		{
			return (int)val[(object)"price"];
		}
		return -1;
	}

	private string GetEquipDesc(int quality, int _typepingji)
	{
		Avatar player = Tools.instance.getPlayer();
		string text = player.firstName + player.lastName;
		string text2 = player.worldTimeMag.getNowTime().Year.ToString();
		List<PutMaterialCell> caiLiaoCells = LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.putCaiLiaoCell.caiLiaoCells;
		string text3 = "";
		for (int i = 0; i < 3; i++)
		{
			text3 += Tools.Code64(_ItemJsonData.DataDict[caiLiaoCells[i].Item.Id].name);
			if (i != 2)
			{
				text3 += "、";
			}
		}
		string text4 = (player.checkHasStudyWuDaoSkillByID(2213) ? "五行相合" : "普通");
		string equipDesc = LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.showEquipCell.getEquipDesc();
		string text5 = ((object)jsonData.instance.LianQiEquipType[LianQiTotalManager.inst.selectTypePageManager.getSelectZhongLei().ToString()][(object)"desc"]).ToString();
		string text6 = "";
		int selectLingWenID = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLingWenID();
		text6 = ((selectLingWenID == -1) ? "聚灵灵纹。" : (jsonData.instance.LianQiLingWenBiao[selectLingWenID.ToString()]["name"].Str + "灵纹,灵力更加强大，但对使用者也要求更高。"));
		return text + "于" + text2 + "年以" + text4 + "的手法将" + text3 + "等材料炼制的" + equipDesc + "，此" + text5 + "铭刻" + text6;
	}

	private void removeCaiLiao()
	{
		Avatar player = Tools.instance.getPlayer();
		List<PutMaterialCell> caiLiaoCells = LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.putCaiLiaoCell.caiLiaoCells;
		for (int i = 0; i < caiLiaoCells.Count; i++)
		{
			if (caiLiaoCells[i].Item.Id != -1)
			{
				player.removeItem(caiLiaoCells[i].Item.Id);
			}
		}
	}

	public void lianQiFailResult()
	{
		LianQiTotalManager.inst.CloseBlack();
		try
		{
			removeCaiLiao();
			reduceHp();
			LianQiCallBack();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public void lianQiSuccessResult()
	{
		LianQiTotalManager.inst.CloseBlack();
		LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.putCaiLiaoCell.caiLiaoCellParent.SetActive(false);
		openLianQiResultPanel();
	}

	private void reduceHp()
	{
		Avatar player = Tools.instance.getPlayer();
		JToken val = LianQiTotalManager.inst.getcurEquipQualityDate();
		int num = 1;
		if (val != null)
		{
			num = (int)val[(object)"quality"];
		}
		JSONObject lianQiJieSuanBiao = jsonData.instance.LianQiJieSuanBiao;
		int num2 = player.HP - lianQiJieSuanBiao[num.ToString()]["damage"].I;
		if (num2 <= 0)
		{
			UIDeath.Inst.Show(DeathType.器毁人亡);
		}
		else
		{
			player.HP = num2;
		}
	}

	private void addLianQiWuDaoExp()
	{
		Avatar player = Tools.instance.getPlayer();
		JToken val = LianQiTotalManager.inst.getcurEquipQualityDate();
		if (val != null)
		{
			int num = (int)val[(object)"quality"];
			int wuDaoLevelByType = player.wuDaoMag.getWuDaoLevelByType(22);
			JSONObject lianQiJieSuanBiao = jsonData.instance.LianQiJieSuanBiao;
			int num2 = wuDaoLevelByType - num;
			if (num2 < 0)
			{
				Debug.Log((object)$"addLianQiWuDaoExp方法出现问题,equiplevel:{num},wudaoLevel:{num}");
			}
			if (num2 == 0)
			{
				player.wuDaoMag.addWuDaoEx(22, lianQiJieSuanBiao[num.ToString()]["exp"].I);
			}
			if (num2 == 1)
			{
				player.wuDaoMag.addWuDaoEx(22, (int)(lianQiJieSuanBiao[num.ToString()]["exp"].n * 0.5f));
			}
			if (num2 == 2)
			{
				player.wuDaoMag.addWuDaoEx(22, (int)(lianQiJieSuanBiao[num.ToString()]["exp"].n * 0.2f));
			}
		}
	}

	public void addLianQiTime()
	{
		Tools.instance.getPlayer().AddTime(0, getCostTime());
	}

	public int getCostTime()
	{
		Avatar player = Tools.instance.getPlayer();
		JToken val = LianQiTotalManager.inst.getcurEquipQualityDate();
		int num = 1;
		if (val != null)
		{
			num = (int)val[(object)"quality"];
		}
		int num2 = jsonData.instance.LianQiJieSuanBiao[num.ToString()]["time"].I;
		if (player.checkHasStudyWuDaoSkillByID(2221))
		{
			num2 = (int)((float)num2 * 0.7f);
		}
		return num2;
	}
}
