using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AFA RID: 2810
	public class TwoByTwoTransformEffectExample : MonoBehaviour
	{
		// Token: 0x06004E5B RID: 20059 RVA: 0x00216650 File Offset: 0x00214850
		private void OnEnable()
		{
			this.skeletonRenderer = base.GetComponent<SkeletonRenderer>();
			if (this.skeletonRenderer == null)
			{
				return;
			}
			this.skeletonRenderer.OnPostProcessVertices -= new MeshGeneratorDelegate(this.ProcessVertices);
			this.skeletonRenderer.OnPostProcessVertices += new MeshGeneratorDelegate(this.ProcessVertices);
			Debug.Log("2x2 Transform Effect Enabled.");
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x002166B0 File Offset: 0x002148B0
		private void ProcessVertices(MeshGeneratorBuffers buffers)
		{
			if (!base.enabled)
			{
				return;
			}
			int vertexCount = buffers.vertexCount;
			Vector3[] vertexBuffer = buffers.vertexBuffer;
			Vector3 vector = default(Vector3);
			for (int i = 0; i < vertexCount; i++)
			{
				Vector3 vector2 = vertexBuffer[i];
				vector.x = this.xAxis.x * vector2.x + this.yAxis.x * vector2.y;
				vector.y = this.xAxis.y * vector2.x + this.yAxis.y * vector2.y;
				vertexBuffer[i] = vector;
			}
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x00216755 File Offset: 0x00214955
		private void OnDisable()
		{
			if (this.skeletonRenderer == null)
			{
				return;
			}
			this.skeletonRenderer.OnPostProcessVertices -= new MeshGeneratorDelegate(this.ProcessVertices);
			Debug.Log("2x2 Transform Effect Disabled.");
		}

		// Token: 0x04004DD0 RID: 19920
		public Vector2 xAxis = new Vector2(1f, 0f);

		// Token: 0x04004DD1 RID: 19921
		public Vector2 yAxis = new Vector2(0f, 1f);

		// Token: 0x04004DD2 RID: 19922
		private SkeletonRenderer skeletonRenderer;
	}
}
