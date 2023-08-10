using GUIPackage;
using UnityEngine;
using UnityEngine.EventSystems;

public class UTooltipSkillTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public int SkillID = 1;

	public int Level = 1;

	private bool isShow;

	private void Awake()
	{
		MessageMag.Instance.Register(MessageName.MSG_APP_OnFocusChanged, OnFocusChanged);
	}

	private void OnDestroy()
	{
		MessageMag.Instance.Remove(MessageName.MSG_APP_OnFocusChanged, OnFocusChanged);
	}

	public void OnFocusChanged(MessageData data)
	{
		if (isShow)
		{
			isShow = false;
			UToolTip.Close();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (SkillID > 0)
		{
			UToolTip.Show(SkillDatebase.instence.Dict[SkillID][Level].skill_Desc);
			isShow = true;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isShow = false;
		UToolTip.Close();
	}
}
