using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsAttacked;
    public BaseAgent attacker { get; private set; }

    public int Health { get; private set; }

    [SerializeField] private float attackAlertTime = 2;
    [SerializeField] private float timeRemaining;
    
    
    public void AttackPlayer(BaseAgent agent)
    {
        this.Health--;
        this.IsAttacked = true;
        this.attacker = agent;
        
        this.timeRemaining = this.attackAlertTime;
    }

    private void Update()
    {
        if (this.timeRemaining > 0)
        {
            this.timeRemaining -= Time.deltaTime;
            return;
        }
        
        this.attacker = null;
        this.IsAttacked = false;
    }
}
