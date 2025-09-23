using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// possible action tipes
public enum ActionType { Move, Rotate, Scale }

// registers of every action
public class ActionRecord
{
    public ActionType Type;
    public Vector3 PreviousPosition;
    public Quaternion PreviousRotation;
    public Vector3 PreviousScale;

    public ActionRecord(ActionType type, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        Type = type;
        PreviousPosition = pos;
        PreviousRotation = rot;
        PreviousScale = scale;
    }

    public void Undo(Transform transform)
    {
        transform.position = PreviousPosition;
        transform.rotation = PreviousRotation;
        transform.localScale = PreviousScale;
    }
}
