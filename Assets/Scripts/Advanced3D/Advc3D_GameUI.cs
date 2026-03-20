using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/*

ß ÍÅ ÁÓÄÓ ÔÈÊÑÈÒÜ ÍÀÁÈÐÀÅÌÛÉ ÒÅÊÑÒ, ÌÅÍß ÂÑ¨ ÓÑÒÐÀÈÂÀÅÒ :)

 */
public class Advc3D_GameUI : MonoBehaviour 
{
    [Header("Links")]
    [SerializeField] private RectTransform _msgPanel;
    [SerializeField] private CanvasGroup _msgGroup;
    [SerializeField] private TMP_Text _msgText;
    [SerializeField] private RectTransform _coinBag;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private RectTransform _infoWindow;

    [Header("Settings")]
    [SerializeField] private float _typeSpeed = 0.1f;
    [SerializeField] private float _bounceStrength = 25f;

    private CancellationTokenSource _cts = new();
    private Sequence _bounceSequence;

    private void OnEnable()
    {
        _msgPanel.gameObject.SetActive(false);
        _infoWindow.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }

    public void ShowMsgPanel(string msg)
    {
        _msgPanel.localEulerAngles = Vector3.zero;
        _msgPanel.gameObject.SetActive(true);
        FadePanel(1f, 1f, _msgGroup);

        _cts = new();
        WriterAndBounceTask(_cts.Token, _msgPanel, _msgText, msg).Forget();
    }

    public void HideMsgPanel()
    {
        _bounceSequence.Kill();
        _cts?.Cancel();

        _msgText.text = "";
        FadePanel(0f, 1f, _msgGroup);
        FadeAwaiter(_cts.Token, 0f, _msgGroup).Forget();
    }
    
    private void FadePanel(float endValue, float duration, CanvasGroup panel)
    {
        panel.DOFade(endValue, duration);
    }

    private void BouncePanel(RectTransform panel)
    {
        _bounceSequence = DOTween.Sequence();

        _bounceSequence
            .Append(panel.DORotate(new Vector3(0, 0, _bounceStrength), _typeSpeed))
            .Append(panel.DORotate(new Vector3(0,0, -_bounceStrength), _typeSpeed))
            .OnComplete(() => panel.DORotate(Vector3.zero, _typeSpeed));
    }

    private async UniTask WriterAndBounceTask(CancellationToken token, RectTransform panel, TMP_Text textWindow, string msg)
    {
        token.ThrowIfCancellationRequested();

        textWindow.text = "";

        foreach (char c in msg)
        {
            textWindow.text += c;
            BouncePanel(panel);
            await UniTask.Delay(TimeSpan.FromSeconds(_typeSpeed));
        }
    }

    private async UniTask FadeAwaiter(CancellationToken token, float valueToWait, CanvasGroup panelGroup)
    {
        await UniTask.WaitUntil(() => panelGroup.alpha != valueToWait);
        panelGroup.gameObject.SetActive(false);
    }

    public void UpdateCoinBagValue(int coinBag)
    {
        _coinText.text = coinBag.ToString();
    }

    public void OnInfoInput(InputAction.CallbackContext context)
    {
        var isActive = _infoWindow.gameObject.activeInHierarchy;
        _infoWindow.gameObject.SetActive(!isActive);
    }
}