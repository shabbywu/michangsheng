using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using Fungus;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TianJiDaBi;

public class UITianJiDaBiSaiChang : MonoBehaviour, IESCClose
{
	public static UITianJiDaBiSaiChang Inst;

	public RectTransform PlayersRT;

	public RectTransform BottomBtnsRT;

	public FpBtn BtnBiShi;

	public FpBtn BtnQiQuan;

	public FpBtn BtnClose;

	public GameObject FightingText;

	public Text RoundText;

	private bool inited;

	[HideInInspector]
	public List<UITianJiDaBi2Player> TianJiDaBi2Players;

	private Action CloseAction;

	[HideInInspector]
	public bool CanClose = true;

	private void Awake()
	{
		Inst = this;
	}

	public void Init()
	{
		if (!inited)
		{
			inited = true;
			TianJiDaBi2Players = new List<UITianJiDaBi2Player>();
			int childCount = ((Transform)PlayersRT).childCount;
			for (int i = 0; i < childCount; i++)
			{
				UITianJiDaBi2Player component = ((Component)((Transform)PlayersRT).GetChild(i)).GetComponent<UITianJiDaBi2Player>();
				TianJiDaBi2Players.Add(component);
			}
		}
	}

	public static void ShowNormal()
	{
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Expected O, but got Unknown
		UITianJiDaBiSaiChang saiChang = LoadUI();
		((Component)saiChang.BottomBtnsRT).gameObject.SetActive(true);
		_ = PlayerEx.Player;
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
		saiChang.BtnBiShi.mouseUpEvent.AddListener((UnityAction)delegate
		{
			GlobalValue.SetTalk(0, 4401, "UITianJiDaBiSaiChang.ShowNormal 比赛开始前talk");
			saiChang.Close();
			Tools.instance.loadMapScenes("S1215");
		});
		UnityAction val = default(UnityAction);
		saiChang.BtnQiQuan.mouseUpEvent.AddListener((UnityAction)delegate
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Expected O, but got Unknown
			//IL_0023: Expected O, but got Unknown
			UnityAction obj = val;
			if (obj == null)
			{
				UnityAction val2 = delegate
				{
					match.PlayerAbandon = true;
					GlobalValue.SetTalk(0, 4403, "UITianJiDaBiSaiChang.ShowNormal 玩家弃权后talk");
					for (int j = match.RoundIndex; j < 6; j++)
					{
						match.NewRound();
						match.AfterRound();
						NpcJieSuanManager.inst.IsNoJieSuan = true;
						PlayerEx.Player.AddTime(1);
					}
					TianJiDaBiManager.AddLastMatchData(match);
					TianJiDaBiManager.AddMatchPlayerEvent(match, isNowTime: true);
					TianJiDaBiManager.SendRewardToNPC(match);
					saiChang.Close();
					Tools.instance.loadMapScenes("S1215");
				};
				UnityAction val3 = val2;
				val = val2;
				obj = val3;
			}
			USelectBox.Show("确定放弃继续比赛吗？", obj);
		});
	}

	public static void ShowOneRoundSim(Command cmd)
	{
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Expected O, but got Unknown
		Match match = TianJiDaBiManager.GetNowMatch();
		UITianJiDaBiSaiChang saiChang = LoadUI();
		saiChang.CanClose = false;
		saiChang.CloseAction = delegate
		{
			cmd.Continue();
		};
		saiChang.FightingText.gameObject.SetActive(true);
		_ = PlayerEx.Player;
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
		((Tween)DOTween.To((DOGetter<float>)(() => v), (DOSetter<float>)delegate(float x)
		{
			v = x;
		}, 1f, 3f)).onComplete = (TweenCallback)delegate
		{
			saiChang.FightingText.gameObject.SetActive(false);
			match.AfterRound();
			if (match.RoundIndex >= 6)
			{
				TianJiDaBiManager.AddLastMatchData(match);
				TianJiDaBiManager.AddMatchPlayerEvent(match, isNowTime: true);
				TianJiDaBiManager.SendRewardToNPC(match);
			}
			saiChang.CanClose = true;
		};
	}

	public static void ShowAllRoundSim(Command cmd)
	{
		UITianJiDaBiSaiChang uITianJiDaBiSaiChang = LoadUI();
		uITianJiDaBiSaiChang.CloseAction = delegate
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
			uITianJiDaBiSaiChang.TianJiDaBi2Players[j / 2].InitData(left, right, nowMatch);
			uITianJiDaBiSaiChang.TianJiDaBi2Players[j / 2].SetWinFail(left, right);
		}
		nowMatch.AfterRound();
		TianJiDaBiManager.AddLastMatchData(nowMatch);
		TianJiDaBiManager.AddMatchPlayerEvent(nowMatch, isNowTime: true);
		TianJiDaBiManager.SendRewardToNPC(nowMatch);
		uITianJiDaBiSaiChang.RoundText.text = "第" + nowMatch.RoundIndex.ToCNNumber() + "轮";
	}

	public IEnumerator ShowAllRoundC()
	{
		Match match = TianJiDaBiManager.GetNowMatch();
		RoundText.text = "第" + (match.RoundIndex + 1).ToCNNumber() + "轮";
		Debug.Log((object)$"开始第{match.RoundIndex + 1}轮模拟");
		UIPopTip.Inst.Pop($"第{match.RoundIndex + 1}轮开始");
		FightingText.gameObject.SetActive(true);
		for (int i = 0; i < match.PlayerCount; i += 2)
		{
			DaBiPlayer left = match.PlayerList[i];
			DaBiPlayer right = match.PlayerList[i + 1];
			TianJiDaBi2Players[i / 2].InitData(left, right, match);
		}
		match.NewRound();
		float v = 0f;
		((Tween)DOTween.To((DOGetter<float>)(() => v), (DOSetter<float>)delegate(float x)
		{
			v = x;
		}, 1f, 3f)).onComplete = (TweenCallback)delegate
		{
			for (int j = 0; j < match.PlayerCount; j += 2)
			{
				DaBiPlayer left2 = match.PlayerList[j];
				DaBiPlayer right2 = match.PlayerList[j + 1];
				TianJiDaBi2Players[j / 2].InitData(left2, right2, match);
				TianJiDaBi2Players[j / 2].SetWinFail(left2, right2);
			}
			FightingText.gameObject.SetActive(false);
			match.AfterRound();
			NpcJieSuanManager.inst.IsNoJieSuan = true;
			PlayerEx.Player.AddTime(1);
		};
		yield return (object)new WaitForSeconds(6f);
		if (match.RoundIndex >= 6)
		{
			UIPopTip.Inst.Pop("全部比完，可以走了");
			CanClose = true;
			TianJiDaBiManager.AddMatchPlayerEvent(match, isNowTime: true);
			TianJiDaBiManager.SendRewardToNPC(match);
		}
		else
		{
			yield return ShowAllRoundC();
		}
	}

	private static UITianJiDaBiSaiChang LoadUI()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		UITianJiDaBiSaiChang component = Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/TianJiDaBi/UITianJiDaBiSaiChang"), ((Component)NewUICanvas.Inst.Canvas).transform).GetComponent<UITianJiDaBiSaiChang>();
		component.Init();
		component.BtnClose.mouseUpEvent.AddListener(new UnityAction(component.Close));
		ESCCloseManager.Inst.RegisterClose(component);
		return component;
	}

	public void Close()
	{
		if (CanClose)
		{
			if (CloseAction != null)
			{
				CloseAction();
			}
			ESCCloseManager.Inst.UnRegisterClose(this);
			Inst = null;
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
	}

	bool IESCClose.TryEscClose()
	{
		if (CanClose)
		{
			Close();
			return true;
		}
		return false;
	}
}
