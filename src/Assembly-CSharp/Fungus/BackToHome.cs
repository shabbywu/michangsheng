using System;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using YSGame;

namespace Fungus
{
	// Token: 0x02001431 RID: 5169
	[CommandInfo("YSTools", "BackToHome", "直接提升一个等级，并把经验值设为0", 0)]
	[AddComponentMenu("")]
	public class BackToHome : Command
	{
		// Token: 0x06007D0F RID: 32015 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007D10 RID: 32016 RVA: 0x000549DB File Offset: 0x00052BDB
		public override void OnEnter()
		{
			YSSaveGame.Reset();
			KBEngineApp.app.entities[10] = null;
			KBEngineApp.app.entities.Remove(10);
			SceneManager.LoadScene("MainMenu");
		}

		// Token: 0x06007D11 RID: 32017 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D12 RID: 32018 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AC0 RID: 27328
		[Tooltip("说明")]
		[SerializeField]
		protected string init = "返回到主界面，不用填什么";
	}
}
