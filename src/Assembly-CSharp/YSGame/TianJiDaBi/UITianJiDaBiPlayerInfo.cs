using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000A96 RID: 2710
	public class UITianJiDaBiPlayerInfo : MonoBehaviour, IESCClose
	{
		// Token: 0x06004BDF RID: 19423 RVA: 0x00205585 File Offset: 0x00203785
		private void Awake()
		{
			UITianJiDaBiPlayerInfo.Inst = this;
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x0020558D File Offset: 0x0020378D
		public static void Show(DaBiPlayer player, Match match)
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/TianJiDaBi/UITianJiDaBiPlayerInfo"), NewUICanvas.Inst.Canvas.transform).GetComponent<UITianJiDaBiPlayerInfo>().RefreshUI(player, match);
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x002055BC File Offset: 0x002037BC
		public void RefreshUI(DaBiPlayer player, Match match)
		{
			this.CloseBtn.mouseUpEvent.RemoveAllListeners();
			this.CloseBtn.mouseUpEvent.AddListener(new UnityAction(this.Close));
			ESCCloseManager.Inst.RegisterClose(this);
			this.ShengChangText.text = player.BigScore.ToString();
			this.TianJiDianText.text = player.SmallScore.ToString();
			for (int i = 0; i < 6; i++)
			{
				if (i < player.FightRecords.Count)
				{
					UITianJiDaBi2Player uitianJiDaBi2Player = this.TianJiDaBi2Players[i];
					FightRecord fightRecord = player.FightRecords[i];
					uitianJiDaBi2Player.gameObject.SetActive(true);
					this.NoStarts[i].gameObject.SetActive(false);
					DaBiPlayer daBiPlayer = null;
					for (int j = 0; j < match.PlayerCount; j++)
					{
						if (match.PlayerList[j].ID == fightRecord.TargetID)
						{
							daBiPlayer = match.PlayerList[j];
							break;
						}
					}
					uitianJiDaBi2Player.LeftName.text = player.Name;
					uitianJiDaBi2Player.RightName.text = daBiPlayer.Name;
					if (fightRecord.WinID == fightRecord.MeID)
					{
						uitianJiDaBi2Player.SetLeftWinFail(true, false);
						uitianJiDaBi2Player.SetRightWinFail(false, false);
						uitianJiDaBi2Player.LeftFire.NumberText.text = "胜";
						uitianJiDaBi2Player.RightFire.NumberText.text = "负";
					}
					else
					{
						uitianJiDaBi2Player.SetLeftWinFail(false, false);
						uitianJiDaBi2Player.SetRightWinFail(true, false);
						uitianJiDaBi2Player.LeftFire.NumberText.text = "负";
						uitianJiDaBi2Player.RightFire.NumberText.text = "胜";
					}
				}
				else
				{
					this.TianJiDaBi2Players[i].gameObject.SetActive(false);
					this.NoStarts[i].gameObject.SetActive(true);
				}
			}
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x002057A3 File Offset: 0x002039A3
		public void Close()
		{
			ESCCloseManager.Inst.UnRegisterClose(this);
			UITianJiDaBiPlayerInfo.Inst = null;
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x002057C1 File Offset: 0x002039C1
		bool IESCClose.TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x04004AFC RID: 19196
		public static UITianJiDaBiPlayerInfo Inst;

		// Token: 0x04004AFD RID: 19197
		public Text ShengChangText;

		// Token: 0x04004AFE RID: 19198
		public Text TianJiDianText;

		// Token: 0x04004AFF RID: 19199
		public FpBtn CloseBtn;

		// Token: 0x04004B00 RID: 19200
		public List<UITianJiDaBi2Player> TianJiDaBi2Players;

		// Token: 0x04004B01 RID: 19201
		public List<GameObject> NoStarts;
	}
}
