using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200143C RID: 5180
	[CommandInfo("YSTools", "ItemRemoveVar", "移除物品", 0)]
	[AddComponentMenu("")]
	public class ItemRemoveVar : Command
	{
		// Token: 0x06007D3A RID: 32058 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007D3B RID: 32059 RVA: 0x00054AF7 File Offset: 0x00052CF7
		public override void OnEnter()
		{
			Tools.instance.RemoveItem(this.ItemID.Value, this.ItemRemoveNum.Value);
			this.Continue();
		}

		// Token: 0x06007D3C RID: 32060 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006AD3 RID: 27347
		[Tooltip("需要移除的物品ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable ItemID;

		// Token: 0x04006AD4 RID: 27348
		[Tooltip("需要移除的物品数量")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable ItemRemoveNum;
	}
}
