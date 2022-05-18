using System;
using UnityEngine;

namespace ToolTips
{
	// Token: 0x02000A25 RID: 2597
	public abstract class BaseToolTips : MonoBehaviour
	{
		// Token: 0x06004355 RID: 17237
		public abstract void Show(object Data);

		// Token: 0x06004356 RID: 17238 RVA: 0x00017C2D File Offset: 0x00015E2D
		public void Hide()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x001CC5B0 File Offset: 0x001CA7B0
		public void PCSetPosition()
		{
			if (this._rectTransform == null)
			{
				this._rectTransform = base.GetComponent<RectTransform>();
			}
			Rect rect = this._rectTransform.rect;
			Vector3 vector;
			vector..ctor(this.GetMousePosition().x, this.GetMousePosition().y, this.GetMousePosition().z);
			vector.x += rect.width / 2f;
			vector.y -= rect.height / 2f;
			if (Input.mousePosition.x > (float)Screen.width / 2f)
			{
				vector.x -= rect.width;
			}
			if (Input.mousePosition.y < (float)Screen.height / 2f)
			{
				vector.y += rect.height;
			}
			base.transform.position = NewUICanvas.Inst.Camera.ScreenToWorldPoint(vector);
		}

		// Token: 0x06004358 RID: 17240 RVA: 0x000301F9 File Offset: 0x0002E3F9
		private Vector3 GetMousePosition()
		{
			return Input.mousePosition;
		}

		// Token: 0x04003B5C RID: 15196
		private RectTransform _rectTransform;
	}
}
