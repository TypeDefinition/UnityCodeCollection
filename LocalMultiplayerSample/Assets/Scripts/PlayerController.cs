using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    // Player index used to differentiate between players. Range is 0 (inclusive) to PlayerDeviceManager.MAX_PLAYERS (exclusive).
    [SerializeField] private int playerIndex = 0;

    private void Start() {
    }

    private void Update() {
    }

    public void OnLeft(InputAction.CallbackContext context) {
        // Ignore input that does not belong to this player.
        if (PlayerDeviceManager.GetInstance().IsPlayerDevice(playerIndex, context.control.device.deviceId)) { return; }

        if (context.performed)
            Debug.Log("Player Index: " + playerIndex.ToString() + ", Action: OnLeft");
    }

    public void OnRight(InputAction.CallbackContext context) {
        // Ignore input that does not belong to this player.
        if (PlayerDeviceManager.GetInstance().IsPlayerDevice(playerIndex, context.control.device.deviceId)) { return; }

        if (context.performed)
            Debug.Log("Player Index: " + playerIndex.ToString() + ", Action: OnRight");
    }

    public void OnUp(InputAction.CallbackContext context) {
        // Ignore input that does not belong to this player.
        if (PlayerDeviceManager.GetInstance().IsPlayerDevice(playerIndex, context.control.device.deviceId)) { return; }

        if (context.performed)
            Debug.Log("Player Index: " + playerIndex.ToString() + ", Action: OnUp");
    }

    public void OnDown(InputAction.CallbackContext context) {
        // Ignore input that does not belong to this player.
        if (PlayerDeviceManager.GetInstance().IsPlayerDevice(playerIndex, context.control.device.deviceId)) { return; }
        
        if (context.performed)
            Debug.Log("Player Index: " + playerIndex.ToString() + ", Action: OnDown");
    }
}