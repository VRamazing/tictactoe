using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Text[] buttonList;
	[HideInInspector] 
	public string playerSide;
	private string whichSide;
	private string computerSide;

	public GameObject gameOverPanel;
	public GameObject choseSidePanel;

	public Text gameOverText;
	private int moveCount;
	private float delay = 5;
	private int value; 

	public bool hardMode = false;

	public GameObject playerXpanel;
	public GameObject playerOpanel;

	public bool gameStarted;

	void Awake(){
		gameOverPanel.SetActive (false);
		choseSidePanel.SetActive (true);
		SetGameControllerReferenceButtons ();
//		playerSide = "X";
//		computerSide = "0";
		playerXpanel.SetActive (false);
		playerOpanel.SetActive (false);
		gameStarted = false;
		SetBoardInteractable (false);

	}

	void SetGameControllerReferenceButtons(){
		for (int i = 0; i < buttonList.Length; i++) {
			buttonList [i].GetComponentInParent<buttonScript>().SetGameControllerReference (this);
		}
	}

	public void setPlayerSide(string str){
		playerSide = str;
		computerSide = (playerSide == "X") ? "O" : "X";
		whichSide = playerSide;
		choseSidePanel.SetActive (false);
		if (whichSide == "X") {
			playerXpanel.SetActive (true);
			playerOpanel.SetActive (false);

		} else if ((whichSide == "O") ) {
			playerOpanel.SetActive (true);
			playerXpanel.SetActive (false);

		}
		gameStarted = true;
		SetBoardInteractable (true);

	}


	public string getWhichSide(){
		return whichSide;
	}

	public void EndTurn(){

		moveCount++;

		for (int i = 0; i < buttonList.Length; i+=4) {
			if (buttonList [i].text == whichSide &&
				buttonList [i + 1].text == whichSide &&
				buttonList [i + 2].text == whichSide &&
				buttonList [i + 3].text == whichSide) {
				Debug.Log ("Win condition");
				GameOver ();

				break;
			}
		}


		//4 is number of columns. Could be optimized
		for (int i = 0; i < 4; i++) {
			if (buttonList [i].text == whichSide &&
				buttonList [i + 4].text == whichSide &&
				buttonList [i + 8].text == whichSide &&
				buttonList [i + 12].text == whichSide) {
				Debug.Log("Win condition");
				GameOver ();

				break;
			}
		}

		if( buttonList [0].text == whichSide &&
			buttonList [5].text ==whichSide &&
			buttonList [10].text == whichSide &&
			buttonList [15].text == whichSide 

			|| 
		
			buttonList [3].text == whichSide &&
			buttonList [6].text == whichSide &&
			buttonList [9].text == whichSide &&
		    buttonList [12].text == whichSide){

			Debug.Log ("Win condition");
			GameOver ();
		}

		if (moveCount == 16) {
			gameOverPanel.SetActive (true);
			SetGameOverText ("It's a draw");

		}

		changeSides ();

	}

	void GameOver(){
		SetBoardInteractable (false);
		SetGameOverText (whichSide + " Wins!");
		gameStarted = false;

	}


	void changeSides(){
		whichSide = (whichSide == playerSide) ? computerSide : playerSide;
		if (whichSide ==  "X") {
			playerXpanel.SetActive (true);
			playerOpanel.SetActive (false);
		} else {
			playerXpanel.SetActive (false);
			playerOpanel.SetActive (true);
		}
	}

	void SetGameOverText(string value){
		gameOverPanel.SetActive (true);
		gameOverText.text = value;
	}

	public void RestartGame(){
//		whichSide = playerSide;
		moveCount = 0;
		delay = 5;
		playerXpanel.SetActive (false);
		playerOpanel.SetActive (false);
		gameOverPanel.SetActive (false);
		SetBoardInteractable (true);
		for (int i = 0; i < buttonList.Length; i++) {
			buttonList[i].text = "";
		}
		choseSidePanel.SetActive (true);
	}

	void SetBoardInteractable(bool toggle){
		for (int i = 0; i < buttonList.Length; i++) {
			buttonList [i].GetComponentInParent<Button> ().interactable = toggle;
		}
	}

	void Update(){
		if(gameStarted){
			if (whichSide == computerSide) {
				if (!hardMode) {
					delay += delay * Time.deltaTime;

					if (delay >= 10) {
						value = Random.Range (0, 16);
						if (buttonList [value].GetComponentInParent<Button> ().interactable) {
							buttonList [value].text = computerSide;
							buttonList [value].GetComponentInParent<Button> ().interactable = false;
							delay = 5;
							EndTurn ();			

						}
					}
				} else {


				}
			}
		}
	}


}