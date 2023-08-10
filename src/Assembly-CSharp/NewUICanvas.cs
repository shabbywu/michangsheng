using UnityEngine;
using UnityEngine.UI;

public class NewUICanvas : MonoBehaviour
{
	public static NewUICanvas Inst;

	[HideInInspector]
	public Canvas Canvas;

	private CanvasScaler scaler;

	public Camera Camera;

	private void Awake()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		Inst = this;
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
		((Component)this).transform.position = new Vector3(-8000f, 0f, 0f);
		Canvas = ((Component)this).GetComponent<Canvas>();
		scaler = ((Component)this).GetComponent<CanvasScaler>();
	}

	private void Update()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		float num = (float)Screen.height / (float)Screen.width;
		scaler.referenceResolution = new Vector2(1080f / num, 1080f);
		if (PlayerEx.Player == null)
		{
			Canvas.worldCamera = null;
		}
		else
		{
			Canvas.worldCamera = Camera;
		}
	}
}
