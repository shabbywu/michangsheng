using System.Collections;
using UnityEngine;

namespace Fungus;

[CommandInfo("Narrative", "Conversation", "Do multiple say and portrait commands in a single block of text. Format is: [character] [portrait] [stage position] [hide] [<<< | >>>] [clear | noclear] [wait | nowait] [fade | nofade] [: Story text]", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class Conversation : Command
{
	[SerializeField]
	protected StringDataMulti conversationText;

	protected ConversationManager conversationManager = new ConversationManager();

	[SerializeField]
	protected BooleanData clearPrevious = new BooleanData(v: true);

	[SerializeField]
	protected BooleanData waitForInput = new BooleanData(v: true);

	[Tooltip("a wait for seconds added to each item of the conversation.")]
	[SerializeField]
	protected FloatData waitForSeconds = new FloatData(0f);

	[SerializeField]
	protected BooleanData fadeWhenDone = new BooleanData(v: true);

	protected virtual void Start()
	{
		conversationManager.PopulateCharacterCache();
	}

	protected virtual IEnumerator DoConversation()
	{
		string conv = GetFlowchart().SubstituteVariables(conversationText.Value);
		conversationManager.ClearPrev = clearPrevious;
		conversationManager.WaitForInput = waitForInput;
		conversationManager.FadeDone = fadeWhenDone;
		conversationManager.WaitForSeconds = waitForSeconds;
		yield return ((MonoBehaviour)this).StartCoroutine(conversationManager.DoConversation(conv));
		Continue();
	}

	public override void OnEnter()
	{
		((MonoBehaviour)this).StartCoroutine(DoConversation());
	}

	public override string GetSummary()
	{
		return conversationText.Value;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)clearPrevious.booleanRef == (Object)(object)variable) && !((Object)(object)waitForInput.booleanRef == (Object)(object)variable) && !((Object)(object)waitForSeconds.floatRef == (Object)(object)variable) && !((Object)(object)fadeWhenDone.booleanRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
