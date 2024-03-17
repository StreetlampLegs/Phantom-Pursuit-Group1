using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{

    public void LoseGame()
    {
        // Pause game & show UI
        Debug.Log("You lose");
    }

    public void WinGame()
    {
        // Pause game & show UI
        Debug.Log("You win");
    }
}
