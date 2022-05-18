using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E52 RID: 3666
	public class JitterEffectExample : MonoBehaviour
	{
		// Token: 0x060057F8 RID: 22520 RVA: 0x00246500 File Offset: 0x00244700
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

		// Token: 0x060057F9 RID: 22521 RVA: 0x00246560 File Offset: 0x00244760
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

		// Token: 0x060057FA RID: 22522 RVA: 0x0003EE78 File Offset: 0x0003D078
		private void OnDisable()
		{
			if (this.skeletonRenderer == null)
			{
				return;
			}
			this.skeletonRenderer.OnPostProcessVertices -= new MeshGeneratorDelegate(this.ProcessVertices);
			Debug.Log("Jitter Effect Disabled.");
		}

		// Token: 0x040057FA RID: 22522
		[Range(0f, 0.8f)]
		public float jitterMagnitude = 0.2f;

		// Token: 0x040057FB RID: 22523
		private SkeletonRenderer skeletonRenderer;
	}
}
