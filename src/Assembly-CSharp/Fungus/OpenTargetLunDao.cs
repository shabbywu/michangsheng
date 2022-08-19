using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x02000F9B RID: 3995
	[CommandInfo("YSTools", "指定论道题目", "指定论道题目", 0)]
	[AddComponentMenu("")]
	public class OpenTargetLunDao : Command
	{
		// Token: 0x06006F98 RID: 28568 RVA: 0x002A71DC File Offset: 0x002A53DC
		public override void OnEnter()
		{
			Tools.instance.FinalScene = SceneManager.GetActiveScene().name;
			Tools.instance.LunDaoNpcId = this.npcId.Value;
			Tools.instance.LunTiList = new List<int>(this.list);
			Tools.instance.loadOtherScenes("LunDao");
			this.Continue();
		}

		// Token: 0x06006F99 RID: 28569 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F9A RID: 28570 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005C1A RID: 23578
		[Tooltip("npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04005C1B RID: 23579
		[Tooltip("随机论题数目")]
		[SerializeField]
		protected List<int> list;
	}
}
