using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class StateDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private Agent agent;

    private Quaternion baseRotation;

    public void Update()
    {
        print(agent.namething);
        textComponent.text = agent.namething;
    }
    
    /*void Update()
    {
        string stateNames = "";
        foreach (Node node in agent.activeNodes)
        {
            stateNames += ("\n" + node.NodeName);
        }
        
        textComponent.text = "\n" +
            $"{agent.name} \n" +
            $"hasWeapon: {agent.hasWeapon} \n" +
            $"detectedPlayer: {agent.detectingPlayer} \n" +
            stateNames +
            $" \n";
        
        textComponent.gameObject.transform.position = agent.transform.position;
    }*/
}
