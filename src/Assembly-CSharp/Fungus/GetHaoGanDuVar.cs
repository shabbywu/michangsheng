using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F41 RID: 3905
	[CommandInfo("YSNew/Get", "GetHaoGanDuVar", "获取好感度保存到一个变量中", 0)]
	[AddComponentMenu("")]
	public class GetHaoGanDuVar : Command
	{
		// Token: 0x06006E46 RID: 28230 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006E47 RID: 28231 RVA: 0x002A4954 File Offset: 0x002A2B54
		public override void OnEnter()
		{
			int num = NPCEx.NPCIDToNew(this.AvatarID.Value);
			int value = (int)jsonData.instance.AvatarRandomJsonData[num.ToString()]["HaoGanDu"].n;
			this.Value.Value = value;
			this.Continue();
		}

		// Token: 0x06006E48 RID: 28232 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005B91 RID: 23441
		[Tooltip("需要获取好感度的武将ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable AvatarID;

		// Token: 0x04005B92 RID: 23442
		[Tooltip("存放值的变量")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable Value;
	}
}
