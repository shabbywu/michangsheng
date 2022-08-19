using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F88 RID: 3976
	[CommandInfo("YSTools", "ItemRemoveVar", "移除物品", 0)]
	[AddComponentMenu("")]
	public class ItemRemoveVar : Command
	{
		// Token: 0x06006F50 RID: 28496 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006F51 RID: 28497 RVA: 0x002A6BE0 File Offset: 0x002A4DE0
		public override void OnEnter()
		{
			if (this.ItemID.Value == 1 || this.ItemID.Value == 117 || this.ItemID.Value == 218 || this.ItemID.Value == 304)
			{
				Tools.instance.RemoveTieJian(this.ItemID.Value);
			}
			else
			{
				Tools.instance.RemoveItem(this.ItemID.Value, this.ItemRemoveNum.Value);
			}
			this.Continue();
		}

		// Token: 0x06006F52 RID: 28498 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005C04 RID: 23556
		[Tooltip("需要移除的物品ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable ItemID;

		// Token: 0x04005C05 RID: 23557
		[Tooltip("需要移除的物品数量")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable ItemRemoveNum;
	}
}
