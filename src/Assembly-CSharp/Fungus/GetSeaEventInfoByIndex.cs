using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F51 RID: 3921
	[CommandInfo("YSNew/Get", "GetSeaEventInfoByIndex", "通过下标获取周围海域的事件信息", 0)]
	[AddComponentMenu("")]
	public class GetSeaEventInfoByIndex : Command
	{
		// Token: 0x06006E88 RID: 28296 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006E89 RID: 28297 RVA: 0x002A509C File Offset: 0x002A329C
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

		// Token: 0x06006E8A RID: 28298 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E8B RID: 28299 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BAD RID: 23469
		[Tooltip("下标")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable index;

		// Token: 0x04005BAE RID: 23470
		[Tooltip("返回的事件ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable EventID;

		// Token: 0x04005BAF RID: 23471
		[Tooltip("返回的名称")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable EventName;
	}
}
