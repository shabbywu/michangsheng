using System;
using UnityEngine;

namespace ToolTips
{
	// Token: 0x020006E6 RID: 1766
	public abstract class BaseToolTips : MonoBehaviour
	{
		// Token: 0x060038EE RID: 14574
		public abstract void Show(object Data);

		// Token: 0x060038EF RID: 14575 RVA: 0x000B5E62 File Offset: 0x000B4062
		public void Hide()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x00184A84 File Offset: 0x00182C84
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

		// Token: 0x060038F1 RID: 14577 RVA: 0x00184B7E File Offset: 0x00182D7E
		private Vector3 GetMousePosition()
		{
			return Input.mousePosition;
		}

		// Token: 0x040030FD RID: 12541
		private RectTransform _rectTransform;
	}
}
