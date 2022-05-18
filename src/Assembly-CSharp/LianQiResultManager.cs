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

// Token: 0x02000463 RID: 1123
public class LianQiResultManager : MonoBehaviour
{
	// Token: 0x06001E10 RID: 7696 RVA: 0x00018FB0 File Offset: 0x000171B0
	public void init()
	{
		base.gameObject.SetActive(false);
		this.lianQiResultPanelIsOpen = false;
	}

	// Token: 0x06001E11 RID: 7697 RVA: 0x001055A0 File Offset: 0x001037A0
	public void openLianQiResultPanel()
	{
		int selectZhongLei = LianQiTotalManager.inst.selectTypePageManager.getSelectZhongLei();
		this.inputFieldEquipName.text = "灵" + jsonData.instance.LianQiEquipType[selectZhongLei.ToString()]["desc"].ToString();
		base.gameObject.SetActive(true);
		this.UpdateEquipImage();
		this.lianQiResultPanelIsOpen = true;
	}

	// Token: 0x06001E12 RID: 7698 RVA: 0x00018FC5 File Offset: 0x000171C5
	public void closeLianQiResultPanel()
	{
		this.inputFieldEquipName.text = "";
		base.gameObject.SetActive(false);
		this.lianQiResultPanelIsOpen = false;
	}

	// Token: 0x06001E13 RID: 7699 RVA: 0x00105610 File Offset: 0x00103810
	public void LianQiCallBack()
	{
		this.closeLianQiResultPanel();
		Dictionary<int, int> needDict = LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.putCaiLiaoCell.GetNeedDict();
		Dictionary<int, BaseItem> dictionary = new Dictionary<int, BaseItem>();
		foreach (int num in needDict.Keys)
		{
			BaseItem item = LianQiTotalManager.inst.Bag.GetItem(num);
			if (item != null)
			{
				dictionary.Add(num, item);
			}
		}
		foreach (PutMaterialCell putMaterialCell in LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.putCaiLiaoCell.caiLiaoCells)
		{
			if (dictionary.ContainsKey(putMaterialCell.Item.Id))
			{
				int id = putMaterialCell.Item.Id;
				dictionary[id].Count--;
				putMaterialCell.Item.Uid = dictionary[id].Uid;
				LianQiTotalManager.inst.Bag.RemoveTempItem(dictionary[id].Uid, 1);
				if (dictionary[id].Count <= 0)
				{
					dictionary.Remove(id);
				}
			}
			else
			{
				putMaterialCell.SetNull();
			}
		}
		LianQiTotalManager.inst.putCaiLiaoCallBack();
		LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.putCaiLiaoCell.caiLiaoCellParent.SetActive(true);
	}

	// Token: 0x06001E14 RID: 7700 RVA: 0x001057AC File Offset: 0x001039AC
	public Dictionary<int, int> GetRemoveDict(Dictionary<int, int> needDict)
	{
		Avatar player = Tools.instance.getPlayer();
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (int num in needDict.Keys)
		{
			int num2 = player.getItemNum(num) - needDict[num];
			if (num2 < 0)
			{
				num2 = -num2;
				dictionary.Add(num, num2);
			}
		}
		return dictionary;
	}

	// Token: 0x06001E15 RID: 7701 RVA: 0x00105830 File Offset: 0x00103A30
	public void setEquipNameClick()
	{
		Regex regex = new Regex("^[一-龥a-zA-Z0-9]+$");
		if (!(this.inputFieldEquipName.text != ""))
		{
			UIPopTip.Inst.Pop("请输入名称", PopTipIconType.叹号);
			return;
		}
		if (regex.IsMatch(this.inputFieldEquipName.text))
		{
			this.createEquip();
			return;
		}
		UIPopTip.Inst.Pop("不允许有特殊字符", PopTipIconType.叹号);
	}

	// Token: 0x06001E16 RID: 7702 RVA: 0x0010589C File Offset: 0x00103A9C
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

	// Token: 0x06001E17 RID: 7703 RVA: 0x000AD1D8 File Offset: 0x000AB3D8
	private JSONObject AddItemSeid(int seid, int value1 = -9999, int value2 = -9999)
	{
		JSONObject jsonobject = new JSONObject();
		jsonobject.SetField("id", seid);
		if (value1 != -9999)
		{
			jsonobject.SetField("value1", value1);
		}
		if (value2 != -9999)
		{
			jsonobject.SetField("value2", value2);
		}
		return jsonobject;
	}

