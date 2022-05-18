using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x0200144C RID: 5196
	[CommandInfo("YSTools", "指定论道题目", "指定论道题目", 0)]
	[AddComponentMenu("")]
	public class OpenTargetLunDao : Command
	{
		// Token: 0x06007D7B RID: 32123 RVA: 0x002C67EC File Offset: 0x002C49EC
		public override void OnEnter()
		{
			Tools.instance.FinalScene = SceneManager.GetActiveScene().name;
			Tools.instance.LunDaoNpcId = this.npcId.Value;
			Tools.instance.LunTiList = new List<int>(this.list);
			Tools.instance.loadOtherScenes("LunDao");
			this.Continue();
		}

		// Token: 0x06007D7C RID: 32124 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D7D RID: 32125 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AE9 RID: 27369
		[Tooltip("npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04006AEA RID: 27370
		[Tooltip("随机论题数目")]
		[SerializeField]
		protected List<int> list;
	}
}
