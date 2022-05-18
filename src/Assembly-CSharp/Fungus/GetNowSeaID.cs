using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013FF RID: 5119
	[CommandInfo("YSNew/Get", "GetNowSeaID", "获取当前海域ID", 0)]
	[AddComponentMenu("")]
	public class GetNowSeaID : Command
	{
		// Token: 0x06007C52 RID: 31826 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007C53 RID: 31827 RVA: 0x002C4B7C File Offset: 0x002C2D7C
		public override void OnEnter()
		{
			Tools.instance.getPlayer();
			int value = 0;
			if (int.TryParse(Tools.getScreenName().Replace("Sea", ""), out value))
			{
				this.SeaID.Value = value;
			}
			this.Continue();
		}

		// Token: 0x06007C54 RID: 31828 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C55 RID: 31829 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A6E RID: 27246
		[Tooltip("海域ID存放的位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable SeaID;
	}
}
