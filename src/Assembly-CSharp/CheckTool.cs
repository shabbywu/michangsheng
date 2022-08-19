using System;
using CaiYao;
using Fight;
using Fungus;
using KBEngine;
using UniRx;
using UnityEngine;

// Token: 0x02000186 RID: 390
public class CheckTool : MonoBehaviour
{
	// Token: 0x06001098 RID: 4248 RVA: 0x00061BA8 File Offset: 0x0005FDA8
	public void Init()
	{
		Tools.instance.getPlayer();
		if (Tools.instance.IsCanLoadSetTalk)
		{
			int talk = GlobalValue.GetTalk(0, "CheckTool.Init");
			int talk2 = GlobalValue.GetTalk(1, "CheckTool.Init");
			if (talk > 0)
			{
				DisposableExtensions.AddTo<IDisposable>(ObservableExtensions.Subscribe<long>(Observable.Timer(TimeSpan.FromSeconds(0.4)), delegate(long _)
				{
					this.showTalk();
				}), this);
			}
			else if (talk == 0 && talk2 == 4 && Tools.instance.CanShowFightUI == 1)
			{
				ResManager.inst.LoadPrefab("TaoPaoUI").Inst(null).GetComponent<RunAwayUI>().Show();
				Tools.instance.CanShowFightUI = 0;
			}
			else if (talk == 0 && talk2 == 2 && Tools.instance.CaiYaoData != null)
			{
				ResManager.inst.LoadPrefab("CaiYaoEvent").Inst(null).GetComponent<CaiYaoUIMag>().ShowAfterFight(Tools.instance.CaiYaoData);
			}
		}
		else
		{
			Tools.instance.IsCanLoadSetTalk = true;
		}
		Tools.instance.CaiYaoData = null;
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x00061CB0 File Offset: 0x0005FEB0
	public void setSay(Flowchart flowchart, string block, JSONObject qiecuoInfo, string excel)
	{
		Say say = (Say)flowchart.FindBlock(block).CommandList[0];
		say.SetStandardText(Tools.instance.Code64ToString(qiecuoInfo[excel].str));
		say.pubAvatarIntID = Tools.instance.MonstarID;
	}

	// Token: 0x0600109A RID: 4250 RVA: 0x00061D00 File Offset: 0x0005FF00
	public void showTalk()
	{
		int talk = GlobalValue.GetTalk(0, "CheckTool.showTalk");
		Avatar player = Tools.instance.getPlayer();
		GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + talk));
		if (talk == 505)
		{
			Flowchart component = gameObject.transform.Find("Flowchart").GetComponent<Flowchart>();
			JSONObject jsonobject = jsonData.instance.QieCuoJsonData.list.Find((JSONObject aa) => aa["AvatarID"].I == Tools.instance.MonstarID);
			if (jsonobject != null)
			{
				this.setSay(component, "win", jsonobject, "win");
				this.setSay(component, "lose", jsonobject, "lose");
				if (GlobalValue.GetTalk(1, "CheckTool.showTalk") == 2 && !player.AvatarQieCuo.HasField(Tools.instance.MonstarID.ToString()))
				{
					player.AvatarQieCuo.AddField(Tools.instance.MonstarID.ToString(), 1);
					jsonData.instance.MonstarSetHaoGanDu(Tools.instance.MonstarID, (int)jsonobject["tisheng"].n);
				}
			}
		}
		GlobalValue.SetTalk(0, 0, "CheckTool.showTalk");
		Tools.instance.CanShowFightUI = 0;
	}
}
