using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F4B RID: 3915
	[CommandInfo("YSNew/Get", "GetNowTime", "获取当前时间并存储到year month day三个变量中", 0)]
	[AddComponentMenu("")]
	public class GetNowTime : Command
	{
		// Token: 0x06006E71 RID: 28273 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006E72 RID: 28274 RVA: 0x002A4C9C File Offset: 0x002A2E9C
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

		// Token: 0x06006E73 RID: 28275 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E74 RID: 28276 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B9E RID: 23454
		[Tooltip("解释")]
		[SerializeField]
		protected string StaticValueID = "获取当前时间并存储到year month day三个变量中，需要创建对应变量，可单独创建一个";
	}
}
