using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipRefSO", menuName = "Scriptable Objects/AudioClipRefSO")]
public class AudioClipRefSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] fail;
    public AudioClip[] success;
    public AudioClip[] pick;
    public AudioClip[] drop;
    public AudioClip[] stove;
    public AudioClip[] trash;
    public AudioClip[] warning;
    public AudioClip[] footsteps;
}
