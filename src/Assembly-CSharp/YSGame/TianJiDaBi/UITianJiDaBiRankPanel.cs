using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000A97 RID: 2711
	public class UITianJiDaBiRankPanel : MonoBehaviour, IESCClose
	{
		// Token: 0x06004BE5 RID: 19429 RVA: 0x002057CA File Offset: 0x002039CA
		private void Awake()
		{
			UITianJiDaBiRankPanel.Inst = this;
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x002057D4 File Offset: 0x002039D4
		public static void Show(Command cmd = null)
		{
			UITianJiDaBiRankPanel component = Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/TianJiDaBi/UITianJiDaBiRankPanel"), NewUICanvas.Inst.Canvas.transform).GetComponent<UITianJiDaBiRankPanel>();
			ESCCloseManager.Inst.RegisterClose(component);
			component.callCmd = cmd;
			component.RefreshUI();
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x00205820 File Offset: 0x00203A20
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

		// Token: 0x06004BE8 RID: 19432 RVA: 0x00205936 File Offset: 0x00203B36
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

		// Token: 0x06004BE9 RID: 19433 RVA: 0x0020596D File Offset: 0x00203B6D
		bool IESCClose.TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x04004B02 RID: 19202
		public static UITianJiDaBiRankPanel Inst;

		// Token: 0x04004B03 RID: 19203
		public FpBtn CloseBtn;

		// Token: 0x04004B04 RID: 19204
		public RectTransform RankSV;

		// Token: 0x04004B05 RID: 19205
		private List<Text> NameList;

		// Token: 0x04004B06 RID: 19206
		private List<Text> TitleList;

		// Token: 0x04004B07 RID: 19207
		private Command callCmd;
	}
}
