using System;
using UnityEngine.Networking;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNetwork
{
	// Token: 0x020015A4 RID: 5540
	public class IsClient : Conditional
	{
		// Token: 0x06008271 RID: 33393 RVA: 0x000595D6 File Offset: 0x000577D6
		public override TaskStatus OnUpdate()
		{
			if (!NetworkClient.active)
			{
				return 1;
			}
			return 2;
		}
	}
}
