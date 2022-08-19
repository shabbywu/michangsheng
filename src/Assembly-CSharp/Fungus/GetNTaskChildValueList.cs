using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F6F RID: 3951
	[CommandInfo("YSTask", "GetNTaskChildValueList", "获取某个任务的子项的值", 0)]
	[AddComponentMenu("")]
	public class GetNTaskChildValueList : Command
	{
		// Token: 0x06006EF4 RID: 28404 RVA: 0x002A5E1C File Offset: 0x002A401C
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			List<JSONObject> ntaskXiangXiList = player.nomelTaskMag.GetNTaskXiangXiList(this.NTaskID.Value);
			int num = 0;
			foreach (JSONObject jsonobject in ntaskXiangXiList)
			{
				int chilidID = player.nomelTaskMag.getChilidID(this.NTaskID.Value, num);
				JSONObject jsonobject2 = jsonData.instance.NTaskSuiJI[chilidID.ToString()];
				if (this.ValueList[num] != null)
				{
					this.ValueList[num].Value = jsonobject2["Value"].I;
				}
				num++;
			}
			this.Continue();
		}

		// Token: 0x06006EF5 RID: 28405 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006EF6 RID: 28406 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BDA RID: 23514
		[Tooltip("需要获取的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;

		// Token: 0x04005BDB RID: 23515
		[Tooltip("值列表")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		public List<IntegerVariable> ValueList;
	}
}
