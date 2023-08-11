using System;
using CaiYao;
using Fight;
using Fungus;
using KBEngine;
using UniRx;
using UnityEngine;

public class CheckTool : MonoBehaviour
{
	public void Init()
	{
		Tools.instance.getPlayer();
		if (Tools.instance.IsCanLoadSetTalk)
		{
			int talk = GlobalValue.GetTalk(0, "CheckTool.Init");
			int talk2 = GlobalValue.GetTalk(1, "CheckTool.Init");
			if (talk > 0)
			{
				DisposableExtensions.AddTo<IDisposable>(ObservableExtensions.Subscribe<long>(Observable.Timer(TimeSpan.FromSeconds(0.4)), (Action<long>)delegate
				{
					showTalk();
				}), (Component)(object)this);
			}
			else if (talk == 0 && talk2 == 4 && Tools.instance.CanShowFightUI == 1)
			{
				ResManager.inst.LoadPrefab("TaoPaoUI").Inst().GetComponent<RunAwayUI>()
					.Show();
				Tools.instance.CanShowFightUI = 0;
			}
			else if (talk == 0 && talk2 == 2 && Tools.instance.CaiYaoData != null)
			{
				ResManager.inst.LoadPrefab("CaiYaoEvent").Inst().GetComponent<CaiYaoUIMag>()
					.ShowAfterFight(Tools.instance.CaiYaoData);
			}
		}
		else
		{
			Tools.instance.IsCanLoadSetTalk = true;
		}
		Tools.instance.CaiYaoData = null;
	}

	public void setSay(Flowchart flowchart, string block, JSONObject qiecuoInfo, string excel)
	{
		Say obj = (Say)flowchart.FindBlock(block).CommandList[0];
		obj.SetStandardText(Tools.instance.Code64ToString(qiecuoInfo[excel].str));
		obj.pubAvatarIntID = Tools.instance.MonstarID;
	}

	public void showTalk()
	{
		int talk = GlobalValue.GetTalk(0, "CheckTool.showTalk");
		Avatar player = Tools.instance.getPlayer();
		GameObject val = Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + talk));
		if (talk == 505)
		{
			Flowchart component = ((Component)val.transform.Find("Flowchart")).GetComponent<Flowchart>();
			JSONObject jSONObject = jsonData.instance.QieCuoJsonData.list.Find((JSONObject aa) => aa["AvatarID"].I == Tools.instance.MonstarID);
			if (jSONObject != null)
			{
				setSay(component, "win", jSONObject, "win");
				setSay(component, "lose", jSONObject, "lose");
				if (GlobalValue.GetTalk(1, "CheckTool.showTalk") == 2 && !player.AvatarQieCuo.HasField(Tools.instance.MonstarID.ToString()))
				{
					player.AvatarQieCuo.AddField(Tools.instance.MonstarID.ToString(), 1);
					jsonData.instance.MonstarSetHaoGanDu(Tools.instance.MonstarID, (int)jSONObject["tisheng"].n);
				}
			}
		}
		GlobalValue.SetTalk(0, 0, "CheckTool.showTalk");
		Tools.instance.CanShowFightUI = 0;
	}
}
