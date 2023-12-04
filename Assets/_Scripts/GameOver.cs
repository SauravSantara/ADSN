using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using LootLocker.Requests;
using TMPro;
using System;

namespace ADSN
{
    public class GameOver : MonoBehaviour
    {
        /*[SerializeField]
        private TMP_InputField inputField;
        [SerializeField]
        private TextMeshProUGUI leaderboardScoreText;
        [SerializeField]
        private TextMeshProUGUI leaderboardNameText;
        [SerializeField]
        private TextMeshProUGUI scoreText;

        private int score = 0;
        private string leaderBoardId = "18612";
        private int leaderBoardTopCount = 10;

        public void StopGame(int score)
        {
            this.score = score;
            scoreText.text = score.ToString();
            //SubmitScore();
            GetLeaderboard();
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void SubmitScore()
        {
            StartCoroutine(SubmitScoreToLeaderboard());
        }

        private IEnumerator SubmitScoreToLeaderboard()
        {
            bool? nameSet = null;
            bool? scoreSubmitted = null;

            /// Checking of player name entered in input field has been saved
            LootLockerSDKManager.SetPlayerName(inputField.text, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Successfully set player name");
                    nameSet = true;
                }
                else
                {
                    Debug.Log("Failed to set player name");
                    nameSet = true;
                }
            });
            /// Setting condition to nameSet is NOT null instead of true/false, otherwise the coroutine may keep on waiting forever.
            yield return new WaitUntil(() => nameSet.HasValue);
            /// below condition does not submit score to leaderboard if user does not has a name
            //if (!nameSet.Value) yield break;

            /// Submitting the score to leaderboard
            LootLockerSDKManager.SubmitScore("", score, leaderBoardId, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Successfully submitted the score");
                    scoreSubmitted = true;
                }
                else
                {
                    Debug.Log("Failed to submit the score");
                    scoreSubmitted = true;
                }
            });
            yield return new WaitUntil(() => scoreSubmitted.HasValue);
            if (!scoreSubmitted.Value) yield break;

            /// Display Leaderboard
            GetLeaderboard();
        }

        private void GetLeaderboard()
        {
            LootLockerSDKManager.GetScoreList(leaderBoardId, leaderBoardTopCount, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Successfully fetched leaderboard.");

                    string leaderboardName = "";
                    string leaderboardScore = "";
                    /// Storing player names / Ids at lootlocker
                    LootLockerLeaderboardMember[] members = response.items;

                    /// Displaying either name or ID of players
                    for (int i = 0; i < members.Length; ++i)
                    {
                        LootLockerPlayer player = members[i].player;

                        if (player == null) continue;

                        if (player.name != "")
                        {
                            leaderboardName += player.name + "\n";
                        }
                        else
                        {
                            leaderboardName += player.id + "\n";
                        }

                        leaderboardScore += members[i].score + "\n";
                    }

                    leaderboardNameText.SetText(leaderboardName);
                    leaderboardScoreText.SetText(leaderboardScore);
                }
                else
                {
                    Debug.Log("Failed to fetch leaderboard.");
                }
            });

        }

        public void AddXP(int score) { }*/
    }

}