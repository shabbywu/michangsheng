using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(UIToggle))]
[AddComponentMenu("NGUI/Interaction/Toggled Components")]
public class UIToggledComponents : MonoBehaviour
{
	public List<MonoBehaviour> activate;

	public List<MonoBehaviour> deactivate;

	[HideInInspector]
	[SerializeField]
	private MonoBehaviour target;

	[HideInInspector]
	[SerializeField]
	private bool inverse;

	private void Awake()
	{
		if ((Object)(object)target != (Object)null)
		{
			if (activate.Count == 0 && deactivate.Count == 0)
			{
				if (inverse)
				{
					deactivate.Add(target);
				}
				else
				{
					activate.Add(target);
				}
			}
			else
			{
				target = null;
			}
		}
		EventDelegate.Add(((Component)this).GetComponent<UIToggle>().onChange, Toggle);
	}

	public void Toggle()
	{
		if (((Behaviour)this).enabled)
		{
			for (int i = 0; i < activate.Count; i++)
			{
				((Behaviour)activate[i]).enabled = UIToggle.current.value;
			}
			for (int j = 0; j < deactivate.Count; j++)
			{
				((Behaviour)deactivate[j]).enabled = !UIToggle.current.value;
			}
		}
	}
}
