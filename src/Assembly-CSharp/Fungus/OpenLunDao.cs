using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x0200144A RID: 5194
	[CommandInfo("YSTools", "OpenLunDao", "加载论道场景", 0)]
	[AddComponentMenu("")]
	public class OpenLunDao : Command
	{
		// Token: 0x06007D73 RID: 32115 RVA: 0x002C6764 File Offset: 0x002C4964
		public override void OnEnter()
		{
			Tools.instance.FinalScene = SceneManager.GetActiveScene().name;
			Tools.instance.LunDaoNpcId = this.npcId.Value;
			if (this.isSuiJiLunTi.Value)
			{
				Tools.instance.IsSuiJiLunTi = this.isSuiJiLunTi.Value;
				Tools.instance.LunTiNum = this.num.Value;
			}
			Tools.instance.loadOtherScenes("LunDao");
			this.Continue();
		}

		// Token: 0x06007D74 RID: 32116 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D75 RID: 32117 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AE4 RID: 27364
		[Tooltip("npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04006AE5 RID: 27365
		[Tooltip("是否随机论题")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable isSuiJiLunTi;

		// Token: 0x04006AE6 RID: 27366
		[Tooltip("随机论题数目")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable num;
	}
}
