using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Singleton class to map input devices to a player index.
public class PlayerDeviceManager {
    public static int MAX_PLAYERS = 4;
    private static PlayerDeviceManager instance;

    private int[] deviceIds = new int[MAX_PLAYERS]; // When the same device is disconnected and reconnected, it will be assigned a different id.
    private int numPlayers = 0;

    public static PlayerDeviceManager GetInstance() {
        if (instance == null) { instance = new PlayerDeviceManager(); }
        return instance;
    }

    PlayerDeviceManager() {
        for (int i = 0; i < deviceIds.Length; ++i) {
            deviceIds[i] = InputDevice.InvalidDeviceId;
        }

        // Iterate through all existing devices, and assign them to players.
        for (int i = 0; i < InputSystem.devices.Count; ++i) {
            // TODO: Will there ever be a case where a device is disconnected but not removed?
            if (!IsSuitableDevice(InputSystem.devices[i])) continue;
            TryAssignDevice(InputSystem.devices[i].deviceId);
        }

        // Listen for any changes to connected devices.
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    ~PlayerDeviceManager() {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    // Change this function to fit your game's needs.
    private bool IsSuitableDevice(InputDevice device) {
        // Ignore anything that isn't a gamepad.
        return device is Gamepad;
    }

    // Assign this device to the first player that does not yet have a device.
    private void TryAssignDevice(int deviceId) {
        for (int i = 0; i < deviceIds.Length; ++i) {
            if (deviceIds[i] == InputDevice.InvalidDeviceId) {
                deviceIds[i] = deviceId;
                ++numPlayers;
                Debug.Log("Player " + i.ToString() + " assigned device " + deviceId.ToString() + ".");
                break;
            }
        }
    }

    // Unassign this device if it has already been assigned to a player.
    private void TryUnassignDevice(int deviceId) {
        for (int i = 0; i < deviceIds.Length; ++i) {
            if (deviceIds[i] == deviceId) {
                deviceIds[i] = InputDevice.InvalidDeviceId;
                --numPlayers;
                Debug.Log("Player " + i.ToString() + " unassigned device " + deviceId.ToString() + ".");
                break;
            }
        }
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change) {
        if (!IsSuitableDevice(device)) return;

        switch (change) {
            case InputDeviceChange.Added:
            case InputDeviceChange.Reconnected:
            case InputDeviceChange.Enabled:
                if (!IsDeviceAssigned(device.deviceId)) {
                    TryAssignDevice(device.deviceId);
                }
                break;
            case InputDeviceChange.Removed:
            case InputDeviceChange.Disconnected:
            case InputDeviceChange.Disabled:
                if (IsDeviceAssigned(device.deviceId)) {
                    TryUnassignDevice(device.deviceId);
                }
                break;
            default:
                break;
        }
    }

    public int GetNumPlayers() { return numPlayers; }

    public bool IsPlayerDevice(int playerIndex, int deviceId) { return deviceIds[playerIndex] == deviceId; }

    public bool IsDeviceAssigned(int deviceId) {
        // This runs in O(n) time, but I'm fine with it since n = 4.
        for (int i = 0; i < deviceIds.Length; ++i) {
            if (deviceIds[i] == deviceId) { return true; }
        }
        return false;
    }
}