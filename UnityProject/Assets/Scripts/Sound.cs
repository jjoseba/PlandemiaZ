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

using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{

	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = .75f;

	[Range(.1f, 3f)]
	public float pitch = 1f;

	public bool loop = false;

	public AudioMixerGroup mixerGroup;

	[HideInInspector]
	public AudioSource source;

}
