using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001407 RID: 5127
	[CommandInfo("YSNew/Get", "GetSeaEventInfoByIndex", "通过下标获取周围海域的事件信息", 0)]
	[AddComponentMenu("")]
	public class GetSeaEventInfoByIndex : Command
	{
		// Token: 0x06007C73 RID: 31859 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007C74 RID: 31860 RVA: 0x002C4F70 File Offset: 0x002C3170
		public override void OnEnter()
		{
			Tools.instance.getPlayer();
			List<SeaAvatarObjBase> roundEventList = EndlessSeaMag.Inst.RoundEventList;
			if (roundEventList.Count > this.index.Value)
			{
				if (this.EventID != null)
				{
					this.EventID.Value = roundEventList[this.index.Value]._EventId;
				}
				if (this.EventName != null)
				{
					this.EventName.Value = (string)roundEventList[this.index.Value].Json["EventName"];
				}
			}
			else
			{
				Debug.LogError("获取海域周围事件时下标越界：GetSeaEventInfoByIndex");
			}
			this.Continue();
		}

		// Token: 0x06007C75 RID: 31861 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C76 RID: 31862 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A7E RID: 27262
		[Tooltip("下标")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable index;

		// Token: 0x04006A7F RID: 27263
		[Tooltip("返回的事件ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable EventID;

		// Token: 0x04006A80 RID: 27264
		[Tooltip("返回的名称")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable EventName;
	}
}
