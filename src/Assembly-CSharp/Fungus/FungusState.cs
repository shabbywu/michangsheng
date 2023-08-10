using UnityEngine;

namespace Fungus;

[AddComponentMenu("")]
public class FungusState : MonoBehaviour
{
	[SerializeField]
	protected Flowchart selectedFlowchart;

	public virtual Flowchart SelectedFlowchart
	{
		get
		{
			return selectedFlowchart;
		}
		set
		{
			selectedFlowchart = value;
		}
	}
}
