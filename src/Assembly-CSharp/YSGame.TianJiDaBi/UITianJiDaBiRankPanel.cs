using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TianJiDaBi;

public class UITianJiDaBiRankPanel : MonoBehaviour, IESCClose
{
	public static UITianJiDaBiRankPanel Inst;

	public FpBtn CloseBtn;

	public RectTransform RankSV;

	private List<Text> NameList;

	private List<Text> TitleList;

	private Command callCmd;

	private void Awake()
	{
		Inst = this;
	}

	public static void Show(Command cmd = null)
	{
		UITianJiDaBiRankPanel component = Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/TianJiDaBi/UITianJiDaBiRankPanel"), ((Component)NewUICanvas.Inst.Canvas).transform).GetComponent<UITianJiDaBiRankPanel>();
		ESCCloseManager.Inst.RegisterClose(component);
		component.callCmd = cmd;
		component.RefreshUI();
	}

	public void RefreshUI()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		((UnityEventBase)CloseBtn.mouseUpEvent).RemoveAllListeners();
		CloseBtn.mouseUpEvent.AddListener(new UnityAction(Close));
		NameList = new List<Text>();
		TitleList = new List<Text>();
		for (int i = 0; i < ((Transform)RankSV).childCount; i++)
		{
			Transform child = ((Transform)RankSV).GetChild(i);
			Text component = ((Component)child.GetChild(0)).GetComponent<Text>();
			Text component2 = ((Component)child.GetChild(1)).GetComponent<Text>();
			NameList.Add(component);
			TitleList.Add(component2);
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
			NameList[j].text = daBiPlayer.Name;
			TitleList[j].text = daBiPlayer.Title;
		}
	}

	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		if ((Object)(object)callCmd != (Object)null)
		{
			callCmd.Continue();
		}
		Inst = null;
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	bool IESCClose.TryEscClose()
	{
		Close();
		return true;
	}
}
