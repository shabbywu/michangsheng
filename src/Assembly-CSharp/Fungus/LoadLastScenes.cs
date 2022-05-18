using System;
using System.Threading;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001441 RID: 5185
	[CommandInfo("YSTools", "LoadLastScenes", "加载上一个场景", 0)]
	[AddComponentMenu("")]
	public class LoadLastScenes : Command
	{
		// Token: 0x06007D54 RID: 32084 RVA: 0x002C6568 File Offset: 0x002C4768
		public void methodName()
		{
			try
			{
				Tools.instance.getPlayer().ResetAllEndlessNode();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
		}

		// Token: 0x06007D55 RID: 32085 RVA: 0x002C6628 File Offset: 0x002C4828
		public override void OnEnter()
		{
			new Thread(new ThreadStart(this.methodName)).Start();
			if (Tools.instance.getPlayer().lastScence.Equals("LoadingScreen") || Tools.instance.getPlayer().lastScence.Equals("") || Tools.instance.getPlayer().lastScence.Equals("MainMenu"))
			{
				Tools.instance.getPlayer().lastScence = "AllMaps";
			}
			Tools.instance.loadMapScenes(Tools.instance.getPlayer().lastScence, true);
		}

		// Token: 0x06007D56 RID: 32086 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x04006ADB RID: 27355
		[Tooltip("说明")]
		[SerializeField]
		protected string desc = "加载上一个场景";
	}
}
