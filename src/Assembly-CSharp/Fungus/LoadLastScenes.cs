using System;
using System.Threading;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F8D RID: 3981
	[CommandInfo("YSTools", "LoadLastScenes", "加载上一个场景", 0)]
	[AddComponentMenu("")]
	public class LoadLastScenes : Command
	{
		// Token: 0x06006F6A RID: 28522 RVA: 0x002A6E64 File Offset: 0x002A5064
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

		// Token: 0x06006F6B RID: 28523 RVA: 0x002A6E9C File Offset: 0x002A509C
		public override void OnEnter()
		{
			new Thread(new ThreadStart(this.methodName)).Start();
			if (Tools.instance.getPlayer().lastScence.Equals("LoadingScreen") || Tools.instance.getPlayer().lastScence.Equals("") || Tools.instance.getPlayer().lastScence.Equals("MainMenu"))
			{
				Tools.instance.getPlayer().lastScence = "AllMaps";
			}
			Tools.instance.loadMapScenes(Tools.instance.getPlayer().lastScence, true);
		}

		// Token: 0x06006F6C RID: 28524 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x04005C0C RID: 23564
		[Tooltip("说明")]
		[SerializeField]
		protected string desc = "加载上一个场景";
	}
}
