using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public int confetti;
    public InventoryController playerInventory;
    [SerializeField] private TextMeshProUGUI confettiText;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerInventory.gameObject.SetActive(true);
        playerInventory.gameObject.SetActive(false);
    }

    public void GetConfetti(int amount)
    {
        confetti += amount;
        confettiText.text = "Confetti: " + confetti;
    }
}
