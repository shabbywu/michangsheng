using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[RequireComponent(typeof(Block))]
[RequireComponent(typeof(Flowchart))]
[AddComponentMenu("")]
public class EventHandler : MonoBehaviour
{
	[HideInInspector]
	[FormerlySerializedAs("parentSequence")]
	[SerializeField]
	protected Block parentBlock;

	public virtual Block ParentBlock
	{
		get
		{
			return parentBlock;
		}
		set
		{
			parentBlock = value;
		}
	}

	public virtual bool ExecuteBlock()
	{
		if ((Object)(object)ParentBlock == (Object)null)
		{
			return false;
		}
		if ((Object)(object)ParentBlock._EventHandler != (Object)(object)this)
		{
			return false;
		}
		Flowchart flowchart = ParentBlock.GetFlowchart();
		if ((Object)(object)flowchart == (Object)null || !((Behaviour)flowchart).isActiveAndEnabled)
		{
			return false;
		}
		if ((Object)(object)flowchart.SelectedBlock == (Object)null)
		{
			flowchart.SelectedBlock = ParentBlock;
		}
		return flowchart.ExecuteBlock(ParentBlock);
	}

	public virtual string GetSummary()
	{
		return "";
	}
}
