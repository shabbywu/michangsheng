using System;
using System.Collections;
using Spine.Unity.Modules;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AF3 RID: 2803
	public class RaggedySpineboy : MonoBehaviour
	{
		// Token: 0x06004E3D RID: 20029 RVA: 0x00215EC6 File Offset: 0x002140C6
		private void Start()
		{
			this.ragdoll = base.GetComponent<SkeletonRagdoll2D>();
			this.naturalCollider = base.GetComponent<Collider2D>();
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x00215EE0 File Offset: 0x002140E0
		private void AddRigidbody()
		{
			base.gameObject.AddComponent<Rigidbody2D>().freezeRotation = true;
			this.naturalCollider.enabled = true;
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x00215EFF File Offset: 0x002140FF
		private void RemoveRigidbody()
		{
			Object.Destroy(base.GetComponent<Rigidbody2D>());
			this.naturalCollider.enabled = false;
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x00215F18 File Offset: 0x00214118
		private void OnMouseUp()
		{
			if (this.naturalCollider.enabled)
			{
				this.Launch();
			}
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x00215F30 File Offset: 0x00214130
		private void Launch()
		{
			this.RemoveRigidbody();
			this.ragdoll.Apply();
			this.ragdoll.RootRigidbody.velocity = new Vector2(Random.Range(-this.launchVelocity.x, this.launchVelocity.x), this.launchVelocity.y);
			base.StartCoroutine(this.WaitUntilStopped());
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x00215F97 File Offset: 0x00214197
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

		// Token: 0x06004E43 RID: 20035 RVA: 0x00215FA6 File Offset: 0x002141A6
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

		// Token: 0x04004DB3 RID: 19891
		public LayerMask groundMask;

		// Token: 0x04004DB4 RID: 19892
		public float restoreDuration = 0.5f;

		// Token: 0x04004DB5 RID: 19893
		public Vector2 launchVelocity = new Vector2(50f, 100f);

		// Token: 0x04004DB6 RID: 19894
		private SkeletonRagdoll2D ragdoll;

		// Token: 0x04004DB7 RID: 19895
		private Collider2D naturalCollider;
	}
}
