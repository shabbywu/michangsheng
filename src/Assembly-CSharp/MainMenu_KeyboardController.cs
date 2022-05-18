using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200018A RID: 394
public class MainMenu_KeyboardController : MonoBehaviour
{
	// Token: 0x06000D1E RID: 3358 RVA: 0x0000ED83 File Offset: 0x0000CF83
	private void Start()
	{
		this.currentSelectedGameobject = this.eventSystem.currentSelectedGameObject;
	}

	// Token: 0x06000D1F RID: 3359 RVA: 0x0009AAC8 File Offset: 0x00098CC8
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

	// Token: 0x06000D20 RID: 3360 RVA: 0x0000ED96 File Offset: 0x0000CF96
	public void SetNextSelectedGameobject(GameObject NextGameObject)
	{
		if (NextGameObject != null)
		{
			this.currentSelectedGameobject = NextGameObject;
			this.eventSystem.SetSelectedGameObject(this.currentSelectedGameobject);
		}
	}

	// Token: 0x04000A54 RID: 2644
	public EventSystem eventSystem;

	// Token: 0x04000A55 RID: 2645
	public GameObject currentSelectedGameobject;
}
