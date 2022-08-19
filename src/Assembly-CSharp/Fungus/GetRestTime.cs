using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F50 RID: 3920
	[CommandInfo("YSNew/Get", "GetRestTime", "检测副本剩余时间是否大于0", 0)]
	[AddComponentMenu("")]
	public class GetRestTime : Command
	{
		// Token: 0x06006E83 RID: 28291 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006E84 RID: 28292 RVA: 0x002A5038 File Offset: 0x002A3238
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			string screenName = Tools.getScreenName();
			this.TempBool.Value = true;
			if (jsonData.instance.FuBenInfoJsonData.HasField(screenName) && player.fubenContorl[screenName].ResidueTimeDay <= 0)
			{
				this.TempBool.Value = false;
			}
			this.Continue();
		}

		// Token: 0x06006E85 RID: 28293 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E86 RID: 28294 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BAC RID: 23468
		[Tooltip("返回是否拥有的值")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempBool;
	}
}
