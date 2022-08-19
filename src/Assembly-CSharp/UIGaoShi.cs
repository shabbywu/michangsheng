using System;
using System.Collections.Generic;
using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002BE RID: 702
public class UIGaoShi : MonoBehaviour, IESCClose
{
	// Token: 0x060018A6 RID: 6310 RVA: 0x000B0E78 File Offset: 0x000AF078
	private void Awake()
	{
		UIGaoShi.Inst = this;
	}

	// Token: 0x060018A7 RID: 6311 RVA: 0x000B0E80 File Offset: 0x000AF080
	public void RefreshUI()
	{
		this.ContentRT.DestoryAllChild();
		string nowSceneName = SceneEx.NowSceneName;
		Avatar player = PlayerEx.Player;
		GaoShiLeiXing gaoShiLeiXing = GaoShiLeiXing.DataDict[nowSceneName];
		if (player.GaoShi.HasField(nowSceneName) && gaoShiLeiXing != null)
		{
			this.Title.text = gaoShiLeiXing.name;
			List<JSONObject> list = player.GaoShi[nowSceneName]["GaoShiList"].list;
			for (int i = 0; i < list.Count; i++)
			{
				JSONObject jsonobject = list[i];
				int i2 = jsonobject["GaoShiID"].I;
				GaoShi gaoShi = GaoShi.DataDict[i2];
				if (gaoShi.type == 1)
				{
					this.CreateShouGouItem(jsonobject, gaoShi, gaoShiLeiXing);
				}
				else if (gaoShi.type == 2)
				{
					this.CreateRenWuItem(jsonobject, gaoShi);
				}
				else if (gaoShi.type == 3)
				{
					this.CreateQingBaoItem(jsonobject, gaoShi);
				}
			}
			return;
		}
		this.Title.text = "无告示";
	}

