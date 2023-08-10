using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fungus;

[CommandInfo("UI", "Get Text", "Gets the text property from a UI Text object and stores it in a string variable.", 0)]
[AddComponentMenu("")]
public class GetText : Command
{
	[Tooltip("Text object to get text value from")]
	[SerializeField]
	protected GameObject targetTextObject;

	[Tooltip("String variable to store the text value in")]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	[SerializeField]
	protected StringVariable stringVariable;

	[HideInInspector]
	[FormerlySerializedAs("textObject")]
	public Text _textObjectObsolete;

	public override void OnEnter()
	{
		if ((Object)(object)stringVariable == (Object)null)
		{
			Continue();
			return;
		}
		TextAdapter textAdapter = new TextAdapter();
		textAdapter.InitFromGameObject(targetTextObject);
		if (textAdapter.HasTextObject())
		{
			stringVariable.Value = textAdapter.Text;
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)targetTextObject == (Object)null)
		{
			return "Error: No text object selected";
		}
		if ((Object)(object)stringVariable == (Object)null)
		{
			return "Error: No variable selected";
		}
		return ((Object)targetTextObject).name + " : " + ((Object)stringVariable).name;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)stringVariable == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected virtual void OnEnable()
	{
		if ((Object)(object)_textObjectObsolete != (Object)null)
		{
			targetTextObject = ((Component)_textObjectObsolete).gameObject;
		}
	}
}
