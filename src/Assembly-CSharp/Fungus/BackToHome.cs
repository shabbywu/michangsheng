using System;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using YSGame;

namespace Fungus
{
	// Token: 0x02000F7A RID: 3962
	[CommandInfo("YSTools", "BackToHome", "直接提升一个等级，并把经验值设为0", 0)]
	[AddComponentMenu("")]
	public class BackToHome : Command
	{
		// Token: 0x06006F1F RID: 28447 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006F20 RID: 28448 RVA: 0x002A6510 File Offset: 0x002A4710
		public override void OnEnter()
		{
			YSSaveGame.Reset();
			KBEngineApp.app.entities[10] = null;
			KBEngineApp.app.entities.Remove(10);
			SceneManager.LoadScene("MainMenu");
		}

		// Token: 0x06006F21 RID: 28449 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F22 RID: 28450 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BEC RID: 23532
		[Tooltip("说明")]
		[SerializeField]
		protected string init = "返回到主界面，不用填什么";
	}
}
