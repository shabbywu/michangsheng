using System.Collections.Generic;
using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIGaoShi : MonoBehaviour, IESCClose
{
	public static UIGaoShi Inst;

	public GameObject ShouGouPrefab;

	public GameObject RenWuPrefab;

	public GameObject QingBaoPrefab;

	public GameObject ScaleObj;

	public RectTransform ContentRT;

	public Text Title;

	public List<Sprite> HuoBiIconList;

	private void Awake()
	{
		Inst = this;
	}

	public void RefreshUI()
	{
		((Transform)(object)ContentRT).DestoryAllChild();
		string nowSceneName = SceneEx.NowSceneName;
		Avatar player = PlayerEx.Player;
		GaoShiLeiXing gaoShiLeiXing = GaoShiLeiXing.DataDict[nowSceneName];
		if (player.GaoShi.HasField(nowSceneName) && gaoShiLeiXing != null)
		{
			Title.text = gaoShiLeiXing.name;
			List<JSONObject> list = player.GaoShi[nowSceneName]["GaoShiList"].list;
			for (int i = 0; i < list.Count; i++)
			{
				JSONObject jSONObject = list[i];
				int i2 = jSONObject["GaoShiID"].I;
				GaoShi gaoShi = GaoShi.DataDict[i2];
				if (gaoShi.type == 1)
				{
					CreateShouGouItem(jSONObject, gaoShi, gaoShiLeiXing);
				}
				else if (gaoShi.type == 2)
				{
					CreateRenWuItem(jSONObject, gaoShi);
				}
				else if (gaoShi.type == 3)
				{
					CreateQingBaoItem(jSONObject, gaoShi);
				}
			}
		}
		else
		{
			Title.text = "无告示";
		}
	}

	public void CreateShouGouItem(JSONObject gaoshiJson, GaoShi gaoshi, GaoShiLeiXing gaoshileixing)
	{
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Expected O, but got Unknown
		Avatar player = PlayerEx.Player;
		UIGaoShiShouGouItem shougou = Object.Instantiate<GameObject>(ShouGouPrefab, (Transform)(object)ContentRT).GetComponent<UIGaoShiShouGouItem>();
		bool b = gaoshiJson["JiaJi"].b;
		if (!gaoshiJson.HasField("Pos"))
		{
			gaoshiJson.SetField("Pos", GaoShiManager.CreateRandomPositionAndRotate());
		}
		if (b)
		{
			((Component)shougou).transform.SetAsFirstSibling();
		}
		bool b2 = gaoshiJson["YiShouGou"].b;
		shougou.JiaJi.SetActive(b);
		_ItemJsonData item = _ItemJsonData.DataDict[gaoshi.itemid];
		string text = $"近日，我{gaoshileixing.name}急需{gaoshi.num}份{item.name}，若是有人能寻来，必有重谢。";
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
			menpaihuobi++;
		}
		if (gaoshi.menpaihuobi > 0)
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[gaoshi.menpaihuobi];
			shougou.LingShiTitle.text = itemJsonData.name + "：";
			shougou.LingShi.text = menpaihuobi.ToString();
			shougou.LingShiIcon.sprite = HuoBiIconList[gaoshi.menpaihuobi - 10009];
		}
		else
		{
			shougou.LingShiTitle.text = "灵石：";
			shougou.LingShi.text = lingshi.ToString();
			shougou.LingShiIcon.sprite = HuoBiIconList[0];
		}
		shougou.ShengWang.text = gaoshi.shengwang.ToString();
		shougou.SetYiShouGou(b2, gaoshiJson["Pos"]);
		shougou.Item.SetItem(item.id);
		int itemCount = player.getItemNum(item.id);
		if (b2)
		{
			shougou.Item.SetCount(gaoshi.num);
			return;
		}
		((Component)shougou.Item.CountText).gameObject.SetActive(true);
		if (itemCount >= gaoshi.num)
		{
			shougou.Item.CountText.text = $"<color=#EAD984>{itemCount}/{gaoshi.num}</color>";
			shougou.SetButtonCanClick(canClick: true);
			shougou.TiJiaoBtn.mouseUpEvent.AddListener((UnityAction)delegate
			{
				itemCount = player.getItemNum(item.id);
				if (itemCount >= gaoshi.num)
				{
					player.removeItem(item.id, gaoshi.num);
					if (gaoshi.menpaihuobi > 0)
					{
						player.addItem(gaoshi.menpaihuobi, menpaihuobi, Tools.CreateItemSeid(gaoshi.menpaihuobi), ShowText: true);
					}
					else
					{
						player.AddMoney(lingshi);
					}
					PlayerEx.AddShengWang(gaoshi.shengwangid, gaoshi.shengwang, show: true);
					gaoshiJson.SetField("YiShouGou", val: true);
					shougou.SetYiShouGou(yiShouGou: true, gaoshiJson["Pos"], anim: true);
				}
				else
				{
					UIPopTip.Inst.Pop("没有足够物品");
				}
			});
		}
		else
		{
			shougou.Item.CountText.text = $"<color=#EAA184>{itemCount}/{gaoshi.num}</color>";
			shougou.SetButtonCanClick(canClick: false);
		}
	}

	public void CreateRenWuItem(JSONObject gaoshiJson, GaoShi gaoshi)
	{
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Expected O, but got Unknown
		Avatar player = PlayerEx.Player;
		player.nomelTaskMag.randomTask(gaoshi.taskid);
		bool flag = gaoshiJson["YiLingQu"].b || player.nomelTaskMag.HasNTask(gaoshi.taskid) || IsNTaskFinish.Do(gaoshi.taskid);
		NTaskAllType nTaskAllType = NTaskAllType.DataDict[gaoshi.taskid];
		NTaskXiangXi nTaskXiangXiData = player.nomelTaskMag.GetNTaskXiangXiData(gaoshi.taskid);
		if (nTaskAllType != null)
		{
			if (nTaskXiangXiData != null)
			{
				UIGaoShiRenWuItem renwu = Object.Instantiate<GameObject>(RenWuPrefab, (Transform)(object)ContentRT).GetComponent<UIGaoShiRenWuItem>();
				if (!gaoshiJson.HasField("Pos"))
				{
					gaoshiJson.SetField("Pos", GaoShiManager.CreateRandomPositionAndRotate());
				}
				renwu.SetYiLingQu(flag, gaoshiJson["Pos"]);
				renwu.Desc.text = NTaskText.GetNTaskDesc(gaoshi.taskid);
				int money = 0;
				int menpaihuobi = 0;
				player.nomelTaskMag.getReward(gaoshi.taskid, ref money, ref menpaihuobi);
				if (nTaskAllType.menpaihuobi > 0)
				{
					_ItemJsonData itemJsonData = _ItemJsonData.DataDict[nTaskAllType.menpaihuobi];
					renwu.LingShiTitle.text = itemJsonData.name + "：";
					renwu.LingShi.text = menpaihuobi.ToString();
					renwu.LingShiIcon.sprite = HuoBiIconList[nTaskAllType.menpaihuobi - 10009];
				}
				else
				{
					renwu.LingShiTitle.text = "灵石：";
					renwu.LingShi.text = money.ToString();
					renwu.LingShiIcon.sprite = HuoBiIconList[0];
				}
				if (nTaskAllType.shili >= 0 && nTaskXiangXiData.ShiLIAdd > 0)
				{
					renwu.ShengWang.text = nTaskXiangXiData.ShiLIAdd.ToString();
				}
				if (!flag)
				{
					renwu.TiJiaoBtn.mouseUpEvent.AddListener((UnityAction)delegate
					{
						StartNTask.Do(gaoshi.taskid);
						renwu.SetYiLingQu(yiLingQu: true, gaoshiJson["Pos"], anim: true);
						gaoshiJson.SetField("YiLingQu", val: true);
					});
				}
			}
			else
			{
				Debug.Log((object)$"创建任务告示失败，GetNTaskXiangXiData({gaoshi.taskid})获取不到xiangxi信息，有可能是人物境界等不达标");
			}
		}
		else
		{
			Debug.Log((object)$"创建任务告示失败，没有id为{gaoshi.taskid}的ntask");
		}
	}

	public void CreateQingBaoItem(JSONObject gaoshiJson, GaoShi gaoshi)
	{
	}

	public void Show()
	{
		ScaleObj.SetActive(true);
		RefreshUI();
		ESCCloseManager.Inst.RegisterClose(this);
	}

	public void Close()
	{
		ScaleObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
