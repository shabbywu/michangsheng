using System.Collections;
using UnityEngine;

namespace Fungus;

[CommandInfo("Flow", "Wait Frames", "Waits for a number of frames before executing the next command in the block.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class WaitFrames : Command
{
	[Tooltip("Number of frames to wait for")]
	[SerializeField]
	protected IntegerData frameCount = new IntegerData(1);

	protected virtual IEnumerator WaitForFrames()
	{
		for (int count = frameCount.Value; count > 0; count--)
		{
			yield return (object)new WaitForEndOfFrame();
		}
		Continue();
	}

	public override void OnEnter()
	{
		((MonoBehaviour)this).StartCoroutine(WaitForFrames());
	}

	public override string GetSummary()
	{
		return frameCount.Value + " frames";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)frameCount.integerRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
