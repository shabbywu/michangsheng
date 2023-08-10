using UnityEngine;

namespace Spine.Unity.Examples;

public class TwoByTwoTransformEffectExample : MonoBehaviour
{
	public Vector2 xAxis = new Vector2(1f, 0f);

	public Vector2 yAxis = new Vector2(0f, 1f);

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
			Debug.Log((object)"2x2 Transform Effect Enabled.");
		}
	}

	private void ProcessVertices(MeshGeneratorBuffers buffers)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		if (((Behaviour)this).enabled)
		{
			int vertexCount = buffers.vertexCount;
			Vector3[] vertexBuffer = buffers.vertexBuffer;
			Vector3 val = default(Vector3);
			for (int i = 0; i < vertexCount; i++)
			{
				Vector3 val2 = vertexBuffer[i];
				val.x = xAxis.x * val2.x + yAxis.x * val2.y;
				val.y = xAxis.y * val2.x + yAxis.y * val2.y;
				vertexBuffer[i] = val;
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
			Debug.Log((object)"2x2 Transform Effect Disabled.");
		}
	}
}
