namespace Bag;

public interface IItem
{
	void SetItem(int id, int count, JSONObject seid);

	void SetItem(int id, int count);

	void Use();

	int GetPlayerPrice();

	int GetPrice();
}
