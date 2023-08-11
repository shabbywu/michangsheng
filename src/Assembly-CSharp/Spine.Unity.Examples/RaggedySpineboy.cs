using System.Collections;
using Spine.Unity.Modules;
using UnityEngine;

namespace Spine.Unity.Examples;

public class RaggedySpineboy : MonoBehaviour
{
	public LayerMask groundMask;

	public float restoreDuration = 0.5f;

	public Vector2 launchVelocity = new Vector2(50f, 100f);

	private SkeletonRagdoll2D ragdoll;

	private Collider2D naturalCollider;

	private void Start()
	{
		ragdoll = ((Component)this).GetComponent<SkeletonRagdoll2D>();
		naturalCollider = ((Component)this).GetComponent<Collider2D>();
	}

	private void AddRigidbody()
	{
		((Component)this).gameObject.AddComponent<Rigidbody2D>().freezeRotation = true;
		((Behaviour)naturalCollider).enabled = true;
	}

	private void RemoveRigidbody()
	{
		Object.Destroy((Object)(object)((Component)this).GetComponent<Rigidbody2D>());
		((Behaviour)naturalCollider).enabled = false;
	}

	private void OnMouseUp()
	{
		if (((Behaviour)naturalCollider).enabled)
		{
			Launch();
		}
	}

	private void Launch()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		RemoveRigidbody();
		ragdoll.Apply();
		ragdoll.RootRigidbody.velocity = new Vector2(Random.Range(0f - launchVelocity.x, launchVelocity.x), launchVelocity.y);
		((MonoBehaviour)this).StartCoroutine(WaitUntilStopped());
	}

	private IEnumerator Restore()
	{
		Vector3 estimatedSkeletonPosition = ragdoll.EstimatedSkeletonPosition;
		Vector3 val = Vector2.op_Implicit(ragdoll.RootRigidbody.position);
		Vector3 skeletonPosition = estimatedSkeletonPosition;
		RaycastHit2D val2 = Physics2D.Raycast(Vector2.op_Implicit(val), Vector2.op_Implicit(estimatedSkeletonPosition - val), Vector3.Distance(estimatedSkeletonPosition, val), LayerMask.op_Implicit(groundMask));
		if ((Object)(object)((RaycastHit2D)(ref val2)).collider != (Object)null)
		{
			skeletonPosition = Vector2.op_Implicit(((RaycastHit2D)(ref val2)).point);
		}
		ragdoll.RootRigidbody.isKinematic = true;
		ragdoll.SetSkeletonPosition(skeletonPosition);
		yield return ragdoll.SmoothMix(0f, restoreDuration);
		ragdoll.Remove();
		AddRigidbody();
	}

	private IEnumerator WaitUntilStopped()
	{
		yield return (object)new WaitForSeconds(0.5f);
		float t = 0f;
		while (t < 0.5f)
		{
			Vector2 velocity = ragdoll.RootRigidbody.velocity;
			t = ((((Vector2)(ref velocity)).magnitude > 0.09f) ? 0f : (t + Time.deltaTime));
			yield return null;
		}
		((MonoBehaviour)this).StartCoroutine(Restore());
	}
}
