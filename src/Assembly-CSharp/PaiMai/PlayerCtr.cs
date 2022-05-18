using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaiMai
{
	// Token: 0x02000A6F RID: 2671
	[Serializable]
	public class PlayerCtr
	{
		// Token: 0x060044C7 RID: 17607 RVA: 0x001D7258 File Offset: 0x001D5458
		public PlayerCtr(PaiMaiAvatar player, Dictionary<string, PlayerCommand> dit, Text moneyText, CommandTips tips)
		{
			this._tips = tips;
			this.CanAction = false;
			this.IsCurShopQuickEnd = false;
			this.IsAllQuickEnd = false;
			this.Player = player;
			this._player = Tools.instance.getPlayer();
			this._commandict = dit;
			this._moneyText = moneyText;
			this.UpdateMoney();
			this.InitCommand();
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x001D72BC File Offset: 0x001D54BC
		private void InitCommand()
		{
			int commandId = 0;
			string tips = "";
			using (Dictionary<string, PlayerCommand>.KeyCollection.Enumerator enumerator = this._commandict.Keys.GetEnumerator())
			{
				UnityAction <>9__8;
				UnityAction <>9__11;
				while (enumerator.MoveNext())
				{
					string key = enumerator.Current;
					if (this._commandict[key].CommandType == CommandType.加价)
					{
						this._commandict[key].AddClickAction(delegate
						{
							if (this.IsAllQuickEnd)
							{
								return;
							}
							if (this.CanAction)
							{
								this._tips.Hide();
								if (SingletonMono<PaiMaiUiMag>.Instance.AddPrice((int)Enum.Parse(typeof(Command), key, true)))
								{
									this.EndAction();
								}
							}
						});
						this._commandict[key].AddMouseEnterListener(delegate
						{
							commandId = (int)Enum.Parse(typeof(Command), key, true);
							int num = PaiMaiChuJia.DataDict[commandId].ZhanBi * SingletonMono<PaiMaiUiMag>.Instance.CurShop.Price;
							num /= 100;
							if (num < 1)
							{
								num = 1;
							}
							num += SingletonMono<PaiMaiUiMag>.Instance.CurShop.CurPrice;
							tips = PaiMaiCommandTips.DataList[commandId].Text.Replace("{lingshi}", string.Format("{0}", num));
							this._tips.ShowTips(tips);
						});
						PlayerCommand playerCommand = this._commandict[key];
						UnityAction action;
						if ((action = <>9__8) == null)
						{
							action = (<>9__8 = delegate()
							{
								this._tips.Hide();
							});
						}
						playerCommand.AddMouseOutsListener(action);
					}
					else if (this._commandict[key].CommandType == CommandType.策略)
					{
						this._commandict[key].AddClickAction(delegate
						{
							if (this.IsAllQuickEnd)
							{
								return;
							}
							SingletonMono<PaiMaiUiMag>.Instance.PlayerUseCeLue(this._commandict[key]);
						});
						this._commandict[key].AddMouseEnterListener(delegate
						{
							commandId = (int)Enum.Parse(typeof(Command), key, true) + 1;
							tips = PaiMaiCommandTips.DataDict[commandId].Text;
							this._tips.ShowTips(tips);
						});
						PlayerCommand playerCommand2 = this._commandict[key];
						UnityAction action2;
						if ((action2 = <>9__11) == null)
						{
							action2 = (<>9__11 = delegate()
							{
								this._tips.Hide();
							});
						}
						playerCommand2.AddMouseOutsListener(action2);
					}
				}
			}
			this._commandict[Command.放弃竞品.ToString()].AddClickAction(delegate
			{
				if (this.IsAllQuickEnd)
				{
					return;
				}
				this._tips.Hide();
				this.GiveUpCurShop();
			});
			this._commandict[Command.放弃竞品.ToString()].AddMouseEnterListener(delegate
			{
				commandId = (int)Enum.Parse(typeof(Command), Command.放弃竞品.ToString(), true) + 1;
				tips = PaiMaiCommandTips.DataDict[commandId].Text;
				this._tips.ShowTips(tips);
			});
			this._commandict[Command.放弃竞品.ToString()].AddMouseOutsListener(delegate
			{
				this._tips.Hide();
			});
			this._commandict[Command.本轮不出.ToString()].AddClickAction(delegate
			{
				if (this.IsAllQuickEnd)
				{
					return;
				}
				if (this.CanAction)
				{
					this._tips.Hide();
					this.EndAction();
				}
			});
			this._commandict[Command.本轮不出.ToString()].AddMouseEnterListener(delegate
			{
				commandId = (int)Enum.Parse(typeof(Command), Command.本轮不出.ToString(), true) + 1;
				tips = PaiMaiCommandTips.DataDict[commandId].Text;
				this._tips.ShowTips(tips);
			});
			this._commandict[Command.本轮不出.ToString()].AddMouseOutsListener(delegate
			{
				this._tips.Hide();
			});
			this.StopAction();
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x001D75AC File Offset: 0x001D57AC
		public void BuyShop()
		{
			this._player.addItem(SingletonMono<PaiMaiUiMag>.Instance.CurShop.ShopId, SingletonMono<PaiMaiUiMag>.Instance.CurShop.Count, SingletonMono<PaiMaiUiMag>.Instance.CurShop.Seid, false);
			this._player.AddMoney(-SingletonMono<PaiMaiUiMag>.Instance.CurShop.CurPrice);
			this.UpdateMoney();
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x00031306 File Offset: 0x0002F506
		private void UpdateMoney()
		{
			this.Player.Money = (int)this._player.money;
			this._moneyText.text = this._player.money.ToString();
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x0003133A File Offset: 0x0002F53A
		public void MallShop()
		{
			this._player.money += (ulong)((float)SingletonMono<PaiMaiUiMag>.Instance.CurShop.CurPrice * 0.9f);
			this.UpdateMoney();
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x001D7614 File Offset: 0x001D5814
		public void StartAction()
		{
			SingletonMono<PaiMaiUiMag>.Instance.CurAvatar = this.Player;
			for (int i = 1; i <= 8; i++)
			{
				Dictionary<string, PlayerCommand> commandict = this._commandict;
				Command command = (Command)i;
				commandict[command.ToString()].PlayBackTo();
			}
			this.CanAction = true;
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x001D7664 File Offset: 0x001D5864
		public void RestartGiveUpCurShop()
		{
			if (!this._commandict[Command.放弃竞品.ToString()].CanClick)
			{
				this._commandict[Command.放弃竞品.ToString()].PlayBackTo();
			}
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x001D76B4 File Offset: 0x001D58B4
		public void GiveUpCurShop()
		{
			this.IsCurShopQuickEnd = true;
			this._commandict[Command.放弃竞品.ToString()].PlayToBack();
			Time.timeScale = 100f;
			if (this.CanAction)
			{
				this.EndAction();
			}
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x0003136B File Offset: 0x0002F56B
		private void EndAction()
		{
			this.CanAction = false;
			this.StopAction();
			Debug.Log("玩家回合结束");
			SingletonMono<PaiMaiUiMag>.Instance.EndRound();
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x001D7700 File Offset: 0x001D5900
		private void StopAction()
		{
			for (int i = 1; i <= 8; i++)
			{
				Dictionary<string, PlayerCommand> commandict = this._commandict;
				Command command = (Command)i;
				commandict[command.ToString()].PlayToBack();
			}
		}

		// Token: 0x04003CE6 RID: 15590
		public PaiMaiAvatar Player;

		// Token: 0x04003CE7 RID: 15591
		public bool IsCurShopQuickEnd;

		// Token: 0x04003CE8 RID: 15592
		public bool IsAllQuickEnd;

		// Token: 0x04003CE9 RID: 15593
		[SerializeField]
		private readonly Dictionary<string, PlayerCommand> _commandict;

		// Token: 0x04003CEA RID: 15594
		private CommandTips _tips;

		// Token: 0x04003CEB RID: 15595
		private Avatar _player;

		// Token: 0x04003CEC RID: 15596
		private Text _moneyText;

		// Token: 0x04003CED RID: 15597
		public bool CanAction;
	}
}
