using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JiaoYi;

public class JiaoYiUIMag : MonoBehaviour, IESCClose
{
	public static JiaoYiUIMag Inst;

	public int NpcId;

	public JiaoBag PlayerBag;

	public PlayerSetRandomFace PlayerFace;

	public BagItemSelect bagItemSelect;

	public Text PlayerName;

	public Text PlayerTitle;

	public JiaoBag NpcBag;

	public PlayerSetRandomFace NpcFace;

	public Text NpcName;

	public Text NpcTitle;

	public int PlayerGetMoney;

	public Text PlayerGetMoneyText;

	public GameObject NpcSayPanel;

	public Text NpcSayText;

	public UnityAction CloseAction;

	private void Awake()
	{
		Inst = this;
		ESCCloseManager.Inst.RegisterClose(this);
	}

	public void Init(int npcId)
	{
		NpcId = NPCEx.NPCIDToNew(npcId);
		InitPlayerData();
		InitNpcData();
		((Component)this).transform.SetAsLastSibling();
	}

	public void Init(int npcId, UnityAction action)
	{
		NpcId = npcId;
		InitPlayerData();
		InitNpcData();
		((Component)this).transform.SetAsLastSibling();
		CloseAction = action;
		PlayerBag.UpdateMoney();
		NpcBag.UpdateMoney();
	}

	private void InitPlayerData()
	{
		PlayerName.SetText(Tools.GetPlayerName());
		PlayerTitle.SetText(Tools.GetPlayerTitle());
		PlayerBag.Init(NpcId, isPlayer: true);
	}

	private void InitNpcData()
	{
		NpcJieSuanManager.inst.SortNpcPack(NpcId);
		NpcName.text = jsonData.instance.AvatarRandomJsonData[NpcId.ToString()]["Name"].Str;
		NpcTitle.text = jsonData.instance.AvatarJsonData[NpcId.ToString()]["Title"].Str;
		NpcFace.SetNPCFace(NpcId);
		NpcBag.Init(NpcId);
	}

	public void SellItem(JiaoYiSlot dragSlot, JiaoYiSlot toSlot = null)
	{
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Expected O, but got Unknown
		if (!dragSlot.Item.CanSale)
		{
			UIPopTip.Inst.Pop("此物品无法交易");
		}
		else
		{
			if ((Object)(object)toSlot != (Object)null && !toSlot.IsNull() && (dragSlot.Item.Id != toSlot.Item.Id || dragSlot.Item.MaxNum < 2))
			{
				return;
			}
			bool isPlayer = dragSlot.IsPlayer;
			JiaoBag jiaoBag = null;
			if (isPlayer)
			{
				jiaoBag = PlayerBag;
			}
			else
			{
				jiaoBag = NpcBag;
			}
			if (dragSlot.Item.Count < 1)
			{
				UIPopTip.Inst.Pop("此物品数量小于0，无法交易");
				return;
			}
			if (dragSlot.Item.Count == 1)
			{
				if ((Object)(object)toSlot == (Object)null)
				{
					toSlot = jiaoBag.GetNullSellList(dragSlot.Item.Uid);
				}
				if ((Object)(object)toSlot == (Object)null)
				{
					UIPopTip.Inst.Pop("没有空的格子");
					return;
				}
				if (toSlot.IsNull())
				{
					toSlot.SetSlotData(dragSlot.Item.Clone());
					toSlot.Item.Count = 1;
				}
				else
				{
					toSlot.Item.Count++;
				}
				toSlot.UpdateUI();
				jiaoBag.RemoveTempItem(dragSlot.Item.Uid, 1);
				dragSlot.Item.Count--;
				if (dragSlot.Item.Count <= 0)
				{
					jiaoBag.UpdateItem();
				}
				UpdatePlayerGetMoney();
				return;
			}
			int num = 0;
			if (dragSlot.Item.Count > 1)
			{
				if (Input.GetKey((KeyCode)304) || Input.GetKey((KeyCode)303))
				{
					num = 5;
					if (dragSlot.Item.Count < 5)
					{
						num = dragSlot.Item.Count;
					}
				}
				if (Input.GetKey((KeyCode)306) || Input.GetKey((KeyCode)305))
				{
					num = dragSlot.Item.Count;
				}
			}
			if (num <= 0)
			{
				bagItemSelect.Init(dragSlot.Item.GetName(), dragSlot.Item.Count, (UnityAction)delegate
				{
					int curNum = bagItemSelect.CurNum;
					if ((Object)(object)toSlot == (Object)null)
					{
						toSlot = jiaoBag.GetNullSellList(dragSlot.Item.Uid);
					}
					if ((Object)(object)toSlot == (Object)null)
					{
						UIPopTip.Inst.Pop("没有空的格子");
					}
					else
					{
						if (toSlot.IsNull())
						{
							toSlot.SetSlotData(dragSlot.Item.Clone());
							toSlot.Item.Count = curNum;
						}
						else
						{
							toSlot.Item.Count += curNum;
						}
						toSlot.UpdateUI();
						jiaoBag.RemoveTempItem(dragSlot.Item.Uid, curNum);
						dragSlot.Item.Count -= curNum;
						if (dragSlot.Item.Count <= 0)
						{
							jiaoBag.UpdateItem();
						}
						else
						{
							dragSlot.UpdateUI();
						}
						UpdatePlayerGetMoney();
					}
				});
				return;
			}
			if ((Object)(object)toSlot == (Object)null)
			{
				toSlot = jiaoBag.GetNullSellList(dragSlot.Item.Uid);
			}
			if ((Object)(object)toSlot == (Object)null)
			{
				UIPopTip.Inst.Pop("没有空的格子");
				return;
			}
			if (toSlot.IsNull())
			{
				toSlot.SetSlotData(dragSlot.Item.Clone());
				toSlot.Item.Count = num;
			}
			else
			{
				toSlot.Item.Count += num;
			}
			toSlot.UpdateUI();
			jiaoBag.RemoveTempItem(dragSlot.Item.Uid, num);
			dragSlot.Item.Count -= num;
			if (dragSlot.Item.Count <= 0)
			{
				jiaoBag.UpdateItem();
			}
			else
			{
				dragSlot.UpdateUI();
			}
			UpdatePlayerGetMoney();
		}
	}

