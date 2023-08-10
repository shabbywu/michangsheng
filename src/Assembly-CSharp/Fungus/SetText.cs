using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fungus;

[CommandInfo("UI", "Set Text", "Sets the text property on a UI Text object and/or an Input Field object.", 0)]
[AddComponentMenu("")]
public class SetText : Command, ILocalizable
{
	[Tooltip("Text object to set text on. Can be a UI Text, Text Field or Text Mesh object.")]
	[SerializeField]
	protected GameObject targetTextObject;

	[Tooltip("String value to assign to the text object")]
	[FormerlySerializedAs("stringData")]
	[SerializeField]
	protected StringDataMulti text;

	[Tooltip("Notes about this story text for other authors, localization, etc.")]
	[SerializeField]
	protected string description;

	[HideInInspector]
	[FormerlySerializedAs("textObject")]
	public Text _textObjectObsolete;

	public override void OnEnter()
	{
		string text = GetFlowchart().SubstituteVariables(this.text.Value);
		if ((Object)(object)targetTextObject == (Object)null)
		{
			Continue();
			return;
		}
		TextAdapter textAdapter = new TextAdapter();
		textAdapter.InitFromGameObject(targetTextObject);
		if (textAdapter.HasTextObject())
		{
			textAdapter.Text = text;
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)targetTextObject != (Object)null)
		{
			return ((Object)targetTextObject).name + " : " + text.Value;
		}
		return "Error: No text object selected";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)text.stringRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
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
		return "SETTEXT." + GetFlowchartLocalizationId() + "." + itemId;
	}

	protected virtual void OnEnable()
	{
		if ((Object)(object)_textObjectObsolete != (Object)null)
		{
			targetTextObject = ((Component)_textObjectObsolete).gameObject;
		}
	}
}
