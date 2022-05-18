using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200143B RID: 5179
	[CommandInfo("YSTools", "ItemRemove", "移除物品", 0)]
	[AddComponentMenu("")]
	public class ItemRemove : Command
	{
		// Token: 0x06007D35 RID: 32053 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007D36 RID: 32054 RVA: 0x00054AD9 File Offset: 0x00052CD9
		public override void OnEnter()
		{
			Tools.instance.RemoveItem(this.ItemID, this.ItemRemoveNum);
			this.Continue();
		}

		// Token: 0x06007D37 RID: 32055 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D38 RID: 32056 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AD1 RID: 27345
		[Tooltip("需要移除的物品ID")]
		[SerializeField]
		protected int ItemID;

		// Token: 0x04006AD2 RID: 27346
		[Tooltip("需要移除的物品数量")]
		[SerializeField]
		protected int ItemRemoveNum;
	}
}
