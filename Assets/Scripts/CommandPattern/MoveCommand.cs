using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    Vector3 directionAndMagnitude = Vector3.zero;
    Rigidbody rb;

    public override void Execute()
    {
        rb.AddForce(directionAndMagnitude);
    }

    public MoveCommand(Vector3 directionAndMagnitude, Rigidbody rb)
    {
        this.directionAndMagnitude = directionAndMagnitude;
        this.rb = rb;
    }

    // UNDO and REDO do not work for WASD as they rely on key being pressed long
    public override void Undo()
	{
        Debug.Log("undo" + directionAndMagnitude + "reverse:" + (-1 * directionAndMagnitude));
        rb.AddForce(-1 * directionAndMagnitude);
    }

	public override void Redo()
	{
        Debug.Log("REDO" + directionAndMagnitude);
        Execute();
	}
}
