using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedUndoController : MonoBehaviour
{
    private MyStack<ActionRecord> undoStack = new MyStack<ActionRecord>();
    public float moveAmount = 1f;
    public float rotationAmount = 15f;
    public float scaleAmount = 0.1f;

    void Update()
    {
        // Save previous state before any action
        Vector3 prevPos = transform.position;
        Quaternion prevRot = transform.rotation;
        Vector3 prevScale = transform.localScale;
        bool actionPerformed = false;

        // Movement
        if (Input.GetKeyDown(KeyCode.UpArrow)) { transform.position += Vector3.up * moveAmount; actionPerformed = true; }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { transform.position += Vector3.down * moveAmount; actionPerformed = true; }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { transform.position += Vector3.left * moveAmount; actionPerformed = true; }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { transform.position += Vector3.right * moveAmount; actionPerformed = true; }

        // Rotation
        if (Input.GetKeyDown(KeyCode.Q)) { transform.Rotate(Vector3.forward, rotationAmount); actionPerformed = true; }
        if (Input.GetKeyDown(KeyCode.E)) { transform.Rotate(Vector3.forward, -rotationAmount); actionPerformed = true; }

        // Scale
        if (Input.GetKeyDown(KeyCode.W)) { transform.localScale += Vector3.one * scaleAmount; actionPerformed = true; }
        if (Input.GetKeyDown(KeyCode.S)) { transform.localScale -= Vector3.one * scaleAmount; actionPerformed = true; }

        // Save action int the pile
        if (actionPerformed)
        {
            ActionType type = DetermineActionType(prevPos, prevRot, prevScale);
            undoStack.Push(new ActionRecord(type, prevPos, prevRot, prevScale));
        }

        // Undo with Z
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (undoStack.TryPop(out ActionRecord action))
            {
                action.Undo(transform);
            }
        }
    }

    private ActionType DetermineActionType(Vector3 prevPos, Quaternion prevRot, Vector3 prevScale)
    {
        if (transform.position != prevPos) return ActionType.Move;
        if (transform.rotation != prevRot) return ActionType.Rotate;
        return ActionType.Scale;
    }
}
