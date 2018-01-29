using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class AO_SceneSwitch : MonoBehaviour {

	[SerializeField] bool isIntro;
	[SerializeField] VideoPlayer videoPlayer;
    // Update is called once per frame

    private void Awake()
    {
        if(isIntro) StartCoroutine(DelayedSwitch(36f));
    }

    private IEnumerator DelayedSwitch(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(1);
    }

	public void CaptainWinScene()
	{
		SceneManager.LoadScene("CaptainWinScene");

	}

	public void EngineerWinScene()
	{
		SceneManager.LoadScene("EngineerWinScene");
		
	}

	public void BiologistWinScene()
	{
		SceneManager.LoadScene("BiologistWinScene");
		
	}
}
