using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x02000994 RID: 2452
	public interface Draw
	{
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06003E9D RID: 16029
		DrawType type { get; }

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06003E9E RID: 16030
		// (set) Token: 0x06003E9F RID: 16031
		long key { get; set; }

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06003EA0 RID: 16032
		// (set) Token: 0x06003EA1 RID: 16033
		Material srcMat { get; set; }

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06003EA2 RID: 16034
		// (set) Token: 0x06003EA3 RID: 16035
		Texture texture { get; set; }

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06003EA4 RID: 16036
		CanvasRenderer canvasRenderer { get; }

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06003EA5 RID: 16037
		RectTransform rectTransform { get; }

		// Token: 0x06003EA6 RID: 16038
		void UpdateSelf(float deltaTime);

		// Token: 0x06003EA7 RID: 16039
		void FillMesh(Mesh workerMesh);

		// Token: 0x06003EA8 RID: 16040
		void UpdateMaterial(Material mat);

		// Token: 0x06003EA9 RID: 16041
		void Release();

		// Token: 0x06003EAA RID: 16042
		void DestroySelf();

		// Token: 0x06003EAB RID: 16043
		void OnInit();
	}
}
