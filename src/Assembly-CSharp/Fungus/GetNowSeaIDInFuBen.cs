using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001400 RID: 5120
	[CommandInfo("YSNew/Get", "GetNowSeaIDInFuBen", "在副本中获取当前海域ID", 0)]
	[AddComponentMenu("")]
	public class GetNowSeaIDInFuBen : Command
	{
		// Token: 0x06007C57 RID: 31831 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007C58 RID: 31832 RVA: 0x002C4BC8 File Offset: 0x002C2DC8
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

		// Token: 0x06007C59 RID: 31833 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C5A RID: 31834 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A6F RID: 27247
		[Tooltip("海域ID存放的位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable SeaID;
	}
}
