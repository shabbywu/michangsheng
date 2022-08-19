using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F49 RID: 3913
	[CommandInfo("YSNew/Get", "GetNowSeaID", "获取当前海域ID", 0)]
	[AddComponentMenu("")]
	public class GetNowSeaID : Command
	{
		// Token: 0x06006E67 RID: 28263 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006E68 RID: 28264 RVA: 0x002A4C01 File Offset: 0x002A2E01
		public override void OnEnter()
		{
			this.SeaID.Value = GetNowSeaID.Do();
			this.Continue();
		}

		// Token: 0x06006E69 RID: 28265 RVA: 0x002A4C1C File Offset: 0x002A2E1C
		public static int Do()
		{
			Tools.instance.getPlayer();
			int result = 0;
			int.TryParse(Tools.getScreenName().Replace("Sea", ""), out result);
			return result;
		}

		// Token: 0x06006E6A RID: 28266 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005B9C RID: 23452
		[Tooltip("海域ID存放的位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable SeaID;
	}
}
