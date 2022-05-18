using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001406 RID: 5126
	[CommandInfo("YSNew/Get", "GetRestTime", "检测副本剩余时间是否大于0", 0)]
	[AddComponentMenu("")]
	public class GetRestTime : Command
	{
		// Token: 0x06007C6E RID: 31854 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007C6F RID: 31855 RVA: 0x002C4F0C File Offset: 0x002C310C
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

		// Token: 0x06007C70 RID: 31856 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C71 RID: 31857 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A7D RID: 27261
		[Tooltip("返回是否拥有的值")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempBool;
	}
}
