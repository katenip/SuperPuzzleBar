using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class volumeScript : MonoBehaviour
{
    public Slider volume;
    void Start()
    {
        if(!PlayerPrefs.HasKey("volume")){
            PlayerPrefs.SetFloat("volume", 50);
        }
        Load();
    }
    private void Load(){
        volume.value = PlayerPrefs.GetFloat("volume");
    }
    private void Save(){
        PlayerPrefs.SetFloat("volume", volume.value);
    }
    public void ChangeVolume(){
        AudioListener.volume = volume.value / 100;
        Save();
    }
    void Update(){
        GetComponent<TMPro.TextMeshProUGUI>().text = ((int)volume.value).ToString();
    }
   
}
