using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fungus;

[EventHandlerInfo("UI", "Button Clicked", "The block will execute when the user clicks on the target UI button object.")]
[AddComponentMenu("")]
public class ButtonClicked : EventHandler
{
	[Tooltip("The UI Button that the user can click on")]
	[SerializeField]
	protected Button targetButton;

	public virtual void Start()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		if ((Object)(object)targetButton != (Object)null)
		{
			((UnityEvent)targetButton.onClick).AddListener(new UnityAction(OnButtonClick));
		}
	}

	protected virtual void OnButtonClick()
	{
		ExecuteBlock();
	}

	public override string GetSummary()
	{
		if ((Object)(object)targetButton != (Object)null)
		{
			return ((Object)targetButton).name;
		}
		return "None";
	}
}
