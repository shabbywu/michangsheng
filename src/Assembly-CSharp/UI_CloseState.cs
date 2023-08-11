using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CloseState : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	private UI_AvatarState state;

	private void Start()
	{
		state = ((Component)((Component)this).transform.parent).GetComponent<UI_AvatarState>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if ((int)eventData.button == 0)
		{
			state.closeInventory();
		}
	}
}
