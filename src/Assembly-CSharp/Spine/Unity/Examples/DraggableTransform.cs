using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AD8 RID: 2776
	public class DraggableTransform : MonoBehaviour
	{
		// Token: 0x06004DC8 RID: 19912 RVA: 0x00214057 File Offset: 0x00212257
		private void Start()
		{
			this.mainCamera = Camera.main;
		}

		// Token: 0x06004DC9 RID: 19913 RVA: 0x00214064 File Offset: 0x00212264
		private void Update()
		{
			Vector2 vector = Input.mousePosition;
			Vector2 vector2 = this.mainCamera.ScreenToWorldPoint(new Vector3(vector.x, vector.y, -this.mainCamera.transform.position.z));
			this.mouseDeltaWorld = vector2 - this.mousePreviousWorld;
			this.mousePreviousWorld = vector2;
		}

		// Token: 0x06004DCA RID: 19914 RVA: 0x002140CD File Offset: 0x002122CD
		private void OnMouseDrag()
		{
			base.transform.Translate(this.mouseDeltaWorld);
		}

		// Token: 0x04004CFB RID: 19707
		private Vector2 mousePreviousWorld;

		// Token: 0x04004CFC RID: 19708
		private Vector2 mouseDeltaWorld;

		// Token: 0x04004CFD RID: 19709
		private Camera mainCamera;
	}
}
