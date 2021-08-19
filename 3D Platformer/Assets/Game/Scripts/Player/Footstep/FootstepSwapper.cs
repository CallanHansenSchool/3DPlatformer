// Code from tutorial by Natty Creations on YouTube

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSwapper : MonoBehaviour
{
    private TerrainChecker checker;

    private string currentLayer;

    public FootstepCollection[] TerrainFootstepCollections;
  
    private PlayerFootstepAudio playerFootstepAudio;
    [SerializeField] private Transform groundChecker;

    void Start()
    {
        playerFootstepAudio = GetComponent<PlayerFootstepAudio>();
        checker = new TerrainChecker();
    }

    public void CheckLayers()
    {
        RaycastHit hit;

        if (Physics.Raycast(groundChecker.position, Vector3.down, out hit, 3))
        {
            if(hit.transform.GetComponent<Terrain>() != null)
            {
                Terrain t = hit.transform.GetComponent<Terrain>();
             
                if (currentLayer != checker.GetLayerName(groundChecker.position, t))
                {
                    currentLayer = checker.GetLayerName(groundChecker.position, t);

                    foreach(FootstepCollection collection in TerrainFootstepCollections)
                    {
                        if(currentLayer == collection.name)
                        {
                            playerFootstepAudio.SwapFootsteps(collection);
                        }
                    }
                }

                Debug.Log(currentLayer);
            }

            if(hit.transform.GetComponent<SurfaceType>() != null)
            {
                FootstepCollection collection = hit.transform.GetComponent<SurfaceType>().FootstepCollection;
                currentLayer = collection.name;
                playerFootstepAudio.SwapFootsteps(collection);
            }
        }
    }

}
