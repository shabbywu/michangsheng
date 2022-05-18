using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001401 RID: 5121
	[CommandInfo("YSNew/Get", "GetNowTime", "获取当前时间并存储到year month day三个变量中", 0)]
	[AddComponentMenu("")]
	public class GetNowTime : Command
	{
		// Token: 0x06007C5C RID: 31836 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007C5D RID: 31837 RVA: 0x002C4C10 File Offset: 0x002C2E10
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			Flowchart flowchart = this.GetFlowchart();
			DateTime nowTime = player.worldTimeMag.getNowTime();
			this.setHasVariable("year", nowTime.Year, flowchart);
			this.setHasVariable("month", nowTime.Month, flowchart);
			this.setHasVariable("day", nowTime.Day, flowchart);
			this.Continue();
		}

		// Token: 0x06007C5E RID: 31838 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C5F RID: 31839 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A70 RID: 27248
		[Tooltip("解释")]
		[SerializeField]
		protected string StaticValueID = "获取当前时间并存储到year month day三个变量中，需要创建对应变量，可单独创建一个";
	}
}
