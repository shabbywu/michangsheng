using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001427 RID: 5159
	[CommandInfo("YSTask", "GetNTaskChildValueList", "获取某个任务的子项的值", 0)]
	[AddComponentMenu("")]
	public class GetNTaskChildValueList : Command
	{
		// Token: 0x06007CE6 RID: 31974 RVA: 0x002C5B60 File Offset: 0x002C3D60
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

		// Token: 0x06007CE7 RID: 31975 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007CE8 RID: 31976 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AB0 RID: 27312
		[Tooltip("需要获取的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;

		// Token: 0x04006AB1 RID: 27313
		[Tooltip("值列表")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		public List<IntegerVariable> ValueList;
	}
}
