using UnityEngine;

namespace WXB;

public interface Draw
{
	DrawType type { get; }

	long key { get; set; }

	Material srcMat { get; set; }

	Texture texture { get; set; }

	CanvasRenderer canvasRenderer { get; }

	RectTransform rectTransform { get; }

	void UpdateSelf(float deltaTime);

	void FillMesh(Mesh workerMesh);

	void UpdateMaterial(Material mat);

	void Release();

	void DestroySelf();

	void OnInit();
}
