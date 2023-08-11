using UnityEngine;
using UnityEngine.UI;

public abstract class ModalBox : MonoBehaviour
{
	[Tooltip("The title text object that will be used to show the title of the dialog.")]
	public Text Title;

	[Tooltip("The message text object that will be used to show the message of the dialog.")]
	public Text Message;

	[Tooltip("The botton object that will be used to interact with the dialog. This will be cloned to produce additional options.")]
	public Button Button;

	[Tooltip("The RectTransform of the panel that contains the frame of the dialog window. This is needed so that it can be centered correctly after it's size is adjusted to the dialogs contents.")]
	public RectTransform Panel;

	private Transform buttonParent;

	private bool isSetup;

	private int stringHeight(string str)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < str.Length; i++)
		{
			num2++;
			if (str[i] == '\n' || num2 == 20)
			{
				num2 = 0;
				num++;
			}
		}
		num++;
		return num * 17 + 15;
	}

	private void FixedUpdate()
	{
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Expected O, but got Unknown
		if (isSetup)
		{
			return;
		}
		isSetup = true;
		if ((Object)(object)Title != (Object)null)
		{
			((Component)Title).GetComponent<LayoutElement>().minHeight = stringHeight(Title.text);
		}
		if ((Object)(object)Message != (Object)null)
		{
			((Component)Message).GetComponent<LayoutElement>().minHeight = Message.preferredHeight;
			if (Message.preferredHeight < 20f)
			{
				Message.alignment = (TextAnchor)1;
			}
		}
		if (!((Object)(object)buttonParent != (Object)null))
		{
			return;
		}
		foreach (Transform item in buttonParent)
		{
			Transform val = item;
			Text componentInChildren = ((Component)val).GetComponentInChildren<Text>();
			if ((Object)(object)componentInChildren != (Object)null)
			{
				LayoutElement component = ((Component)val).GetComponent<LayoutElement>();
				if ((Object)(object)component != (Object)null)
				{
					component.minHeight = stringHeight(componentInChildren.text);
				}
			}
		}
	}

	public virtual void Close()
	{
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	protected void SetText(string message, string title)
	{
		if ((Object)(object)Button != (Object)null)
		{
			buttonParent = ((Component)Button).transform.parent;
		}
		if ((Object)(object)Title != (Object)null)
		{
			if (!string.IsNullOrEmpty(title))
			{
				Title.text = title;
			}
			else
			{
				Object.Destroy((Object)(object)((Component)Title).gameObject);
				Title = null;
			}
		}
		if ((Object)(object)Message != (Object)null)
		{
			if (!string.IsNullOrEmpty(message))
			{
				Message.text = (MessageBox.LocalizeTitleAndMessage ? MessageBox.Localize(message) : message);
				return;
			}
			Object.Destroy((Object)(object)((Component)Message).gameObject);
			Message = null;
		}
	}
}
