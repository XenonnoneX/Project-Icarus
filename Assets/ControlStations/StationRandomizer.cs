using System.Collections.Generic;
using UnityEngine;

public class StationRandomizer : MonoBehaviour
{
    [SerializeField] List<Interactable> interactables = new List<Interactable>(); // vllt doch hier den parent rein tun un dann automatisch weiter geben? Aber werden halt auch nicht immer so viele Stationen geaddet, aber manchmal halt schon und dann solls easy sein
    [SerializeField] List<Transform> stationPossiblePositions;
    List<int> occupiedPositions = new List<int>();

    private void Start()
    {
        RandomizeStationPositions();
    }

    public void RandomizeStationPositions()
    {
        if(stationPossiblePositions.Count < interactables.Count)
        {
            Debug.LogError("Not enough positions for all interactables");
            return;
        }

        occupiedPositions.Clear();

        for (int i = 0; i < interactables.Count; i++)
        {
            int rand = UnityEngine.Random.Range(0, stationPossiblePositions.Count);

            if (occupiedPositions.Contains(rand))
            {
                i--;
                continue;
            }

            occupiedPositions.Add(rand);

            interactables[i].transform.position = stationPossiblePositions[rand].position;
        }

    }
}