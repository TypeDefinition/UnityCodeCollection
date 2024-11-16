using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExampleAIState {
    Blue,
    Red,

    Num,
}

public class ExampleAI : MonoBehaviour {
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material redMaterial;

    private FiniteStateMachine fsm = new FiniteStateMachine();
    private float timer = 0.0f;

    private void Awake() {
        // How many states are in this state machine?
        fsm.SetNumStates((int)ExampleAIState.Num);

        // Set the functions for blue state.
        fsm.SetStateEntry((int) ExampleAIState.Blue, EnterBlue);
        fsm.SetStateUpdate((int)ExampleAIState.Blue, UpdateBlue);
        fsm.SetStateLateUpdate((int)ExampleAIState.Blue, LateUpdateBlue);
        fsm.SetStateExit((int)ExampleAIState.Blue, ExitBlue);

        // Set the functions for red state.
        fsm.SetStateEntry((int)ExampleAIState.Red, EnterRed);
        fsm.SetStateUpdate((int)ExampleAIState.Red, UpdateRed);
        fsm.SetStateLateUpdate((int)ExampleAIState.Red, LateUpdateRed);
        fsm.SetStateExit((int)ExampleAIState.Red, ExitRed);
    }

    private void Start() {
        fsm.ChangeState((int)ExampleAIState.Blue);
    }

    private void Update() {
        fsm.Update();
    }

    private void LateUpdate() {
        fsm.LateUpdate();
    }

    // ******** Blue State ********
    private void EnterBlue() {
        Debug.Log("Entering blue state.");
        
        GetComponent<MeshRenderer>().material = blueMaterial;
        timer = 5.0f;
    }

    private void UpdateBlue() {
        timer -= Time.deltaTime;
        if (timer <= 0.0f) {
            fsm.ChangeState((int)ExampleAIState.Red);
        }
    }

    private void LateUpdateBlue() { }

    private void ExitBlue() {
        Debug.Log("Exiting blue state.");
    }

    // ******** Red State ********
    private void EnterRed() {
        Debug.Log("Entering red state.");

        GetComponent<MeshRenderer>().material = redMaterial;
        timer = 7.0f;
    }

    private void UpdateRed() {
        timer -= Time.deltaTime;
        if (timer <= 0.0f) {
            fsm.ChangeState((int)ExampleAIState.Blue);
        }
    }

    private void LateUpdateRed() { }

    private void ExitRed() {
        Debug.Log("Exiting red state.");
    }
}