	public void UpdatePlayerGetMoney()
	{
		PlayerGetMoney = 0;
		foreach (JiaoYiSlot sell in PlayerBag.SellList)
		{
			if (!sell.IsNull())
			{
				PlayerGetMoney += sell.Item.GetJiaoYiPrice(NpcId, isPlayer: true) * sell.Item.Count;
			}
		}
		foreach (JiaoYiSlot sell2 in NpcBag.SellList)
		{
			if (!sell2.IsNull())
			{
				PlayerGetMoney -= sell2.Item.GetJiaoYiPrice(NpcId) * sell2.Item.Count;
			}
		}
		if (PlayerGetMoney >= 0)
		{
			PlayerGetMoneyText.SetText($"+{PlayerGetMoney}");
		}
		else
		{
			PlayerGetMoneyText.SetText($"{PlayerGetMoney}");
		}
	}

	public void BackItem(JiaoYiSlot dragSlot, JiaoYiSlot toSlot = null)
	{
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		if (!dragSlot.Item.CanSale)
		{
			UIPopTip.Inst.Pop("此物品无法交易");
			return;
		}
		bool isPlayer = dragSlot.IsPlayer;
		JiaoBag jiaoBag = null;
		if (isPlayer)
		{
			jiaoBag = PlayerBag;
		}
		else
		{
			jiaoBag = NpcBag;
		}
		if (dragSlot.Item.Count < 1)
		{
			UIPopTip.Inst.Pop("此物品数量小于0，无法交易");
			return;
		}
		if ((Object)(object)toSlot == (Object)null)
		{
			toSlot = (JiaoYiSlot)jiaoBag.GetNullBagSlot(dragSlot.Item.Uid);
		}
		if (dragSlot.Item.Count == 1)
		{
			jiaoBag.AddTempItem(dragSlot.Item, 1);
			if ((Object)(object)toSlot == (Object)null)
			{
				jiaoBag.UpdateItem();
			}
			else
			{
				if (toSlot.IsNull())
				{
					toSlot.SetSlotData(dragSlot.Item.Clone());
					toSlot.Item.Count = 1;
				}
				else if (toSlot.Item.Id == dragSlot.Item.Id && toSlot.Item.MaxNum > 1)
				{
					toSlot.Item.Count++;
				}
				else
				{
					jiaoBag.UpdateItem();
				}
				toSlot.UpdateUI();
			}
			dragSlot.Item.Count--;
			if (dragSlot.Item.Count <= 0)
			{
				dragSlot.SetNull();
			}
			jiaoBag.UpdateItem();
			UpdatePlayerGetMoney();
			return;
		}
		int num = 0;
		if (dragSlot.Item.Count > 1)
		{
			if (Input.GetKey((KeyCode)304) || Input.GetKey((KeyCode)303))
			{
				num = 5;
				if (dragSlot.Item.Count < 5)
				{
					num = dragSlot.Item.Count;
				}
			}
			if (Input.GetKey((KeyCode)306) || Input.GetKey((KeyCode)305))
			{
				num = dragSlot.Item.Count;
			}
		}
		if (num <= 0)
		{
			bagItemSelect.Init(dragSlot.Item.GetName(), dragSlot.Item.Count, (UnityAction)delegate
			{
				int curNum = bagItemSelect.CurNum;
				jiaoBag.AddTempItem(dragSlot.Item, curNum);
				if ((Object)(object)toSlot == (Object)null)
				{
					jiaoBag.UpdateItem();
				}
				else
				{
					if (toSlot.IsNull())
					{
						toSlot.SetSlotData(dragSlot.Item.Clone());
						toSlot.Item.Count = curNum;
					}
					else if (toSlot.Item.Id == dragSlot.Item.Id && toSlot.Item.MaxNum > 1)
					{
						toSlot.Item.Count += curNum;
					}
					else
					{
						jiaoBag.UpdateItem();
					}
					toSlot.UpdateUI();
				}
				dragSlot.Item.Count -= curNum;
				if (dragSlot.Item.Count <= 0)
				{
					dragSlot.SetNull();
				}
				else
				{
					dragSlot.UpdateUI();
				}
				jiaoBag.UpdateItem();
				UpdatePlayerGetMoney();
			});
			return;
		}
		jiaoBag.AddTempItem(dragSlot.Item, num);
		if ((Object)(object)toSlot == (Object)null)
		{
			jiaoBag.UpdateItem();
		}
		else
		{
			if (toSlot.IsNull())
			{
				toSlot.SetSlotData(dragSlot.Item.Clone());
				toSlot.Item.Count = num;
			}
			else if (toSlot.Item.Id == dragSlot.Item.Id && toSlot.Item.MaxNum > 1)
			{
				toSlot.Item.Count += num;
			}
			else
			{
				jiaoBag.UpdateItem();
			}
			toSlot.UpdateUI();
		}
		dragSlot.Item.Count -= num;
		if (dragSlot.Item.Count <= 0)
		{
			dragSlot.SetNull();
		}
		else
		{
			dragSlot.UpdateUI();
		}
		jiaoBag.UpdateItem();
		UpdatePlayerGetMoney();
	}

