using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000FA7 RID: 4007
	[CommandInfo("YSTools", "YSSaveGame", "保存游戏", 0)]
	[AddComponentMenu("")]
	public class YSSaveGame : Command
	{
		// Token: 0x06006FC5 RID: 28613 RVA: 0x002A7C1D File Offset: 0x002A5E1D
		public override void OnEnter()
		{
			YSNewSaveSystem.SaveGame(PlayerPrefs.GetInt("NowPlayerFileAvatar"), 0, null, this.ignoreSlot0Time);
			this.Continue();
		}

		// Token: 0x06006FC6 RID: 28614 RVA: 0x002A7C3C File Offset: 0x002A5E3C
		public void saveAnimation()
		{
			GameObject.Find("savagameUI").GetComponent<Animation>().Play("SaveGameUI");
		}

		// Token: 0x06006FC7 RID: 28615 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005C3B RID: 23611
		[Tooltip("说明")]
		[SerializeField]
		protected string init = "该指令调用后将保存游戏存档到第一号存档中";

		// Token: 0x04005C3C RID: 23612
		[Tooltip("是否忽略自动保存间隔时间")]
		[SerializeField]
		protected bool ignoreSlot0Time;
	}
}
