using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{

	public void OpenSpritesLink()
	{
		#if !UNITY_EDITOR
				openWindow("https://opengameart.org/content/space-shooter-redux");
		#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}