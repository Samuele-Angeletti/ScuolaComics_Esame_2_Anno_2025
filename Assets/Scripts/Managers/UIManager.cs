using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Turret Buttons")]
    public List<TurretButton> turretButtons;
    [SerializeField] TextMeshProUGUI playerCoins;

    private void Start()
    {
        UpdateTurretButtons();
    }

    private void Update()
    {
        // Controlla e aggiorna i pulsanti ogni frame (opzionale, ma semplice)
        // TODO: si potrebbe gestire meglio usando i DesignPattern...
        UpdateTurretButtons();
        UpdatePlayerCoins();
    }

    private void UpdatePlayerCoins()
    {
        playerCoins.text = $"{GameManager.Instance.CurrentCoins}";
    }

    public void UpdateTurretButtons()
    {
        int playerCoins = GameManager.Instance.CurrentCoins;

        foreach (var button in turretButtons)
        {
            button.UpdateButtonState(playerCoins);
        }
    }
}
