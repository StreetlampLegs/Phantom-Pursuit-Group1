using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{

    public GameObject SteelDoorReference;

    public int doorCount;

    int maxSwitchesRequired = 6;

    public MonsterBehaviour mh;

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

    public void ActivateSwitch()
    {
        doorCount += 1;
        if (doorCount == 1)
        {
            // Turn the monster on 
            mh.ActivateAI();
        }
        if (doorCount == maxSwitchesRequired)
        {
            SteelDoorReference.GetComponent<Door>().Open(Vector3.zero);
        }
    }
}
