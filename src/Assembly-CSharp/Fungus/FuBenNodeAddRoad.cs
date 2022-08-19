using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F11 RID: 3857
	[CommandInfo("YSFuBen", "FuBenNodeAddRoad", "给当前副本中的一个节点新增可行走路线", 0)]
	[AddComponentMenu("")]
	public class FuBenNodeAddRoad : Command
	{
		// Token: 0x06006D73 RID: 28019 RVA: 0x002A33FC File Offset: 0x002A15FC
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			foreach (int toIndex in this.NextNodeList)
			{
				player.fubenContorl[Tools.getScreenName()].AddNodeRoad(this.NodeID, toIndex);
			}
			this.Continue();
		}

		// Token: 0x06006D74 RID: 28020 RVA: 0x002A3478 File Offset: 0x002A1678
		public void removeWait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = false;
			}
		}

		// Token: 0x06006D75 RID: 28021 RVA: 0x002A34A8 File Offset: 0x002A16A8
		public void wait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = true;
			}
		}

		// Token: 0x06006D76 RID: 28022 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006D77 RID: 28023 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B2B RID: 23339
		[Tooltip("要设置路线的节点")]
		[SerializeField]
		protected int NodeID;

		// Token: 0x04005B2C RID: 23340
		[Tooltip("可以走的路线的路径")]
		[SerializeField]
		protected List<int> NextNodeList;
	}
}