	// Token: 0x06001E18 RID: 7704 RVA: 0x00105904 File Offset: 0x00103B04
	private void GetEquipSkillSeid(JSONObject skillSeids, JSONObject itemSeid, ref int Damage, ref string seidDesc, JSONObject shuXingIdList)
	{
		skillSeids.Add(this.AddItemSeid(29, this.getItemCD(), -9999));
		Dictionary<int, int> entryDictionary = LianQiTotalManager.inst.putMaterialPageManager.showXiaoGuoManager.entryDictionary;
		foreach (int val in entryDictionary.Keys)
		{
			shuXingIdList.Add(val);
		}
		JSONObject lianQiHeCheng = jsonData.instance.LianQiHeCheng;
		int selectLinWenType = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLinWenType();
		int curSelectEquipType = LianQiTotalManager.inst.getCurSelectEquipType();
		this.SetLingWenSeid(skillSeids, itemSeid);
		foreach (int num in entryDictionary.Keys)
		{
			string descBy = LianQiTotalManager.inst.putMaterialPageManager.showXiaoGuoManager.getDescBy(num);
			if (descBy != "")
			{
				seidDesc += descBy;
				seidDesc += "\n";
			}
			int num2 = entryDictionary[num];
			if (num == 49)
			{
				int duoDuanIDByLingLi = LianQiTotalManager.inst.putMaterialPageManager.showXiaoGuoManager.getDuoDuanIDByLingLi(num2);
				if (duoDuanIDByLingLi != 0)
				{
					JSONObject jsonobject = jsonData.instance.LianQiDuoDuanShangHaiBiao[duoDuanIDByLingLi.ToString()];
					JSONObject jsonobject2 = new JSONObject();
					jsonobject2.SetField("id", jsonobject["seid"].I);
					jsonobject2.SetField("value1", jsonobject["value1"].I);
					jsonobject2.SetField("value2", jsonobject["value2"].I);
					jsonobject2.SetField("value3", jsonobject["value3"].I);
					jsonobject2.SetField("AttackType", this.GetEquipAttackType());
					skillSeids.Add(jsonobject2);
				}
			}
			else
			{
				if (lianQiHeCheng[num.ToString()]["seid"].I != 0)
				{
					JSONObject jsonobject3 = new JSONObject();
					jsonobject3.SetField("id", lianQiHeCheng[num.ToString()]["seid"].I);
					for (int i = 1; i < 3; i++)
					{
						int num3 = lianQiHeCheng[num.ToString()]["fanbei"].HasItem(i) ? num2 : 1;
						if (lianQiHeCheng[num.ToString()].HasField("intvalue" + i) && lianQiHeCheng[num.ToString()]["intvalue" + i].I != 0)
						{
							jsonobject3.SetField("value" + i, lianQiHeCheng[num.ToString()]["intvalue" + i].I * num3);
						}
					}
					for (int j = 1; j < 3; j++)
					{
						if (lianQiHeCheng[num.ToString()].HasField("listvalue" + j) && lianQiHeCheng[num.ToString()]["listvalue" + j].list.Count != 0)
						{
							int num4 = lianQiHeCheng[num.ToString()]["fanbei"].HasItem(j) ? num2 : 1;
							JSONObject jsonobject4 = new JSONObject(JSONObject.Type.ARRAY);
							foreach (JSONObject jsonobject5 in lianQiHeCheng[num.ToString()]["listvalue" + j].list)
							{
								jsonobject4.Add(jsonobject5.I * num4);
							}
							jsonobject3.SetField("value" + j, jsonobject4);
						}
					}
					skillSeids.Add(jsonobject3);
				}
				if (lianQiHeCheng[num.ToString()]["Itemseid"].I != 0)
				{
					JSONObject jsonobject6 = new JSONObject();
					int num5 = num2;
					jsonobject6.SetField("id", lianQiHeCheng[num.ToString()]["Itemseid"].I);
					JSONObject jsonobject7 = new JSONObject(JSONObject.Type.ARRAY);
					JSONObject jsonobject8 = new JSONObject(JSONObject.Type.ARRAY);
					for (int k = 0; k < lianQiHeCheng[num.ToString()]["Itemintvalue1"].Count; k++)
					{
						jsonobject7.Add(lianQiHeCheng[num.ToString()]["Itemintvalue1"][k].I);
						jsonobject8.Add(lianQiHeCheng[num.ToString()]["Itemintvalue2"][k].I * num5);
					}
					jsonobject6.SetField("value1", jsonobject7);
					jsonobject6.SetField("value2", jsonobject8);
					itemSeid.Add(jsonobject6);
				}
				Damage += lianQiHeCheng[num.ToString()]["HP"].I * num2;
			}
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

	// Token: 0x06001E19 RID: 7705 RVA: 0x00106010 File Offset: 0x00104210
	public void createEquip()
	{
		try
		{
			Avatar player = Tools.instance.getPlayer();
			int curSelectEquipMuBanID = LianQiTotalManager.inst.getCurSelectEquipMuBanID();
			int num = 0;
			string val = "";
			JSONObject jsonobject = new JSONObject(JSONObject.Type.ARRAY);
			JSONObject jsonobject2 = new JSONObject(JSONObject.Type.ARRAY);
			JSONObject jsonobject3 = Tools.CreateItemSeid(curSelectEquipMuBanID);
			JSONObject jsonobject4 = new JSONObject(JSONObject.Type.ARRAY);
			if (LianQiTotalManager.inst.putMaterialPageManager.showXiaoGuoManager.checkHasOneChiTiao())
			{
				this.GetEquipSkillSeid(jsonobject, jsonobject2, ref num, ref val, jsonobject4);
				JToken jtoken = LianQiTotalManager.inst.getcurEquipQualityDate();
				if (jtoken != null)
				{
					int num2 = (int)jtoken["quality"];
					int num3 = (int)jtoken["shangxia"];
					jsonobject3.AddField("SkillSeids", jsonobject);
					jsonobject3.AddField("shuXingIdList", jsonobject4);
					if (jsonobject2.list.Count > 0)
					{
						jsonobject3.AddField("ItemSeids", jsonobject2);
					}
					jsonobject3.AddField("ItemID", curSelectEquipMuBanID);
					jsonobject3.AddField("Name", this.inputFieldEquipName.text);
					jsonobject3.AddField("SeidDesc", val);
					jsonobject3.AddField("ItemIcon", this.GetIconPath());
					if (num > 0)
					{
						jsonobject3.AddField("Damage", num);
					}
					jsonobject3.AddField("quality", num2);
					jsonobject3.AddField("QPingZhi", num3);
					jsonobject3.AddField("qualitydesc", LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.showEquipCell.getEquipDesc());
					jsonobject3.AddField("Desc", this.GetEquipDesc(num2, num3));
					new JSONObject(JSONObject.Type.ARRAY);
					jsonobject3.AddField("AttackType", this.GetEquipAttackType());
					jsonobject3.AddField("Money", this.getEquipMoney());
					jsonobject3.AddField("ItemFlag", this.GetEquipItemFlag());
					player.addItem(curSelectEquipMuBanID, 1, jsonobject3, false);
					this.removeCaiLiao();
					this.addLianQiWuDaoExp();
					int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
					PlayTutorial.CheckLianQi3(num2, num3);
					Tools.instance.Save(@int, 0, null);
					UIPopTip.Inst.Pop("获得" + this.inputFieldEquipName.text, PopTipIconType.叹号);
					this.LianQiCallBack();
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex);
		}
	}

	// Token: 0x06001E1A RID: 7706 RVA: 0x0010626C File Offset: 0x0010446C
	public string GetImagePath()
	{
		int equipQuality = (int)LianQiTotalManager.inst.getcurEquipQualityDate()["quality"];
		int i = this.GetEquipAttackType()[0].I;
		return FightFaBaoShow.GetEquipFightShowPath(LianQiTotalManager.inst.selectTypePageManager.getSelectZhongLei(), i, equipQuality).Replace("FightFaBao", "LianQiImage");
	}

	// Token: 0x06001E1B RID: 7707 RVA: 0x001062CC File Offset: 0x001044CC
	public string GetIconPath()
	{
		int equipQuality = (int)LianQiTotalManager.inst.getcurEquipQualityDate()["quality"];
		int i = this.GetEquipAttackType()[0].I;
		return FightFaBaoShow.GetEquipFightShowPath(LianQiTotalManager.inst.selectTypePageManager.getSelectZhongLei(), i, equipQuality).Replace("FightFaBao", "LianQiIcon");
	}

	// Token: 0x06001E1C RID: 7708 RVA: 0x00018FEA File Offset: 0x000171EA
	public void UpdateEquipImage()
	{
		this.equipImage.sprite = ResManager.inst.LoadSprite(this.GetImagePath());
		this.equipImage.SetNativeSize();
		this.equipImage.gameObject.SetActive(true);
	}

	// Token: 0x06001E1D RID: 7709 RVA: 0x0010632C File Offset: 0x0010452C
	public void SetLingWenSeid(JSONObject skillSeids, JSONObject itemSeid)
	{
		int selectLingWenID = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLingWenID();
		int curSelectEquipType = LianQiTotalManager.inst.getCurSelectEquipType();
		Debug.Log(string.Format("LingWenID:{0}", selectLingWenID));
		Debug.Log(string.Format("equipType:{0}", curSelectEquipType));
		if (selectLingWenID == -1)
		{
			return;
		}
		JSONObject jsonobject = jsonData.instance.LianQiLingWenBiao[selectLingWenID.ToString()];
		if (jsonobject["type"].I == 1)
		{
			return;
		}
		JSONObject jsonobject2 = new JSONObject();
		if (curSelectEquipType == 1)
		{
			jsonobject2.SetField("id", jsonobject["seid"].I);
			if (jsonobject["seid"].I == 77)
			{
				jsonobject2.SetField("value1", jsonobject["listvalue1"]);
			}
			else if (jsonobject["seid"].I == 80)
			{
				jsonobject2.SetField("value1", jsonobject["listvalue1"]);
				jsonobject2.SetField("value2", jsonobject["listvalue2"]);
			}
			else if (jsonobject["seid"].I == 145)
			{
				jsonobject2.SetField("value1", jsonobject["listvalue1"][0]);
			}
			skillSeids.Add(jsonobject2);
			return;
		}
		if (curSelectEquipType - 2 > 1)
		{
			return;
		}
		jsonobject2.SetField("id", jsonobject["Itemseid"].I);
		if (jsonobject["seid"].I == 62)
		{
			jsonobject2.SetField("value1", jsonobject["Itemintvalue1"]);
		}
		else
		{
			jsonobject2.SetField("value1", jsonobject["Itemintvalue1"]);
			jsonobject2.SetField("value2", jsonobject["Itemintvalue2"]);
		}
		itemSeid.Add(jsonobject2);
	}

	// Token: 0x06001E1E RID: 7710 RVA: 0x00106508 File Offset: 0x00104708
	private JSONObject GetEquipAttackType()
	{
		JSONObject jsonobject = new JSONObject(JSONObject.Type.ARRAY);
		JSONObject jsonobject2 = new JSONObject(JSONObject.Type.ARRAY);
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
					jsonobject2.Add(caiLiaoCells[i].attackType);
				}
				jsonobject.Add(caiLiaoCells[i].attackType);
			}
		}
		if (num > 0)
		{
			return jsonobject2;
		}
		return jsonobject;
	}

