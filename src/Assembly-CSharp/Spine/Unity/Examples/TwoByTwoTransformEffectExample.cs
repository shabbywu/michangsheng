using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E53 RID: 3667
	public class TwoByTwoTransformEffectExample : MonoBehaviour
	{
		// Token: 0x060057FC RID: 22524 RVA: 0x002465BC File Offset: 0x002447BC
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

		// Token: 0x060057FD RID: 22525 RVA: 0x0024661C File Offset: 0x0024481C
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

		// Token: 0x060057FE RID: 22526 RVA: 0x0003EEBD File Offset: 0x0003D0BD
		private void OnDisable()
		{
			if (this.skeletonRenderer == null)
			{
				return;
			}
			this.skeletonRenderer.OnPostProcessVertices -= new MeshGeneratorDelegate(this.ProcessVertices);
			Debug.Log("2x2 Transform Effect Disabled.");
		}

		// Token: 0x040057FC RID: 22524
		public Vector2 xAxis = new Vector2(1f, 0f);

		// Token: 0x040057FD RID: 22525
		public Vector2 yAxis = new Vector2(0f, 1f);

		// Token: 0x040057FE RID: 22526
		private SkeletonRenderer skeletonRenderer;
	}
}
