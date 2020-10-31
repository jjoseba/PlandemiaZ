using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public Sound[] sounds;
	private Sound currentSound;

	void Awake()
	{
		Debug.Log("Awaken, my love!");

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.volume = s.volume;
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			if (currentSound != null && currentSound.name == s.name)
            {
				currentSound = s;
            }
		}
	}

	public void Play(string sound, bool replaceCurrent)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + sound + " not found!");
			return;
		}
		if (replaceCurrent)
        {
			if (currentSound != null)
				currentSound.source.Stop();
			currentSound = s;
		}
		
		s.source.Play();
		
	}

	public void ClearSound()
    {
		if (currentSound != null)
        {
			currentSound.source.Stop();
			currentSound = null;
        }
    }

	public void Pause()
    {
		if (currentSound != null)
        {
			currentSound.source.Pause();
        }
    }

	public void Resume()
    {
		if (currentSound != null)
		{
			currentSound.source.UnPause();
		}
	}

}
