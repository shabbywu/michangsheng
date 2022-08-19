using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AF9 RID: 2809
	public class JitterEffectExample : MonoBehaviour
	{
		// Token: 0x06004E57 RID: 20055 RVA: 0x0021654C File Offset: 0x0021474C
		private void OnEnable()
		{
			this.skeletonRenderer = base.GetComponent<SkeletonRenderer>();
			if (this.skeletonRenderer == null)
			{
				return;
			}
			this.skeletonRenderer.OnPostProcessVertices -= new MeshGeneratorDelegate(this.ProcessVertices);
			this.skeletonRenderer.OnPostProcessVertices += new MeshGeneratorDelegate(this.ProcessVertices);
			Debug.Log("Jitter Effect Enabled.");
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x002165AC File Offset: 0x002147AC
		private void ProcessVertices(MeshGeneratorBuffers buffers)
		{
			if (!base.enabled)
			{
				return;
			}
			int vertexCount = buffers.vertexCount;
			Vector3[] vertexBuffer = buffers.vertexBuffer;
			for (int i = 0; i < vertexCount; i++)
			{
				vertexBuffer[i] += Random.insideUnitCircle * this.jitterMagnitude;
			}
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x00216608 File Offset: 0x00214808
		private void OnDisable()
		{
			if (this.skeletonRenderer == null)
			{
				return;
			}
			this.skeletonRenderer.OnPostProcessVertices -= new MeshGeneratorDelegate(this.ProcessVertices);
			Debug.Log("Jitter Effect Disabled.");
		}

		// Token: 0x04004DCE RID: 19918
		[Range(0f, 0.8f)]
		public float jitterMagnitude = 0.2f;

		// Token: 0x04004DCF RID: 19919
		private SkeletonRenderer skeletonRenderer;
	}
}
