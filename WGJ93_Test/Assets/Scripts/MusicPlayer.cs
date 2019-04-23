using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MusicPlayer : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string musicEvent;
    FMOD.Studio.EventInstance musicInst;

    public static MusicPlayer thisPlayer;

    private void Awake()
    {
        if (thisPlayer != null)
        {
            Destroy(gameObject);
        }
        else {
            thisPlayer = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        musicInst = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        musicInst.start();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
