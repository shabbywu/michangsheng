using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x0200051F RID: 1311
public class UIShowCtr : MonoBehaviour
{
	// Token: 0x060021B2 RID: 8626 RVA: 0x0001BB36 File Offset: 0x00019D36
	private void Awake()
	{
		this.refreshAction = delegate(MessageData d)
		{
			this.Refresh(d.valueBool);
		};
		MessageMag.Instance.Register(MessageName.MSG_Sea_TanSuoDu_Refresh, this.refreshAction);
	}

	// Token: 0x060021B3 RID: 8627 RVA: 0x0001BB5F File Offset: 0x00019D5F
	private void OnEnable()
	{
		this.Refresh(false);
	}

	// Token: 0x060021B4 RID: 8628 RVA: 0x0001BB68 File Offset: 0x00019D68
	private void OnDestroy()
	{
		MessageMag.Instance.Remove(MessageName.MSG_Sea_TanSuoDu_Refresh, this.refreshAction);
	}

	// Token: 0x060021B5 RID: 8629 RVA: 0x00118D48 File Offset: 0x00116F48
	public void Refresh(bool moveCamera = false)
	{
		if (GlobalValue.Get(this.Id, "UIShowCtr.Refresh") == 0)
		{
			base.gameObject.SetActive(false);
			return;
		}
		MapSeaCompent component = base.transform.parent.parent.GetComponent<MapSeaCompent>();
		component.Refresh();
		base.gameObject.SetActive(true);
		if (moveCamera && CamaraFollow.Inst != null)
		{
			Vector2 vector;
			vector..ctor(component.transform.position.x, component.transform.position.y);
			vector = CamaraFollow.Inst.LimitPos(vector);
			ShortcutExtensions.DOMove(CamaraFollow.Inst.transform, new Vector3(vector.x, vector.y, CamaraFollow.Inst.transform.position.z), 2f, false);
		}
	}

	// Token: 0x04001D38 RID: 7480
	public int Id;

	// Token: 0x04001D39 RID: 7481
	private Action<MessageData> refreshAction;
}