	public void JiaoYiBtn()
	{
		if (!NpcSay())
		{
			return;
		}
		Tools.instance.getPlayer().AddMoney(PlayerGetMoney);
		NpcJieSuanManager.inst.npcSetField.AddNpcMoney(NpcId, -PlayerGetMoney);
		foreach (JiaoYiSlot sell in PlayerBag.SellList)
		{
			if (!sell.IsNull())
			{
				Tools.instance.RemoveItem(sell.Item.Uid, sell.Item.Count);
				NpcJieSuanManager.inst.AddItemToNpcBackpack(NpcId, sell.Item.Id, sell.Item.Count, sell.Item.Seid.Copy());
			}
		}
		foreach (JiaoYiSlot sell2 in NpcBag.SellList)
		{
			if (!sell2.IsNull())
			{
				Tools.instance.NewAddItem(sell2.Item.Id, sell2.Item.Count, sell2.Item.Seid.Copy(), sell2.Item.Uid);
				NpcJieSuanManager.inst.RemoveItem(NpcId, sell2.Item.Id, sell2.Item.Count, sell2.Item.Uid);
			}
		}
		NpcBag.JiaoYiCallBack();
		PlayerBag.JiaoYiCallBack();
		UpdatePlayerGetMoney();
	}

	public bool NpcSay()
	{
		bool result = true;
		int money = NpcBag.GetMoney();
		int money2 = PlayerBag.GetMoney();
		if (PlayerGetMoney >= 0 && money < PlayerGetMoney)
		{
			result = false;
			int num = jsonData.instance.getRandom() % 10;
			NpcSayText.SetText(Tools.getStr("exchengePlayer" + num));
			NpcSayPanel.gameObject.SetActive(true);
			((MonoBehaviour)this).Invoke("CloseSay", 1.5f);
		}
		else if (PlayerGetMoney < 0 && money2 + PlayerGetMoney < 0)
		{
			result = false;
			int num2 = jsonData.instance.getRandom() % 10;
			NpcSayText.SetText(Tools.getStr("exchengeMonstar" + num2));
			NpcSayPanel.gameObject.SetActive(true);
			((MonoBehaviour)this).Invoke("CloseSay", 1.5f);
		}
		return result;
	}

	public void CloseSay()
	{
		NpcSayPanel.gameObject.SetActive(false);
	}

	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		if (CloseAction != null)
		{
			CloseAction.Invoke();
		}
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
