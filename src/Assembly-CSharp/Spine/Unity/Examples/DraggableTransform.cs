using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E1F RID: 3615
	public class DraggableTransform : MonoBehaviour
	{
		// Token: 0x06005726 RID: 22310 RVA: 0x0003E4F0 File Offset: 0x0003C6F0
		private void Start()
		{
			this.mainCamera = Camera.main;
		}

		// Token: 0x06005727 RID: 22311 RVA: 0x00243FA8 File Offset: 0x002421A8
		private void Update()
		{
			Vector2 vector = Input.mousePosition;
			Vector2 vector2 = this.mainCamera.ScreenToWorldPoint(new Vector3(vector.x, vector.y, -this.mainCamera.transform.position.z));
			this.mouseDeltaWorld = vector2 - this.mousePreviousWorld;
			this.mousePreviousWorld = vector2;
		}

		// Token: 0x06005728 RID: 22312 RVA: 0x0003E4FD File Offset: 0x0003C6FD
		private void OnMouseDrag()
		{
			base.transform.Translate(this.mouseDeltaWorld);
		}

		// Token: 0x040056E9 RID: 22249
		private Vector2 mousePreviousWorld;

		// Token: 0x040056EA RID: 22250
		private Vector2 mouseDeltaWorld;

		// Token: 0x040056EB RID: 22251
		private Camera mainCamera;
	}
}
