using System;
using UnityEngine.Networking;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNetwork
{
	// Token: 0x020010EA RID: 4330
	public class IsClient : Conditional
	{
		// Token: 0x06007477 RID: 29815 RVA: 0x002B2675 File Offset: 0x002B0875
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
