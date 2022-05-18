using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001408 RID: 5128
	[CommandInfo("YSNew/Get", "GetSeaEventNum", "获取周围海域事件数量", 0)]
	[AddComponentMenu("")]
	public class GetSeaEventNum : Command
	{
		// Token: 0x06007C78 RID: 31864 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007C79 RID: 31865 RVA: 0x002C5028 File Offset: 0x002C3228
		public override void OnEnter()
		{
			Tools.instance.getPlayer();
			List<SeaAvatarObjBase> aroundEventList = EndlessSeaMag.Inst.GetAroundEventList(this.radius.Value, this.EventType.Value);
			this.EventNum.Value = aroundEventList.Count;
			this.Continue();
		}

		// Token: 0x06007C7A RID: 31866 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C7B RID: 31867 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A81 RID: 27265
		[Tooltip("半径")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable radius;

		// Token: 0x04006A82 RID: 27266
		[Tooltip("事件类型")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable EventType;

		// Token: 0x04006A83 RID: 27267
		[Tooltip("返回的数量值")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable EventNum;
	}
}
