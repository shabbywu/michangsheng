using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013D5 RID: 5077
	[CommandInfo("YS", "ChoiceNum", "玩家选择一个数量，并将玩家选择的数量返回到一个变量中", 0)]
	[AddComponentMenu("")]
	public class ChoiceNum : Command
	{
		// Token: 0x06007BB4 RID: 31668 RVA: 0x00054369 File Offset: 0x00052569
		public override void OnEnter()
		{
			Tools.instance.getPlayer();
			selectNum.instence.setChoice(new EventDelegate(delegate()
			{
				int value = int.Parse(selectNum.instence.gameObject.GetComponent<UI_chaifen>().inputNum.value);
				this.FinalSelectNum.Value = value;
				this.Continue();
			}), new EventDelegate(delegate()
			{
				this.FinalSelectNum.Value = 0;
				this.Continue();
			}), this.desc);
		}

		// Token: 0x06007BB5 RID: 31669 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BB6 RID: 31670 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A1F RID: 27167
		[Tooltip("弹框描述")]
		[SerializeField]
		protected string desc = "选择数量";

		// Token: 0x04006A20 RID: 27168
		[Tooltip("玩家最终选择的个数")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable FinalSelectNum;
	}
}
