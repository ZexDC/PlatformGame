using UnityEngine;

public static class GameEventManager {
	
	public delegate void GameEvent();
	
	public static event GameEvent GameStart, GameOver;
	
	public static void TriggerGameStart(){
		if(GameStart != null){
			GameStart();
		}
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit(); 
	}
	
	public static void TriggerGameOver(){
		if(GameOver != null){
			GameOver();
		}
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit(); 
	}
}