using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x02000F97 RID: 3991
	[CommandInfo("YSTools", "OpenLunDao", "加载论道场景", 0)]
	[AddComponentMenu("")]
	public class OpenLunDao : Command
	{
		// Token: 0x06006F8C RID: 28556 RVA: 0x002A7104 File Offset: 0x002A5304
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

		// Token: 0x06006F8D RID: 28557 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F8E RID: 28558 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005C15 RID: 23573
		[Tooltip("npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04005C16 RID: 23574
		[Tooltip("是否随机论题")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable isSuiJiLunTi;

		// Token: 0x04005C17 RID: 23575
		[Tooltip("随机论题数目")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable num;
	}
}
