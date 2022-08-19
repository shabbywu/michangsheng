using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F0E RID: 3854
	[CommandInfo("YSFuBen", "FuBenAvatarFanHui", "角色返回之前的点", 0)]
	[AddComponentMenu("")]
	public class FuBenAvatarFanHui : Command
	{
		// Token: 0x06006D5D RID: 27997 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006D5E RID: 27998 RVA: 0x002A3100 File Offset: 0x002A1300
		public override void OnEnter()
		{
			Tools.instance.getPlayer();
			if (AllMapManage.instance != null && AllMapManage.instance.mapIndex.ContainsKey(Tools.instance.fubenLastIndex))
			{
				AllMapManage.instance.mapIndex[Tools.instance.fubenLastIndex].AvatarMoveToThis();
			}
			this.Continue();
		}

		// Token: 0x06006D5F RID: 27999 RVA: 0x002A3164 File Offset: 0x002A1364
		public void removeWait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = false;
			}
		}

		// Token: 0x06006D60 RID: 28000 RVA: 0x002A3194 File Offset: 0x002A1394
		public void wait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = true;
			}
		}

		// Token: 0x06006D61 RID: 28001 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006D62 RID: 28002 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B25 RID: 23333
		[Tooltip("说明")]
		[SerializeField]
		protected string Desc = "角色返回到上一次点击的点";
	}
}
