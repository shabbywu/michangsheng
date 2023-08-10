using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fungus;

[EventHandlerInfo("UI", "End Edit", "The block will execute when the user finishes editing the text in the input field.")]
[AddComponentMenu("")]
public class EndEdit : EventHandler
{
	[Tooltip("The UI Input Field that the user can enter text into")]
	[SerializeField]
	protected InputField targetInputField;

	protected virtual void Start()
	{
		((UnityEvent<string>)(object)targetInputField.onEndEdit).AddListener((UnityAction<string>)OnEndEdit);
	}

	protected virtual void OnEndEdit(string text)
	{
		ExecuteBlock();
	}

	public override string GetSummary()
	{
		if ((Object)(object)targetInputField != (Object)null)
		{
			return ((Object)targetInputField).name;
		}
		return "None";
	}
}
