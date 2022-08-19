using System;

namespace Fungus
{
	// Token: 0x02000EF5 RID: 3829
	[Flags]
	public enum ExecuteMethod
	{
		// Token: 0x04005AAA RID: 23210
		AfterPeriodOfTime = 1,
		// Token: 0x04005AAB RID: 23211
		Start = 2,
		// Token: 0x04005AAC RID: 23212
		Update = 4,
		// Token: 0x04005AAD RID: 23213
		FixedUpdate = 8,
		// Token: 0x04005AAE RID: 23214
		LateUpdate = 16,
		// Token: 0x04005AAF RID: 23215
		OnDestroy = 32,
		// Token: 0x04005AB0 RID: 23216
		OnEnable = 64,
		// Token: 0x04005AB1 RID: 23217
		OnDisable = 128,
		// Token: 0x04005AB2 RID: 23218
		OnControllerColliderHit = 256,
		// Token: 0x04005AB3 RID: 23219
		OnParticleCollision = 512,
		// Token: 0x04005AB4 RID: 23220
		OnJointBreak = 1024,
		// Token: 0x04005AB5 RID: 23221
		OnBecameInvisible = 2048,
		// Token: 0x04005AB6 RID: 23222
		OnBecameVisible = 4096,
		// Token: 0x04005AB7 RID: 23223
		OnTriggerEnter = 8192,
		// Token: 0x04005AB8 RID: 23224
		OnTriggerExit = 16384,
		// Token: 0x04005AB9 RID: 23225
		OnTriggerStay = 32768,
		// Token: 0x04005ABA RID: 23226
		OnCollisionEnter = 65536,
		// Token: 0x04005ABB RID: 23227
		OnCollisionExit = 131072,
		// Token: 0x04005ABC RID: 23228
		OnCollisionStay = 262144,
		// Token: 0x04005ABD RID: 23229
		OnTriggerEnter2D = 524288,
		// Token: 0x04005ABE RID: 23230
		OnTriggerExit2D = 1048576,
		// Token: 0x04005ABF RID: 23231
		OnTriggerStay2D = 2097152,
		// Token: 0x04005AC0 RID: 23232
		OnCollisionEnter2D = 4194304,
		// Token: 0x04005AC1 RID: 23233
		OnCollisionExit2D = 8388608,
		// Token: 0x04005AC2 RID: 23234
		OnCollisionStay2D = 16777216
	}
}
