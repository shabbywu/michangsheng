using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013F7 RID: 5111
	[CommandInfo("YSNew/Get", "GetHaoGanDuVar", "获取好感度保存到一个变量中", 0)]
	[AddComponentMenu("")]
	public class GetHaoGanDuVar : Command
	{
		// Token: 0x06007C31 RID: 31793 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007C32 RID: 31794 RVA: 0x002C491C File Offset: 0x002C2B1C
		public override void OnEnter()
		{
			int num = NPCEx.NPCIDToNew(this.AvatarID.Value);
			int value = (int)jsonData.instance.AvatarRandomJsonData[num.ToString()]["HaoGanDu"].n;
			this.Value.Value = value;
			this.Continue();
		}

		// Token: 0x06007C33 RID: 31795 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A63 RID: 27235
		[Tooltip("需要获取好感度的武将ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable AvatarID;

		// Token: 0x04006A64 RID: 27236
		[Tooltip("存放值的变量")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable Value;
	}
}
