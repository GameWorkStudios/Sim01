using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InspectorPanelController : MonoSingleton<InspectorPanelController>
{
    [Header("Events")]
    [SerializeField] private AnimalProgressInformationEvent OnAnimalProgressInformationGathered;

    [Header("Panel Reference")]
    [SerializeField] private GameObject inspectorPanel;  

    [Header("Progress Bars and TextFields")]
    [SerializeField] private TextMeshProUGUI nameTextField;
    [SerializeField] private TextMeshProUGUI genderTextField;
    [SerializeField] private Image tirednessProgressBar;
    [SerializeField] private Image hungerProgressBar;
    [SerializeField] private Image thirstProgressBar;
    [SerializeField] private Image matingProgressBar;


    void Start()
    {
        inspectorPanel.SetActive(false);
    }

    private void OnEnable() {
        this.OnAnimalProgressInformationGathered.AddListener(AnimalProgressInformationGathered);
    }

    private void OnDisable() {
        this.OnAnimalProgressInformationGathered.RemoveListener(AnimalProgressInformationGathered);
    }

    private void AnimalProgressInformationGathered(AnimalProgressInformation progressInformation){ 
        ResetProgressBars();       
        tirednessProgressBar.fillAmount = progressInformation.tirednessProgress;
        hungerProgressBar.fillAmount = progressInformation.hungerProgress;
        thirstProgressBar.fillAmount = progressInformation.thirstProgress;
        matingProgressBar.fillAmount = progressInformation.mateProgress;
        inspectorPanel.SetActive(true);
    }

    public void ResetProgressBars(){
        tirednessProgressBar.fillAmount = 0;
        hungerProgressBar.fillAmount = 0;
        thirstProgressBar.fillAmount = 0;
        matingProgressBar.fillAmount = 0;
    }

}
