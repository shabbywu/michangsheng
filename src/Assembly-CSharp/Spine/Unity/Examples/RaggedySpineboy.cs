using System;
using System.Collections;
using Spine.Unity.Modules;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E4A RID: 3658
	public class RaggedySpineboy : MonoBehaviour
	{
		// Token: 0x060057D2 RID: 22482 RVA: 0x0003EC9B File Offset: 0x0003CE9B
		private void Start()
		{
			this.ragdoll = base.GetComponent<SkeletonRagdoll2D>();
			this.naturalCollider = base.GetComponent<Collider2D>();
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x0003ECB5 File Offset: 0x0003CEB5
		private void AddRigidbody()
		{
			base.gameObject.AddComponent<Rigidbody2D>().freezeRotation = true;
			this.naturalCollider.enabled = true;
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x0003ECD4 File Offset: 0x0003CED4
		private void RemoveRigidbody()
		{
			Object.Destroy(base.GetComponent<Rigidbody2D>());
			this.naturalCollider.enabled = false;
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x0003ECED File Offset: 0x0003CEED
		private void OnMouseUp()
		{
			if (this.naturalCollider.enabled)
			{
				this.Launch();
			}
		}

		// Token: 0x060057D6 RID: 22486 RVA: 0x00245E60 File Offset: 0x00244060
		private void Launch()
		{
			this.RemoveRigidbody();
			this.ragdoll.Apply();
			this.ragdoll.RootRigidbody.velocity = new Vector2(Random.Range(-this.launchVelocity.x, this.launchVelocity.x), this.launchVelocity.y);
			base.StartCoroutine(this.WaitUntilStopped());
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x0003ED02 File Offset: 0x0003CF02
		private IEnumerator Restore()
		{
			Vector3 estimatedSkeletonPosition = this.ragdoll.EstimatedSkeletonPosition;
			Vector3 vector = this.ragdoll.RootRigidbody.position;
			Vector3 skeletonPosition = estimatedSkeletonPosition;
			RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, estimatedSkeletonPosition - vector, Vector3.Distance(estimatedSkeletonPosition, vector), this.groundMask);
			if (raycastHit2D.collider != null)
			{
				skeletonPosition = raycastHit2D.point;
			}
			this.ragdoll.RootRigidbody.isKinematic = true;
			this.ragdoll.SetSkeletonPosition(skeletonPosition);
			yield return this.ragdoll.SmoothMix(0f, this.restoreDuration);
			this.ragdoll.Remove();
			this.AddRigidbody();
			yield break;
		}

		// Token: 0x060057D8 RID: 22488 RVA: 0x0003ED11 File Offset: 0x0003CF11
		private IEnumerator WaitUntilStopped()
		{
			yield return new WaitForSeconds(0.5f);
			float t = 0f;
			while (t < 0.5f)
			{
				t = ((this.ragdoll.RootRigidbody.velocity.magnitude > 0.09f) ? 0f : (t + Time.deltaTime));
				yield return null;
			}
			base.StartCoroutine(this.Restore());
			yield break;
		}

		// Token: 0x040057D8 RID: 22488
		public LayerMask groundMask;

		// Token: 0x040057D9 RID: 22489
		public float restoreDuration = 0.5f;

		// Token: 0x040057DA RID: 22490
		public Vector2 launchVelocity = new Vector2(50f, 100f);

		// Token: 0x040057DB RID: 22491
		private SkeletonRagdoll2D ragdoll;

		// Token: 0x040057DC RID: 22492
		private Collider2D naturalCollider;
	}
}