	// Token: 0x06001E1F RID: 7711 RVA: 0x001065DC File Offset: 0x001047DC
	private JSONObject GetEquipItemFlag()
	{
		JSONObject jsonobject = new JSONObject(JSONObject.Type.ARRAY);
		try
		{
			int num = LianQiTotalManager.inst.getCurSelectEquipType() * 100 + (int)LianQiTotalManager.inst.getcurEquipQualityDate()["quality"];
			jsonobject.Add(num);
			JSONObject equipAttackType = this.GetEquipAttackType();
			for (int i = 0; i < equipAttackType.Count; i++)
			{
				int val = num * 10 + equipAttackType[i].I;
				jsonobject.Add(val);
			}
			jsonobject.Add(LianQiTotalManager.inst.getCurSelectEquipType());
		}
		catch (Exception arg)
		{
			Debug.LogError(string.Format("GetEquipItemFlag:{0}", arg));
		}
		return jsonobject;
	}

	// Token: 0x06001E20 RID: 7712 RVA: 0x0010668C File Offset: 0x0010488C
	private int getEquipMoney()
	{
		JToken jtoken = LianQiTotalManager.inst.getcurEquipQualityDate();
		if (jtoken != null)
		{
			return (int)jtoken["price"];
		}
		return -1;
	}

