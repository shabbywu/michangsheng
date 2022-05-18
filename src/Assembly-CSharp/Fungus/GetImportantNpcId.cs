using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013F9 RID: 5113
	[CommandInfo("YSNew/Get", "GetImportantNpcId", "根据固定NpcId获取绑定该Id的NpcId", 0)]
	[AddComponentMenu("")]
	public class GetImportantNpcId : Command
	{
		// Token: 0x06007C38 RID: 31800 RVA: 0x002C4974 File Offset: 0x002C2B74
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

		// Token: 0x06007C39 RID: 31801 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C3A RID: 31802 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A66 RID: 27238
		[Tooltip("Npc绑定Id")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NpcBingDingId;

		// Token: 0x04006A67 RID: 27239
		[Tooltip("NpcId存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NpcId;
	}
}
