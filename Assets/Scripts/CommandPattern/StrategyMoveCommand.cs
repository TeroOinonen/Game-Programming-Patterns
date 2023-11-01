using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyMoveCommand : Command
{
    Vector3 directionAndMagnitude = Vector3.zero;
    Transform tf;
    Stack<StrategyMoveCommand> moveLog;

    public override void Execute()
    {
        tf.position += directionAndMagnitude;
        moveLog.Push(this);
    }

    internal void Undo()
    {
        tf.position -= directionAndMagnitude;
    }

    public StrategyMoveCommand(Vector3 directionAndMagnitude, Transform tf, ref Stack<StrategyMoveCommand> moveLog)
    {
        this.directionAndMagnitude = directionAndMagnitude;
        this.tf = tf;
        this.moveLog = moveLog;
    }
}
