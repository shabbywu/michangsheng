using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013F6 RID: 5110
	[CommandInfo("YSNew/Get", "GetHaoGanDuSay", "进行请教功法对话，只能用在最后，无法在链接回该talk", 0)]
	[AddComponentMenu("")]
	public class GetHaoGanDuSay : Command
	{
		// Token: 0x06007C2C RID: 31788 RVA: 0x0005468B File Offset: 0x0005288B
		public override void OnEnter()
		{
			((ThreeSceneMag)Object.FindObjectOfType(typeof(ThreeSceneMag))).qingJiaoGongFanom(this.StaticValueID.Value);
			this.Continue();
		}

		// Token: 0x06007C2D RID: 31789 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C2E RID: 31790 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A61 RID: 27233
		public static Dictionary<string, JObject> JsonData = new Dictionary<string, JObject>();

		// Token: 0x04006A62 RID: 27234
		[Tooltip("NPC的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable StaticValueID;
	}
}
