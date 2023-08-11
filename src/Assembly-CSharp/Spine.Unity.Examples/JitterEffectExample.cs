using UnityEngine;

namespace Spine.Unity.Examples;

public class JitterEffectExample : MonoBehaviour
{
	[Range(0f, 0.8f)]
	public float jitterMagnitude = 0.2f;

	private SkeletonRenderer skeletonRenderer;

	private void OnEnable()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		skeletonRenderer = ((Component)this).GetComponent<SkeletonRenderer>();
		if (!((Object)(object)skeletonRenderer == (Object)null))
		{
			skeletonRenderer.OnPostProcessVertices -= new MeshGeneratorDelegate(ProcessVertices);
			skeletonRenderer.OnPostProcessVertices += new MeshGeneratorDelegate(ProcessVertices);
			Debug.Log((object)"Jitter Effect Enabled.");
		}
	}

	private void ProcessVertices(MeshGeneratorBuffers buffers)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		if (((Behaviour)this).enabled)
		{
			int vertexCount = buffers.vertexCount;
			Vector3[] vertexBuffer = buffers.vertexBuffer;
			for (int i = 0; i < vertexCount; i++)
			{
				ref Vector3 reference = ref vertexBuffer[i];
				reference += Vector2.op_Implicit(Random.insideUnitCircle * jitterMagnitude);
			}
		}
	}

	private void OnDisable()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		if (!((Object)(object)skeletonRenderer == (Object)null))
		{
			skeletonRenderer.OnPostProcessVertices -= new MeshGeneratorDelegate(ProcessVertices);
			Debug.Log((object)"Jitter Effect Disabled.");
		}
	}
}
