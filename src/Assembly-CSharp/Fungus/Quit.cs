using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E19 RID: 3609
	[CommandInfo("Flow", "Quit", "Quits the application. Does not work in Editor or Webplayer builds. Shouldn't generally be used on iOS.", 0)]
	[AddComponentMenu("")]
	public class Quit : Command
	{
		// Token: 0x060065D3 RID: 26067 RVA: 0x0028439E File Offset: 0x0028259E
		public override void OnEnter()
		{
			Application.Quit();
			this.Continue();
		}

		// Token: 0x060065D4 RID: 26068 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}
	}
}
