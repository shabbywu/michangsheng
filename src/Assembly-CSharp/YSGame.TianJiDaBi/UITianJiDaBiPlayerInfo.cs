using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TianJiDaBi;

public class UITianJiDaBiPlayerInfo : MonoBehaviour, IESCClose
{
	public static UITianJiDaBiPlayerInfo Inst;

	public Text ShengChangText;

	public Text TianJiDianText;

	public FpBtn CloseBtn;

	public List<UITianJiDaBi2Player> TianJiDaBi2Players;

	public List<GameObject> NoStarts;

	private void Awake()
	{
		Inst = this;
	}

	public static void Show(DaBiPlayer player, Match match)
	{
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/TianJiDaBi/UITianJiDaBiPlayerInfo"), ((Component)NewUICanvas.Inst.Canvas).transform).GetComponent<UITianJiDaBiPlayerInfo>().RefreshUI(player, match);
	}

	public void RefreshUI(DaBiPlayer player, Match match)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		((UnityEventBase)CloseBtn.mouseUpEvent).RemoveAllListeners();
		CloseBtn.mouseUpEvent.AddListener(new UnityAction(Close));
		ESCCloseManager.Inst.RegisterClose(this);
		ShengChangText.text = player.BigScore.ToString();
		TianJiDianText.text = player.SmallScore.ToString();
		for (int i = 0; i < 6; i++)
		{
			if (i < player.FightRecords.Count)
			{
				UITianJiDaBi2Player uITianJiDaBi2Player = TianJiDaBi2Players[i];
				FightRecord fightRecord = player.FightRecords[i];
				((Component)uITianJiDaBi2Player).gameObject.SetActive(true);
				NoStarts[i].gameObject.SetActive(false);
				DaBiPlayer daBiPlayer = null;
				for (int j = 0; j < match.PlayerCount; j++)
				{
					if (match.PlayerList[j].ID == fightRecord.TargetID)
					{
						daBiPlayer = match.PlayerList[j];
						break;
					}
				}
				uITianJiDaBi2Player.LeftName.text = player.Name;
				uITianJiDaBi2Player.RightName.text = daBiPlayer.Name;
				if (fightRecord.WinID == fightRecord.MeID)
				{
					uITianJiDaBi2Player.SetLeftWinFail(win: true, showText: false);
					uITianJiDaBi2Player.SetRightWinFail(win: false, showText: false);
					uITianJiDaBi2Player.LeftFire.NumberText.text = "胜";
					uITianJiDaBi2Player.RightFire.NumberText.text = "负";
				}
				else
				{
					uITianJiDaBi2Player.SetLeftWinFail(win: false, showText: false);
					uITianJiDaBi2Player.SetRightWinFail(win: true, showText: false);
					uITianJiDaBi2Player.LeftFire.NumberText.text = "负";
					uITianJiDaBi2Player.RightFire.NumberText.text = "胜";
				}
			}
			else
			{
				((Component)TianJiDaBi2Players[i]).gameObject.SetActive(false);
				NoStarts[i].gameObject.SetActive(true);
			}
		}
	}

	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		Inst = null;
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	bool IESCClose.TryEscClose()
	{
		Close();
		return true;
	}
}
