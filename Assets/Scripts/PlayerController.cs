using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    // Player index used to differentiate between players. Range is 0 (inclusive) to PlayerDeviceManager.MAX_PLAYERS (exclusive).
    [SerializeField] private int playerIndex = 0;
    // Do not use the Unity's PlayerInput component. That's fucking garbage and sometimes breaks. Do this instead.
    private GameControls gameControl;

    private void Awake() {
        gameControl = new GameControls();
    }

    private void OnEnable() {
        gameControl.Enable();
        gameControl.Player.Up.performed += OnUp;
        gameControl.Player.Down.performed += OnDown;
        gameControl.Player.Left.performed += OnLeft;
        gameControl.Player.Right.performed += OnRight;
    }

    private void OnDisable () {
        gameControl.Player.Up.performed -= OnUp;
        gameControl.Player.Down.performed -= OnDown;
        gameControl.Player.Left.performed -= OnLeft;
        gameControl.Player.Right.performed -= OnRight;
        gameControl.Disable();
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
