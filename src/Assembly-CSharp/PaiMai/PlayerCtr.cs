using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaiMai;

[Serializable]
public class PlayerCtr
{
	public PaiMaiAvatar Player;

	public bool IsCurShopQuickEnd;

	public bool IsAllQuickEnd;

	[SerializeField]
	private readonly Dictionary<string, PlayerCommand> _commandict;

	private CommandTips _tips;

	private Avatar _player;

	private Text _moneyText;

	public bool CanAction;

	public PlayerCtr(PaiMaiAvatar player, Dictionary<string, PlayerCommand> dit, Text moneyText, CommandTips tips)
	{
		_tips = tips;
		CanAction = false;
		IsCurShopQuickEnd = false;
		IsAllQuickEnd = false;
		Player = player;
		_player = Tools.instance.getPlayer();
		_commandict = dit;
		_moneyText = moneyText;
		UpdateMoney();
		InitCommand();
	}

	private void InitCommand()
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Expected O, but got Unknown
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Expected O, but got Unknown
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Expected O, but got Unknown
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Expected O, but got Unknown
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Expected O, but got Unknown
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Expected O, but got Unknown
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00ed: Expected O, but got Unknown
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Expected O, but got Unknown
		//IL_0196: Expected O, but got Unknown
		int commandId = 0;
		string tips = "";
		UnityAction val = default(UnityAction);
		UnityAction val4 = default(UnityAction);
		foreach (string key in _commandict.Keys)
		{
			if (_commandict[key].CommandType == CommandType.加价)
			{
				_commandict[key].AddClickAction((UnityAction)delegate
				{
					if (!IsAllQuickEnd && CanAction)
					{
						_tips.Hide();
						if (SingletonMono<PaiMaiUiMag>.Instance.AddPrice((int)Enum.Parse(typeof(Command), key, ignoreCase: true)))
						{
							EndAction();
						}
					}
				});
				_commandict[key].AddMouseEnterListener((UnityAction)delegate
				{
					commandId = (int)Enum.Parse(typeof(Command), key, ignoreCase: true);
					int num = PaiMaiChuJia.DataDict[commandId].ZhanBi * SingletonMono<PaiMaiUiMag>.Instance.CurShop.Price;
					num /= 100;
					if (num < 1)
					{
						num = 1;
					}
					num += SingletonMono<PaiMaiUiMag>.Instance.CurShop.CurPrice;
					tips = PaiMaiCommandTips.DataList[commandId].Text.Replace("{lingshi}", $"{num}");
					_tips.ShowTips(tips);
				});
				PlayerCommand playerCommand = _commandict[key];
				UnityAction obj = val;
				if (obj == null)
				{
					UnityAction val2 = delegate
					{
						_tips.Hide();
					};
					UnityAction val3 = val2;
					val = val2;
					obj = val3;
				}
				playerCommand.AddMouseOutsListener(obj);
			}
			else
			{
				if (_commandict[key].CommandType != CommandType.策略)
				{
					continue;
				}
				_commandict[key].AddClickAction((UnityAction)delegate
				{
					if (!IsAllQuickEnd)
					{
						SingletonMono<PaiMaiUiMag>.Instance.PlayerUseCeLue(_commandict[key]);
					}
				});
				_commandict[key].AddMouseEnterListener((UnityAction)delegate
				{
					commandId = (int)Enum.Parse(typeof(Command), key, ignoreCase: true) + 1;
					tips = PaiMaiCommandTips.DataDict[commandId].Text;
					_tips.ShowTips(tips);
				});
				PlayerCommand playerCommand2 = _commandict[key];
				UnityAction obj2 = val4;
				if (obj2 == null)
				{
					UnityAction val5 = delegate
					{
						_tips.Hide();
					};
					UnityAction val3 = val5;
					val4 = val5;
					obj2 = val3;
				}
				playerCommand2.AddMouseOutsListener(obj2);
			}
		}
		_commandict[Command.放弃竞品.ToString()].AddClickAction((UnityAction)delegate
		{
			if (!IsAllQuickEnd)
			{
				_tips.Hide();
				GiveUpCurShop();
			}
		});
		_commandict[Command.放弃竞品.ToString()].AddMouseEnterListener((UnityAction)delegate
		{
			commandId = (int)Enum.Parse(typeof(Command), Command.放弃竞品.ToString(), ignoreCase: true) + 1;
			tips = PaiMaiCommandTips.DataDict[commandId].Text;
			_tips.ShowTips(tips);
		});
		_commandict[Command.放弃竞品.ToString()].AddMouseOutsListener((UnityAction)delegate
		{
			_tips.Hide();
		});
		_commandict[Command.本轮不出.ToString()].AddClickAction((UnityAction)delegate
		{
			if (!IsAllQuickEnd && CanAction)
			{
				_tips.Hide();
				EndAction();
			}
		});
		_commandict[Command.本轮不出.ToString()].AddMouseEnterListener((UnityAction)delegate
		{
			commandId = (int)Enum.Parse(typeof(Command), Command.本轮不出.ToString(), ignoreCase: true) + 1;
			tips = PaiMaiCommandTips.DataDict[commandId].Text;
			_tips.ShowTips(tips);
		});
		_commandict[Command.本轮不出.ToString()].AddMouseOutsListener((UnityAction)delegate
		{
			_tips.Hide();
		});
		StopAction();
	}

	public void BuyShop()
	{
		_player.addItem(SingletonMono<PaiMaiUiMag>.Instance.CurShop.ShopId, SingletonMono<PaiMaiUiMag>.Instance.CurShop.Count, SingletonMono<PaiMaiUiMag>.Instance.CurShop.Seid);
		_player.AddMoney(-SingletonMono<PaiMaiUiMag>.Instance.CurShop.CurPrice);
		UpdateMoney();
	}

	private void UpdateMoney()
	{
		Player.Money = (int)_player.money;
		_moneyText.text = _player.money.ToString();
	}

	public void MallShop()
	{
		_player.money += (ulong)((float)SingletonMono<PaiMaiUiMag>.Instance.CurShop.CurPrice * 0.9f);
		UpdateMoney();
	}

	public void StartAction()
	{
		SingletonMono<PaiMaiUiMag>.Instance.CurAvatar = Player;
		for (int i = 1; i <= 8; i++)
		{
			Dictionary<string, PlayerCommand> commandict = _commandict;
			Command command = (Command)i;
			commandict[command.ToString()].PlayBackTo();
		}
		CanAction = true;
	}

	public void RestartGiveUpCurShop()
	{
		if (!_commandict[Command.放弃竞品.ToString()].CanClick)
		{
			_commandict[Command.放弃竞品.ToString()].PlayBackTo();
		}
	}

	public void GiveUpCurShop()
	{
		IsCurShopQuickEnd = true;
		_commandict[Command.放弃竞品.ToString()].PlayToBack();
		Time.timeScale = 100f;
		if (CanAction)
		{
			EndAction();
		}
	}

	private void EndAction()
	{
		CanAction = false;
		StopAction();
		Debug.Log((object)"玩家回合结束");
		SingletonMono<PaiMaiUiMag>.Instance.EndRound();
	}

	private void StopAction()
	{
		for (int i = 1; i <= 8; i++)
		{
			Dictionary<string, PlayerCommand> commandict = _commandict;
			Command command = (Command)i;
			commandict[command.ToString()].PlayToBack();
		}
	}
}
