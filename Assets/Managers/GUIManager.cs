using UnityEngine;

public class GUIManager : MonoBehaviour {
	
	public GUIText boostsText, distanceText, gameOverText, instructionsText, runnerText;
	private static GUIManager instance;
	
	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameOverText.enabled = false;
	}
	
	public static void SetBoosts(int boosts){
		instance.boostsText.text = "Boost Left: " + boosts.ToString() + "\nSwipe while in the air to activate";
	}
	
	public static void SetDistance(float distance){
		instance.distanceText.text = "Distance: "+ distance.ToString("f0");
	}
	
	void Update () {
		if(Input.GetButtonDown("Jump")||(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)){
			GameEventManager.TriggerGameStart();
		}
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit(); 
	}

	private void GameStart () {
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		runnerText.enabled = false;
		enabled = false;
	}

	private void GameOver () {
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		enabled = true;
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit(); 
	}
}