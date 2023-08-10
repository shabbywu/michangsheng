using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUIPackage;

public class Store : MonoBehaviour
{
	public List<item> store = new List<item>();

	public GameObject StoreUI;

	public GameObject NumInput;

	public GameObject Sure;

	public GameObject Cancel;

	public GameObject Notice;

	private ItemDatebase datebase;

	private bool Show_Store;

	private int ShopID;

	private int num = 1;

	private void Start()
	{
		InitNumInput();
		HideNumInput();
		datebase = ((Component)jsonData.instance).gameObject.GetComponent<ItemDatebase>();
		initStore();
	}

	private void Update()
	{
		SetMaxShop();
	}

	private void InitNumInput()
	{
		Sure = ((Component)((Component)this).transform.Find("Win/NumInput/Sure")).gameObject;
		Cancel = ((Component)((Component)this).transform.Find("Win/NumInput/Cancel")).gameObject;
		UIEventListener.Get(Sure).onClick = SureClick;
		UIEventListener.Get(Cancel).onClick = CancelClick;
	}

	private void SureClick(GameObject button)
	{
		if (NumInput.GetComponentInChildren<UIInput>().value != "" && num > 0)
		{
			Shop_Item(ShopID);
			HideNumInput();
		}
	}

	private void SetMaxShop()
	{
		if (!NumInput.activeSelf || !(NumInput.GetComponentInChildren<UIInput>().value != ""))
		{
			return;
		}
		num = int.Parse(NumInput.GetComponentInChildren<UIInput>().value);
		if (num <= 0)
		{
			return;
		}
		if (store[ShopID].itemType == item.ItemType.Potion)
		{
			if (num > store[ShopID].itemMaxNum)
			{
				NumInput.GetComponentInChildren<UIInput>().value = store[ShopID].itemMaxNum.ToString();
			}
		}
		else if (num > Singleton.inventory.GetSoltNum())
		{
			NumInput.GetComponentInChildren<UIInput>().value = Singleton.inventory.GetSoltNum().ToString();
		}
	}

	private void CancelClick(GameObject button)
	{
		HideNumInput();
	}

	private void initStore()
	{
		((Component)this).GetComponentInChildren<UIGrid>().repositionNow = true;
		((Component)this).GetComponentInChildren<UIScrollView>().Press(pressed: true);
		StoreUI.SetActive(false);
		Notice.SetActive(false);
	}

	private void OnGUI()
	{
	}

	private void Show()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		Show_Store = !Show_Store;
		if (!Show_Store)
		{
			Singleton.inventory.showTooltip = false;
		}
		if (Show_Store)
		{
			((Component)this).transform.Find("Win").position = ((Component)this).transform.position;
		}
		StoreUI.SetActive(Show_Store);
		Singleton.UI.UI_Top(StoreUI.transform.parent);
	}

	public void ShowNumInput(int id)
	{
		NumInput.SetActive(true);
		ShopID = id;
		if (store[id].itemType != item.ItemType.Potion)
		{
			NumInput.GetComponentInChildren<UIInput>().value = "1";
		}
	}

	public void HideNumInput()
	{
		NumInput.SetActive(false);
	}

	public void Shop_Item(int ID)
	{
		if (!Singleton.inventory.is_Full(store[ID], num))
		{
			if (Singleton.money.money >= store[ID].itemPrice * num)
			{
				for (int i = 0; i < num; i++)
				{
					Singleton.inventory.AddItem(store[ID].itemID);
					Tools.instance.getPlayer().addItem(store[ID].itemID, store[ID].Seid);
				}
				Singleton.money.Set_money(Singleton.money.money - store[ID].itemPrice * num);
			}
			else
			{
				((MonoBehaviour)this).StartCoroutine(ShowNotice("金币不足,无法购买"));
			}
		}
		else
		{
			((MonoBehaviour)this).StartCoroutine(ShowNotice("背包已满"));
		}
	}

	private IEnumerator ShowNotice(string s)
	{
		Notice.SetActive(true);
		Notice.GetComponentInChildren<UILabel>().text = "提示:" + s;
		yield return (object)new WaitForSeconds(3f);
		Notice.SetActive(false);
	}
}
