using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Center Scroll View on Click")]
public class UICenterOnClick : MonoBehaviour
{
	private void OnClick()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		UICenterOnChild uICenterOnChild = NGUITools.FindInParents<UICenterOnChild>(((Component)this).gameObject);
		UIPanel uIPanel = NGUITools.FindInParents<UIPanel>(((Component)this).gameObject);
		if ((Object)(object)uICenterOnChild != (Object)null)
		{
			if (((Behaviour)uICenterOnChild).enabled)
			{
				uICenterOnChild.CenterOn(((Component)this).transform);
			}
		}
		else if ((Object)(object)uIPanel != (Object)null && uIPanel.clipping != 0)
		{
			UIScrollView component = ((Component)uIPanel).GetComponent<UIScrollView>();
			Vector3 pos = -uIPanel.cachedTransform.InverseTransformPoint(((Component)this).transform.position);
			if (!component.canMoveHorizontally)
			{
				pos.x = uIPanel.cachedTransform.localPosition.x;
			}
			if (!component.canMoveVertically)
			{
				pos.y = uIPanel.cachedTransform.localPosition.y;
			}
			SpringPanel.Begin(uIPanel.cachedGameObject, pos, 6f);
		}
	}
}
