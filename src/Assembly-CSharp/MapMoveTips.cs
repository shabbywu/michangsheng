using UnityEngine;

public class MapMoveTips : MonoBehaviour
{
	public static MapMoveTips Inst;

	private void Awake()
	{
		Inst = this;
	}

	public static void Show()
	{
		if ((Object)(object)Inst == (Object)null)
		{
			ResManager.inst.LoadPrefab("MoveTips").Inst(((Component)NewUICanvas.Inst).transform);
			((Component)Inst).transform.SetAsLastSibling();
		}
		((Component)Inst).gameObject.SetActive(true);
	}

	public static void Hide()
	{
		if ((Object)(object)Inst == (Object)null)
		{
			ResManager.inst.LoadPrefab("MoveTips").Inst(((Component)NewUICanvas.Inst).transform);
			((Component)Inst).transform.SetAsLastSibling();
		}
		((Component)Inst).gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		Inst = null;
	}
}
