using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013E7 RID: 5095
	[CommandInfo("YSNew/Add", "AddNpcField", "增加npc属性", 0)]
	[AddComponentMenu("")]
	public class AddNpcField : Command
	{
		// Token: 0x06007BF3 RID: 31731 RVA: 0x002C44F0 File Offset: 0x002C26F0
		public override void OnEnter()
		{
			int i = jsonData.instance.AvatarJsonData[this.npcId.Value.ToString()][this.fieldName].I;
			jsonData.instance.AvatarJsonData[this.npcId.Value.ToString()].SetField(this.fieldName, i + this.addNum);
			this.Continue();
		}

		// Token: 0x06007BF4 RID: 31732 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BF5 RID: 31733 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A48 RID: 27208
		[Tooltip("npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04006A49 RID: 27209
		[Tooltip("增加的属性")]
		[SerializeField]
		public string fieldName;

		// Token: 0x04006A4A RID: 27210
		[Tooltip("增加的数量")]
		[SerializeField]
		public int addNum;
	}
}
