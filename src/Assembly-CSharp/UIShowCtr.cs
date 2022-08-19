using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000396 RID: 918
public class UIShowCtr : MonoBehaviour
{
	// Token: 0x06001E31 RID: 7729 RVA: 0x000D52D0 File Offset: 0x000D34D0
	private void Awake()
	{
		this.refreshAction = delegate(MessageData d)
		{
			this.Refresh(d.valueBool);
		};
		MessageMag.Instance.Register(MessageName.MSG_Sea_TanSuoDu_Refresh, this.refreshAction);
	}

	// Token: 0x06001E32 RID: 7730 RVA: 0x000D52F9 File Offset: 0x000D34F9
	private void OnEnable()
	{
		this.Refresh(false);
	}

	// Token: 0x06001E33 RID: 7731 RVA: 0x000D5302 File Offset: 0x000D3502
	private void OnDestroy()
	{
		MessageMag.Instance.Remove(MessageName.MSG_Sea_TanSuoDu_Refresh, this.refreshAction);
	}

	// Token: 0x06001E34 RID: 7732 RVA: 0x000D531C File Offset: 0x000D351C
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

	// Token: 0x040018CF RID: 6351
	public int Id;

	// Token: 0x040018D0 RID: 6352
	private Action<MessageData> refreshAction;
}
