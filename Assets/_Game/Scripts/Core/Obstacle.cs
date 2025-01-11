using UnityEngine;

namespace Sans.Core
{
    public class Obstacle : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Ball ball = collision.gameObject.GetComponent<Ball>();
                Collider thisColl = GetComponent<Collider>();
                ball.HitObstacle(thisColl);

                SoundController.Instance.StartVibration();
            }
        }
    }
}