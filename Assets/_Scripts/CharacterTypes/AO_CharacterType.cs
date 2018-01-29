using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AO_CharacterType : ScriptableObject {

    public string[] Quotes;

    public AO_CharacterSounds characterSounds;

    public GameObject SpitPrefab;
    public SpitProjectileData SpitData;

    public abstract void OnDeath();
}
