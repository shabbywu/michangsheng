using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001399 RID: 5017
	public class InfoText : MonoBehaviour
	{
		// Token: 0x06007985 RID: 31109 RVA: 0x00052F83 File Offset: 0x00051183
		protected virtual void OnGUI()
		{
			GUI.Label(new Rect(0f, 0f, (float)(Screen.width / 2), (float)Screen.height), this.info);
		}

		// Token: 0x04006938 RID: 26936
		[Tooltip("The information text to display")]
		[TextArea(20, 20)]
		[SerializeField]
		protected string info = "";
	}
}
