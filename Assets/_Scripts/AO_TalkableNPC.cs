using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AO_TalkableNPC : MonoBehaviour,IInterractable {

    private AO_CharacterType _type;
    public UnityEngine.UI.Text Txt;

    public float TimeoutTime = 3f;

    private AO_PlayerController _playerController;

    private void Awake()
    {
        _type = GetComponent<AO_CharacterHumanController>().CharacterType;
        _playerController = FindObjectOfType<AO_PlayerController>();

        if (Txt)
        {
            Txt = FindObjectOfType<UnityEngine.UI.Text>();
        }
    }

    public void Interract()
    {
        if (_playerController.ReadingText) return;
        _playerController.ReadingText = true;
        int count = _type.Quotes.Length;

        int randomQuoteIndex = Random.Range(0, count);

        Txt.gameObject.SetActive(true);

        Txt.text = _type.Quotes[randomQuoteIndex];

        StartCoroutine(TimeOutText());
    }

    private IEnumerator TimeOutText()
    {
        yield return new WaitForSeconds(TimeoutTime);

        Txt.gameObject.SetActive(false);
        _playerController.ReadingText = false;
    }
    

    private void OnDestroy()
    {
        if(Txt != null) Txt.gameObject.SetActive(false);
        _playerController.ReadingText = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        AO_CharacterHumanController ctrl = other.gameObject.GetComponent<AO_CharacterHumanController>();
        if (ctrl != null && ctrl.Equals(_playerController.CharacterCtrlr))
        {
            ctrl.AddUniqueInteractable(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AO_CharacterHumanController ctrl = other.gameObject.GetComponent<AO_CharacterHumanController>();
        if (ctrl != null && ctrl.Equals(_playerController.CharacterCtrlr))
        {
            _playerController.CharacterCtrlr.interractables.Remove(this);
        }
    }
}
