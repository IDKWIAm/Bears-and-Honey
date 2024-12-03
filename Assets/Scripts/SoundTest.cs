using UnityEngine;

public class SoundTest : Sounds
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaySound(sounds[0]);
        }
        if (Input.GetMouseButtonDown(1))
        {
            PlaySound(sounds[1]);
        }
        if (Input.GetMouseButtonDown(2))
        {
            PlaySound(sounds[2]);
        }
    }
}
