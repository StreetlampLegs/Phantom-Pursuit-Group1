using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour

{
		
		public void PlayIntro()
		{
			SceneManager.LoadScene(1);
		}
		public void PlayGame ()
		{
			SceneManager.LoadScene(2);
		}


		public void QuitGame ()
		{
			Debug.Log("QUIT!");
			Application.Quit();
		}
	
}

