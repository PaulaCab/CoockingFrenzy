using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioClipRefSO audioClipRefSO;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += Delivery_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFail += Delivery_OnRecipeFail;
        Player.Instance.OnPick += Player_OnPick;
        BaseCounter.OnDrop += BaseCounter_OnDrop;
        CuttingCounter.OnCut += CuttingCounter_OnCut;
        TrashCounter.OnThrowAway += TrashCounter_OnThrowAway;
    }

    public void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    
    public void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
    
    
    //events
    private void Delivery_OnRecipeSuccess(object sender, EventArgs e)
    {
        PlaySound(audioClipRefSO.success, DeliveryManager.Instance.deliveryCounter.transform.position);
    }

    private void Delivery_OnRecipeFail(object sender, EventArgs e)
    {
        PlaySound(audioClipRefSO.fail, DeliveryManager.Instance.deliveryCounter.transform.position);
    }
    
    private void Player_OnPick(object sender, EventArgs e)
    {
        PlaySound(audioClipRefSO.pick, Player.Instance.transform.position);
    }

    private void BaseCounter_OnDrop(object sender, EventArgs e)
    {
        BaseCounter counter = sender as BaseCounter;
        PlaySound(audioClipRefSO.drop, counter.transform.position);
    }
    private void CuttingCounter_OnCut(object sender, EventArgs e)
    {
        CuttingCounter counter = sender as CuttingCounter;
        PlaySound(audioClipRefSO.chop, counter.transform.position);
    }

    private void TrashCounter_OnThrowAway(object sender, EventArgs e)
    {
        TrashCounter counter = sender as TrashCounter;
        PlaySound(audioClipRefSO.trash, counter.transform.position);
    }
}
