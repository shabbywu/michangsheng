using UnityEngine;
using UnityEngine.UI;

namespace Fungus;

[RequireComponent(typeof(Selectable))]
public class SelectOnEnable : MonoBehaviour
{
	protected Selectable selectable;

	protected void Awake()
	{
		selectable = ((Component)this).GetComponent<Selectable>();
	}

	protected void OnEnable()
	{
		selectable.Select();
	}
}
