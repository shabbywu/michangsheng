using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaiMai
{
	// Token: 0x0200071C RID: 1820
	[Serializable]
	public class PlayerCtr
	{
		// Token: 0x06003A38 RID: 14904 RVA: 0x0018FDC4 File Offset: 0x0018DFC4
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

		// Token: 0x06003A39 RID: 14905 RVA: 0x0018FE28 File Offset: 0x0018E028
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

		// Token: 0x06003A3A RID: 14906 RVA: 0x00190118 File Offset: 0x0018E318
		public void BuyShop()
		{
			this._player.addItem(SingletonMono<PaiMaiUiMag>.Instance.CurShop.ShopId, SingletonMono<PaiMaiUiMag>.Instance.CurShop.Count, SingletonMono<PaiMaiUiMag>.Instance.CurShop.Seid, false);
			this._player.AddMoney(-SingletonMono<PaiMaiUiMag>.Instance.CurShop.CurPrice);
			this.UpdateMoney();
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x0019017F File Offset: 0x0018E37F
		private void UpdateMoney()
		{
			this.Player.Money = (int)this._player.money;
			this._moneyText.text = this._player.money.ToString();
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x001901B3 File Offset: 0x0018E3B3
		public void MallShop()
		{
			this._player.money += (ulong)((float)SingletonMono<PaiMaiUiMag>.Instance.CurShop.CurPrice * 0.9f);
			this.UpdateMoney();
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x001901E4 File Offset: 0x0018E3E4
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

		// Token: 0x06003A3E RID: 14910 RVA: 0x00190234 File Offset: 0x0018E434
		public void RestartGiveUpCurShop()
		{
			if (!this._commandict[Command.放弃竞品.ToString()].CanClick)
			{
				this._commandict[Command.放弃竞品.ToString()].PlayBackTo();
			}
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x00190284 File Offset: 0x0018E484
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

		// Token: 0x06003A40 RID: 14912 RVA: 0x001902CF File Offset: 0x0018E4CF
		private void EndAction()
		{
			this.CanAction = false;
			this.StopAction();
			Debug.Log("玩家回合结束");
			SingletonMono<PaiMaiUiMag>.Instance.EndRound();
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x001902F4 File Offset: 0x0018E4F4
		private void StopAction()
		{
			for (int i = 1; i <= 8; i++)
			{
				Dictionary<string, PlayerCommand> commandict = this._commandict;
				Command command = (Command)i;
				commandict[command.ToString()].PlayToBack();
			}
		}

		// Token: 0x04003255 RID: 12885
		public PaiMaiAvatar Player;

		// Token: 0x04003256 RID: 12886
		public bool IsCurShopQuickEnd;

		// Token: 0x04003257 RID: 12887
		public bool IsAllQuickEnd;

		// Token: 0x04003258 RID: 12888
		[SerializeField]
		private readonly Dictionary<string, PlayerCommand> _commandict;

		// Token: 0x04003259 RID: 12889
		private CommandTips _tips;

		// Token: 0x0400325A RID: 12890
		private Avatar _player;

		// Token: 0x0400325B RID: 12891
		private Text _moneyText;

		// Token: 0x0400325C RID: 12892
		public bool CanAction;
	}
}
