using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F4A RID: 3914
	[CommandInfo("YSNew/Get", "GetNowSeaIDInFuBen", "在副本中获取当前海域ID", 0)]
	[AddComponentMenu("")]
	public class GetNowSeaIDInFuBen : Command
	{
		// Token: 0x06006E6C RID: 28268 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006E6D RID: 28269 RVA: 0x002A4C54 File Offset: 0x002A2E54
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			int value = 0;
			if (int.TryParse(player.lastFuBenScence.Replace("Sea", ""), out value))
			{
				this.SeaID.Value = value;
			}
			this.Continue();
		}

		// Token: 0x06006E6E RID: 28270 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E6F RID: 28271 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B9D RID: 23453
		[Tooltip("海域ID存放的位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable SeaID;
	}
}
