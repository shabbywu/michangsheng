using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F52 RID: 3922
	[CommandInfo("YSNew/Get", "GetSeaEventNum", "获取周围海域事件数量", 0)]
	[AddComponentMenu("")]
	public class GetSeaEventNum : Command
	{
		// Token: 0x06006E8D RID: 28301 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006E8E RID: 28302 RVA: 0x002A5154 File Offset: 0x002A3354
		public override void OnEnter()
		{
			Tools.instance.getPlayer();
			List<SeaAvatarObjBase> aroundEventList = EndlessSeaMag.Inst.GetAroundEventList(this.radius.Value, this.EventType.Value);
			this.EventNum.Value = aroundEventList.Count;
			this.Continue();
		}

		// Token: 0x06006E8F RID: 28303 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E90 RID: 28304 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BB0 RID: 23472
		[Tooltip("半径")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable radius;

		// Token: 0x04005BB1 RID: 23473
		[Tooltip("事件类型")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable EventType;

		// Token: 0x04005BB2 RID: 23474
		[Tooltip("返回的数量值")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable EventNum;
	}
}
