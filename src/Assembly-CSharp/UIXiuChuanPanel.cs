using Bag;
using JSONClass;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIXiuChuanPanel : MonoBehaviour, IESCClose
{
	public static UIXiuChuanPanel Inst;

	public Text Tip;

	public BaseSlot Slot;

	public FpBtn OkBtn;

	public FpBtn CancelBtn;

	private void Awake()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		Inst = this;
		CancelBtn.mouseUpEvent.AddListener(new UnityAction(Close));
		ESCCloseManager.Inst.RegisterClose(this);
	}

	public void Close()
	{
		Inst = null;
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void SetLingZhou(ITEM_INFO item)
	{
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Expected O, but got Unknown
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[item.itemId];
		int i = item.Seid["NaiJiu"].I;
		JToken val = jsonData.instance.LingZhouPinJie[itemJsonData.quality.ToString()];
		int qualityNaiJiu = (int)val[(object)"Naijiu"];
		int num = (int)val[(object)"xiuli"];
		int num2 = qualityNaiJiu - i;
		int needLingShi = num2 * num;
		Slot.SetSlotData(BaseItem.Create(item.itemId, 1, item.uuid, item.Seid));
		Tip.text = $"修复<color=#{ColorEx.ItemQualityColor[itemJsonData.quality - 1].ColorToString()}>{itemJsonData.name}</color>{num2}耐久需要{needLingShi}灵石";
		OkBtn.mouseUpEvent.AddListener((UnityAction)delegate
		{
			Avatar player = PlayerEx.Player;
			if ((int)player.money >= needLingShi)
			{
				Close();
				player.AddMoney(-needLingShi);
				item.Seid.SetField("NaiJiu", qualityNaiJiu);
				UIPopTip.Inst.Pop("修复完毕");
			}
			else
			{
				UIPopTip.Inst.Pop("灵石不足");
			}
		});
	}

	public static void OpenDefaultXiuChuan()
	{
		Avatar player = PlayerEx.Player;
		ITEM_INFO iTEM_INFO = null;
		_ItemJsonData itemJsonData = null;
		foreach (ITEM_INFO value in player.equipItemList.values)
		{
			_ItemJsonData itemJsonData2 = _ItemJsonData.DataDict[value.itemId];
			if (itemJsonData2.type == 14)
			{
				iTEM_INFO = value;
				itemJsonData = itemJsonData2;
				break;
			}
		}
		if (iTEM_INFO == null)
		{
			UIPopTip.Inst.Pop("当前没有装备灵舟");
			return;
		}
		if (iTEM_INFO.Seid == null || !iTEM_INFO.Seid.HasField("NaiJiu"))
		{
			UIPopTip.Inst.Pop("灵舟数据异常");
			return;
		}
		int i = iTEM_INFO.Seid["NaiJiu"].I;
		int num = (int)jsonData.instance.LingZhouPinJie[itemJsonData.quality.ToString()][(object)"Naijiu"];
		if (i >= num)
		{
			UIPopTip.Inst.Pop("此灵舟不需要修理");
		}
		else
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefab/UIXiuChuan"), ((Component)NewUICanvas.Inst.Canvas).transform).GetComponent<UIXiuChuanPanel>().SetLingZhou(iTEM_INFO);
		}
	}

	bool IESCClose.TryEscClose()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		Close();
		return true;
	}
}
