using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Build.Content;
using System.Linq;

public class RoundManager : MonoBehaviour
{
    // Create a dictionary to store points for each player.
    private Dictionary<int, int> playerPoints = new Dictionary<int, int>();

    private int roundsRemaining; // rounds left in the game
    private int currentRound; // current round Number
    private bool roundEnded; // is the round over
    private bool isDrawRound; // is it a draw round
    private bool scoreTied; // is there a tied score 
    //public GameUtils gameUtilityScript; // the script that has a function for getting the active players count
    [SerializeField] private GameObject ResultsPanel;
    public SceneChanger sceneChangingScript;

    void Start()
    {
        sceneChangingScript = new SceneChanger();
        ResultsPanel.SetActive(false);
        //gameUtilityScript = FindObjectOfType<GameUtils>();
    }

    void Update()
    {
        // if the round ends and there isn't a tie
        if(roundEnded && !scoreTied)
        {
            PlayerPrefs.SetString("isDraw", "false"); 
            ResultsPanel.SetActive(true);
         
        }

        //if  the round ends in a tie
        else if(roundEnded && scoreTied)
        {
            PlayerPrefs.SetString("isDraw", "true");
            ResultsPanel.SetActive(true);
        }

        if (ResultsPanel.activeSelf) // if the player is looking at the results of the round (player scores) and they press a button 
        {
            
            if (Input.GetKeyDown(KeyCode.A)) // placeholder input (CHANGE ME)
            {
                if (isDrawRound)
                {
                    // if the next round should be a draw round, load the draw round scene
                    sceneChangingScript.loadChosenSceneWithDelay("Draw");
                }

                else
                {
                    //if the next round should be a regular round // load the round scene
                    sceneChangingScript.loadChosenSceneWithDelay("Round");
                }
            }
        }
    }

    public void ManageRounds()
    {
        if(PlayerPrefs.GetString("isDraw") == "true")
        {
            isDrawRound = true;
            return;
        }

        if(PlayerPrefs.GetInt("TotalRounds") !=0)
        {
            roundsRemaining = PlayerPrefs.GetInt("TotalRounds");
        }

        else
        {
           // var numberOfRemainingPlayers = gameUtilityScript.GetActivePlayers().Count;
            //roundsRemaining = numberOfRemainingPlayers - 1;
        }

        if (PlayerPrefs.GetInt("CurrentRoundNumber") != 0)
        {
            currentRound = PlayerPrefs.GetInt("CurrentRoundNumber") + 1;
        }

        if (currentRound > roundsRemaining)
        {
            //Go to end game logic
          //  SceneManager.LoadScene("");
        }


    }

    public void GetFinalScores()
    {
        List<string> activePlayers;
        if (isDrawRound)
        {
            activePlayers = PlayerPrefs.GetString("DrawingPlayers").Split(',').ToList();
        }

        else
        {
            activePlayers = PlayerPrefs.GetString("RemainingPlayers").Split(',').ToList();
        }

        foreach (string player in activePlayers)
        {
            int playerNum = int.Parse(player[1].ToString()) - 1;
           // GameObject currentPlayer = GameObject.FindGameObjectWithTag(player + " Player");
           //int score = currentPlayer.GetComponent<Claw_Manager>().getPoints();
           // playerPoints[player] = score;
        }

        var scoreHolder = 0;
       // var winningScore = playerPoints.TryGetValue(PlayerPrefs.GetString("RemainingPlayers"), out scoreHolder);
        PlayerPrefs.SetString("RoundEndScores", PlayerPrefs.GetString("RemainingPlayers") + ":" + scoreHolder);
    }

    public int GetPointsForPlayers(int playerID)
    {
        if (playerPoints.ContainsKey(playerID))
        {
            return playerPoints[playerID];
        }
        else
        {
            return 0;
        }
    }

    public string CompareScores()
    {
        GetFinalScores();
        // initialize variables to track lowest score and respective player
        int lowestScore = int.MaxValue; // Initialize with a value higher than the possible scores.
        string playerWithLowestScore = "";

        //list to store players with identical scores
        List<int> playersWithIdenticalLowestScores = new List<int>();

        foreach (int playerID in playerPoints.Keys)
        {
            int playerScore = GetPointsForPlayers(playerID);

            if (playerScore < lowestScore)
            {
                lowestScore = playerScore;
                playerWithLowestScore = playerID.ToString();

                // Reset the list of players with identical lowest scores.
                playersWithIdenticalLowestScores.Clear();
                playersWithIdenticalLowestScores.Add(playerID);
            }
            else if (playerScore == lowestScore)
            {
                // Add the player to the list of players with identical lowest scores.
                playersWithIdenticalLowestScores.Add(playerID);

                PlayerPrefs.SetString("DrawingPlayers", string.Join(",", playersWithIdenticalLowestScores));
                PlayerPrefs.SetString("isDraw", "true");
            }
        }

        if (playersWithIdenticalLowestScores.Count >= 2)
        {
            PlayerPrefs.SetString("isDraw", "true");
            
            isDrawRound = true;
           // DrawTextBox.text = playersWithIdenticalLowestScores[0] + " VS " + playersWithIdenticalLowestScores[1];
            return "Tie among players: " + string.Join(", ", playersWithIdenticalLowestScores);
        }
    
        else
        {
         
            if (isDrawRound)
            {
               // var currentPlayers = GameUtils.getActivePlayers();
               // PlayerPrefs.SetString("RemainingPlayers", string.Join(",", currentPlayers));
            
            }
            else
            {
               // var currentPlayers = GameUtils.getActivePlayers();
               // currentPlayers.Remove(playerWithLowestScore);
               // PlayerPrefs.SetString("RemainingPlayers", string.Join(",", currentPlayers));
            }

            // Return the playerID with the lowest score.
            return playerWithLowestScore;
        }
    }

    public void EndRound()
    {
      CompareScores();
      EliminateLoser();
      PushRoundData();
      roundEnded = true;
      ResultsPanel.SetActive(true);
    }


    public void EliminateLoser()
    {
        string playerWithLowestScore = CompareScores(); // Get the player with the lowest score.
        // Check if there's only one player with the lowest score and remove them.
        if (playerWithLowestScore != "" && !scoreTied)
        {
        
            List<string> activePlayers = PlayerPrefs.GetString("RemainingPlayers").Split(',').ToList();
            activePlayers.Remove(playerWithLowestScore);
            PlayerPrefs.SetString("RemainingPlayers", string.Join(",", activePlayers)); // Update PlayerPrefs with the modified active players list.
        }
    }

    private void PushRoundData()
    {
        Debug.Log("Pushing Info!");
        PlayerPrefs.SetInt("TotalRounds", roundsRemaining);
        PlayerPrefs.SetInt("CurrentRoundNumber", currentRound);
    }

}

