using System;
using System.Collections;
using System.Collections.Generic;
using JSONClass;
using script.EventMsg;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000FA8 RID: 4008
	[CommandInfo("YSTools", "YSSay", "最近发生的事情", 0)]
	[AddComponentMenu("")]
	public class YSSay : Command
	{
		// Token: 0x06006FC9 RID: 28617 RVA: 0x002A7C6B File Offset: 0x002A5E6B
		public override void OnEnter()
		{
			this.Say();
			this.Continue();
		}

		// Token: 0x06006FCA RID: 28618 RVA: 0x002A7C7C File Offset: 0x002A5E7C
		public void Say()
		{
			DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
			Flowchart component = Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/BasePrefab/NPCTalk")).transform.Find("Flowchart").GetComponent<Flowchart>();
			Block block = component.FindBlock("Splash");
			Say say = (Say)block.CommandList[0];
			say.pubAvatarIntID = this.AvatarID;
			List<EventData> list = EventMag.Inst.GetList(this.ID);
			if (list.Count == 0)
			{
				say.SetStandardText("最近没有发生什么特别值得注意的事情。");
				return;
			}
			int num = 0;
			foreach (EventData eventData in list)
			{
				Say say2 = say;
				if (num != 0)
				{
					Say say3 = component.gameObject.AddComponent<Say>();
					say2 = component.gameObject.AddComponent<Say>();
					this.addSayinfo(say3, component, block);
					this.addSayinfo(say2, component, block);
					say3.SetStandardText("让我想想...");
				}
				string standardText;
				if (eventData.Type == 0)
				{
					standardText = LiShiChuanWen.DataDict[eventData.Id].text.Replace("{Xyear}", string.Concat(nowTime.Year - eventData.StartYear));
				}
				else
				{
					standardText = DongTaiChuanWenBaio.DataDict[eventData.Id].text.Replace("{Xyear}", string.Concat(nowTime.Year - eventData.StartYear)).Replace("{npc}", eventData.npcName);
				}
				say2.SetStandardText(standardText);
				num++;
			}
		}

		// Token: 0x06006FCB RID: 28619 RVA: 0x002A7E50 File Offset: 0x002A6050
		public void addSayinfo(Say say, Flowchart flowChat, Block statr)
		{
			say.pubAvatarIntID = this.AvatarID;
			say.ItemId = flowChat.NextItemId();
			say.pubAvatarIDSetType = StartFight.MonstarType.Normal;
			say.ParentBlock = statr;
			say.IsExecuting = true;
			say.CommandIndex = statr.CommandList.Count;
			statr.CommandList.Add(say);
		}

		// Token: 0x06006FCC RID: 28620 RVA: 0x002A7EA7 File Offset: 0x002A60A7
		public IEnumerator creataaaa(string text)
		{
			yield return new WaitForSeconds(0.1f);
			Say say = (Say)Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/BasePrefab/NPCTalk")).transform.Find("Flowchart").GetComponent<Flowchart>().FindBlock("Splash").CommandList[0];
			say.pubAvatarIntID = this.AvatarID;
			say.SetStandardText(text);
			yield break;
		}

		// Token: 0x06006FCD RID: 28621 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005C3D RID: 23613
		[Tooltip("说话的武将ID")]
		[SerializeField]
		protected int AvatarID = 1;

		// Token: 0x04005C3E RID: 23614
		[Tooltip("表中的类型编号")]
		[SerializeField]
		protected int ID = 1;
	}
}
