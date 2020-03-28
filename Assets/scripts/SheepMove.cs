using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepMove : MonoBehaviour
{
    // Start is called before the first frame update
    int rd;
    Vector3 moveDirection;
    public float sheepSpeed;
    float timeCount=0;
    enum action {hori,verti,eat,stop,shock};
    action sheepAction;
    public Animator sheep;

    void Start()
    {
        sheep= this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CountTime();
        CorrectDirection(moveDirection);
        Run();
    }

    void ActionChoose()
    {
        rd = Random.Range(0, 8);
        switch (rd)
        {
            case 0:
                moveDirection = Vector3.zero;
                sheepAction = action.stop;
                sheep.SetInteger("action", 0);
                break;
            case 1:
                moveDirection = Vector3.right;
                sheepAction = action.hori;
                sheep.SetInteger("action", 1);
                break;
            case 2:
                moveDirection = Vector3.left;
                sheepAction = action.hori;
                sheep.SetInteger("action", 1);
                break;
            case 3:
                moveDirection = Vector3.up;
                sheepAction = action.verti;
                sheep.SetInteger("action", 3);
                break;
            case 4:
                moveDirection = Vector3.down;
                sheepAction = action.verti;
                sheep.SetInteger("action", 4);
                break;
            case 5:
                moveDirection = Vector3.zero;
                sheepAction = action.eat;
                sheep.SetInteger("action", 5);
                break;
        }
        int integer = sheep.GetInteger("action");
        //Debug.Log("rd:" + rd + "action:" + integer);
    }
    
    void CountTime()
    {
        timeCount = timeCount + Time.deltaTime;
        if(timeCount>2)
        {
            ActionChoose();
            timeCount = 0; 
        }
    }

    void Run()
    {
        this.transform.Translate(moveDirection * sheepSpeed * Time.deltaTime, Space.World);
    }

    private void CorrectDirection(Vector3 speedDirection)
    {
        transform.right = speedDirection;
    }

}
