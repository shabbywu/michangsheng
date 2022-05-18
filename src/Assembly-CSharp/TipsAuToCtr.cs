using System;
using UnityEngine;

// Token: 0x02000558 RID: 1368
public class TipsAuToCtr : MonoBehaviour
{
	// Token: 0x060022E8 RID: 8936 RVA: 0x0001C787 File Offset: 0x0001A987
	public void Init(Camera camera, float x, float y, float LineY)
	{
		this.StartX = x;
		this.NextScenePostion = new Vector3(x, LineY);
		this.BottomScenePostion = new Vector3(x, y);
		this._isFirst = true;
		this._camera = camera;
	}

	// Token: 0x060022E9 RID: 8937 RVA: 0x001200EC File Offset: 0x0011E2EC
	public Vector3 GetNextPostionToLeft(Rect rect, float jianGe)
	{
		Vector3 result = this._camera.ScreenToWorldPoint(new Vector2(this.NextScenePostion.x - rect.width - jianGe, this.NextScenePostion.y));
		this.NextScenePostion.y = this.NextScenePostion.y - rect.height - jianGe;
		return result;
	}

	// Token: 0x060022EA RID: 8938 RVA: 0x00120150 File Offset: 0x0011E350
	public Vector3 GetNextPostionToRight(Rect rect, float jianGe)
	{
		Vector3 result = this._camera.ScreenToWorldPoint(new Vector2(this.NextScenePostion.x + rect.width + jianGe, this.NextScenePostion.y));
		this.NextScenePostion.y = this.NextScenePostion.y - rect.height - jianGe;
		return result;
	}

	// Token: 0x060022EB RID: 8939 RVA: 0x001201B4 File Offset: 0x0011E3B4
	public Vector3 GetNextPostionToBottom(Rect rect, float jianGe)
	{
		Vector3 result = this._camera.ScreenToWorldPoint(new Vector2(this.BottomScenePostion.x, this.BottomScenePostion.y));
		this.BottomScenePostion.y = this.BottomScenePostion.y - rect.height - jianGe;
		return result;
	}

	// Token: 0x04001DF0 RID: 7664
	public float StartX;

	// Token: 0x04001DF1 RID: 7665
	public Vector3 NextScenePostion;

	// Token: 0x04001DF2 RID: 7666
	public Vector3 BottomScenePostion;

	// Token: 0x04001DF3 RID: 7667
	private bool _isFirst;

	// Token: 0x04001DF4 RID: 7668
	private Camera _camera;
}
