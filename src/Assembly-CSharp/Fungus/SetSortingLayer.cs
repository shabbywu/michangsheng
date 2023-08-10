using UnityEngine;

namespace Fungus;

[CommandInfo("Sprite", "Set Sorting Layer", "Sets the Renderer sorting layer of every child of a game object. Applies to all Renderers (including mesh, skinned mesh, and sprite).", 0)]
[AddComponentMenu("")]
public class SetSortingLayer : Command
{
	[Tooltip("Root Object that will have the Sorting Layer set. Any children will also be affected")]
	[SerializeField]
	protected GameObject targetObject;

	[Tooltip("The New Layer Name to apply")]
	[SerializeField]
	protected string sortingLayer;

	protected void ApplySortingLayer(Transform target, string layerName)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		Renderer component = ((Component)target).gameObject.GetComponent<Renderer>();
		if (Object.op_Implicit((Object)(object)component))
		{
			component.sortingLayerName = layerName;
			Debug.Log((object)((Object)target).name);
		}
		foreach (Transform item in ((Component)target).transform)
		{
			Transform target2 = item;
			ApplySortingLayer(target2, layerName);
		}
	}

	public override void OnEnter()
	{
		if ((Object)(object)targetObject != (Object)null)
		{
			ApplySortingLayer(targetObject.transform, sortingLayer);
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)targetObject == (Object)null)
		{
			return "Error: No game object selected";
		}
		return ((Object)targetObject).name;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}
}
