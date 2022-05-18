using System;
using System.Collections.Generic;
using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003FB RID: 1019
public class UIGaoShi : MonoBehaviour, IESCClose
{
	// Token: 0x06001B9C RID: 7068 RVA: 0x0001730A File Offset: 0x0001550A
	private void Awake()
	{
		UIGaoShi.Inst = this;
	}

	// Token: 0x06001B9D RID: 7069 RVA: 0x000F73C4 File Offset: 0x000F55C4
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

	// Token: 0x06001B9E RID: 7070 RVA: 0x000F74CC File Offset: 0x000F56CC
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

	// Token: 0x06001B9F RID: 7071 RVA: 0x000F78A4 File Offset: 0x000F5AA4
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

	// Token: 0x06001BA0 RID: 7072 RVA: 0x000042DD File Offset: 0x000024DD
	public void CreateQingBaoItem(JSONObject gaoshiJson, GaoShi gaoshi)
	{
	}

	// Token: 0x06001BA1 RID: 7073 RVA: 0x00017312 File Offset: 0x00015512
	public void Show()
	{
		this.ScaleObj.SetActive(true);
		this.RefreshUI();
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001BA2 RID: 7074 RVA: 0x00017331 File Offset: 0x00015531
	public void Close()
	{
		this.ScaleObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001BA3 RID: 7075 RVA: 0x0001734A File Offset: 0x0001554A
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x04001751 RID: 5969
	public static UIGaoShi Inst;

	// Token: 0x04001752 RID: 5970
	public GameObject ShouGouPrefab;

	// Token: 0x04001753 RID: 5971
	public GameObject RenWuPrefab;

	// Token: 0x04001754 RID: 5972
	public GameObject QingBaoPrefab;

	// Token: 0x04001755 RID: 5973
	public GameObject ScaleObj;

	// Token: 0x04001756 RID: 5974
	public RectTransform ContentRT;

	// Token: 0x04001757 RID: 5975
	public Text Title;

	// Token: 0x04001758 RID: 5976
	public List<Sprite> HuoBiIconList;
}
