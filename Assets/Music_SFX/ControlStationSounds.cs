using UnityEngine;

public class ControlStationSounds : MonoBehaviour
{
    ControlStation controlStation;

    [SerializeField] AudioClip breakSound;
    [SerializeField] AudioClip destroySound;
    [SerializeField] AudioClip fixSound;

    [SerializeField] AudioClip completeTaskSound;


    private void Awake()
    {
        controlStation = GetComponent<ControlStation>();
    }
    private void Start()
    {
        controlStation.onChangeStationState += OnChangeStationState;
        controlStation.onCompleteTask += OnCompleteTask;
    }

    private void OnChangeStationState(StationState stationState)
    {
        if(stationState == StationState.Broken)
        {
            SoundManager.instance.PlaySound(breakSound);
        }
        else if(stationState == StationState.Working)
        {
            SoundManager.instance.PlaySound(fixSound);
        }else if(stationState == StationState.Destroyed)
        {
            SoundManager.instance.PlaySound(destroySound);
        }
    }

    private void OnCompleteTask()
    {
        SoundManager.instance.PlaySound(completeTaskSound);
    }
}