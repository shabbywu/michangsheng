using UnityEngine;

public class TipsAuToCtr : MonoBehaviour
{
	public float StartX;

	public Vector3 NextScenePostion;

	public Vector3 BottomScenePostion;

	private bool _isFirst;

	private Camera _camera;

	public void Init(Camera camera, float x, float y, float LineY)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		StartX = x;
		NextScenePostion = new Vector3(x, LineY);
		BottomScenePostion = new Vector3(x, y);
		_isFirst = true;
		_camera = camera;
	}

	public Vector3 GetNextPostionToLeft(Rect rect, float jianGe)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		Vector3 result = _camera.ScreenToWorldPoint(Vector2.op_Implicit(new Vector2(NextScenePostion.x - ((Rect)(ref rect)).width - jianGe, NextScenePostion.y)));
		NextScenePostion.y = NextScenePostion.y - ((Rect)(ref rect)).height - jianGe;
		return result;
	}

	public Vector3 GetNextPostionToRight(Rect rect, float jianGe)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		Vector3 result = _camera.ScreenToWorldPoint(Vector2.op_Implicit(new Vector2(NextScenePostion.x + ((Rect)(ref rect)).width + jianGe, NextScenePostion.y)));
		NextScenePostion.y = NextScenePostion.y - ((Rect)(ref rect)).height - jianGe;
		return result;
	}

	public Vector3 GetNextPostionToBottom(Rect rect, float jianGe)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		Vector3 result = _camera.ScreenToWorldPoint(Vector2.op_Implicit(new Vector2(BottomScenePostion.x, BottomScenePostion.y)));
		BottomScenePostion.y = BottomScenePostion.y - ((Rect)(ref rect)).height - jianGe;
		return result;
	}
}
