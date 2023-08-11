using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus;

public class TextAdapter : IWriterTextDestination
{
	protected Text textUI;

	protected InputField inputField;

	protected TextMesh textMesh;

	protected TMP_Text tmpro;

	protected Component textComponent;

	protected PropertyInfo textProperty;

	protected IWriterTextDestination writerTextDestination;

	public virtual string Text
	{
		get
		{
			if ((Object)(object)textUI != (Object)null)
			{
				return textUI.text;
			}
			if ((Object)(object)inputField != (Object)null)
			{
				return inputField.text;
			}
			if (writerTextDestination != null)
			{
				return Text;
			}
			if ((Object)(object)textMesh != (Object)null)
			{
				return textMesh.text;
			}
			if ((Object)(object)tmpro != (Object)null)
			{
				return tmpro.text;
			}
			if (textProperty != null)
			{
				return textProperty.GetValue(textComponent, null) as string;
			}
			return "";
		}
		set
		{
			if ((Object)(object)textUI != (Object)null)
			{
				textUI.text = value;
			}
			else if ((Object)(object)inputField != (Object)null)
			{
				inputField.text = value;
			}
			else if (writerTextDestination != null)
			{
				Text = value;
			}
			else if ((Object)(object)textMesh != (Object)null)
			{
				textMesh.text = value;
			}
			else if ((Object)(object)tmpro != (Object)null)
			{
				tmpro.text = value;
			}
			else if (textProperty != null)
			{
				textProperty.SetValue(textComponent, value, null);
			}
		}
	}

	public void InitFromGameObject(GameObject go, bool includeChildren = false)
	{
		if ((Object)(object)go == (Object)null)
		{
			return;
		}
		if (!includeChildren)
		{
			textUI = go.GetComponent<Text>();
			inputField = go.GetComponent<InputField>();
			textMesh = go.GetComponent<TextMesh>();
			tmpro = go.GetComponent<TMP_Text>();
			writerTextDestination = go.GetComponent<IWriterTextDestination>();
		}
		else
		{
			textUI = go.GetComponentInChildren<Text>();
			inputField = go.GetComponentInChildren<InputField>();
			textMesh = go.GetComponentInChildren<TextMesh>();
			tmpro = go.GetComponentInChildren<TMP_Text>();
			writerTextDestination = go.GetComponentInChildren<IWriterTextDestination>();
		}
		if (!((Object)(object)textUI == (Object)null) || !((Object)(object)inputField == (Object)null) || !((Object)(object)textMesh == (Object)null) || writerTextDestination != null)
		{
			return;
		}
		Component[] array = null;
		array = (includeChildren ? go.GetComponentsInChildren<Component>() : go.GetComponents<Component>());
		foreach (Component val in array)
		{
			textProperty = ((object)val).GetType().GetProperty("text");
			if (textProperty != null)
			{
				textComponent = val;
				break;
			}
		}
	}

	public void ForceRichText()
	{
		if ((Object)(object)textUI != (Object)null)
		{
			textUI.supportRichText = true;
		}
		if ((Object)(object)textMesh != (Object)null)
		{
			textMesh.richText = true;
		}
		if ((Object)(object)tmpro != (Object)null)
		{
			tmpro.richText = true;
		}
		if (writerTextDestination != null)
		{
			writerTextDestination.ForceRichText();
		}
	}

	public void SetTextColor(Color textColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)textUI != (Object)null)
		{
			((Graphic)textUI).color = textColor;
		}
		else if ((Object)(object)inputField != (Object)null)
		{
			if ((Object)(object)inputField.textComponent != (Object)null)
			{
				((Graphic)inputField.textComponent).color = textColor;
			}
		}
		else if ((Object)(object)textMesh != (Object)null)
		{
			textMesh.color = textColor;
		}
		else if ((Object)(object)tmpro != (Object)null)
		{
			((Graphic)tmpro).color = textColor;
		}
		else if (writerTextDestination != null)
		{
			writerTextDestination.SetTextColor(textColor);
		}
	}

	public void SetTextAlpha(float textAlpha)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)textUI != (Object)null)
		{
			Color color = ((Graphic)textUI).color;
			color.a = textAlpha;
			((Graphic)textUI).color = color;
		}
		else if ((Object)(object)inputField != (Object)null)
		{
			if ((Object)(object)inputField.textComponent != (Object)null)
			{
				Color color2 = ((Graphic)inputField.textComponent).color;
				color2.a = textAlpha;
				((Graphic)inputField.textComponent).color = color2;
			}
		}
		else if ((Object)(object)textMesh != (Object)null)
		{
			Color color3 = textMesh.color;
			color3.a = textAlpha;
			textMesh.color = color3;
		}
		else if ((Object)(object)tmpro != (Object)null)
		{
			tmpro.alpha = textAlpha;
		}
		else if (writerTextDestination != null)
		{
			writerTextDestination.SetTextAlpha(textAlpha);
		}
	}

	public void SetTextSize(int size)
	{
		if ((Object)(object)textUI != (Object)null)
		{
			textUI.fontSize = size;
		}
	}

	public bool HasTextObject()
	{
		if (!((Object)(object)textUI != (Object)null) && !((Object)(object)inputField != (Object)null) && !((Object)(object)textMesh != (Object)null) && !((Object)(object)textComponent != (Object)null) && !((Object)(object)tmpro != (Object)null))
		{
			return writerTextDestination != null;
		}
		return true;
	}

	public bool SupportsRichText()
	{
		if ((Object)(object)textUI != (Object)null)
		{
			return textUI.supportRichText;
		}
		if ((Object)(object)inputField != (Object)null)
		{
			return false;
		}
		if ((Object)(object)textMesh != (Object)null)
		{
			return textMesh.richText;
		}
		if ((Object)(object)tmpro != (Object)null)
		{
			return true;
		}
		if (writerTextDestination != null)
		{
			return writerTextDestination.SupportsRichText();
		}
		return false;
	}
}
