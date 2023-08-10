using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaiMai;

public class PaiMaiUiMag : SingletonMono<PaiMaiUiMag>
{
	public int PaiMaiId;

	[SerializeField]
	private AvatarCtr _avatarCtr;

	[SerializeField]
	private PlayerCtr _playerCtr;

	[SerializeField]
	private PaiMaiSay _playerSayCtr;

	[SerializeField]
	public FungusSay _fungusSay;

	public List<PaiMaiShop> ShopList;

	public List<Sprite> StateSprites;

	public List<Sprite> HostSprites;

	public Dictionary<PaiMaiAvatar.StateType, string> StateColors;

	public PaiMaiShop CurShop;

	public int ShopIndex;

	public PaiMaiAvatar CurAvatar;

	public PaiMaiHost Host;

	public PaiMaiItem ItemUI;

	public Dictionary<int, List<int>> WordDict;

	public Dictionary<int, List<int>> AddPriceWordDict;

	private int _curRoundAddNum;

	private int _round;

	private CeLueType _ceLueType;

	[SerializeField]
	private Text _roundText;

	[SerializeField]
	private GameObject _ceLueMask;

	[SerializeField]
	private GameObject _endPanel;

	[SerializeField]
	private Text _paiMaiJiLu;

	[SerializeField]
	private Text _sceneName;

