using System;

namespace Fungus
{
	// Token: 0x02001396 RID: 5014
	[Flags]
	public enum ExecuteMethod
	{
		// Token: 0x04006913 RID: 26899
		AfterPeriodOfTime = 1,
		// Token: 0x04006914 RID: 26900
		Start = 2,
		// Token: 0x04006915 RID: 26901
		Update = 4,
		// Token: 0x04006916 RID: 26902
		FixedUpdate = 8,
		// Token: 0x04006917 RID: 26903
		LateUpdate = 16,
		// Token: 0x04006918 RID: 26904
		OnDestroy = 32,
		// Token: 0x04006919 RID: 26905
		OnEnable = 64,
		// Token: 0x0400691A RID: 26906
		OnDisable = 128,
		// Token: 0x0400691B RID: 26907
		OnControllerColliderHit = 256,
		// Token: 0x0400691C RID: 26908
		OnParticleCollision = 512,
		// Token: 0x0400691D RID: 26909
		OnJointBreak = 1024,
		// Token: 0x0400691E RID: 26910
		OnBecameInvisible = 2048,
		// Token: 0x0400691F RID: 26911
		OnBecameVisible = 4096,
		// Token: 0x04006920 RID: 26912
		OnTriggerEnter = 8192,
		// Token: 0x04006921 RID: 26913
		OnTriggerExit = 16384,
		// Token: 0x04006922 RID: 26914
		OnTriggerStay = 32768,
		// Token: 0x04006923 RID: 26915
		OnCollisionEnter = 65536,
		// Token: 0x04006924 RID: 26916
		OnCollisionExit = 131072,
		// Token: 0x04006925 RID: 26917
		OnCollisionStay = 262144,
		// Token: 0x04006926 RID: 26918
		OnTriggerEnter2D = 524288,
		// Token: 0x04006927 RID: 26919
		OnTriggerExit2D = 1048576,
		// Token: 0x04006928 RID: 26920
		OnTriggerStay2D = 2097152,
		// Token: 0x04006929 RID: 26921
		OnCollisionEnter2D = 4194304,
		// Token: 0x0400692A RID: 26922
		OnCollisionExit2D = 8388608,
		// Token: 0x0400692B RID: 26923
		OnCollisionStay2D = 16777216
	}
}
