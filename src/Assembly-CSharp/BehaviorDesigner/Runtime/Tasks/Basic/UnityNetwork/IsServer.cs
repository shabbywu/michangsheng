using System;
using UnityEngine.Networking;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNetwork
{
	// Token: 0x020010EB RID: 4331
	public class IsServer : Conditional
	{
		// Token: 0x06007479 RID: 29817 RVA: 0x002B2681 File Offset: 0x002B0881
		public override TaskStatus OnUpdate()
		{
			if (!NetworkServer.active)
			{
				return 1;
			}
			return 2;
		}
	}
}
