using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManagerSounds : MonoBehaviour
{
    MissionManager missionManager;

    [SerializeField] AudioClip missionCompleteSound;

    private void Awake()
    {
        missionManager = GetComponent<MissionManager>();
        missionManager.onMissionCompleted += PlayMissionCompleteSound;
    }

    private void PlayMissionCompleteSound()
    {
        SoundManager.instance.PlaySound(missionCompleteSound);
    }
}
