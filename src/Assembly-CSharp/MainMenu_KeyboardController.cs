using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000107 RID: 263
public class MainMenu_KeyboardController : MonoBehaviour
{
	// Token: 0x06000C03 RID: 3075 RVA: 0x00048EC1 File Offset: 0x000470C1
	private void Start()
	{
		this.currentSelectedGameobject = this.eventSystem.currentSelectedGameObject;
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x00048ED4 File Offset: 0x000470D4
	private void Update()
	{
		if (this.eventSystem.currentSelectedGameObject != this.currentSelectedGameobject)
		{
			if (this.eventSystem.currentSelectedGameObject == null)
			{
				this.eventSystem.SetSelectedGameObject(this.currentSelectedGameobject);
				return;
			}
			this.currentSelectedGameobject = this.eventSystem.currentSelectedGameObject;
		}
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x00048F2F File Offset: 0x0004712F
	public void SetNextSelectedGameobject(GameObject NextGameObject)
	{
		if (NextGameObject != null)
		{
			this.currentSelectedGameobject = NextGameObject;
			this.eventSystem.SetSelectedGameObject(this.currentSelectedGameobject);
		}
	}

	// Token: 0x0400085B RID: 2139
	public EventSystem eventSystem;

	// Token: 0x0400085C RID: 2140
	public GameObject currentSelectedGameobject;
}
