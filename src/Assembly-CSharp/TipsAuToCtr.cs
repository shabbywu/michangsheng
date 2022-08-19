using System;
using UnityEngine;

// Token: 0x020003C5 RID: 965
public class TipsAuToCtr : MonoBehaviour
{
	// Token: 0x06001F75 RID: 8053 RVA: 0x000DD4D5 File Offset: 0x000DB6D5
	public void Init(Camera camera, float x, float y, float LineY)
	{
		this.StartX = x;
		this.NextScenePostion = new Vector3(x, LineY);
		this.BottomScenePostion = new Vector3(x, y);
		this._isFirst = true;
		this._camera = camera;
	}

	// Token: 0x06001F76 RID: 8054 RVA: 0x000DD508 File Offset: 0x000DB708
	public Vector3 GetNextPostionToLeft(Rect rect, float jianGe)
	{
		Vector3 result = this._camera.ScreenToWorldPoint(new Vector2(this.NextScenePostion.x - rect.width - jianGe, this.NextScenePostion.y));
		this.NextScenePostion.y = this.NextScenePostion.y - rect.height - jianGe;
		return result;
	}

	// Token: 0x06001F77 RID: 8055 RVA: 0x000DD56C File Offset: 0x000DB76C
	public Vector3 GetNextPostionToRight(Rect rect, float jianGe)
	{
		Vector3 result = this._camera.ScreenToWorldPoint(new Vector2(this.NextScenePostion.x + rect.width + jianGe, this.NextScenePostion.y));
		this.NextScenePostion.y = this.NextScenePostion.y - rect.height - jianGe;
		return result;
	}

	// Token: 0x06001F78 RID: 8056 RVA: 0x000DD5D0 File Offset: 0x000DB7D0
	public Vector3 GetNextPostionToBottom(Rect rect, float jianGe)
	{
		Vector3 result = this._camera.ScreenToWorldPoint(new Vector2(this.BottomScenePostion.x, this.BottomScenePostion.y));
		this.BottomScenePostion.y = this.BottomScenePostion.y - rect.height - jianGe;
		return result;
	}

	// Token: 0x04001975 RID: 6517
	public float StartX;

	// Token: 0x04001976 RID: 6518
	public Vector3 NextScenePostion;

	// Token: 0x04001977 RID: 6519
	public Vector3 BottomScenePostion;

	// Token: 0x04001978 RID: 6520
	private bool _isFirst;

	// Token: 0x04001979 RID: 6521
	private Camera _camera;
}
