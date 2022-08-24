using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourForm : StateMachineBehaviour
{
    [SerializeField]
    private float timeUntilBored;

    [SerializeField]
    private int numberOfBoredAnimations;
    private bool isBored;
    private float idleTime;
    int boredAnimation;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       timeUntilBored = Random.Range(12,timeUntilBored + 1);

       ResetIdle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    
       if(isBored == false){
           idleTime += Time.deltaTime;
           
            if(idleTime >= timeUntilBored && stateInfo.normalizedTime % 1 < 0.02){
            
               isBored = true;
               boredAnimation = Random.Range(1, numberOfBoredAnimations + 1);
               boredAnimation = boredAnimation * 2 -1;
               animator.SetFloat("BoredAnimation",boredAnimation);
  
            }
       }
       else if(stateInfo.normalizedTime % 1 > 0.99){
                ResetIdle();
            }
        animator.SetFloat("BoredAnimation", boredAnimation);
       
    }

    private void ResetIdle(){
        if(isBored){
            boredAnimation--;
        }
       
        idleTime = 0;
        isBored = false;

      
       
    }

    
}
