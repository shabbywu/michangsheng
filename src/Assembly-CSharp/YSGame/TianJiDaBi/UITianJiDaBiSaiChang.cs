using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fungus;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000A98 RID: 2712
	public class UITianJiDaBiSaiChang : MonoBehaviour, IESCClose
	{
		// Token: 0x06004BEB RID: 19435 RVA: 0x00205976 File Offset: 0x00203B76
		private void Awake()
		{
			UITianJiDaBiSaiChang.Inst = this;
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x00205980 File Offset: 0x00203B80
		public void Init()
		{
			if (!this.inited)
			{
				this.inited = true;
				this.TianJiDaBi2Players = new List<UITianJiDaBi2Player>();
				int childCount = this.PlayersRT.childCount;
				for (int i = 0; i < childCount; i++)
				{
					UITianJiDaBi2Player component = this.PlayersRT.GetChild(i).GetComponent<UITianJiDaBi2Player>();
					this.TianJiDaBi2Players.Add(component);
				}
			}
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x002059E0 File Offset: 0x00203BE0
		public static void ShowNormal()
		{
			UITianJiDaBiSaiChang saiChang = UITianJiDaBiSaiChang.LoadUI();
			saiChang.BottomBtnsRT.gameObject.SetActive(true);
			Avatar player = PlayerEx.Player;
			Match match = TianJiDaBiManager.GetNowMatch();
			int num = match.RoundIndex + 1;
			GlobalValue.Set(1410, num, "UITianJiDaBiSaiChang.ShowNormal 天机大比当前轮次");
			saiChang.RoundText.text = "第" + num.ToCNNumber() + "轮";
			for (int i = 0; i < match.PlayerCount; i += 2)
			{
				DaBiPlayer daBiPlayer = match.PlayerList[i];
				DaBiPlayer daBiPlayer2 = match.PlayerList[i + 1];
				saiChang.TianJiDaBi2Players[i / 2].InitData(daBiPlayer, daBiPlayer2, match);
				if (daBiPlayer.IsWanJia)
				{
					GlobalValue.Set(1411, daBiPlayer2.ID, "UITianJiDaBiSaiChang.ShowNormal 天机大比当前对手ID");
				}
				else if (daBiPlayer2.IsWanJia)
				{
					GlobalValue.Set(1411, daBiPlayer.ID, "UITianJiDaBiSaiChang.ShowNormal 天机大比当前对手ID");
				}
			}
			saiChang.BtnBiShi.mouseUpEvent.AddListener(delegate()
			{
				GlobalValue.SetTalk(0, 4401, "UITianJiDaBiSaiChang.ShowNormal 比赛开始前talk");
				saiChang.Close();
				Tools.instance.loadMapScenes("S1215", true);
			});
			UnityAction <>9__2;
			saiChang.BtnQiQuan.mouseUpEvent.AddListener(delegate()
			{
				string text = "确定放弃继续比赛吗？";
				UnityAction onOK;
				if ((onOK = <>9__2) == null)
				{
					onOK = (<>9__2 = delegate()
					{
						match.PlayerAbandon = true;
						GlobalValue.SetTalk(0, 4403, "UITianJiDaBiSaiChang.ShowNormal 玩家弃权后talk");
						for (int j = match.RoundIndex; j < 6; j++)
						{
							match.NewRound();
							match.AfterRound();
							NpcJieSuanManager.inst.IsNoJieSuan = true;
							PlayerEx.Player.AddTime(1, 0, 0);
						}
						TianJiDaBiManager.AddLastMatchData(match);
						TianJiDaBiManager.AddMatchPlayerEvent(match, true);
						TianJiDaBiManager.SendRewardToNPC(match);
						saiChang.Close();
						Tools.instance.loadMapScenes("S1215", true);
					});
				}
				USelectBox.Show(text, onOK, null);
			});
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x00205B4C File Offset: 0x00203D4C
		public static void ShowOneRoundSim(Command cmd)
		{
			Match match = TianJiDaBiManager.GetNowMatch();
			UITianJiDaBiSaiChang saiChang = UITianJiDaBiSaiChang.LoadUI();
			saiChang.CanClose = false;
			saiChang.CloseAction = delegate()
			{
				cmd.Continue();
			};
			saiChang.FightingText.gameObject.SetActive(true);
			Avatar player = PlayerEx.Player;
			int num = match.RoundIndex + 1;
			GlobalValue.Set(1410, num, "UITianJiDaBiSaiChang.ShowOneRoundSim 天机大比当前轮次");
			saiChang.RoundText.text = "第" + num.ToCNNumber() + "轮";
			for (int i = 0; i < match.PlayerCount; i += 2)
			{
				DaBiPlayer daBiPlayer = match.PlayerList[i];
				DaBiPlayer daBiPlayer2 = match.PlayerList[i + 1];
				saiChang.TianJiDaBi2Players[i / 2].InitData(daBiPlayer, daBiPlayer2, match);
				saiChang.TianJiDaBi2Players[i / 2].JianAnimCtl.Play("UITianJiDaBiJianEndAnim");
				if (daBiPlayer.IsWanJia || daBiPlayer2.IsWanJia)
				{
					saiChang.TianJiDaBi2Players[i / 2].SetWinFail(daBiPlayer, daBiPlayer2);
				}
			}
			match.NewRound();
			float v = 0f;
			for (int j = 0; j < match.PlayerCount; j += 2)
			{
				DaBiPlayer left = match.PlayerList[j];
				DaBiPlayer right = match.PlayerList[j + 1];
				UITianJiDaBi2Player tianJiDaBi2Player = saiChang.TianJiDaBi2Players[j / 2];
				tianJiDaBi2Player.PlayFightAnim(delegate
				{
					tianJiDaBi2Player.InitData(left, right, match);
					tianJiDaBi2Player.SetWinFail(left, right);
				});
			}
			DOTween.To(() => v, delegate(float x)
			{
				v = x;
			}, 1f, 3f).onComplete = delegate()
			{
				saiChang.FightingText.gameObject.SetActive(false);
				match.AfterRound();
				if (match.RoundIndex >= 6)
				{
					TianJiDaBiManager.AddLastMatchData(match);
					TianJiDaBiManager.AddMatchPlayerEvent(match, true);
					TianJiDaBiManager.SendRewardToNPC(match);
				}
				saiChang.CanClose = true;
			};
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x00205DA8 File Offset: 0x00203FA8
		public static void ShowAllRoundSim(Command cmd)
		{
			UITianJiDaBiSaiChang uitianJiDaBiSaiChang = UITianJiDaBiSaiChang.LoadUI();
			uitianJiDaBiSaiChang.CloseAction = delegate()
			{
				cmd.Continue();
			};
			Match nowMatch = TianJiDaBiManager.GetNowMatch();
			for (int i = 0; i < 5; i++)
			{
				nowMatch.NewRound();
				nowMatch.AfterRound();
			}
			nowMatch.NewRound();
			for (int j = 0; j < nowMatch.PlayerCount; j += 2)
			{
				DaBiPlayer left = nowMatch.PlayerList[j];
				DaBiPlayer right = nowMatch.PlayerList[j + 1];
				uitianJiDaBiSaiChang.TianJiDaBi2Players[j / 2].InitData(left, right, nowMatch);
				uitianJiDaBiSaiChang.TianJiDaBi2Players[j / 2].SetWinFail(left, right);
			}
			nowMatch.AfterRound();
			TianJiDaBiManager.AddLastMatchData(nowMatch);
			TianJiDaBiManager.AddMatchPlayerEvent(nowMatch, true);
			TianJiDaBiManager.SendRewardToNPC(nowMatch);
			uitianJiDaBiSaiChang.RoundText.text = "第" + nowMatch.RoundIndex.ToCNNumber() + "轮";
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x00205EA2 File Offset: 0x002040A2
		public IEnumerator ShowAllRoundC()
		{
			Match match = TianJiDaBiManager.GetNowMatch();
			this.RoundText.text = "第" + (match.RoundIndex + 1).ToCNNumber() + "轮";
			Debug.Log(string.Format("开始第{0}轮模拟", match.RoundIndex + 1));
			UIPopTip.Inst.Pop(string.Format("第{0}轮开始", match.RoundIndex + 1), PopTipIconType.叹号);
			this.FightingText.gameObject.SetActive(true);
			for (int i = 0; i < match.PlayerCount; i += 2)
			{
				DaBiPlayer left = match.PlayerList[i];
				DaBiPlayer right = match.PlayerList[i + 1];
				this.TianJiDaBi2Players[i / 2].InitData(left, right, match);
			}
			match.NewRound();
			float v = 0f;
			DOTween.To(() => v, delegate(float x)
			{
				v = x;
			}, 1f, 3f).onComplete = delegate()
			{
				for (int j = 0; j < match.PlayerCount; j += 2)
				{
					DaBiPlayer left2 = match.PlayerList[j];
					DaBiPlayer right2 = match.PlayerList[j + 1];
					this.TianJiDaBi2Players[j / 2].InitData(left2, right2, match);
					this.TianJiDaBi2Players[j / 2].SetWinFail(left2, right2);
				}
				this.FightingText.gameObject.SetActive(false);
				match.AfterRound();
				NpcJieSuanManager.inst.IsNoJieSuan = true;
				PlayerEx.Player.AddTime(1, 0, 0);
			};
			yield return new WaitForSeconds(6f);
			if (match.RoundIndex >= 6)
			{
				UIPopTip.Inst.Pop("全部比完，可以走了", PopTipIconType.叹号);
				this.CanClose = true;
				TianJiDaBiManager.AddMatchPlayerEvent(match, true);
				TianJiDaBiManager.SendRewardToNPC(match);
				yield break;
			}
			yield return this.ShowAllRoundC();
			yield break;
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x00205EB4 File Offset: 0x002040B4
		private static UITianJiDaBiSaiChang LoadUI()
		{
			UITianJiDaBiSaiChang component = Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/TianJiDaBi/UITianJiDaBiSaiChang"), NewUICanvas.Inst.Canvas.transform).GetComponent<UITianJiDaBiSaiChang>();
			component.Init();
			component.BtnClose.mouseUpEvent.AddListener(new UnityAction(component.Close));
			ESCCloseManager.Inst.RegisterClose(component);
			return component;
		}

		// Token: 0x06004BF2 RID: 19442 RVA: 0x00205F13 File Offset: 0x00204113
		public void Close()
		{
			if (!this.CanClose)
			{
				return;
			}
			if (this.CloseAction != null)
			{
				this.CloseAction();
			}
			ESCCloseManager.Inst.UnRegisterClose(this);
			UITianJiDaBiSaiChang.Inst = null;
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x00205F4D File Offset: 0x0020414D
		bool IESCClose.TryEscClose()
		{
			if (this.CanClose)
			{
				this.Close();
				return true;
			}
			return false;
		}

		// Token: 0x04004B08 RID: 19208
		public static UITianJiDaBiSaiChang Inst;

		// Token: 0x04004B09 RID: 19209
		public RectTransform PlayersRT;

		// Token: 0x04004B0A RID: 19210
		public RectTransform BottomBtnsRT;

		// Token: 0x04004B0B RID: 19211
		public FpBtn BtnBiShi;

		// Token: 0x04004B0C RID: 19212
		public FpBtn BtnQiQuan;

		// Token: 0x04004B0D RID: 19213
		public FpBtn BtnClose;

		// Token: 0x04004B0E RID: 19214
		public GameObject FightingText;

		// Token: 0x04004B0F RID: 19215
		public Text RoundText;

		// Token: 0x04004B10 RID: 19216
		private bool inited;

		// Token: 0x04004B11 RID: 19217
		[HideInInspector]
		public List<UITianJiDaBi2Player> TianJiDaBi2Players;

		// Token: 0x04004B12 RID: 19218
		private Action CloseAction;

		// Token: 0x04004B13 RID: 19219
		[HideInInspector]
		public bool CanClose = true;
	}
}
