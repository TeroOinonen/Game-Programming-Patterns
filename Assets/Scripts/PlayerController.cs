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

    Stack<Command> moves;
	Stack<Command> redoMoves;

	private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moves = new Stack<Command>();
        redoMoves = new Stack<Command>();

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
        // Movement using rigidbody WASD + Space, UNDO and REDO do not work for WASD as they rely on key being pressed long
        if (Input.GetKey(KeyCode.W))
        {
            ExecuteAndBuffer(forwardCmd);
        }

        if (Input.GetKey(KeyCode.S))
        {
            ExecuteAndBuffer(backCmd);
        }

        if (Input.GetKey(KeyCode.D))
        {
            ExecuteAndBuffer(rightCmd);
        }

        if (Input.GetKey(KeyCode.A))
        {
            ExecuteAndBuffer(leftCmd);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            ExecuteAndBuffer(jumpCmd);
        }

        // Reverse A D keys
        if (Input.GetKeyDown(KeyCode.X))
        {
            SwapCommands(ref leftCmd, ref rightCmd);
        }
        
        // Strategy Game movement using transform UJHK + O P for Redo/Undo
        if (Input.GetKeyDown(KeyCode.U))
        {
            ExecuteAndBuffer(strForwardCmd);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            ExecuteAndBuffer(strBackCmd);

		}

        if (Input.GetKeyDown(KeyCode.K))
        {
            ExecuteAndBuffer(strRightCmd);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ExecuteAndBuffer(strLeftCmd);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (moves.Count < 1)
                return;

            Command smc = moves.Pop();
			redoMoves.Push(smc);
			smc.Undo();
            Debug.Log("Undo buffer count:" + moves.Count);
        }

		if (Input.GetKeyDown(KeyCode.O))
		{
            if (redoMoves.Count < 1)
                return;

            Command smc = redoMoves.Pop();
			moves.Push(smc);
			smc.Redo();
            Debug.Log("redoMoves buffer count:" + redoMoves.Count);
        }
	}

    private void ExecuteAndBuffer(Command executeCmd)
    {
        executeCmd.Execute();
        moves.Push(executeCmd);
        redoMoves.Clear(); // Remove redo commands, so that undo-undo-new command will not mess up redo queue
        Debug.Log("Undo buffer count:" + moves.Count);
    }
}
