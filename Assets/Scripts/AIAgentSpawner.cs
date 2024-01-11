using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgentSpawner : MonoBehaviour
{
    [SerializeField] Agent[] agents;
    [SerializeField] LayerMask layerMask;

    int index = 0;

    void Update()
    {
        //press 'tab' to switch agent to spawm
        if (Input.GetKeyDown(KeyCode.Tab)) index = (++index % agents.Length);

        // click ti spawn or hold left control and mouse btn to spawn multiplt
        if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl)))
        {
            //get ray from cam to scrn position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))
            {
                //spwn agent at hit point and rand rotation
                Instantiate(agents[index], hitInfo.point, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up));
            }
        }
    }
}