	private void Awake()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		SingletonMono<PaiMaiUiMag>._instance = this;
		_curRoundAddNum = 0;
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localScale = new Vector3(0.821f, 0.821f, 0.821f);
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).transform.SetAsLastSibling();
		WordDict = new Dictionary<int, List<int>>();
		AddPriceWordDict = new Dictionary<int, List<int>>();
		int key = 0;
		foreach (PaiMaiDuiHuaBiao data in PaiMaiDuiHuaBiao.DataList)
		{
			if (data.huanhua.Count == 2)
			{
				key = data.huanhua[0] * 10 + data.huanhua[1];
			}
			else if (data.huanhua.Count == 1)
			{
				key = data.huanhua[0] * 10;
			}
			if (WordDict.ContainsKey(key))
			{
				WordDict[key].Add(data.id);
				continue;
			}
			WordDict.Add(key, new List<int> { data.id });
		}
		foreach (PaiMaiNpcAddPriceSay data2 in PaiMaiNpcAddPriceSay.DataList)
		{
			if (AddPriceWordDict.ContainsKey(data2.Type))
			{
				AddPriceWordDict[data2.Type].Add(data2.id);
				continue;
			}
			AddPriceWordDict.Add(data2.Type, new List<int> { data2.id });
		}
		_round = 0;
		Init();
	}

	public void Init()
	{
		ShopIndex = 0;
		PaiMaiShopData paiMaiShopData = (PaiMaiShopData)BindData.Get("PaiMaiData");
		PaiMaiId = paiMaiShopData.id;
		ShopList = paiMaiShopData.ShopList;
		_sceneName.text = PaiMaiBiao.DataDict[PaiMaiId].Name;
		Host.Init();
		StateColors = new Dictionary<PaiMaiAvatar.StateType, string>();
		StateColors.Add(PaiMaiAvatar.StateType.略感兴趣, "98ffa4");
		StateColors.Add(PaiMaiAvatar.StateType.跃跃欲试, "89f7ff");
		StateColors.Add(PaiMaiAvatar.StateType.势在必得, "ffb098");
		CreatePaiMaiNpcList();
		StartPaiMai();
	}

	private void CreatePaiMaiNpcList()
	{
		List<int> list = new List<int>();
		PaiMaiCanYuAvatar paiMaiCanYuAvatar = null;
		foreach (PaiMaiCanYuAvatar data in PaiMaiCanYuAvatar.DataList)
		{
			if (data.PaiMaiID == PaiMaiId)
			{
				paiMaiCanYuAvatar = data;
				break;
			}
		}
		if (paiMaiCanYuAvatar == null)
		{
			Debug.LogError((object)$"找不到拍卖会ID不存在，ID：{PaiMaiId}");
			return;
		}
		int num = paiMaiCanYuAvatar.AvatrNum;
		if (paiMaiCanYuAvatar.Jie != 0 && (paiMaiCanYuAvatar.Jie == -1 || paiMaiCanYuAvatar.Jie == Tools.instance.getPlayer().StreamData.PaiMaiDataMag.PaiMaiDict[PaiMaiId].No))
		{
			foreach (int item in paiMaiCanYuAvatar.AvatrID)
			{
				if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(item))
				{
					list.Add(NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[item]);
				}
				else
				{
					if (item < 20000)
					{
						continue;
					}
					list.Add(item);
				}
				num--;
			}
		}
		List<int> paiMaiListByPaiMaiId = NpcJieSuanManager.inst.GetPaiMaiListByPaiMaiId(PaiMaiId);
		if (paiMaiListByPaiMaiId.Count > 0)
		{
			for (int i = 0; i < paiMaiListByPaiMaiId.Count; i++)
			{
				if (num <= 0)
				{
					break;
				}
				list.Add(paiMaiListByPaiMaiId[i]);
				num--;
			}
		}
		if (num > 0)
		{
			foreach (PaiMaiOldAvatar data2 in PaiMaiOldAvatar.DataList)
			{
				if (num == 0)
				{
					break;
				}
				if (!jsonData.instance.MonstarIsDeath(data2.id) && Tools.instance.GetRandomInt(0, 100) <= data2.GaiLv)
				{
					list.Add(data2.id);
					num--;
				}
			}
		}
		if (num > 0)
		{
			int num2 = 0;
			string text = "";
			JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
			List<string> list2 = new List<string>();
			foreach (string key in jsonData.instance.AvatarJsonData.keys)
			{
				if (int.Parse(key) >= 20000)
				{
					list2.Add(key);
				}
			}
			while (num > 0)
			{
				text = list2[Tools.instance.GetRandomInt(0, list2.Count - 1)];
				num2 = int.Parse(text);
				if (avatarJsonData[text]["paimaifenzu"].ToList().Contains(PaiMaiBiao.DataDict[PaiMaiId].paimaifenzu) && !avatarJsonData[text]["isImportant"].b && paiMaiCanYuAvatar.JinJie[0] <= avatarJsonData[text]["Level"].I && avatarJsonData[text]["Level"].I <= paiMaiCanYuAvatar.JinJie[1])
				{
					if (!list.Contains(num2))
					{
						list.Add(num2);
						num--;
					}
					if (num == 0)
					{
						break;
					}
				}
			}
		}
		int num3 = 0;
		Transform val = ((Component)this).transform.Find("NpcList");
		_avatarCtr.AvatarList = new List<PaiMaiAvatar>();
		foreach (int item2 in list)
		{
			PaiMaiAvatar paiMaiAvatar = new PaiMaiAvatar(item2);
			paiMaiAvatar.UiCtr = ((Component)val.GetChild(num3)).GetComponent<AvatarUI>();
			paiMaiAvatar.UiCtr.Init(paiMaiAvatar);
			_avatarCtr.AvatarList.Add(paiMaiAvatar);
			num3++;
		}
		Dictionary<string, PlayerCommand> dictionary = new Dictionary<string, PlayerCommand>();
		Transform val2 = ((Component)this).transform.Find("Player/Command");
		Text component = ((Component)((Component)this).transform.Find("Player/Money/Num")).GetComponent<Text>();
		CommandTips tips = ((Component)((Component)this).transform.Find("Player/Tips")).gameObject.AddComponent<CommandTips>();
		for (int j = 0; j < val2.childCount; j++)
		{
			dictionary.Add(((Object)val2.GetChild(j)).name, ((Component)val2.GetChild(j)).GetComponent<PlayerCommand>());
		}
		PaiMaiAvatar player = new PaiMaiAvatar(Tools.instance.getPlayer().name);
		_playerCtr = new PlayerCtr(player, dictionary, component, tips);
	}

	public void PlayerUseCeLue(PlayerCommand command)
	{
		_ceLueMask.SetActive(true);
		_ceLueType = command.CeLueType;
		_avatarCtr.SetCanSelect(_ceLueType);
	}

	public void SelectAvatarCallBack(PaiMaiAvatar avatar)
	{
		_ceLueMask.SetActive(false);
		_avatarCtr.StopSelect();
		if (!Host.ReduceNaiXin(_ceLueType))
		{
			_playerCtr.GiveUpCurShop();
			PaiMaiSayData sayData = new PaiMaiSayData
			{
				Id = 0,
				Msg = "你的恶意行为似乎惹怒了镇守拍卖会的修士，一道冰冷的神识扫过，你顿时动弹不得，惊出一身冷汗。"
			};
			_fungusSay.Say(sayData);
			return;
		}
		int ceLueType = (int)_ceLueType;
		int num = 0;
		int num2 = 0;
		if (_ceLueType == CeLueType.神识威慑)
		{
			if (avatar.ShenShi < Tools.instance.getPlayer().shengShi)
			{
				avatar.UiCtr.SetState(PaiMaiAvatar.StateType.放弃, isSay: true);
				num = 1;
				NPCEx.AddFavor(avatar.NpcId, -2);
			}
			else
			{
				num = 2;
			}
		}
		else if (_ceLueType == CeLueType.言语恐吓)
		{
			if (avatar.Level < Tools.instance.getPlayer().level)
			{
				avatar.UiCtr.SetState(PaiMaiAvatar.StateType.放弃, isSay: true);
				num = 1;
				NPCEx.AddFavor(avatar.NpcId, -2);
			}
			else
			{
				num = 2;
			}
		}
		else if (_ceLueType == CeLueType.出言挑衅)
		{
			if (avatar.State != PaiMaiAvatar.StateType.势在必得)
			{
				avatar.UiCtr.SetState(avatar.State + 1, isSay: true);
			}
			num = 1;
			NPCEx.AddFavor(avatar.NpcId, -2);
			avatar.AddMaxMoney(0.05f);
		}
		if (avatar.NpcId < 20000)
		{
			num2 = 0;
		}
		else if (PlayerEx.IsTheather(avatar.NpcId))
		{
			num2 = 13;
		}
		else if (PlayerEx.IsBrother(avatar.NpcId))
		{
			num2 = 12;
		}
		else if (PlayerEx.IsDaoLv(avatar.NpcId))
		{
			num2 = 11;
		}
		else if (UINPCHeadFavor.GetFavorLevel(NPCEx.GetFavor(avatar.NpcId)) > 5)
		{
			num2 = 1;
		}
		foreach (PaiMaiDuiHuaAI data in PaiMaiDuiHuaAI.DataList)
		{
			if (data.CeLue != ceLueType || data.JieGuo != num || data.MuBiao != num2)
			{
				continue;
			}
			PaiMaiSayData paiMaiSayData = new PaiMaiSayData();
			if (data.DuiHua.Contains("{pangbai}"))
			{
				paiMaiSayData.Id = 0;
				paiMaiSayData.Msg = data.DuiHua.Replace("{pangbai}", "");
			}
			else
			{
				paiMaiSayData.Id = 1;
				paiMaiSayData.Msg = data.DuiHua;
			}
			if (data.HuiFu != "")
			{
				paiMaiSayData.Action = delegate
				{
					PaiMaiSayData sayData2 = new PaiMaiSayData
					{
						Id = avatar.NpcId,
						Msg = data.HuiFu
					};
					_fungusSay.Say(sayData2);
				};
			}
			_fungusSay.Say(paiMaiSayData);
			break;
		}
	}

	public void CancelUserCelue()
	{
		_ceLueMask.SetActive(false);
		_avatarCtr.StopSelect();
	}

	private void StartPaiMai()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		ShopIndex = 0;
		CurShop = ShopList[ShopIndex];
		ItemUI.UpdateItem();
		_avatarCtr.AllAvatarThinkItem();
		Host.SayWord("很荣幸大家来到本场拍卖会", (UnityAction)delegate
		{
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Expected O, but got Unknown
			Host.SayWord("下面是本场拍卖会的第" + ShopIndex.ToCNNumber() + "件拍品，" + $"{GetDesc()}{CurShop.ShopName}。底价<color=#096272>{CurShop.CurPrice}</color>灵石，" + $"每次加价不得少于<color=#096272>{CurShop.MinAddPrice}</color>灵石。", (UnityAction)delegate
			{
				_avatarCtr.AvatarSayWord();
				_avatarCtr.AvatarStart();
			}, 2f);
		});
	}

	private string GetDesc()
	{
		string result = "";
		foreach (PaiMaiMiaoShuBiao data in PaiMaiMiaoShuBiao.DataList)
		{
			if (data.Type == CurShop.Type && data.Type2 == CurShop.BaseQuality)
			{
				result = data.Text + data.Text2;
				break;
			}
		}
		return result;
	}

	public bool AddPrice(int type = 0)
	{
		int num = 0;
		int num2 = 0;
		if (CurAvatar.IsPlayer)
		{
			num2 = PaiMaiChuJia.DataDict[type].ZhanBi * CurShop.Price / 100;
			if (num2 < 1)
			{
				num2 = 1;
			}
			num = CurShop.CurPrice + num2;
			if (CurAvatar.Money < num)
			{
				UIPopTip.Inst.Pop("灵石不足");
				return false;
			}
		}
		else
		{
			if (CurAvatar.State == PaiMaiAvatar.StateType.略感兴趣 && _curRoundAddNum != 0)
			{
				int key = AddPriceWordDict[0][Tools.instance.GetRandomInt(0, AddPriceWordDict[0].Count - 1)];
				CurAvatar.UiCtr.SayWord(PaiMaiNpcAddPriceSay.DataDict[key].ChuJiaDuiHua);
				return false;
			}
			if (CurShop.Owner != null && CurShop.Owner.NpcId == CurAvatar.NpcId)
			{
				int key2 = AddPriceWordDict[99][Tools.instance.GetRandomInt(0, AddPriceWordDict[99].Count - 1)];
				CurAvatar.UiCtr.SayWord(PaiMaiNpcAddPriceSay.DataDict[key2].ChuJiaDuiHua);
				return false;
			}
			type = (int)(CurAvatar.State - 1);
			num2 = PaiMaiChuJia.DataDict[type].ZhanBi * CurShop.Price / 100;
			if (num2 < 1)
			{
				num2 = 1;
			}
			num = CurShop.CurPrice + num2;
			if (num > CurAvatar.MaxPrice)
			{
				type = 1;
				num2 = PaiMaiChuJia.DataDict[type].ZhanBi * CurShop.Price / 100;
				if (num2 < 1)
				{
					num2 = 1;
				}
				num = CurShop.CurPrice + num2;
				if (num > CurAvatar.MaxPrice)
				{
					CurAvatar.UiCtr.SetState(PaiMaiAvatar.StateType.放弃, isSay: true);
					return false;
				}
			}
		}
		if (!CurAvatar.IsPlayer)
		{
			int key3 = AddPriceWordDict[(int)(CurAvatar.State - 1)][Tools.instance.GetRandomInt(0, AddPriceWordDict[(int)(CurAvatar.State - 1)].Count - 1)];
			CurAvatar.UiCtr.SayWord(PaiMaiNpcAddPriceSay.DataDict[key3].ChuJiaDuiHua.Replace("{price}", $"<color=#096272>{num}</color>"));
		}
		else
		{
			string text = $"我出价<color=#096272>{num}</color>灵石";
			text = ((type < 3) ? (text + "。") : (text + "!"));
			_playerSayCtr.SayWord(text);
		}
		CurShop.CurPrice = num;
		CurShop.Owner = CurAvatar;
		_curRoundAddNum++;
		_avatarCtr.AddTagetStateMaxPrice(CurAvatar, PaiMaiAvatar.StateType.势在必得, 20, 0.01f);
		if (type == 4)
		{
			_avatarCtr.AddTagetStateMaxPrice(CurAvatar, PaiMaiAvatar.StateType.所有状态, 100, -0.03f);
		}
		AddPriceCallBack(type);
		return true;
	}

	private void AddPriceCallBack(int type)
	{
		ItemUI.UpdateUI();
		PaiMaiChuJiaAI paiMaiChuJiaAI = PaiMaiChuJiaAI.DataDict[type];
		if (paiMaiChuJiaAI.Type.Count < 1)
		{
			return;
		}
		int num = -1;
		foreach (PaiMaiAvatar avatar in _avatarCtr.AvatarList)
		{
			if (avatar.NpcId == CurAvatar.NpcId)
			{
				continue;
			}
			num = paiMaiChuJiaAI.Type.IndexOf((int)avatar.State);
			if (num >= 0 && Tools.instance.GetRandomInt(0, 100) <= paiMaiChuJiaAI.GaiLv[num])
			{
				if (paiMaiChuJiaAI.YingXiang[num] == 1)
				{
					avatar.UiCtr.SetState(avatar.State + 1, isSay: true);
				}
				else if (paiMaiChuJiaAI.YingXiang[num] == 2)
				{
					avatar.UiCtr.SetState(avatar.State - 1, isSay: true);
				}
			}
		}
	}

	public void EndRound()
	{
		if (CurAvatar == null)
		{
			Debug.LogError((object)"当前CurAvatar为空");
		}
		else if (CurAvatar.IsPlayer)
		{
			if (_avatarCtr.IsAllGiveUp() && CurShop.Owner != null && CurShop.Owner.IsPlayer)
			{
				EndCurShop();
			}
			else if (_curRoundAddNum == 0)
			{
				EndCurShop();
			}
			else
			{
				NextRound();
			}
		}
		else if (_playerCtr.IsCurShopQuickEnd || _playerCtr.IsAllQuickEnd)
		{
			if (_curRoundAddNum == 0)
			{
				EndCurShop();
			}
			else
			{
				NextRound();
			}
		}
		else if (CurShop.Owner != null && CurShop.Owner.IsPlayer && _avatarCtr.IsAllGiveUp())
		{
			EndCurShop();
		}
		else
		{
			_playerCtr.StartAction();
		}
	}

	private void NextRound()
	{
		AddRound();
		_curRoundAddNum = 0;
		_avatarCtr.AvatarStart();
	}

	private void EndCurShop()
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Expected O, but got Unknown
		ShopIndex++;
		if (ItemUI.Owner == null)
		{
			if (CurShop.IsPlayer)
			{
				Tools.instance.getPlayer().addItem(CurShop.ShopId, CurShop.Count, CurShop.Seid);
			}
			Host.SayWord("由于没有任何人出价，我宣布，" + CurShop.ShopName + "流拍。", new UnityAction(NextShop), 2f);
			return;
		}
		CurAvatar = ItemUI.Owner;
		CurAvatar.Money -= CurShop.CurPrice;
		if (ItemUI.Owner.IsPlayer)
		{
			_playerCtr.BuyShop();
		}
		else
		{
			if (CurAvatar.NpcId >= 20000)
			{
				NpcJieSuanManager.inst.npcSetField.AddNpcMoney(CurAvatar.NpcId, -CurShop.CurPrice);
			}
			NpcJieSuanManager.inst.AddItemToNpcBackpack(CurAvatar.NpcId, CurShop.ShopId, CurShop.Count, CurShop.Seid, isPaiMai: true);
		}
		Debug.Log((object)"");
		if (CurShop.IsPlayer)
		{
			_playerCtr.MallShop();
		}
		Host.SayWord($"恭喜{ItemUI.Owner.Name}道友以{CurShop.CurPrice}灵石的价格拍得这件宝物。", new UnityAction(NextShop), 2f);
	}

	private void NextShop()
	{
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Expected O, but got Unknown
		RestartRound();
		if (ShopIndex >= ShopList.Count)
		{
			EndPaiMai();
			return;
		}
		CurShop = ShopList[ShopIndex];
		ItemUI.UpdateItem();
		Host.AddNaiXin();
		_avatarCtr.AllAvatarThinkItem();
		if (_playerCtr.IsCurShopQuickEnd)
		{
			_playerCtr.IsCurShopQuickEnd = false;
			Time.timeScale = 1f;
		}
		_playerCtr.RestartGiveUpCurShop();
		if (ShopIndex == ShopList.Count - 1)
		{
			Host.SayWord("下面是本场拍卖会的压轴拍品，" + $"{GetDesc()}{CurShop.ShopName}。底价{CurShop.CurPrice}灵石，" + $"每次加价不得少于{CurShop.MinAddPrice}灵石。", (UnityAction)delegate
			{
				_avatarCtr.AvatarSayWord();
				_avatarCtr.AvatarStart();
			}, 2f);
			return;
		}
		Host.SayWord("下面是本场拍卖会的第" + (ShopIndex + 1).ToCNNumber() + "件拍品，" + $"{GetDesc()}{CurShop.ShopName}。底价{CurShop.CurPrice}灵石，" + $"每次加价不得少于{CurShop.MinAddPrice}灵石。", (UnityAction)delegate
		{
			_avatarCtr.AvatarSayWord();
			_avatarCtr.AvatarStart();
		}, 2f);
	}

	private void EndPaiMai()
	{
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		foreach (PaiMaiShop shop in ShopList)
		{
			if (shop.Owner != null)
			{
				Text paiMaiJiLu = _paiMaiJiLu;
				paiMaiJiLu.text = paiMaiJiLu.text + "<color=#1c5e4c>" + shop.Owner.Name + "</color>" + $"以<color=#1c5e4c>{shop.CurPrice}</color>灵石的价格购买了" + "<color=#1c5e4c>" + shop.ShopName + "</color>\n";
			}
			else
			{
				Text paiMaiJiLu2 = _paiMaiJiLu;
				paiMaiJiLu2.text = paiMaiJiLu2.text + "物品<color=#1c5e4c>" + shop.ShopName + "</color>无人竞价已流拍\n";
			}
		}
		Time.timeScale = 1f;
		Host.SayWord("本次拍卖到此结束，感谢诸位道友的参与。", (UnityAction)delegate
		{
			_endPanel.SetActive(true);
		}, 2f);
	}

	private void AddRound()
	{
		_round++;
		_roundText.text = "第" + _round.ToCNNumber() + "轮";
	}

	private void RestartRound()
	{
		_round = 1;
		_roundText.text = "第" + _round.ToCNNumber() + "轮";
	}

	public void QuickEnd()
	{
		if (!_playerCtr.IsAllQuickEnd)
		{
			Time.timeScale = 100f;
			_playerCtr.IsAllQuickEnd = true;
			if (_playerCtr.CanAction)
			{
				EndRound();
			}
		}
	}

	public void Close()
	{
		Object.Destroy((Object)(object)((Component)this).gameObject);
		Time.timeScale = 1f;
		Tools.instance.loadMapScenes(Tools.instance.getPlayer().lastScence);
	}
}
