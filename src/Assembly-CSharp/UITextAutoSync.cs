using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UITextAutoSync : MonoBehaviour
{
	private Text me;

	public Text Target;

	private void Awake()
	{
		me = ((Component)this).GetComponent<Text>();
	}

	private void Update()
	{
		if ((Object)(object)Target != (Object)null && me.text != Target.text)
		{
			me.text = Target.text;
		}
	}
}