	// Token: 0x06001E21 RID: 7713 RVA: 0x001066BC File Offset: 0x001048BC
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
		string text4 = player.checkHasStudyWuDaoSkillByID(2213) ? "五行相合" : "普通";
		string equipDesc = LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.showEquipCell.getEquipDesc();
		string text5 = jsonData.instance.LianQiEquipType[LianQiTotalManager.inst.selectTypePageManager.getSelectZhongLei().ToString()]["desc"].ToString();
		int selectLingWenID = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLingWenID();
		string text6;
		if (selectLingWenID != -1)
		{
			text6 = jsonData.instance.LianQiLingWenBiao[selectLingWenID.ToString()]["name"].Str + "灵纹,灵力更加强大，但对使用者也要求更高。";
		}
		else
		{
			text6 = "聚灵灵纹。";
		}
		return string.Concat(new string[]
		{
			text,
			"于",
			text2,
			"年以",
			text4,
			"的手法将",
			text3,
			"等材料炼制的",
			equipDesc,
			"，此",
			text5,
			"铭刻",
			text6
		});
	}

	// Token: 0x06001E22 RID: 7714 RVA: 0x001068A4 File Offset: 0x00104AA4
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

	// Token: 0x06001E23 RID: 7715 RVA: 0x00106914 File Offset: 0x00104B14
	public void lianQiFailResult()
	{
		LianQiTotalManager.inst.CloseBlack();
		try
		{
			this.removeCaiLiao();
			this.reduceHp();
			this.LianQiCallBack();
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
	}

	// Token: 0x06001E24 RID: 7716 RVA: 0x00019023 File Offset: 0x00017223
	public void lianQiSuccessResult()
	{
		LianQiTotalManager.inst.CloseBlack();
		LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.putCaiLiaoCell.caiLiaoCellParent.SetActive(false);
		this.openLianQiResultPanel();
	}

	// Token: 0x06001E25 RID: 7717 RVA: 0x00106958 File Offset: 0x00104B58
	private void reduceHp()
	{
		Avatar player = Tools.instance.getPlayer();
		JToken jtoken = LianQiTotalManager.inst.getcurEquipQualityDate();
		int num = 1;
		if (jtoken != null)
		{
			num = (int)jtoken["quality"];
		}
		JSONObject lianQiJieSuanBiao = jsonData.instance.LianQiJieSuanBiao;
		int num2 = player.HP - lianQiJieSuanBiao[num.ToString()]["damage"].I;
		if (num2 <= 0)
		{
			UIDeath.Inst.Show(DeathType.器毁人亡);
			return;
		}
		player.HP = num2;
	}

	// Token: 0x06001E26 RID: 7718 RVA: 0x001069DC File Offset: 0x00104BDC
	private void addLianQiWuDaoExp()
	{
		Avatar player = Tools.instance.getPlayer();
		JToken jtoken = LianQiTotalManager.inst.getcurEquipQualityDate();
		if (jtoken == null)
		{
			return;
		}
		int num = (int)jtoken["quality"];
		int wuDaoLevelByType = player.wuDaoMag.getWuDaoLevelByType(22);
		JSONObject lianQiJieSuanBiao = jsonData.instance.LianQiJieSuanBiao;
		int num2 = wuDaoLevelByType - num;
		if (num2 < 0)
		{
			Debug.Log(string.Format("addLianQiWuDaoExp方法出现问题,equiplevel:{0},wudaoLevel:{1}", num, num));
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

	// Token: 0x06001E27 RID: 7719 RVA: 0x00019054 File Offset: 0x00017254
	public void addLianQiTime()
	{
		Tools.instance.getPlayer().AddTime(0, this.getCostTime(), 0);
	}

	// Token: 0x06001E28 RID: 7720 RVA: 0x00106AE0 File Offset: 0x00104CE0
	public int getCostTime()
	{
		Avatar player = Tools.instance.getPlayer();
		JToken jtoken = LianQiTotalManager.inst.getcurEquipQualityDate();
		int num = 1;
		if (jtoken != null)
		{
			num = (int)jtoken["quality"];
		}
		int num2 = jsonData.instance.LianQiJieSuanBiao[num.ToString()]["time"].I;
		if (player.checkHasStudyWuDaoSkillByID(2221))
		{
			num2 = (int)((float)num2 * 0.7f);
		}
		return num2;
	}

	// Token: 0x040019A7 RID: 6567
	[SerializeField]
	private InputField inputFieldEquipName;

	// Token: 0x040019A8 RID: 6568
	[SerializeField]
	private Image equipImage;

	// Token: 0x040019A9 RID: 6569
	public bool lianQiResultPanelIsOpen;
}
