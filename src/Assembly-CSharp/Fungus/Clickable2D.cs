using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fungus;

public class Clickable2D : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	[Tooltip("Is object clicking enabled")]
	[SerializeField]
	protected bool clickEnabled = true;

	[Tooltip("Mouse texture to use when hovering mouse over object")]
	[SerializeField]
	protected Texture2D hoverCursor;

	[Tooltip("Use the UI Event System to check for clicks. Clicks that hit an overlapping UI object will be ignored. Camera must have a PhysicsRaycaster component, or a Physics2DRaycaster for 2D colliders.")]
	[SerializeField]
	protected bool useEventSystem;

	public bool ShouldScale = true;

	private List<string> btnName = new List<string>
	{
		"likai", "caiji", "xiuxi", "biguan", "tupo", "shop", "kefang", "ui8", "yaofang", "shenbingge",
		"wudao", "chuhai", "shanglou", "liexi"
	};

	private bool isIn;

	private bool isCanDo = true;

	private Vector3 oriScale;

	public bool ClickEnabled
	{
		set
		{
			clickEnabled = value;
		}
	}

	protected virtual void ChangeCursor(Texture2D cursorTexture)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		if (clickEnabled)
		{
			Cursor.SetCursor(cursorTexture, Vector2.zero, (CursorMode)0);
		}
	}

	protected virtual void DoPointerClick()
	{
		if (clickEnabled && Tools.instance.canClick())
		{
			FungusManager.Instance.EventDispatcher.Raise(new ObjectClicked.ObjectClickedEvent(this));
		}
	}

	protected virtual void DoPointerEnter()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		MapComponent component = ((Component)this).GetComponent<MapComponent>();
		bool flag = true;
		if ((Object)(object)component != (Object)null && component.getAvatarNowMapIndex() == component.NodeIndex)
		{
			flag = false;
		}
		if (Tools.instance.canClick() && ShouldScale && flag)
		{
			ChangeCursor(hoverCursor);
			isIn = true;
		}
		if (ShouldScale)
		{
			oriScale = ((Component)this).transform.localScale;
			((Component)this).transform.localScale = oriScale * 1.1f;
		}
	}

	protected virtual void DoPointerExit()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		MapComponent component = ((Component)this).GetComponent<MapComponent>();
		bool flag = true;
		if ((Object)(object)component != (Object)null && component.getAvatarNowMapIndex() == component.NodeIndex)
		{
			flag = false;
		}
		if ((Tools.instance.canClick() && ShouldScale && flag) || isIn)
		{
			SetMouseCursor.ResetMouseCursor();
			isIn = false;
		}
		if (ShouldScale)
		{
			((Component)this).transform.localScale = oriScale;
		}
	}

	private void OnMouseDown()
	{
		if (!useEventSystem)
		{
			isCanDo = true;
		}
	}

	protected virtual void OnMouseUp()
	{
		if (!useEventSystem && isCanDo)
		{
			DoPointerClick();
		}
	}

	protected virtual void OnMouseEnter()
	{
		if (!useEventSystem)
		{
			DoPointerEnter();
		}
	}

	protected virtual void OnMouseExit()
	{
		if (!useEventSystem)
		{
			DoPointerExit();
			isCanDo = false;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (useEventSystem)
		{
			DoPointerClick();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (useEventSystem)
		{
			DoPointerEnter();
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (useEventSystem)
		{
			DoPointerExit();
		}
	}
}
