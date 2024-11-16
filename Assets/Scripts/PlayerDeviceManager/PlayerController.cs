using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    // Player index used to differentiate between players. Range is 0 (inclusive) to PlayerDeviceManager.MAX_PLAYERS (exclusive).
    [SerializeField] private int playerIndex = 0;
    // Do not use the Unity's PlayerInput component. That's fucking garbage and sometimes breaks. Do this instead.
    private GameInput gameInput;

    private void Awake() {
        gameInput = new GameInput();
    }

    private void OnEnable() {
        gameInput.Enable();
        gameInput.Player.Up.performed += OnUp;
        gameInput.Player.Down.performed += OnDown;
        gameInput.Player.Left.performed += OnLeft;
        gameInput.Player.Right.performed += OnRight;
    }

    private void OnDisable () {
        gameInput.Player.Up.performed -= OnUp;
        gameInput.Player.Down.performed -= OnDown;
        gameInput.Player.Left.performed -= OnLeft;
        gameInput.Player.Right.performed -= OnRight;
        gameInput.Disable();
    }

    private void Start() {
    }

    private void Update() {
    }

    public void OnLeft(InputAction.CallbackContext context) {
        // Ignore input that does not belong to this player.
        if (!PlayerDeviceManager.GetInstance().IsPlayerDevice(playerIndex, context.control.device.deviceId)) { return; }

        Debug.Log("Player Index: " + playerIndex.ToString() + ", Action: OnLeft");
    }

    public void OnRight(InputAction.CallbackContext context) {
        // Ignore input that does not belong to this player.
        if (!PlayerDeviceManager.GetInstance().IsPlayerDevice(playerIndex, context.control.device.deviceId)) { return; }

        Debug.Log("Player Index: " + playerIndex.ToString() + ", Action: OnRight");
    }

    public void OnUp(InputAction.CallbackContext context) {
        // Ignore input that does not belong to this player.
        if (!PlayerDeviceManager.GetInstance().IsPlayerDevice(playerIndex, context.control.device.deviceId)) { return; }

        Debug.Log("Player Index: " + playerIndex.ToString() + ", Action: OnUp");
    }

    public void OnDown(InputAction.CallbackContext context) {
        // Ignore input that does not belong to this player.
        if (!PlayerDeviceManager.GetInstance().IsPlayerDevice(playerIndex, context.control.device.deviceId)) { return; }
        
        Debug.Log("Player Index: " + playerIndex.ToString() + ", Action: OnDown");
    }
}
