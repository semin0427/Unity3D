using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
public class AudioVolume : MonoBehaviour {

    public AudioMixer masterMixer;
    public void SetSfxLvl(float sfxlvl)
    {
        masterMixer.SetFloat("sfxVol", sfxlvl);
    }
    public void SetMusicLvl(float musicxlvl)
    {
        masterMixer.SetFloat("MusicVol", musicxlvl);
    }
}
