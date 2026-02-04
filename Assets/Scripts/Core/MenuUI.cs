using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// я хотел чтобы через bool работали изменения отображения но я понятия не имею почему 
// public bool BackButton => { get => _backButton; set {_backButton = value; _backButton.gameObject.SetActive(value)} }
public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _toListButton;

    [SerializeField] private GameObject _listPanel;

    [SerializeField] private Transform _buttonsContainer;
    [SerializeField] private GameObject _loadButtonPrefab;
    private Dictionary<string, int> _sceneDictionary = new();

    private void Awake()
    {
        InitMenu();
    }

    private void InitMenu()
    {
        _loadButtonPrefab = Resources.Load<GameObject>("UI/ButtonPrefab");

        _backButton.onClick.AddListener(ResetMenu);
        _toListButton.onClick.AddListener(ShowSceneList);

        ResetMenu();
        InitSceneDictionary();
        InitSceneList();
    }

    private void InitSceneDictionary()
    {
        _sceneDictionary.Add("Menu", 0);
        _sceneDictionary.Add("NewInputSystem", 1);
        _sceneDictionary.Add("DoomController (dotween)", 0);
        _sceneDictionary.Add("2D Platformer", 0);
    }

    private void InitSceneList()
    {
        foreach (var item in _sceneDictionary.Keys)
        {
            GameObject newLoadButton = Instantiate(_loadButtonPrefab, _buttonsContainer.transform);
            TMP_Text text = newLoadButton.GetComponentInChildren<TMP_Text>();
            Button button = newLoadButton.GetComponent<Button>();
            
            text.text = item;
            newLoadButton.name = item;

            button.onClick.AddListener(() => { SceneManager.LoadScene(_sceneDictionary[item]); });
        }
    }

    private void ResetMenu()
    {
        _backButton.gameObject.SetActive(false);
        _toListButton.gameObject.SetActive(true);
        _listPanel.SetActive(false);
    }

    private void ShowSceneList()
    {
        _backButton.gameObject.SetActive(true);
        _toListButton.gameObject.SetActive(false);
        _listPanel.SetActive(true);
    }
}
