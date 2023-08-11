using UnityEngine;
using UnityEngine.EventSystems;

namespace Fungus;

public class DialogInput : MonoBehaviour
{
	[Tooltip("Click to advance story")]
	[SerializeField]
	protected ClickMode clickMode;

	[Tooltip("Delay between consecutive clicks. Useful to prevent accidentally clicking through story.")]
	[SerializeField]
	protected float nextClickDelay;

	[Tooltip("Allow holding Cancel to fast forward text")]
	[SerializeField]
	protected bool cancelEnabled = true;

	[Tooltip("Ignore input if a Menu dialog is currently active")]
	[SerializeField]
	protected bool ignoreMenuClicks = true;

	protected bool dialogClickedFlag;

	protected bool nextLineInputFlag;

	protected float ignoreClickTimer;

	protected StandaloneInputModule currentStandaloneInputModule;

	protected Writer writer;

	protected virtual void Awake()
	{
		writer = ((Component)this).GetComponent<Writer>();
		CheckEventSystem();
	}

	protected virtual void CheckEventSystem()
	{
		if ((Object)(object)Object.FindObjectOfType<EventSystem>() == (Object)null)
		{
			GameObject val = Resources.Load<GameObject>("Prefabs/EventSystem");
			if ((Object)(object)val != (Object)null)
			{
				((Object)Object.Instantiate<GameObject>(val)).name = "EventSystem";
			}
		}
	}

	protected virtual void Update()
	{
		if ((Object)(object)EventSystem.current == (Object)null)
		{
			return;
		}
		if ((Object)(object)currentStandaloneInputModule == (Object)null)
		{
			currentStandaloneInputModule = ((Component)EventSystem.current).GetComponent<StandaloneInputModule>();
		}
		if ((Object)(object)writer != (Object)null && writer.IsWriting && (Input.GetButtonDown(currentStandaloneInputModule.submitButton) || (cancelEnabled && Input.GetButton(currentStandaloneInputModule.cancelButton))))
		{
			SetNextLineFlag();
		}
		switch (clickMode)
		{
		case ClickMode.ClickAnywhere:
			if (Input.GetMouseButtonDown(0))
			{
				SetNextLineFlag();
			}
			break;
		case ClickMode.ClickOnDialog:
			if (dialogClickedFlag)
			{
				SetNextLineFlag();
				dialogClickedFlag = false;
			}
			break;
		}
		if (ignoreClickTimer > 0f)
		{
			ignoreClickTimer = Mathf.Max(ignoreClickTimer - Time.deltaTime, 0f);
		}
		if (ignoreMenuClicks && (Object)(object)MenuDialog.ActiveMenuDialog != (Object)null && MenuDialog.ActiveMenuDialog.IsActive() && MenuDialog.ActiveMenuDialog.DisplayedOptionsCount > 0)
		{
			dialogClickedFlag = false;
			nextLineInputFlag = false;
		}
		if (nextLineInputFlag)
		{
			IDialogInputListener[] componentsInChildren = ((Component)this).gameObject.GetComponentsInChildren<IDialogInputListener>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].OnNextLineEvent();
			}
			nextLineInputFlag = false;
		}
	}

	public virtual void SetNextLineFlag()
	{
		nextLineInputFlag = true;
	}

	public virtual void SetDialogClickedFlag()
	{
		if (!(ignoreClickTimer > 0f))
		{
			ignoreClickTimer = nextClickDelay;
			if (clickMode == ClickMode.ClickOnDialog)
			{
				dialogClickedFlag = true;
			}
		}
	}

	public virtual void SetButtonClickedFlag()
	{
		if (clickMode != 0)
		{
			SetNextLineFlag();
		}
	}
}
