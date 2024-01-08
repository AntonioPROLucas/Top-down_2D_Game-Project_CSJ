using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float runSpeed;

    private float initialSpeed;
    private Rigidbody2D rig;
    private Vector2 _direction;
    private bool _isRunning;
    private bool _isRolling;

    public Vector2 direction
    {
        get {return _direction;}
        set {_direction = value;}
    }
    public bool isRunning
    {
        get {return _isRunning;}
        set {_isRunning = value;}
    }
    public bool isRolling
    {
        get {return _isRolling;}
        set {_isRolling = value;}
    }

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
    }

    // Update is called once per frame
    private void Update()
    {
       OnInput();
       OnRun();
       OnRolling();
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    #region Movement

    void OnInput()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) ;
    }

    void OnMove()
    {
        rig.MovePosition(rig.position + direction * speed * Time.fixedDeltaTime);
    }

    void OnRun()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed;
            _isRunning = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initialSpeed;
            _isRunning = false;
        }
    }
    
    void OnRolling()
    {
        if(Input.GetMouseButtonDown(1)||Input.GetKeyDown(KeyCode.Space))
        {
            isRolling = true;
            speed = runSpeed;
        }
        if(Input.GetMouseButtonUp(1)||Input.GetKeyUp(KeyCode.Space))
        {
            isRolling = false;
            speed = initialSpeed;
        }
    }

    #endregion
}
