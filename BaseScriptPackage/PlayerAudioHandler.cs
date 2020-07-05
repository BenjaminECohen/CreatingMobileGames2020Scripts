using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{
    // THIS SCRIPT IS REQUIRED FOR ANIMATION BEHAVIOR SCRIPTS IN THIS PACKAGE
    // This script is used by the animation behaviors. This way, multiple sounds can be played during animations
    // Each variable should have it's own audio source, so you should have a minimum of 3 separate audio sources.

    public AudioSource attackAudioSource;
    public AudioSource walkAudioSource;
    public AudioSource jumpAudioSource;


}
