using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
[AddComponentMenu("NGUI/Interaction/Center Scroll View on Click")]
public class UICenterOnClick : MonoBehaviour
{
	// Token: 0x060004D4 RID: 1236 RVA: 0x0001A6C4 File Offset: 0x000188C4
	private void OnClick()
	{
		UICenterOnChild uicenterOnChild = NGUITools.FindInParents<UICenterOnChild>(base.gameObject);
		UIPanel uipanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
		if (uicenterOnChild != null)
		{
			if (uicenterOnChild.enabled)
			{
				uicenterOnChild.CenterOn(base.transform);
				return;
			}
		}
		else if (uipanel != null && uipanel.clipping != UIDrawCall.Clipping.None)
		{
			UIScrollView component = uipanel.GetComponent<UIScrollView>();
			Vector3 pos = -uipanel.cachedTransform.InverseTransformPoint(base.transform.position);
			if (!component.canMoveHorizontally)
			{
				pos.x = uipanel.cachedTransform.localPosition.x;
			}
			if (!component.canMoveVertically)
			{
				pos.y = uipanel.cachedTransform.localPosition.y;
			}
			SpringPanel.Begin(uipanel.cachedGameObject, pos, 6f);
		}
	}
}
