using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001266 RID: 4710
	[CommandInfo("Flow", "Quit", "Quits the application. Does not work in Editor or Webplayer builds. Shouldn't generally be used on iOS.", 0)]
	[AddComponentMenu("")]
	public class Quit : Command
	{
		// Token: 0x06007261 RID: 29281 RVA: 0x0004DD4E File Offset: 0x0004BF4E
		public override void OnEnter()
		{
			Application.Quit();
			this.Continue();
		}

		// Token: 0x06007262 RID: 29282 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}
	}
}
