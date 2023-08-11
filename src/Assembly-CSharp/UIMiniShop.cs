using Bag;
using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMiniShop : MonoBehaviour, IESCClose
{
	public static UIMiniShop Inst;

	public BaseSlot ItemSlot;

	public Text CostText;

	public FpBtn LeftBtn;

	public FpBtn RightBtn;

	public FpBtn OkBtn;

	public FpBtn CloseBtn;

	public Slider NumSlider;

	private int itemID;

	private int price;

	private int maxSellCount;

	private BaseItem item;

	private Command cmd;

	private int nowSelectCount;

	public static void Show(int itemID, int price, int maxSellCount, Command cmd = null)
	{
		UIMiniShop component = Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefab/UIMiniShop"), ((Component)NewUICanvas.Inst.Canvas).transform).GetComponent<UIMiniShop>();
		component.itemID = itemID;
		component.price = price;
		component.cmd = cmd;
		component.maxSellCount = maxSellCount;
		component.RefreshUI();
	}

	public void RefreshUI()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		Inst = this;
		item = BaseItem.Create(itemID, 1, Tools.getUUID(), Tools.CreateItemSeid(itemID));
		ItemSlot.SetSlotData(item);
		OkBtn.mouseUpEvent.AddListener(new UnityAction(OnOkBtnClick));
		CloseBtn.mouseUpEvent.AddListener(new UnityAction(Close));
		LeftBtn.mouseUpEvent.AddListener(new UnityAction(OnSubClick));
		RightBtn.mouseUpEvent.AddListener(new UnityAction(OnAddClick));
		NumSlider.minValue = 0f;
		int num = (int)PlayerEx.Player.money;
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[itemID];
		if (price == 0)
		{
			price = itemJsonData.price;
			if (price == 0)
			{
				price = 1;
				Debug.LogError((object)"MiniShop传入了价格为0的商品，以自动保底为1灵石");
			}
		}
		int num2 = num / price;
		if (maxSellCount > 0)
		{
			num2 = Mathf.Min(num2, maxSellCount);
		}
		NumSlider.maxValue = num2;
		if (num2 < 1)
		{
			((Selectable)NumSlider).interactable = false;
			((Behaviour)LeftBtn).enabled = false;
			((Behaviour)RightBtn).enabled = false;
		}
		else
		{
			NumSlider.value = 1f;
			nowSelectCount = 1;
			RefreshCount();
			((UnityEvent<float>)(object)NumSlider.onValueChanged).AddListener((UnityAction<float>)OnSliderValueChanged);
		}
	}

	public void RefreshCount()
	{
		ItemSlot.Item.Count = nowSelectCount;
		ItemSlot.UpdateUI();
		CostText.text = (nowSelectCount * price).ToString();
	}

	public void OnSliderValueChanged(float value)
	{
		int num = Mathf.RoundToInt(value);
		nowSelectCount = num;
		RefreshCount();
	}

	public void OnAddClick()
	{
		Slider numSlider = NumSlider;
		numSlider.value += 1f;
	}

	public void OnSubClick()
	{
		Slider numSlider = NumSlider;
		numSlider.value -= 1f;
	}

	public void OnOkBtnClick()
	{
		if (nowSelectCount == 0)
		{
			Close();
			return;
		}
		int num = nowSelectCount * price;
		Avatar player = PlayerEx.Player;
		if ((int)player.money >= num)
		{
			player.AddMoney(-num);
			player.addItem(itemID, item.Seid, item.Count);
			UIPopTip.Inst.PopAddItem(item.GetName(), item.Count);
			Close();
		}
		else
		{
			UIPopTip.Inst.Pop("灵石不足");
		}
	}

	private void Close()
	{
		if ((Object)(object)cmd != (Object)null)
		{
			cmd.Continue();
		}
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	bool IESCClose.TryEscClose()
	{
		Close();
		return true;
	}
}
