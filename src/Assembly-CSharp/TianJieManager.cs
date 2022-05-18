using System;
using KBEngine;
using UnityEngine;
using YSGame;

// Token: 0x02000653 RID: 1619
public class TianJieManager : MonoBehaviour
{
	// Token: 0x0600283E RID: 10302 RVA: 0x0013B248 File Offset: 0x00139448
	public static void StartTianJieCD()
	{
		Avatar player = PlayerEx.Player;
		player.TianJie.SetField("HuaShenTime", player.worldTimeMag.nowTime);
		player.TianJie.SetField("JiaSuYear", 0);
		TianJieManager.OnAddTime();
		player.TianJie.SetField("ShowTianJieCD", true);
	}

	// Token: 0x0600283F RID: 10303 RVA: 0x0013B2A0 File Offset: 0x001394A0
	public static void TianJieJiaSu(int year)
	{
		int i = PlayerEx.Player.TianJie["JiaSuYear"].I;
		PlayerEx.Player.TianJie.SetField("JiaSuYear", i + year);
		TianJieManager.OnAddTime();
	}

	// Token: 0x06002840 RID: 10304 RVA: 0x0013B2E4 File Offset: 0x001394E4
	public static void OnAddTime()
	{
		Avatar player = PlayerEx.Player;
		if (player != null)
		{
			DateTime nowTime = player.worldTimeMag.getNowTime();
			DateTime t = DateTime.Parse(player.TianJie["HuaShenTime"].str).AddYears(1000 - player.TianJie["JiaSuYear"].I);
			if (nowTime >= t)
			{
				player.TianJie.SetField("ShengYuTime", "0年0月0日");
				player.TianJie.SetField("ShowTianJieCD", false);
				Debug.Log("开始天劫");
				ESCCloseManager.Inst.CloseAll();
				UCheckBox.Show("后续内容正在开发中，尚未开放\n确定后将返回主界面", delegate
				{
					YSSaveGame.Reset();
					KBEngineApp.app.entities[10] = null;
					KBEngineApp.app.entities.Remove(10);
					Tools.instance.loadOtherScenes("MainMenu");
				});
				return;
			}
			int num = (int)(t.Subtract(nowTime).TotalDays / 365.0);
			player.TianJie.SetField("ShengYuTimeValue", num);
			if (num > 0)
			{
				player.TianJie.SetField("ShengYuTime", string.Format("{0}年", num));
				return;
			}
			player.TianJie.SetField("ShengYuTime", "不足1年");
		}
	}
}
