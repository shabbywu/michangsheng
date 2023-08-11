using UnityEngine;

namespace Fungus;

[EventHandlerInfo("Scene", "Flowchart Enabled", "The block will execute when the Flowchart game object is enabled.")]
[AddComponentMenu("")]
public class FlowchartEnabled : EventHandler
{
	protected virtual void OnEnable()
	{
		((MonoBehaviour)this).Invoke("DoEvent", 0f);
	}

	protected virtual void DoEvent()
	{
		ExecuteBlock();
	}
}
