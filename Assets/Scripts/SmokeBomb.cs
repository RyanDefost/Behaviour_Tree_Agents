using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class SmokeBomb : MonoBehaviour
    {
        [SerializeField] private float duration = 5f;
        private float timeRemaining;

        private void OnEnable()
        {
            this.timeRemaining = duration;
        }

        private void Update()
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                return;
            }
        
            Destroy(this.gameObject);
        }
        

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent(out GuardAgent guardAgent))
            {
                Debug.Log("FOUND" +  guardAgent.name);
                guardAgent.blackboard.SetValue("ISBLINDED", true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out GuardAgent guardAgent))
            {
                Debug.Log("UNFOUND" +  guardAgent.name);
                guardAgent.blackboard.SetValue("ISBLINDED", false);
            }
        }
    }
}