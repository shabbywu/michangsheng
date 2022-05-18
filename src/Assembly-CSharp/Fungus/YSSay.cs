using System;
using System.Collections;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200145C RID: 5212
	[CommandInfo("YSTools", "YSSay", "最近发生的事情", 0)]
	[AddComponentMenu("")]
	public class YSSay : Command
	{
		// Token: 0x06007DAD RID: 32173 RVA: 0x002C72D0 File Offset: 0x002C54D0
		public bool ManZuValue(int staticValueID, int num, string type)
		{
			int num2 = GlobalValue.Get(staticValueID, base.GetCommandSourceDesc());
			if (type == "=")
			{
				return num2 == num;
			}
			if (type == "<")
			{
				return num2 < num;
			}
			return !(type == ">") || num2 > num;
		}

		// Token: 0x06007DAE RID: 32174 RVA: 0x00054F79 File Offset: 0x00053179
		public override void OnEnter()
		{
			this.setload();
			this.Continue();
		}

		// Token: 0x06007DAF RID: 32175 RVA: 0x002C7324 File Offset: 0x002C5524
		public void setload()
		{
			Avatar avatar = Tools.instance.getPlayer();
			DateTime nowtime = avatar.worldTimeMag.getNowTime();
			List<JSONObject> list = jsonData.instance.LiShiChuanWen.list.FindAll(delegate(JSONObject aa)
			{
				if ((int)aa["TypeID"].n == this.ID)
				{
					if (aa["NTaskID"].I != 0 && avatar.nomelTaskMag.HasNTask(aa["NTaskID"].I))
					{
						return true;
					}
					int num2 = (int)aa["StartTime"].n;
					int num3 = (int)aa["cunZaiShiJian"].n;
					if (nowtime.Year > num2 && nowtime.Year - num2 <= num3)
					{
						if (aa["EventLv"].list.Count <= 0)
						{
							return true;
						}
						if (this.ManZuValue((int)aa["EventLv"][0].n, (int)aa["EventLv"][1].n, aa["fuhao"].str))
						{
							return true;
						}
					}
				}
				return false;
			});
			Flowchart component = Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/BasePrefab/NPCTalk")).transform.Find("Flowchart").GetComponent<Flowchart>();
			Block block = component.FindBlock("Splash");
			Say say = (Say)block.CommandList[0];
			say.pubAvatarIntID = this.AvatarID;
			if (list.Count == 0)
			{
				say.SetStandardText("最近没有发生什么特别值得注意的事情。");
				return;
			}
			int num = 0;
			foreach (JSONObject jsonobject in list)
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
				string standardText = Tools.Code64(jsonobject["text"].str).Replace("{Xyear}", string.Concat(nowtime.Year - (int)jsonobject["StartTime"].n));
				if (jsonobject["NTaskID"].I != 0)
				{
					standardText = NTaskText.GetNTaskDesc(jsonobject["NTaskID"].I);
				}
				say2.SetStandardText(standardText);
				if ((int)jsonobject["getChuanWen"].n > 0)
				{
					int taskId = (int)jsonobject["getChuanWen"].n;
					avatar.taskMag.addTask(taskId);
					string str = Tools.instance.Code64ToString(jsonData.instance.TaskJsonData[taskId.ToString()]["Name"].str);
					if (jsonData.instance.TaskJsonData[taskId.ToString()]["Type"].n != 0f)
					{
						"<color=#FF0000>" + str + "</color>任务已开启";
					}
				}
				num++;
			}
		}

		// Token: 0x06007DB0 RID: 32176 RVA: 0x002C75B0 File Offset: 0x002C57B0
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

		// Token: 0x06007DB1 RID: 32177 RVA: 0x00054F87 File Offset: 0x00053187
		public void creatTalk(string text)
		{
			base.StartCoroutine(this.creataaaa(text));
		}

		// Token: 0x06007DB2 RID: 32178 RVA: 0x00054F97 File Offset: 0x00053197
		public IEnumerator creataaaa(string text)
		{
			yield return new WaitForSeconds(0.1f);
			Say say = (Say)Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/BasePrefab/NPCTalk")).transform.Find("Flowchart").GetComponent<Flowchart>().FindBlock("Splash").CommandList[0];
			say.pubAvatarIntID = this.AvatarID;
			say.SetStandardText(text);
			yield break;
		}

		// Token: 0x06007DB3 RID: 32179 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006B2A RID: 27434
		[Tooltip("说话的武将ID")]
		[SerializeField]
		protected int AvatarID = 1;

		// Token: 0x04006B2B RID: 27435
		[Tooltip("表中的类型编号")]
		[SerializeField]
		protected int ID = 1;
	}
}
