using System;
using System.Collections;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using script.EventMsg;

namespace Fungus;

[CommandInfo("YSTools", "YSSay", "最近发生的事情", 0)]
[AddComponentMenu("")]
public class YSSay : Command
{
	[Tooltip("说话的武将ID")]
	[SerializeField]
	protected int AvatarID = 1;

	[Tooltip("表中的类型编号")]
	[SerializeField]
	protected int ID = 1;

	public override void OnEnter()
	{
		Say();
		Continue();
	}

	public void Say()
	{
		DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
		Flowchart component = ((Component)Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/BasePrefab/NPCTalk")).transform.Find("Flowchart")).GetComponent<Flowchart>();
		Block block = component.FindBlock("Splash");
		Say say = (Say)block.CommandList[0];
		say.pubAvatarIntID = AvatarID;
		List<EventData> list = EventMag.Inst.GetList(ID);
		if (list.Count == 0)
		{
			say.SetStandardText("最近没有发生什么特别值得注意的事情。");
			return;
		}
		int num = 0;
		foreach (EventData item in list)
		{
			Say say2 = say;
			if (num != 0)
			{
				Say say3 = ((Component)component).gameObject.AddComponent<Say>();
				say2 = ((Component)component).gameObject.AddComponent<Say>();
				addSayinfo(say3, component, block);
				addSayinfo(say2, component, block);
				say3.SetStandardText("让我想想...");
			}
			string text = "";
			text = ((item.Type != 0) ? DongTaiChuanWenBaio.DataDict[item.Id].text.Replace("{Xyear}", string.Concat(nowTime.Year - item.StartYear)).Replace("{npc}", item.npcName) : LiShiChuanWen.DataDict[item.Id].text.Replace("{Xyear}", string.Concat(nowTime.Year - item.StartYear)));
			say2.SetStandardText(text);
			num++;
		}
	}

	public void addSayinfo(Say say, Flowchart flowChat, Block statr)
	{
		say.pubAvatarIntID = AvatarID;
		say.ItemId = flowChat.NextItemId();
		say.pubAvatarIDSetType = StartFight.MonstarType.Normal;
		say.ParentBlock = statr;
		say.IsExecuting = true;
		say.CommandIndex = statr.CommandList.Count;
		statr.CommandList.Add(say);
	}

	public IEnumerator creataaaa(string text)
	{
		yield return (object)new WaitForSeconds(0.1f);
		Say obj = (Say)((Component)Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/BasePrefab/NPCTalk")).transform.Find("Flowchart")).GetComponent<Flowchart>().FindBlock("Splash").CommandList[0];
		obj.pubAvatarIntID = AvatarID;
		obj.SetStandardText(text);
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
