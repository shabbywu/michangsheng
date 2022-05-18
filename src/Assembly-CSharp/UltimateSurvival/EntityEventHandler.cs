using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200088C RID: 2188
	public class EntityEventHandler : MonoBehaviour
	{
		// Token: 0x040032C4 RID: 12996
		public Value<float> Health = new Value<float>(100f);

		// Token: 0x040032C5 RID: 12997
		public Attempt<HealthEventData> ChangeHealth = new Attempt<HealthEventData>();

		// Token: 0x040032C6 RID: 12998
		public Value<bool> IsGrounded = new Value<bool>(true);

		// Token: 0x040032C7 RID: 12999
		public Value<Vector3> Velocity = new Value<Vector3>(Vector3.zero);

		// Token: 0x040032C8 RID: 13000
		public Message<float> Land = new Message<float>();

		// Token: 0x040032C9 RID: 13001
		public Message Death = new Message();

		// Token: 0x040032CA RID: 13002
		public Message Respawn = new Message();
	}
}
