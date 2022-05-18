using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000DCB RID: 3531
	public class UITianJiDaBiRankPanel : MonoBehaviour, IESCClose
	{
		// Token: 0x0600550E RID: 21774 RVA: 0x0003CCD6 File Offset: 0x0003AED6
		private void Awake()
		{
			UITianJiDaBiRankPanel.Inst = this;
		}

		// Token: 0x0600550F RID: 21775 RVA: 0x00236B2C File Offset: 0x00234D2C
		public static void Show(Command cmd = null)
		{
			UITianJiDaBiRankPanel component = Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/TianJiDaBi/UITianJiDaBiRankPanel"), NewUICanvas.Inst.Canvas.transform).GetComponent<UITianJiDaBiRankPanel>();
			ESCCloseManager.Inst.RegisterClose(component);
			component.callCmd = cmd;
			component.RefreshUI();
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x00236B78 File Offset: 0x00234D78
		public void RefreshUI()
		{
			this.CloseBtn.mouseUpEvent.RemoveAllListeners();
			this.CloseBtn.mouseUpEvent.AddListener(new UnityAction(this.Close));
			this.NameList = new List<Text>();
			this.TitleList = new List<Text>();
			for (int i = 0; i < this.RankSV.childCount; i++)
			{
				Transform child = this.RankSV.GetChild(i);
				Text component = child.GetChild(0).GetComponent<Text>();
				Text component2 = child.GetChild(1).GetComponent<Text>();
				this.NameList.Add(component);
				this.TitleList.Add(component2);
			}
			TianJiDaBiSaveData tianJiDaBiSaveData = PlayerEx.Player.StreamData.TianJiDaBiSaveData;
			if (tianJiDaBiSaveData.LastMatch == null)
			{
				TianJiDaBiManager.OnAddTime();
			}
			Match lastMatch = tianJiDaBiSaveData.LastMatch;
			for (int j = 0; j < 10; j++)
			{
				DaBiPlayer daBiPlayer = lastMatch.PlayerList[j];
				this.NameList[j].text = daBiPlayer.Name;
				this.TitleList[j].text = daBiPlayer.Title;
			}
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x0003CCDE File Offset: 0x0003AEDE
		public void Close()
		{
			ESCCloseManager.Inst.UnRegisterClose(this);
			if (this.callCmd != null)
			{
				this.callCmd.Continue();
			}
			UITianJiDaBiRankPanel.Inst = null;
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x0003CD15 File Offset: 0x0003AF15
		bool IESCClose.TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x040054C7 RID: 21703
		public static UITianJiDaBiRankPanel Inst;

		// Token: 0x040054C8 RID: 21704
		public FpBtn CloseBtn;

		// Token: 0x040054C9 RID: 21705
		public RectTransform RankSV;

		// Token: 0x040054CA RID: 21706
		private List<Text> NameList;

		// Token: 0x040054CB RID: 21707
		private List<Text> TitleList;

		// Token: 0x040054CC RID: 21708
		private Command callCmd;
	}
}
