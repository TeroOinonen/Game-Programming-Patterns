using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyMoveCommand : Command
{
    Vector3 directionAndMagnitude = Vector3.zero;
    Transform tf;

    public override void Execute()
    {
        tf.position += directionAndMagnitude;
    }

    public StrategyMoveCommand(Vector3 directionAndMagnitude, Transform tf)
    {
        this.directionAndMagnitude = directionAndMagnitude;
        this.tf = tf;
    }

	public override void Undo()
	{
		tf.position -= directionAndMagnitude;
	}

	public override void Redo()
	{
        Execute();
	}
}
