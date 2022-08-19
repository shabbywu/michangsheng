using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F31 RID: 3889
	[CommandInfo("YSNew/Add", "AddNpcField", "增加npc属性", 0)]
	[AddComponentMenu("")]
	public class AddNpcField : Command
	{
		// Token: 0x06006E08 RID: 28168 RVA: 0x002A43A0 File Offset: 0x002A25A0
		public override void OnEnter()
		{
			int i = jsonData.instance.AvatarJsonData[this.npcId.Value.ToString()][this.fieldName].I;
			jsonData.instance.AvatarJsonData[this.npcId.Value.ToString()].SetField(this.fieldName, i + this.addNum);
			this.Continue();
		}

		// Token: 0x06006E09 RID: 28169 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E0A RID: 28170 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B76 RID: 23414
		[Tooltip("npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04005B77 RID: 23415
		[Tooltip("增加的属性")]
		[SerializeField]
		public string fieldName;

		// Token: 0x04005B78 RID: 23416
		[Tooltip("增加的数量")]
		[SerializeField]
		public int addNum;
	}
}
