using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;
	private Sound currentSound;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.volume = s.volume;
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
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
