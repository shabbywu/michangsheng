using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F43 RID: 3907
	[CommandInfo("YSNew/Get", "GetImportantNpcId", "根据固定NpcId获取绑定该Id的NpcId", 0)]
	[AddComponentMenu("")]
	public class GetImportantNpcId : Command
	{
		// Token: 0x06006E4D RID: 28237 RVA: 0x002A49DC File Offset: 0x002A2BDC
		public override void OnEnter()
		{
			if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(this.NpcBingDingId.Value))
			{
				this.NpcId.Value = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[this.NpcBingDingId.Value];
			}
			else
			{
				this.NpcId.Value = 0;
			}
			this.Continue();
		}

		// Token: 0x06006E4E RID: 28238 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E4F RID: 28239 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B94 RID: 23444
		[Tooltip("Npc绑定Id")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NpcBingDingId;

		// Token: 0x04005B95 RID: 23445
		[Tooltip("NpcId存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NpcId;
	}
}
