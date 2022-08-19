using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005CA RID: 1482
	public class EntityEventHandler : MonoBehaviour
	{
		// Token: 0x04002A2E RID: 10798
		public Value<float> Health = new Value<float>(100f);

		// Token: 0x04002A2F RID: 10799
		public Attempt<HealthEventData> ChangeHealth = new Attempt<HealthEventData>();

		// Token: 0x04002A30 RID: 10800
		public Value<bool> IsGrounded = new Value<bool>(true);

		// Token: 0x04002A31 RID: 10801
		public Value<Vector3> Velocity = new Value<Vector3>(Vector3.zero);

		// Token: 0x04002A32 RID: 10802
		public Message<float> Land = new Message<float>();

		// Token: 0x04002A33 RID: 10803
		public Message Death = new Message();

		// Token: 0x04002A34 RID: 10804
		public Message Respawn = new Message();
	}
}
