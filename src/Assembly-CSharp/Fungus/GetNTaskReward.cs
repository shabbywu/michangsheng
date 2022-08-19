using System;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F70 RID: 3952
	[CommandInfo("YSTask", "GetNTaskReward", "获取任务奖励文字描述", 0)]
	[AddComponentMenu("")]
	public class GetNTaskReward : Command
	{
		// Token: 0x06006EF8 RID: 28408 RVA: 0x002A5EF8 File Offset: 0x002A40F8
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			int num = 0;
			int num2 = 0;
			player.nomelTaskMag.getReward(this.NTaskID.Value, ref num, ref num2);
			int i = jsonData.instance.NTaskAllType[this.NTaskID.Value.ToString()]["menpaihuobi"].I;
			if (i == 0 && num > 0)
			{
				this.Desc.Value = num + "灵石";
			}
			if (i > 0 && num2 > 0)
			{
				this.Desc.Value = num2 + "枚" + _ItemJsonData.DataDict[i].name;
			}
			this.Continue();
		}

		// Token: 0x06006EF9 RID: 28409 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005BDC RID: 23516
		[Tooltip("需要获取的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;

		// Token: 0x04005BDD RID: 23517
		[Tooltip("奖励描述")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable Desc;
	}
}
