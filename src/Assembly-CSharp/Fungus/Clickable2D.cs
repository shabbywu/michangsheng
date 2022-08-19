using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fungus
{
	// Token: 0x02000E66 RID: 3686
	public class Clickable2D : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
	{
		// Token: 0x060067A7 RID: 26535 RVA: 0x0028AF2E File Offset: 0x0028912E
		protected virtual void ChangeCursor(Texture2D cursorTexture)
		{
			if (!this.clickEnabled)
			{
				return;
			}
			Cursor.SetCursor(cursorTexture, Vector2.zero, 0);
		}

		// Token: 0x060067A8 RID: 26536 RVA: 0x0028AF45 File Offset: 0x00289145
		protected virtual void DoPointerClick()
		{
			if (!this.clickEnabled)
			{
				return;
			}
			if (!Tools.instance.canClick(false, true))
			{
				return;
			}
			FungusManager.Instance.EventDispatcher.Raise<ObjectClicked.ObjectClickedEvent>(new ObjectClicked.ObjectClickedEvent(this));
		}

		// Token: 0x060067A9 RID: 26537 RVA: 0x0028AF74 File Offset: 0x00289174
		protected virtual void DoPointerEnter()
		{
			MapComponent component = base.GetComponent<MapComponent>();
			bool flag = true;
			if (component != null && component.getAvatarNowMapIndex() == component.NodeIndex)
			{
				flag = false;
			}
			if (Tools.instance.canClick(false, true) && this.ShouldScale && flag)
			{
				this.ChangeCursor(this.hoverCursor);
				this.isIn = true;
			}
			if (this.ShouldScale)
			{
				this.oriScale = base.transform.localScale;
				base.transform.localScale = this.oriScale * 1.1f;
			}
		}

		// Token: 0x060067AA RID: 26538 RVA: 0x0028B008 File Offset: 0x00289208
		protected virtual void DoPointerExit()
		{
			MapComponent component = base.GetComponent<MapComponent>();
			bool flag = true;
			if (component != null && component.getAvatarNowMapIndex() == component.NodeIndex)
			{
				flag = false;
			}
			if ((Tools.instance.canClick(false, true) && this.ShouldScale && flag) || this.isIn)
			{
				SetMouseCursor.ResetMouseCursor();
				this.isIn = false;
			}
			if (this.ShouldScale)
			{
				base.transform.localScale = this.oriScale;
			}
		}

		// Token: 0x060067AB RID: 26539 RVA: 0x0028B07F File Offset: 0x0028927F
		private void OnMouseDown()
		{
			if (!this.useEventSystem)
			{
				this.isCanDo = true;
			}
		}

		// Token: 0x060067AC RID: 26540 RVA: 0x0028B090 File Offset: 0x00289290
		protected virtual void OnMouseUp()
		{
			if (!this.useEventSystem && this.isCanDo)
			{
				this.DoPointerClick();
			}
		}

		// Token: 0x060067AD RID: 26541 RVA: 0x0028B0A8 File Offset: 0x002892A8
		protected virtual void OnMouseEnter()
		{
			if (!this.useEventSystem)
			{
				this.DoPointerEnter();
			}
		}

		// Token: 0x060067AE RID: 26542 RVA: 0x0028B0B8 File Offset: 0x002892B8
		protected virtual void OnMouseExit()
		{
			if (!this.useEventSystem)
			{
				this.DoPointerExit();
				this.isCanDo = false;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (set) Token: 0x060067AF RID: 26543 RVA: 0x0028B0CF File Offset: 0x002892CF
		public bool ClickEnabled
		{
			set
			{
				this.clickEnabled = value;
			}
		}

		// Token: 0x060067B0 RID: 26544 RVA: 0x0028B0D8 File Offset: 0x002892D8
		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.useEventSystem)
			{
				this.DoPointerClick();
			}
		}

		// Token: 0x060067B1 RID: 26545 RVA: 0x0028B0E8 File Offset: 0x002892E8
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.useEventSystem)
			{
				this.DoPointerEnter();
			}
		}

		// Token: 0x060067B2 RID: 26546 RVA: 0x0028B0F8 File Offset: 0x002892F8
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.useEventSystem)
			{
				this.DoPointerExit();
			}
		}

		// Token: 0x0400588F RID: 22671
		[Tooltip("Is object clicking enabled")]
		[SerializeField]
		protected bool clickEnabled = true;

		// Token: 0x04005890 RID: 22672
		[Tooltip("Mouse texture to use when hovering mouse over object")]
		[SerializeField]
		protected Texture2D hoverCursor;

		// Token: 0x04005891 RID: 22673
		[Tooltip("Use the UI Event System to check for clicks. Clicks that hit an overlapping UI object will be ignored. Camera must have a PhysicsRaycaster component, or a Physics2DRaycaster for 2D colliders.")]
		[SerializeField]
		protected bool useEventSystem;

		// Token: 0x04005892 RID: 22674
		public bool ShouldScale = true;

		// Token: 0x04005893 RID: 22675
		private List<string> btnName = new List<string>
		{
			"likai",
			"caiji",
			"xiuxi",
			"biguan",
			"tupo",
			"shop",
			"kefang",
			"ui8",
			"yaofang",
			"shenbingge",
			"wudao",
			"chuhai",
			"shanglou",
			"liexi"
		};

		// Token: 0x04005894 RID: 22676
		private bool isIn;

		// Token: 0x04005895 RID: 22677
		private bool isCanDo = true;

		// Token: 0x04005896 RID: 22678
		private Vector3 oriScale;
	}
}
