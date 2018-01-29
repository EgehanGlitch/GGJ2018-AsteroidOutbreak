using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Sounds", fileName = "Create")]
public class AO_CharacterSounds : ScriptableObject{

	[SerializeField] public AudioClip[] _footSounds;
	[SerializeField] public AudioClip[] _clothSounds;
	[SerializeField] public AudioClip[] _spitSounds;
	[SerializeField] public AudioClip[] _teethSounds;
	[SerializeField] public AudioClip[] _screamSounds;

	public void PlayFootSound(AudioSource source)
	{
		int randFootSound = Random.Range(0,_footSounds.Length-1);
		source.PlayOneShot(_footSounds[randFootSound]);
		AudioClip temp = _footSounds[randFootSound];
		_footSounds[randFootSound] = _footSounds[_footSounds.Length-1];
		_footSounds[_footSounds.Length-1] = temp;
	}

	public void PlayClothSound(AudioSource source)
	{
		int randSound = Random.Range(0,_clothSounds.Length-1);
		source.PlayOneShot(_clothSounds[randSound]);
		AudioClip temp = _clothSounds[randSound];
		_clothSounds[randSound] = _footSounds[_clothSounds.Length-1];
		_clothSounds[_clothSounds.Length-1] = temp;
	}

	public void PlaySpitSound(AudioSource source)
	{
		int randSound = Random.Range(0,_spitSounds.Length-1);
		source.PlayOneShot(_spitSounds[randSound]);
		AudioClip temp = _spitSounds[randSound];
		_spitSounds[randSound] = _spitSounds[_spitSounds.Length-1];
		_spitSounds[_spitSounds.Length-1] = temp;
	}

	public void PlayTeethSounds(AudioSource source)
	{
		int randSound = Random.Range(0,_teethSounds.Length-1);
		source.PlayOneShot(_teethSounds[randSound]);
		AudioClip temp = _teethSounds[randSound];
		_teethSounds[randSound] = _teethSounds[_teethSounds.Length-1];
		_teethSounds[_teethSounds.Length-1] = temp;
	}

	public void PlayScreenSounds(AudioSource source)
	{
		int randSound = Random.Range(0,_screamSounds.Length-1);
		source.PlayOneShot(_screamSounds[randSound]);
		AudioClip temp = _screamSounds[randSound];
		_screamSounds[randSound] = _screamSounds[_screamSounds.Length-1];
		_screamSounds[_screamSounds.Length-1] = temp;
	}
}

