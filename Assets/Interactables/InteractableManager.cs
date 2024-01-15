using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    [SerializeField] List<Interactable> interactables = new List<Interactable>(); // vllt doch hier den parent rein tun un dann automatisch weiter geben? Aber werden halt auch nicht immer so viele Stationen geaddet, aber manchmal halt schon und dann solls easy sein
    public List<Interactable> GetInteractables() { return interactables; }
    [SerializeField] List<Transform> interactablePossiblePositions;
    List<int> occupiedPositions = new List<int>();

    [SerializeField] bool randomizePositions = false;

    private void Start()
    {
        if(randomizePositions) RandomizeStationPositions();
    }

    public void RandomizeStationPositions()
    {
        if(interactablePossiblePositions.Count < interactables.Count)
        {
            Debug.LogError("Not enough positions for all interactables");
            return;
        }

        occupiedPositions.Clear();

        for (int i = 0; i < interactables.Count; i++)
        {
            int rand = UnityEngine.Random.Range(0, interactablePossiblePositions.Count);

            if (occupiedPositions.Contains(rand))
            {
                i--;
                continue;
            }

            occupiedPositions.Add(rand);

            interactables[i].transform.position = interactablePossiblePositions[rand].position;
        }

    }
}