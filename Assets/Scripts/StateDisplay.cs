using System.Linq;
using TMPro;
using UnityEngine;

public class StateDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    
    public void SetDisplay(BaseAgent agent, string nodeNames, params string[] values)
    {
        string valueText = values.Aggregate("", (current, BBValue) 
            => current + $"{BBValue}: {agent.blackboard.GetValue<bool>(BBValue)} \n");

        textComponent.text = "\n" +
             $"{agent.name} \n" +
             valueText +
             nodeNames +
             $" \n";
        
        textComponent.gameObject.transform.position = new Vector3(
            agent.transform.position.x,
            5,
            agent.transform.position.z
        );
    }

}
//             $"hasWeapon: {agent.blackboard.GetValue<bool>("HASWEAPON")} \n" +
// $"detectedPlayer: {agent.blackboard.GetValue<bool>("DETECTEDPLAYER")} \n" +