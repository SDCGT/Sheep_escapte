using UnityEngine;


public class Hedge : MonoBehaviour
{
    public bool isBroken;
    void Start()
    {
        isBroken = false;
    }
    
    void Update()
    {
        
    }

    public void Broke()    //破坏动作
    {
        isBroken = true;
        //TODO:改变贴图、碰撞等
    }
    public void Repair()    //修复动作
    {
        isBroken = false;
        //TODO:改变贴图、碰撞等
    }
    
}
