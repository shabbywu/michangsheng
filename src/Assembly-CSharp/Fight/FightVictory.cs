using DG.Tweening;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame.Fight;

namespace Fight;

public class FightVictory : MonoBehaviour, IESCClose
{
	[SerializeField]
	private Transform ItemList;

	[SerializeField]
	private GameObject ItemPrefab;

	[SerializeField]
	private Text Desc;

	[SerializeField]
	private Text GetMoney;

	[SerializeField]
	private FpBtn Btn;

	[SerializeField]
	private Transform panel;

	private int monstarID;

	private Avatar avatar;

	private void Awake()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		SetVictory();
		if ((Object)(object)UIFightPanel.Inst != (Object)null)
		{
			UIFightPanel.Inst.Close();
		}
		ESCCloseManager.Inst.RegisterClose(this);
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).transform.localScale = Vector3.one;
		((Component)this).transform.SetAsLastSibling();
		Tools.canClickFlag = false;
		monstarID = Tools.instance.MonstarID;
		avatar = Tools.instance.getPlayer();
		((Component)this).gameObject.SetActive(true);
		Init();
		panel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		ShortcutExtensions.DOScale(panel, Vector3.one, 0.5f);
	}

	private void SetVictory()
	{
		GlobalValue.SetTalk(1, 2, "Avatar.die");
		PlayerEx.Player.StaticValue.talk[1] = 2;
	}

	private void Init()
	{
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Expected O, but got Unknown
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Expected O, but got Unknown
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		_ = (float)(Tools.instance.monstarMag.gameStartHP - avatar.HP) / (float)avatar.HP_Max;
		int num = (int)jsonData.instance.AvatarJsonData[monstarID.ToString()]["dropType"].n;
		foreach (JSONObject item in jsonData.instance.DropInfoJsonData.list)
		{
			if (num != (int)item["dropType"].n)
			{
				continue;
			}
			Desc.text = item["TextDesc"].Str;
			addMoney(item["moneydrop"].n / 100f);
			foreach (JSONObject item2 in NpcJieSuanManager.inst.npcFight.npcDrop.dropReward(item["wepen"].n / 100f, item["backpack"].n / 100f, monstarID).list)
			{
				Tools.instance.getPlayer().addItem(item2["ID"].I, item2["Num"].I, item2["seid"]);
				UIIconShow component = ItemPrefab.Inst(ItemList).GetComponent<UIIconShow>();
				component.SetItem(new item(item2["ID"].I)
				{
					Seid = item2["seid"],
					itemNum = item2["Num"].I
				});
				component.Count = item2["Num"].I;
				component.CanDrag = false;
				((Graphic)component.BottomBG).color = Color32.op_Implicit(new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)0));
			}
			if (Tools.instance.MonstarID >= 20000)
			{
				if (NpcJieSuanManager.inst.isCanJieSuan)
				{
					NpcJieSuanManager.inst.npcDeath.SetNpcDeath(2, Tools.instance.MonstarID);
				}
				else
				{
					NpcJieSuanManager.inst.npcDeath.SetNpcDeath(2, Tools.instance.MonstarID, 0, after: true);
				}
				NpcJieSuanManager.inst.npcMap.RemoveNpcByList(Tools.instance.MonstarID);
				NpcJieSuanManager.inst.isUpDateNpcList = true;
			}
			else if ((Object)(object)RoundManager.instance == (Object)null)
			{
				if (GlobalValue.Get(401, "Avatar.die") == Tools.instance.MonstarID)
				{
					PlayerEx.Player.nomelTaskMag.AutoNTaskSetKillAvatar(Tools.instance.MonstarID);
				}
				jsonData.instance.setMonstarDeath(Tools.instance.MonstarID);
				Tools.instance.AutoSetSeaMonstartDie();
			}
			else
			{
				Tools.instance.getPlayer().OtherAvatar?.die();
			}
			if (!PlayerEx.Player.HasDefeatNpcList.Contains(Tools.instance.MonstarID))
			{
				PlayerEx.Player.HasDefeatNpcList.Add(Tools.instance.MonstarID);
			}
			break;
		}
		Btn.mouseUpEvent.AddListener(new UnityAction(Close));
		Btn.mouseUpEvent.AddListener((UnityAction)delegate
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		});
	}

	private void addMoney(float percent)
	{
		int num = (int)(jsonData.instance.AvatarBackpackJsonData[string.Concat(monstarID)]["money"].n * percent);
		avatar.money += (ulong)num;
		GetMoney.text = num.ToString();
	}

	private void Close()
	{
		PanelMamager.CanOpenOrClose = true;
		Tools.canClickFlag = true;
		Tools.instance.loadMapScenes(Tools.instance.FinalScene);
		Object.Destroy((Object)(object)((Component)this).gameObject);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
