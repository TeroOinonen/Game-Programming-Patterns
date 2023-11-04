using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movespeed = 30f;
    private float jumpspeed = 30f;

    MoveCommand forwardCmd;
    MoveCommand backCmd;
    MoveCommand leftCmd;
    MoveCommand rightCmd;

    MoveCommand jumpCmd;

    StrategyMoveCommand strForwardCmd;
    StrategyMoveCommand strBackCmd;
    StrategyMoveCommand strLeftCmd;
    StrategyMoveCommand strRightCmd;

    Stack<StrategyMoveCommand> moves;
	Stack<StrategyMoveCommand> redoMoves;

	private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moves = new Stack<StrategyMoveCommand>();
        redoMoves = new Stack<StrategyMoveCommand>();

		forwardCmd = new MoveCommand(Vector3.forward * movespeed, rb);
        backCmd = new MoveCommand(Vector3.back * movespeed, rb);
        leftCmd = new MoveCommand(Vector3.left * movespeed, rb);
        rightCmd = new MoveCommand(Vector3.right * movespeed, rb);
        jumpCmd = new MoveCommand(Vector3.up * jumpspeed, rb);

        strForwardCmd = new StrategyMoveCommand(Vector3.forward, transform);
        strBackCmd = new StrategyMoveCommand(Vector3.back, transform);
        strLeftCmd = new StrategyMoveCommand(Vector3.left, transform);
        strRightCmd = new StrategyMoveCommand(Vector3.right, transform);
    }

    private void SwapCommands(ref MoveCommand A, ref MoveCommand B)
    {
        MoveCommand tmp = A;
        A = B;
        B = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement using rigidbody WASD + Space
        if (Input.GetKey(KeyCode.W))
        {
            forwardCmd.Execute();
        }

        if (Input.GetKey(KeyCode.S))
        {
            backCmd.Execute();
        }

        if (Input.GetKey(KeyCode.D))
        {
            rightCmd.Execute();
        }

        if (Input.GetKey(KeyCode.A))
        {
            leftCmd.Execute();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            jumpCmd.Execute();
        }

        // Reverse A D keys
        if (Input.GetKeyDown(KeyCode.X))
        {
            SwapCommands(ref leftCmd, ref rightCmd);
        }
        
        // Strategy Game movement using transform UJHK + O P for Redo/Undo
        if (Input.GetKeyDown(KeyCode.U))
        {
			strForwardCmd.Execute();
            moves.Push(strForwardCmd);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            strBackCmd.Execute();
			moves.Push(strBackCmd);

		}

        if (Input.GetKeyDown(KeyCode.K))
        {
            strRightCmd.Execute();
            moves.Push(strRightCmd);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            strLeftCmd.Execute();
            moves.Push(strLeftCmd);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            StrategyMoveCommand smc = moves.Pop();
			redoMoves.Push(smc);
			smc.Undo();
        }

		if (Input.GetKeyDown(KeyCode.O))
		{
			StrategyMoveCommand smc = redoMoves.Pop();
			moves.Push(smc);
			smc.Redo();
		}
	}
}
