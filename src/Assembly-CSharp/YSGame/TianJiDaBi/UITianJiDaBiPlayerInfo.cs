using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000DCA RID: 3530
	public class UITianJiDaBiPlayerInfo : MonoBehaviour, IESCClose
	{
		// Token: 0x06005508 RID: 21768 RVA: 0x0003CC7B File Offset: 0x0003AE7B
		private void Awake()
		{
			UITianJiDaBiPlayerInfo.Inst = this;
		}

		// Token: 0x06005509 RID: 21769 RVA: 0x0003CC83 File Offset: 0x0003AE83
		public static void Show(DaBiPlayer player, Match match)
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/TianJiDaBi/UITianJiDaBiPlayerInfo"), NewUICanvas.Inst.Canvas.transform).GetComponent<UITianJiDaBiPlayerInfo>().RefreshUI(player, match);
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x00236944 File Offset: 0x00234B44
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

		// Token: 0x0600550B RID: 21771 RVA: 0x0003CCAF File Offset: 0x0003AEAF
		public void Close()
		{
			ESCCloseManager.Inst.UnRegisterClose(this);
			UITianJiDaBiPlayerInfo.Inst = null;
			Object.Destroy(base.gameObject);
		}

		// Token: 0x0600550C RID: 21772 RVA: 0x0003CCCD File Offset: 0x0003AECD
		bool IESCClose.TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x040054C1 RID: 21697
		public static UITianJiDaBiPlayerInfo Inst;

		// Token: 0x040054C2 RID: 21698
		public Text ShengChangText;

		// Token: 0x040054C3 RID: 21699
		public Text TianJiDianText;

		// Token: 0x040054C4 RID: 21700
		public FpBtn CloseBtn;

		// Token: 0x040054C5 RID: 21701
		public List<UITianJiDaBi2Player> TianJiDaBi2Players;

		// Token: 0x040054C6 RID: 21702
		public List<GameObject> NoStarts;
	}
}
