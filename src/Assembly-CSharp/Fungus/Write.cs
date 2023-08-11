using UnityEngine;

namespace Fungus;

[CommandInfo("UI", "Write", "Writes content to a UI Text or Text Mesh object.", 0)]
[AddComponentMenu("")]
public class Write : Command, ILocalizable
{
	[Tooltip("Text object to set text on. Text, Input Field and Text Mesh objects are supported.")]
	[SerializeField]
	protected GameObject textObject;

	[Tooltip("String value to assign to the text object")]
	[SerializeField]
	protected StringDataMulti text;

	[Tooltip("Notes about this story text for other authors, localization, etc.")]
	[SerializeField]
	protected string description;

	[Tooltip("Clear existing text before writing new text")]
	[SerializeField]
	protected bool clearText = true;

	[Tooltip("Wait until this command finishes before executing the next command")]
	[SerializeField]
	protected bool waitUntilFinished = true;

	[Tooltip("Color mode to apply to the text.")]
	[SerializeField]
	protected TextColor textColor;

	[Tooltip("Alpha to apply to the text.")]
	[SerializeField]
	protected FloatData setAlpha = new FloatData(1f);

	[Tooltip("Color to apply to the text.")]
	[SerializeField]
	protected ColorData setColor = new ColorData(Color.white);

	protected Writer GetWriter()
	{
		Writer writer = textObject.GetComponent<Writer>();
		if ((Object)(object)writer == (Object)null)
		{
			writer = textObject.AddComponent<Writer>();
		}
		return writer;
	}

	public override void OnEnter()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)textObject == (Object)null)
		{
			Continue();
			return;
		}
		Writer writer = GetWriter();
		if ((Object)(object)writer == (Object)null)
		{
			Continue();
			return;
		}
		switch (textColor)
		{
		case TextColor.SetAlpha:
			writer.SetTextAlpha(setAlpha);
			break;
		case TextColor.SetColor:
			writer.SetTextColor(setColor);
			break;
		case TextColor.SetVisible:
			writer.SetTextAlpha(1f);
			break;
		}
		string content = GetFlowchart().SubstituteVariables(text.Value);
		if (!waitUntilFinished)
		{
			((MonoBehaviour)this).StartCoroutine(writer.Write(content, clearText, waitForInput: false, stopAudio: true, waitForVO: false, null, null));
			Continue();
		}
		else
		{
			((MonoBehaviour)this).StartCoroutine(writer.Write(content, clearText, waitForInput: false, stopAudio: true, waitForVO: false, null, delegate
			{
				Continue();
			}));
		}
	}

	public override string GetSummary()
	{
		if ((Object)(object)textObject != (Object)null)
		{
			return ((Object)textObject).name + " : " + text.Value;
		}
		return "Error: No text object selected";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override void OnStopExecuting()
	{
		GetWriter().Stop();
	}

	public virtual string GetStandardText()
	{
		return text;
	}

	public virtual void SetStandardText(string standardText)
	{
		text.Value = standardText;
	}

	public virtual string GetDescription()
	{
		return description;
	}

	public virtual string GetStringId()
	{
		return "WRITE." + GetFlowchartLocalizationId() + "." + itemId;
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)text.stringRef == (Object)(object)variable) && !((Object)(object)setAlpha.floatRef == (Object)(object)variable) && !((Object)(object)setColor.colorRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
