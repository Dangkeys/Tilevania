using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int pointsForCoinPickup = 10;
    [SerializeField] AudioClip coinPickupSFX;
    bool wasCollected = false;
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Player") && !wasCollected) return;
        wasCollected = true;
        FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
        AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
