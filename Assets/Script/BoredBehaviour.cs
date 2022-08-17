using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoredBehaviour : StateMachineBehaviour
{
    [SerializeField]float timeUntilBored;

    [SerializeField]int numberOfBoredAnimation;
    private bool isBored;
    private float idleTime;
    private int boredAnimation;

    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       ResetIdle();
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(isBored == false){
            idleTime += Time.deltaTime;}  

            if(idleTime > timeUntilBored && stateInfo.normalizedTime % 1< 0.02f){
                isBored = true;
                boredAnimation = Random.Range(1 ,numberOfBoredAnimation + 1);
                boredAnimation = boredAnimation * 2 - 1;

                animator.SetFloat("BoredAnimation", boredAnimation - 1);
                
            }
        else if(stateInfo.normalizedTime % 1 > 0.98){
            ResetIdle();
        }

        animator.SetFloat("BoredAnimation",boredAnimation, 0.2f, Time.deltaTime);
     }

   void ResetIdle(){
       if(isBored)
       {
           boredAnimation--;
       }
       idleTime = 0;
       isBored = false;

       
   }
}
