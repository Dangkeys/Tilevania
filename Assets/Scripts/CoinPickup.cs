using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int pointsForCoinPickup = 10;
    [SerializeField] AudioClip[] coinPickupSFX;
    AudioClip SFXThatPlay;
    bool wasCollected = false;
    private void Start() {
        int randSound = Random.Range(0, coinPickupSFX.Length);
        SFXThatPlay = coinPickupSFX[randSound];
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            AudioSource.PlayClipAtPoint(SFXThatPlay, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
