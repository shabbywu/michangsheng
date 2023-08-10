using System.Collections.Generic;
using Bag;
using LianQi;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LianQiTotalManager : MonoBehaviour
{
	public static LianQiTotalManager inst;

	[SerializeField]
	private GameObject lianQiPanel;

	public SelectTypePageManager selectTypePageManager;

	public GameObject Black;

	public PutMaterialPageManager putMaterialPageManager;

	public LianQiBag Bag;

	public LianQiResultManager lianQiResultManager;

	public LianQiSlot ToSlot;

	public bool IsFirstSelect = true;

	public Object[] equipSprites;

	private int curSelectEquipMuBanID = -1;

	private int curSelectEquipType = -1;

	public bool checkIsInLianQiPage()
	{
		if (lianQiPanel.activeSelf)
		{
			return true;
		}
		return false;
	}

	public void OpenBlack()
	{
		Black.SetActive(true);
	}

	public void CloseBlack()
	{
		Black.SetActive(false);
	}

	public void setCurSelectEquipMuBanID(int num)
	{
		curSelectEquipMuBanID = num;
	}

	public int getCurSelectEquipMuBanID()
	{
		return curSelectEquipMuBanID;
	}

	public void setCurSelectEquipType(int num)
	{
		curSelectEquipType = num;
	}

	public int getCurSelectEquipType()
	{
		return curSelectEquipType;
	}

	private void Awake()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		inst = this;
		Transform transform = ((Component)this).transform;
		transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		transform.localScale = Vector3.one;
		transform.SetAsLastSibling();
		transform.localPosition = Vector3.zero;
		equipSprites = ResManager.inst.LoadSprites("NewUI/LianQi/BigEquip/BigEquip");
		openLianQiPanle();
		if ((Object)(object)UIHeadPanel.Inst != (Object)null)
		{
			((Component)UIHeadPanel.Inst).transform.SetAsLastSibling();
		}
		if ((Object)(object)UIMiniTaskPanel.Inst != (Object)null)
		{
			((Component)UIMiniTaskPanel.Inst).transform.SetAsLastSibling();
		}
	}

	public void openLianQiPanle()
	{
		init();
		selectTypePageManager.OpenSelectEquipPanel();
	}

	private void init()
	{
		curSelectEquipMuBanID = -1;
		curSelectEquipType = -1;
		selectTypePageManager.init();
		putMaterialPageManager.init();
		lianQiResultManager.init();
	}

	public void ChangeEquip()
	{
		curSelectEquipMuBanID = -1;
		curSelectEquipType = -1;
		selectTypePageManager.init();
		putMaterialPageManager.init();
		lianQiResultManager.init();
	}

	public void closeLianQiPanel()
	{
		PanelMamager.inst.closePanel(PanelMamager.PanelType.炼器);
	}

	public void restartSelectEquip()
	{
		ChangeEquip();
		selectTypePageManager.OpenSelectEquipPanel();
	}

	public void putCaiLiaoCallBack()
	{
		putMaterialPageManager.wuWeiManager.updateWuWei();
		putMaterialPageManager.chuShiLingLiManager.updateChushiLingLi();
		putMaterialPageManager.zhongLingLiManager.updateZhongLingLi();
		putMaterialPageManager.lianQiPageManager.showEquipCell.updateEquipPingJie();
		putMaterialPageManager.showXiaoGuoManager.updateEquipCiTiao();
		putMaterialPageManager.updateWuXingXiangKeTips();
		putMaterialPageManager.updateCaiLiaoSum();
	}

	public void selectEquipCallBack()
	{
		putMaterialPageManager.openPutMaterialPage();
	}

	public void selectLingWenCiTiaoCallBack()
	{
		putMaterialPageManager.zhongLingLiManager.updateZhongLingLi();
		putMaterialPageManager.showXiaoGuoManager.updateEquipCiTiao();
	}

	public string buildNomalLingWenDesc(JSONObject obj)
	{
		string text = ((obj["value3"].I == 1) ? "x" : "+");
		string result = "";
		switch (obj["type"].I)
		{
		case 1:
			result = string.Format("{0}<color=#fff227>x{1}</color>,灵力<color=#fff227>{2}{3}</color>", obj["desc"].str, obj["value1"].I, text, obj["value4"].I);
			break;
		case 2:
			result = string.Format("对自己造成<color=#fff227>x{0}</color>点真实伤害,灵力<color=#fff227>{1}{2}</color>", obj["value1"].I, text, obj["value4"].I);
			break;
		case 4:
			result = string.Format("{0}才能使用,灵力<color=#fff227>{1}{2}</color>", obj["desc"].str, text, obj["value4"].n);
			break;
		}
		return result;
	}

	public int getLingWenIDByBUFFIDAndSum(int id, int sum)
	{
		List<JSONObject> list = jsonData.instance.LianQiLingWenBiao.list;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i]["value1"].I == id && list[i]["value2"].I == sum)
			{
				return list[i]["id"].I;
			}
		}
		return -1;
	}

	public JToken getcurEquipQualityDate()
	{
		JObject lianQiWuQiQuality = jsonData.instance.LianQiWuQiQuality;
		int realZongLingLi = inst.putMaterialPageManager.zhongLingLiManager.getRealZongLingLi();
		JToken result = null;
		foreach (KeyValuePair<string, JToken> item in lianQiWuQiQuality)
		{
			if (realZongLingLi >= (int)item.Value[(object)"power"])
			{
				result = item.Value;
			}
		}
		return result;
	}

	public void lianQiSuccess()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		lianQiResultManager.addLianQiTime();
		Tools.instance.playFader("正在炼制法宝...", (UnityAction)delegate
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			OpenBlack();
			UCheckBox.Show("法阵之中灵气涌动，最终灵气四溢开来，炼成的灵宝呈现在你眼前", new UnityAction(lianQiResultManager.lianQiSuccessResult));
		});
	}

	public void lianQiFaile()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		lianQiResultManager.addLianQiTime();
		Tools.instance.playFader("正在炼制法宝...", (UnityAction)delegate
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			OpenBlack();
			UCheckBox.Show("法阵之中灵气涌动，最终灵气四射开来，眼前的器胚“轰”的一声爆炸了", new UnityAction(lianQiResultManager.lianQiFailResult));
		});
	}

	public void OpenBag()
	{
		Bag.Open();
	}

	public void CloseBag()
	{
		Bag.Close();
	}

	public void PutItem(LianQiSlot dragSlot)
	{
		if (!((Object)(object)ToSlot == (Object)null))
		{
			BaseItem baseItem = dragSlot.Item.Clone();
			baseItem.Count = 1;
			if (!ToSlot.IsNull())
			{
				Bag.AddTempItem(ToSlot.Item, 1);
			}
			Bag.RemoveTempItem(dragSlot.Item.Uid, 1);
			ToSlot.SetSlotData(baseItem);
			CloseBag();
			putCaiLiaoCallBack();
		}
	}

	private void OnDestroy()
	{
		Tools.canClickFlag = true;
		inst = null;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.炼器, 1);
		if ((Object)(object)UIMiniTaskPanel.Inst != (Object)null)
		{
			((Component)UIMiniTaskPanel.Inst).transform.SetAsFirstSibling();
		}
		if ((Object)(object)UIHeadPanel.Inst != (Object)null)
		{
			((Component)UIHeadPanel.Inst).transform.SetAsFirstSibling();
		}
	}
}
