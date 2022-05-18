using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013C1 RID: 5057
	[CommandInfo("YSFuBen", "FuBenAvatarFanHui", "角色返回之前的点", 0)]
	[AddComponentMenu("")]
	public class FuBenAvatarFanHui : Command
	{
		// Token: 0x06007B46 RID: 31558 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007B47 RID: 31559 RVA: 0x002C3780 File Offset: 0x002C1980
		public override void OnEnter()
		{
			Tools.instance.getPlayer();
			if (AllMapManage.instance != null && AllMapManage.instance.mapIndex.ContainsKey(Tools.instance.fubenLastIndex))
			{
				AllMapManage.instance.mapIndex[Tools.instance.fubenLastIndex].AvatarMoveToThis();
			}
			this.Continue();
		}

		// Token: 0x06007B48 RID: 31560 RVA: 0x002C37E4 File Offset: 0x002C19E4
		public void removeWait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = false;
			}
		}

		// Token: 0x06007B49 RID: 31561 RVA: 0x002C3814 File Offset: 0x002C1A14
		public void wait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = true;
			}
		}

		// Token: 0x06007B4A RID: 31562 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007B4B RID: 31563 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x040069EC RID: 27116
		[Tooltip("说明")]
		[SerializeField]
		protected string Desc = "角色返回到上一次点击的点";
	}
}
