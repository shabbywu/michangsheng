namespace WXB;

public interface IEffect
{
	void UpdateEffect(Draw draw, float deltaTime);

	void Release();
}
