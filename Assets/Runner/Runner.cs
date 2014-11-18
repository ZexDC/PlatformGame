using UnityEngine;

public class Runner : MonoBehaviour {
	
	public static float distanceTraveled;	
	public float acceleration;	
	private bool touchingPlatform;
	private Vector3 startPosition;
	public float gameOverY;
	private static int boosts;
	public Vector3 boostVelocity, jumpVelocity;
	
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
	}
	
	private void GameOver () {
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit(); 
	}
	
	void Update () {
		if(Input.GetButtonDown("Jump")||(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)){
			if(touchingPlatform){
				rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
				touchingPlatform = false;
			}
			else if(boosts > 0){
				rigidbody.AddForce(boostVelocity, ForceMode.VelocityChange);
				boosts -= 1;
				GUIManager.SetBoosts(boosts);
			}
		}
		distanceTraveled = transform.localPosition.x;
		GUIManager.SetDistance(distanceTraveled);
		
		if(transform.localPosition.y < gameOverY){
			GameEventManager.TriggerGameOver();
		}
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit(); 
	}
	
	private void GameStart () {
		boosts = 0;
		GUIManager.SetBoosts(boosts);
		distanceTraveled = 0f;
		GUIManager.SetDistance(distanceTraveled);
		transform.localPosition = startPosition;
		renderer.enabled = true;
		rigidbody.isKinematic = false;
		enabled = true;
	}
	
	public static void AddBoost(){
		boosts += 1;
		GUIManager.SetBoosts(boosts);
	}
	
	void FixedUpdate () {
		if(touchingPlatform){
			rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}

	void OnCollisionEnter () {
		touchingPlatform = true;
	}
	
	void OnCollisionExit () {
		touchingPlatform = false;
	}
}