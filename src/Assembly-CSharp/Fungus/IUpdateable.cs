namespace Fungus;

internal interface IUpdateable
{
	void UpdateToVersion(int oldVersion, int newVersion);
}
