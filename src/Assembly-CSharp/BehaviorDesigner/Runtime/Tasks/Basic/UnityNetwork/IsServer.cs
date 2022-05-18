using System;
using UnityEngine.Networking;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNetwork
{
	// Token: 0x020015A5 RID: 5541
	public class IsServer : Conditional
	{
		// Token: 0x06008273 RID: 33395 RVA: 0x000595E2 File Offset: 0x000577E2
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
