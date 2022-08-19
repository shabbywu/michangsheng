using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F87 RID: 3975
	[CommandInfo("YSTools", "ItemRemove", "移除物品", 0)]
	[AddComponentMenu("")]
	public class ItemRemove : Command
	{
		// Token: 0x06006F4B RID: 28491 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006F4C RID: 28492 RVA: 0x002A6B78 File Offset: 0x002A4D78
		public override void OnEnter()
		{
			if (this.ItemID == 1 || this.ItemID == 117 || this.ItemID == 218 || this.ItemID == 304)
			{
				Tools.instance.RemoveTieJian(this.ItemID);
			}
			else
			{
				Tools.instance.RemoveItem(this.ItemID, this.ItemRemoveNum);
			}
			this.Continue();
		}

		// Token: 0x06006F4D RID: 28493 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F4E RID: 28494 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005C02 RID: 23554
		[Tooltip("需要移除的物品ID")]
		[SerializeField]
		protected int ItemID;

		// Token: 0x04005C03 RID: 23555
		[Tooltip("需要移除的物品数量")]
		[SerializeField]
		protected int ItemRemoveNum;
	}
}
