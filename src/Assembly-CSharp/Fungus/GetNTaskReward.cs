using System;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001428 RID: 5160
	[CommandInfo("YSTask", "GetNTaskReward", "获取任务奖励文字描述", 0)]
	[AddComponentMenu("")]
	public class GetNTaskReward : Command
	{
		// Token: 0x06007CEA RID: 31978 RVA: 0x002C5C3C File Offset: 0x002C3E3C
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

		// Token: 0x06007CEB RID: 31979 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006AB2 RID: 27314
		[Tooltip("需要获取的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;

		// Token: 0x04006AB3 RID: 27315
		[Tooltip("奖励描述")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable Desc;
	}
}
