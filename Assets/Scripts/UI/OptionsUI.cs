using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Transform pressToRebindKeyTransform;
     
    private void Awake()
    {
        Instance = this;
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });

        moveUpButton.onClick.AddListener(() =>{ RebindBinding(GameInput.Binding.Move_Up);});
        moveDownButton.onClick.AddListener(() =>{ RebindBinding(GameInput.Binding.Move_Down);});
        moveLeftButton.onClick.AddListener(() =>{ RebindBinding(GameInput.Binding.Move_Left);});
        interactButton.onClick.AddListener(() =>{ RebindBinding(GameInput.Binding.Interact);});
        interactAlternateButton.onClick.AddListener(() =>{ RebindBinding(GameInput.Binding.InteractAlternate);});
        pauseButton.onClick.AddListener(() =>{ RebindBinding(GameInput.Binding.Pause);});
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnGameUnPaused += KitchenGameManager_OnGameUnPaused;

        UpdateVisual();
        HidePressToRebindKey();
        Hide();
    }

    private void KitchenGameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        musicText.text = "Master Volume: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up); 
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down); 
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left); 
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right); 
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact); 
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate); 
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause); 
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }


    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}