using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

[CommandInfo("Sprite", "Set Collider", "Sets all collider (2d or 3d) components on the target objects to be active / inactive", 0)]
[AddComponentMenu("")]
public class SetCollider : Command
{
	[Tooltip("A list of gameobjects containing collider components to be set active / inactive")]
	[SerializeField]
	protected List<GameObject> targetObjects = new List<GameObject>();

	[Tooltip("All objects with this tag will have their collider set active / inactive")]
	[SerializeField]
	protected string targetTag = "";

	[Tooltip("Set to true to enable the collider components")]
	[SerializeField]
	protected BooleanData activeState;

	protected virtual void SetColliderActive(GameObject go)
	{
		if ((Object)(object)go != (Object)null)
		{
			Collider[] componentsInChildren = go.GetComponentsInChildren<Collider>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = activeState.Value;
			}
			Collider2D[] componentsInChildren2 = go.GetComponentsInChildren<Collider2D>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				((Behaviour)componentsInChildren2[j]).enabled = activeState.Value;
			}
		}
	}

	public override void OnEnter()
	{
		for (int i = 0; i < targetObjects.Count; i++)
		{
			GameObject colliderActive = targetObjects[i];
			SetColliderActive(colliderActive);
		}
		GameObject[] array = null;
		try
		{
			array = GameObject.FindGameObjectsWithTag(targetTag);
		}
		catch
		{
		}
		if (array != null)
		{
			foreach (GameObject colliderActive2 in array)
			{
				SetColliderActive(colliderActive2);
			}
		}
		Continue();
	}

	public override string GetSummary()
	{
		int count = targetObjects.Count;
		if (activeState.Value)
		{
			return "Enable " + count + " collider objects.";
		}
		return "Disable " + count + " collider objects.";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool IsReorderableArray(string propertyName)
	{
		return propertyName == "targetObjects";
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)activeState.booleanRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
