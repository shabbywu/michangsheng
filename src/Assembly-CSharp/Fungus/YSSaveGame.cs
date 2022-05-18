using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200145B RID: 5211
	[CommandInfo("YSTools", "YSSaveGame", "保存游戏", 0)]
	[AddComponentMenu("")]
	public class YSSaveGame : Command
	{
		// Token: 0x06007DA8 RID: 32168 RVA: 0x002C72A4 File Offset: 0x002C54A4
		public override void OnEnter()
		{
			int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
			Tools.instance.Save(@int, 0, null);
			this.Continue();
		}

		// Token: 0x06007DA9 RID: 32169 RVA: 0x00054F4A File Offset: 0x0005314A
		public void saveAnimation()
		{
			GameObject.Find("savagameUI").GetComponent<Animation>().Play("SaveGameUI");
		}

		// Token: 0x06007DAA RID: 32170 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007DAB RID: 32171 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006B29 RID: 27433
		[Tooltip("说明")]
		[SerializeField]
		protected string init = "该指令调用后将保存游戏存档到第一号存档中";
	}
}
