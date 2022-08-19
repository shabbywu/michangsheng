using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F40 RID: 3904
	[CommandInfo("YSNew/Get", "GetHaoGanDuSay", "进行请教功法对话，只能用在最后，无法在链接回该talk", 0)]
	[AddComponentMenu("")]
	public class GetHaoGanDuSay : Command
	{
		// Token: 0x06006E41 RID: 28225 RVA: 0x002A491A File Offset: 0x002A2B1A
		public override void OnEnter()
		{
			((ThreeSceneMag)Object.FindObjectOfType(typeof(ThreeSceneMag))).qingJiaoGongFanom(this.StaticValueID.Value);
			this.Continue();
		}

		// Token: 0x06006E42 RID: 28226 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E43 RID: 28227 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B8F RID: 23439
		public static Dictionary<string, JObject> JsonData = new Dictionary<string, JObject>();

		// Token: 0x04005B90 RID: 23440
		[Tooltip("NPC的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable StaticValueID;
	}
}
