/*
	Copyright (C) 2020 Anarres

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/> 
*/

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
