using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class StateDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    
    public void SetDisplay(Agent agent, string nodeNames)
    {
        textComponent.text = "\n" +
             $"{agent.name} \n" +
             $"hasWeapon: {agent.hasWeapon} \n" +
             $"detectedPlayer: {agent.detectingPlayer} \n" +
             nodeNames +
             $" \n";
        
        textComponent.gameObject.transform.position = new Vector3(
            agent.transform.position.x,
            5,
            agent.transform.position.z
        );
    }

}
