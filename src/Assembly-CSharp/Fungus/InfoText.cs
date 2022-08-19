using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EF7 RID: 3831
	public class InfoText : MonoBehaviour
	{
		// Token: 0x06006BE0 RID: 27616 RVA: 0x0029733B File Offset: 0x0029553B
		protected virtual void OnGUI()
		{
			GUI.Label(new Rect(0f, 0f, (float)(Screen.width / 2), (float)Screen.height), this.info);
		}

		// Token: 0x04005ACC RID: 23244
		[Tooltip("The information text to display")]
		[TextArea(20, 20)]
		[SerializeField]
		protected string info = "";
	}
}
