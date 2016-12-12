using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
  public AudioSource audioSource;
  public AudioClips[] audioClips;

  public int maximumSoundsAtOnce = 20;

  public static SoundManager Instance { get; private set; }

  public enum SoundTypes {
    StampAppliedSymbol,
    StampAppliedGlow,
    ConditionalMet,
    ContitionalFailed,
    PickupMachine,
    PlaceDownMachine,
    StartTheMachine,
    TestCasesPassed,
    TestCasesFailed,
    MachineHasFinished,
    ClickOnTabletQuad,
    ToggleBetweenInputs,
    MachineDestroyed,
    RotateMover
  }

  [Serializable]
  public struct AudioClips {
    public SoundManager.SoundTypes type;
    public AudioClip clip;
  }

  private Dictionary<string, int> audioClipDictionary;

  private int soundCount;

  void Awake () {
    if (Instance != null) {
      Destroy(this.gameObject);
    }
    else {
      Instance = this;

      audioClipDictionary = new Dictionary<string, int>();
      soundCount = 0;

      for (int i = 0; i < audioClips.Length; i++) {
        audioClipDictionary.Add(audioClips[i].type.ToString(), i);
      }
    }
  }    

  public void PlaySound (SoundTypes type) {
    if (soundCount < maximumSoundsAtOnce) {
      AudioClip clip = audioClips[audioClipDictionary[type.ToString()]].clip;

      audioSource.PlayOneShot(clip);
      StartCoroutine(IncrementThenDecrementSoundCount(clip.length));  
    }
  }

  private IEnumerator IncrementThenDecrementSoundCount (float seconds) {
    soundCount++;

    yield return new WaitForSeconds(seconds);

    soundCount--;
    if (soundCount < 0) {
      soundCount = 0;
    }
  }
}
