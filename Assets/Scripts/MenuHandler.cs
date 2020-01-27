using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuHandler : MonoBehaviour
{
	public TextMeshProUGUI highScoreText;

	private void Update()
	{
		highScoreText.text = "HighScore : " + PlayerPrefs.GetInt("HighScore", 0);

	}

	public void Play()
	{
		SceneManager.LoadScene("Game");
	}

	public void Reset()
	{
		PlayerPrefs.DeleteKey("HighScore");
		//to delete all the saved data on the device use PlayerPrefs.DeleteAll();
	}

	public void Exit()
	{
		#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
		#else
				Application.Quit();
		#endif
	}
}
