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

	public override void Undo()
	{
		// Nothing
	}

	public override void Redo()
	{
		// Nothing
	}
}
