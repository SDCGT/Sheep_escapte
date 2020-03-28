using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMove : MonoBehaviour
{
    // Start is called before the first frame update
    int rd;
    Vector3 moveDirection;
    public float dogSpeed;
    float timeCount = 0;
    enum action { stop,leftrun,rightrun};
    action dogAction;
    public Animator dog;

    void Start()
    {
        dog = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CountTime();
        Run();
        CorrectDirection(moveDirection);
    }

    void ActionChoose()
    {
        rd = Random.Range(0, 8);
        /*switch (rd)
        {
            case 0:
                moveDirection = Vector3.zero;
                dogAction = action.stop;
                dog.SetInteger("action", 0);
                break;
            case 1:
                moveDirection = Vector3.right;
                dogAction = action.run;
                dog.SetInteger("action", 1);
                break;
        }*/
        if(rd>6)
        {
            moveDirection = Vector3.zero;
            dogAction = action.stop;
            dog.SetInteger("action", 0);
        }
        if(rd<=3)
        {
            moveDirection = Vector3.right;
            dogAction = action.rightrun;
            dog.SetInteger("action", 1);
        }
        if (rd > 3 && rd<=6)
        {
            moveDirection = Vector3.left;
            dogAction = action.leftrun;
            dog.SetInteger("action", 1);
        }
        int integer = dog.GetInteger("action");
        //Debug.Log("rd:" + rd + "action:" + integer);
    }

    void CountTime()
    {
        timeCount = timeCount + Time.deltaTime;
        if (timeCount > 2)
        {
            ActionChoose();
            timeCount = 0;
        }
    }

    void Run()
    {
        this.transform.Translate(moveDirection * dogSpeed * Time.deltaTime, Space.World);
        //Debug.Log(moveDirection);
    }

    private void CorrectDirection(Vector3 speedDirection)
    {
        transform.right = speedDirection;
    }
}
