using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F21 RID: 3873
	[CommandInfo("YS", "ChoiceNum", "玩家选择一个数量，并将玩家选择的数量返回到一个变量中", 0)]
	[AddComponentMenu("")]
	public class ChoiceNum : Command
	{
		// Token: 0x06006DC9 RID: 28105 RVA: 0x002A3E05 File Offset: 0x002A2005
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

		// Token: 0x06006DCA RID: 28106 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DCB RID: 28107 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B55 RID: 23381
		[Tooltip("弹框描述")]
		[SerializeField]
		protected string desc = "选择数量";

		// Token: 0x04005B56 RID: 23382
		[Tooltip("玩家最终选择的个数")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable FinalSelectNum;
	}
}
