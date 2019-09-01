using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class VideoControl : MonoBehaviour {

	private VideoPlayer videoPlayer;
	// Use this for initialization
	void Start () {
		videoPlayer = this.GetComponent<VideoPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playOrPause() {
		if(videoPlayer.isPlaying) {
			videoPlayer.Pause();
		}
		else {
			videoPlayer.Play();
		}
	}

	public void goTaping() {
		videoPlayer.Stop();
		
		StaticData.resetAll();
		SceneManager.LoadScene(StaticData.getTargetNumName());
	}

	public void goToLobby() {
		videoPlayer.Stop();

		SceneManager.LoadScene("testMain");
	}

	public void replay() {
		// meet the last frame
		if((ulong)videoPlayer.frame == videoPlayer.frameCount) {
			Debug.Log("finish");
			videoPlayer.Play();
		}
		else {
			Debug.Log("DO NOTHING");			
		}
	}
}
