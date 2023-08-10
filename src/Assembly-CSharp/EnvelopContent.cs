using UnityEngine;

[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Examples/Envelop Content")]
public class EnvelopContent : MonoBehaviour
{
	public Transform targetRoot;

	public int padLeft;

	public int padRight;

	public int padBottom;

	public int padTop;

	private bool mStarted;

	private void Start()
	{
		mStarted = true;
		Execute();
	}

	private void OnEnable()
	{
		if (mStarted)
		{
			Execute();
		}
	}

	[ContextMenu("Execute")]
	public void Execute()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)targetRoot == (Object)(object)((Component)this).transform)
		{
			Debug.LogError((object)"Target Root object cannot be the same object that has Envelop Content. Make it a sibling instead.", (Object)(object)this);
			return;
		}
		if (NGUITools.IsChild(targetRoot, ((Component)this).transform))
		{
			Debug.LogError((object)"Target Root object should not be a parent of Envelop Content. Make it a sibling instead.", (Object)(object)this);
			return;
		}
		Bounds val = NGUIMath.CalculateRelativeWidgetBounds(((Component)this).transform.parent, targetRoot, considerInactive: false);
		float num = ((Bounds)(ref val)).min.x + (float)padLeft;
		float num2 = ((Bounds)(ref val)).min.y + (float)padBottom;
		float num3 = ((Bounds)(ref val)).max.x + (float)padRight;
		float num4 = ((Bounds)(ref val)).max.y + (float)padTop;
		((Component)this).GetComponent<UIWidget>().SetRect(num, num2, num3 - num, num4 - num2);
		((Component)this).BroadcastMessage("UpdateAnchors", (SendMessageOptions)1);
	}
}
