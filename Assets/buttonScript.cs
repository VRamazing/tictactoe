using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class buttonScript : MonoBehaviour {

	public Button button;
	public Text buttonText;

	private GameController gameController;
	private string playerSide; 

	public void SetSpace(){
		string whichSide =  gameController.getWhichSide();
		playerSide = gameController.playerSide;
		if (whichSide == playerSide && gameController.gameStarted) {
			buttonText.text = whichSide;
			button.interactable = false;
			gameController.EndTurn ();
		}
	}

	public void SetGameControllerReference(GameController controller){
		gameController = controller;
	}

}
