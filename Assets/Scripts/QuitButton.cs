using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class QuitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Sprite defaultSprite;
    public Sprite hoverSprite;
    public Sprite clickSprite;

    public AudioClip clickSound;

    public float clickSpriteDuration = 0.5f;

    private Image buttonImage;
    private AudioSource audioSource;

    private bool isQuitting = false;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = defaultSprite;

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isQuitting)
            buttonImage.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isQuitting)
            buttonImage.sprite = defaultSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isQuitting) return;

        isQuitting = true;

        buttonImage.sprite = clickSprite;

        StartCoroutine(PlaySoundAndQuit());
    }

    private IEnumerator PlaySoundAndQuit()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);

            yield return new WaitForSeconds(clickSound.length);
        }

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
