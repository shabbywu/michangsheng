using UnityEngine;

namespace Fungus;

public class InfoText : MonoBehaviour
{
	[Tooltip("The information text to display")]
	[TextArea(20, 20)]
	[SerializeField]
	protected string info = "";

	protected virtual void OnGUI()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		GUI.Label(new Rect(0f, 0f, (float)(Screen.width / 2), (float)Screen.height), info);
	}
}
