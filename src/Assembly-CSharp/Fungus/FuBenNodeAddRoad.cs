using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013C4 RID: 5060
	[CommandInfo("YSFuBen", "FuBenNodeAddRoad", "给当前副本中的一个节点新增可行走路线", 0)]
	[AddComponentMenu("")]
	public class FuBenNodeAddRoad : Command
	{
		// Token: 0x06007B5C RID: 31580 RVA: 0x002C39A4 File Offset: 0x002C1BA4
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			foreach (int toIndex in this.NextNodeList)
			{
				player.fubenContorl[Tools.getScreenName()].AddNodeRoad(this.NodeID, toIndex);
			}
			this.Continue();
		}

		// Token: 0x06007B5D RID: 31581 RVA: 0x002C37E4 File Offset: 0x002C19E4
		public void removeWait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = false;
			}
		}

		// Token: 0x06007B5E RID: 31582 RVA: 0x002C3814 File Offset: 0x002C1A14
		public void wait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = true;
			}
		}

		// Token: 0x06007B5F RID: 31583 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007B60 RID: 31584 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x040069F2 RID: 27122
		[Tooltip("要设置路线的节点")]
		[SerializeField]
		protected int NodeID;

		// Token: 0x040069F3 RID: 27123
		[Tooltip("可以走的路线的路径")]
		[SerializeField]
		protected List<int> NextNodeList;
	}
}
