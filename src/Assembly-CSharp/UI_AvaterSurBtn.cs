using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_AvaterSurBtn : MonoBehaviour
{
	public int AvaterType = 1;

	public int AvaterSurface = 1;

	private void Start()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		((UnityEvent)((Component)this).GetComponent<Button>().onClick).AddListener(new UnityAction(ChoiceAvaterSurface));
	}

	public void ChoiceAvaterSurface()
	{
		Event.fireOut("ChoiceAvaterSurface", AvaterSurface);
	}
}
