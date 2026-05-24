using UnityEngine;

public class BasketScoreZone : MonoBehaviour
{
    public int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BasketBall"))
        {
            score += 1;
            Debug.Log("Goal! Score = " + score);
        }
    }
}