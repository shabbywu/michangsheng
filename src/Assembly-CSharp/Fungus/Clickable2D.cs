using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fungus
{
	// Token: 0x020012C3 RID: 4803
	public class Clickable2D : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
	{
		// Token: 0x06007459 RID: 29785 RVA: 0x0004F6B8 File Offset: 0x0004D8B8
		protected virtual void ChangeCursor(Texture2D cursorTexture)
		{
			if (!this.clickEnabled)
			{
				return;
			}
			Cursor.SetCursor(cursorTexture, Vector2.zero, 0);
		}

		// Token: 0x0600745A RID: 29786 RVA: 0x0004F6CF File Offset: 0x0004D8CF
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

		// Token: 0x0600745B RID: 29787 RVA: 0x002ADD84 File Offset: 0x002ABF84
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

		// Token: 0x0600745C RID: 29788 RVA: 0x002ADE18 File Offset: 0x002AC018
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

		// Token: 0x0600745D RID: 29789 RVA: 0x0004F6FE File Offset: 0x0004D8FE
		private void OnMouseDown()
		{
			if (!this.useEventSystem)
			{
				this.isCanDo = true;
			}
		}

		// Token: 0x0600745E RID: 29790 RVA: 0x0004F70F File Offset: 0x0004D90F
		protected virtual void OnMouseUp()
		{
			if (!this.useEventSystem && this.isCanDo)
			{
				this.DoPointerClick();
			}
		}

		// Token: 0x0600745F RID: 29791 RVA: 0x0004F727 File Offset: 0x0004D927
		protected virtual void OnMouseEnter()
		{
			if (!this.useEventSystem)
			{
				this.DoPointerEnter();
			}
		}

		// Token: 0x06007460 RID: 29792 RVA: 0x0004F737 File Offset: 0x0004D937
		protected virtual void OnMouseExit()
		{
			if (!this.useEventSystem)
			{
				this.DoPointerExit();
				this.isCanDo = false;
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (set) Token: 0x06007461 RID: 29793 RVA: 0x0004F74E File Offset: 0x0004D94E
		public bool ClickEnabled
		{
			set
			{
				this.clickEnabled = value;
			}
		}

		// Token: 0x06007462 RID: 29794 RVA: 0x0004F757 File Offset: 0x0004D957
		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.useEventSystem)
			{
				this.DoPointerClick();
			}
		}

		// Token: 0x06007463 RID: 29795 RVA: 0x0004F767 File Offset: 0x0004D967
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.useEventSystem)
			{
				this.DoPointerEnter();
			}
		}

		// Token: 0x06007464 RID: 29796 RVA: 0x0004F777 File Offset: 0x0004D977
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.useEventSystem)
			{
				this.DoPointerExit();
			}
		}

		// Token: 0x04006627 RID: 26151
		[Tooltip("Is object clicking enabled")]
		[SerializeField]
		protected bool clickEnabled = true;

		// Token: 0x04006628 RID: 26152
		[Tooltip("Mouse texture to use when hovering mouse over object")]
		[SerializeField]
		protected Texture2D hoverCursor;

		// Token: 0x04006629 RID: 26153
		[Tooltip("Use the UI Event System to check for clicks. Clicks that hit an overlapping UI object will be ignored. Camera must have a PhysicsRaycaster component, or a Physics2DRaycaster for 2D colliders.")]
		[SerializeField]
		protected bool useEventSystem;

		// Token: 0x0400662A RID: 26154
		public bool ShouldScale = true;

		// Token: 0x0400662B RID: 26155
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

		// Token: 0x0400662C RID: 26156
		private bool isIn;

		// Token: 0x0400662D RID: 26157
		private bool isCanDo = true;

		// Token: 0x0400662E RID: 26158
		private Vector3 oriScale;
	}
}