	// Token: 0x060018A8 RID: 6312 RVA: 0x000B0F88 File Offset: 0x000AF188
	public void CreateShouGouItem(JSONObject gaoshiJson, GaoShi gaoshi, GaoShiLeiXing gaoshileixing)
	{
		Avatar player = PlayerEx.Player;
		UIGaoShiShouGouItem shougou = Object.Instantiate<GameObject>(this.ShouGouPrefab, this.ContentRT).GetComponent<UIGaoShiShouGouItem>();
		bool b = gaoshiJson["JiaJi"].b;
		if (!gaoshiJson.HasField("Pos"))
		{
			gaoshiJson.SetField("Pos", GaoShiManager.CreateRandomPositionAndRotate());
		}
		if (b)
		{
			shougou.transform.SetAsFirstSibling();
		}
		bool b2 = gaoshiJson["YiShouGou"].b;
		shougou.JiaJi.SetActive(b);
		_ItemJsonData item = _ItemJsonData.DataDict[gaoshi.itemid];
		string text = string.Format("近日，我{0}急需{1}份{2}，若是有人能寻来，必有重谢。", gaoshileixing.name, gaoshi.num, item.name);
		shougou.Desc.text = text;
		float num = 1f;
		if (b)
		{
			num = 1.5f;
		}
		int lingshi = (int)((float)(item.price * gaoshi.num * gaoshi.jiagexishu) * num / 100f);
		int menpaihuobi = lingshi / 100;
		if (lingshi % 100 > 0)
		{
			int menpaihuobi2 = menpaihuobi;
			menpaihuobi = menpaihuobi2 + 1;
		}
		if (gaoshi.menpaihuobi > 0)
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[gaoshi.menpaihuobi];
			shougou.LingShiTitle.text = itemJsonData.name + "：";
			shougou.LingShi.text = menpaihuobi.ToString();
			shougou.LingShiIcon.sprite = this.HuoBiIconList[gaoshi.menpaihuobi - 10009];
		}
		else
		{
			shougou.LingShiTitle.text = "灵石：";
			shougou.LingShi.text = lingshi.ToString();
			shougou.LingShiIcon.sprite = this.HuoBiIconList[0];
		}
		shougou.ShengWang.text = gaoshi.shengwang.ToString();
		shougou.SetYiShouGou(b2, gaoshiJson["Pos"], false);
		shougou.Item.SetItem(item.id);
		int itemCount = player.getItemNum(item.id);
		if (b2)
		{
			shougou.Item.SetCount(gaoshi.num);
			return;
		}
		shougou.Item.CountText.gameObject.SetActive(true);
		if (itemCount >= gaoshi.num)
		{
			shougou.Item.CountText.text = string.Format("<color=#EAD984>{0}/{1}</color>", itemCount, gaoshi.num);
			shougou.SetButtonCanClick(true);
			shougou.TiJiaoBtn.mouseUpEvent.AddListener(delegate()
			{
				itemCount = player.getItemNum(item.id);
				if (itemCount >= gaoshi.num)
				{
					player.removeItem(item.id, gaoshi.num);
					if (gaoshi.menpaihuobi > 0)
					{
						player.addItem(gaoshi.menpaihuobi, menpaihuobi, Tools.CreateItemSeid(gaoshi.menpaihuobi), true);
					}
					else
					{
						player.AddMoney(lingshi);
					}
					PlayerEx.AddShengWang(gaoshi.shengwangid, gaoshi.shengwang, true);
					gaoshiJson.SetField("YiShouGou", true);
					shougou.SetYiShouGou(true, gaoshiJson["Pos"], true);
					return;
				}
				UIPopTip.Inst.Pop("没有足够物品", PopTipIconType.叹号);
			});
			return;
		}
		shougou.Item.CountText.text = string.Format("<color=#EAA184>{0}/{1}</color>", itemCount, gaoshi.num);
		shougou.SetButtonCanClick(false);
	}

	// Token: 0x060018A9 RID: 6313 RVA: 0x000B1360 File Offset: 0x000AF560
	public void CreateRenWuItem(JSONObject gaoshiJson, GaoShi gaoshi)
	{
		Avatar player = PlayerEx.Player;
		player.nomelTaskMag.randomTask(gaoshi.taskid, false);
		bool flag = gaoshiJson["YiLingQu"].b || player.nomelTaskMag.HasNTask(gaoshi.taskid) || IsNTaskFinish.Do(gaoshi.taskid);
		NTaskAllType ntaskAllType = NTaskAllType.DataDict[gaoshi.taskid];
		NTaskXiangXi ntaskXiangXiData = player.nomelTaskMag.GetNTaskXiangXiData(gaoshi.taskid);
		if (ntaskAllType != null)
		{
			if (ntaskXiangXiData == null)
			{
				Debug.Log(string.Format("创建任务告示失败，GetNTaskXiangXiData({0})获取不到xiangxi信息，有可能是人物境界等不达标", gaoshi.taskid));
				return;
			}
			UIGaoShiRenWuItem renwu = Object.Instantiate<GameObject>(this.RenWuPrefab, this.ContentRT).GetComponent<UIGaoShiRenWuItem>();
			if (!gaoshiJson.HasField("Pos"))
			{
				gaoshiJson.SetField("Pos", GaoShiManager.CreateRandomPositionAndRotate());
			}
			renwu.SetYiLingQu(flag, gaoshiJson["Pos"], false);
			renwu.Desc.text = NTaskText.GetNTaskDesc(gaoshi.taskid);
			int num = 0;
			int num2 = 0;
			player.nomelTaskMag.getReward(gaoshi.taskid, ref num, ref num2);
			if (ntaskAllType.menpaihuobi > 0)
			{
				_ItemJsonData itemJsonData = _ItemJsonData.DataDict[ntaskAllType.menpaihuobi];
				renwu.LingShiTitle.text = itemJsonData.name + "：";
				renwu.LingShi.text = num2.ToString();
				renwu.LingShiIcon.sprite = this.HuoBiIconList[ntaskAllType.menpaihuobi - 10009];
			}
			else
			{
				renwu.LingShiTitle.text = "灵石：";
				renwu.LingShi.text = num.ToString();
				renwu.LingShiIcon.sprite = this.HuoBiIconList[0];
			}
			if (ntaskAllType.shili >= 0 && ntaskXiangXiData.ShiLIAdd > 0)
			{
				renwu.ShengWang.text = ntaskXiangXiData.ShiLIAdd.ToString();
			}
			if (!flag)
			{
				renwu.TiJiaoBtn.mouseUpEvent.AddListener(delegate()
				{
					StartNTask.Do(gaoshi.taskid);
					renwu.SetYiLingQu(true, gaoshiJson["Pos"], true);
					gaoshiJson.SetField("YiLingQu", true);
				});
				return;
			}
		}
		else
		{
			Debug.Log(string.Format("创建任务告示失败，没有id为{0}的ntask", gaoshi.taskid));
		}
	}

	// Token: 0x060018AA RID: 6314 RVA: 0x00004095 File Offset: 0x00002295
	public void CreateQingBaoItem(JSONObject gaoshiJson, GaoShi gaoshi)
	{
	}

	// Token: 0x060018AB RID: 6315 RVA: 0x000B164C File Offset: 0x000AF84C
	public void Show()
	{
		this.ScaleObj.SetActive(true);
		this.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x060018AC RID: 6316 RVA: 0x000B166B File Offset: 0x000AF86B
	public void Close()
	{
		this.ScaleObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x060018AD RID: 6317 RVA: 0x000B1684 File Offset: 0x000AF884
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x040013AD RID: 5037
	public static UIGaoShi Inst;

	// Token: 0x040013AE RID: 5038
	public GameObject ShouGouPrefab;

	// Token: 0x040013AF RID: 5039
	public GameObject RenWuPrefab;

	// Token: 0x040013B0 RID: 5040
	public GameObject QingBaoPrefab;

	// Token: 0x040013B1 RID: 5041
	public GameObject ScaleObj;

	// Token: 0x040013B2 RID: 5042
	public RectTransform ContentRT;

	// Token: 0x040013B3 RID: 5043
	public Text Title;

	// Token: 0x040013B4 RID: 5044
	public List<Sprite> HuoBiIconList;
}
