// using UnityEngine;
// using Dan.Main;
// public class LeaderBoardManager : MonoBehaviour
// {


//     private void Start()
//     {
//         LoadEntries();
//     }

//     private void LoadEntries()
//     {
//         Leaderboards.MadDashLeaderboard.GetEntries(entries =>
//         {
//             foreach (var t in _entryTextObjects)
//                 t.text = "";

//             var length = Mathf.Min(_entryTextObjects.Length, entries.Length);
//             for (int i = 0; i < length; i++)
//             {
//                 _entryTextObjects[i].text = $"{entries[i].Rank}. {entries[i].Username} - {entries[i].Score}";
//             }
//         });
//     }


//     public void UploadEntry()
//     {
//         Leaderboards.TutorialLeaderboard.UploadNewEntry(_usernameInputField.text, Score, isSuccessful =>
//         {
//             if (isSuccessful)
//                 LoadEntries();
//         });
//     }


// }
