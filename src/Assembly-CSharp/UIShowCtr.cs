using System;
using DG.Tweening;
using UnityEngine;

public class UIShowCtr : MonoBehaviour
{
	public int Id;

	private Action<MessageData> refreshAction;

	private void Awake()
	{
		refreshAction = delegate(MessageData d)
		{
			Refresh(d.valueBool);
		};
		MessageMag.Instance.Register(MessageName.MSG_Sea_TanSuoDu_Refresh, refreshAction);
	}

	private void OnEnable()
	{
		Refresh();
	}

	private void OnDestroy()
	{
		MessageMag.Instance.Remove(MessageName.MSG_Sea_TanSuoDu_Refresh, refreshAction);
	}

	public void Refresh(bool moveCamera = false)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		if (GlobalValue.Get(Id, "UIShowCtr.Refresh") == 0)
		{
			((Component)this).gameObject.SetActive(false);
			return;
		}
		MapSeaCompent component = ((Component)((Component)this).transform.parent.parent).GetComponent<MapSeaCompent>();
		component.Refresh();
		((Component)this).gameObject.SetActive(true);
		if (moveCamera && (Object)(object)CamaraFollow.Inst != (Object)null)
		{
			Vector2 val = default(Vector2);
			((Vector2)(ref val))._002Ector(((Component)component).transform.position.x, ((Component)component).transform.position.y);
			val = CamaraFollow.Inst.LimitPos(val);
			ShortcutExtensions.DOMove(((Component)CamaraFollow.Inst).transform, new Vector3(val.x, val.y, ((Component)CamaraFollow.Inst).transform.position.z), 2f, false);
		}
	}
}
