using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x02000686 RID: 1670
	public interface Draw
	{
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060034EB RID: 13547
		DrawType type { get; }

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060034EC RID: 13548
		// (set) Token: 0x060034ED RID: 13549
		long key { get; set; }

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060034EE RID: 13550
		// (set) Token: 0x060034EF RID: 13551
		Material srcMat { get; set; }

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060034F0 RID: 13552
		// (set) Token: 0x060034F1 RID: 13553
		Texture texture { get; set; }

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060034F2 RID: 13554
		CanvasRenderer canvasRenderer { get; }

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060034F3 RID: 13555
		RectTransform rectTransform { get; }

		// Token: 0x060034F4 RID: 13556
		void UpdateSelf(float deltaTime);

		// Token: 0x060034F5 RID: 13557
		void FillMesh(Mesh workerMesh);

		// Token: 0x060034F6 RID: 13558
		void UpdateMaterial(Material mat);

		// Token: 0x060034F7 RID: 13559
		void Release();

		// Token: 0x060034F8 RID: 13560
		void DestroySelf();

		// Token: 0x060034F9 RID: 13561
		void OnInit();
	}
}
