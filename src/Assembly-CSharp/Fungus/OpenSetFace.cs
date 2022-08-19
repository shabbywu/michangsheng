using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F99 RID: 3993
	[CommandInfo("YSTools", "打开捏脸界面", "打开捏脸界面", 0)]
	[AddComponentMenu("")]
	public class OpenSetFace : Command, INoCommand
	{
		// Token: 0x06006F94 RID: 28564 RVA: 0x002A71BC File Offset: 0x002A53BC
		public override void OnEnter()
		{
			SetFaceUI.Open();
			this.Continue();
		}
	}
}
