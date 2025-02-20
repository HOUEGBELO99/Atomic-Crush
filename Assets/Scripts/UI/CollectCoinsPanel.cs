using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectCoinsPanel: ShowHidable
{
    [SerializeField] private int minRewardAmount = 50;
    [SerializeField] private TextMeshProUGUI _coinsCollectedTxt;

    public LevelController levelController;


    public Slider slider;
    private int currentMultiplierIndex = 0;
    private int[] multipliers = { 2, 3, 4, 5, 4, 3, 2 };
    private float minSliderValue = 0f;
    private float maxSliderValue = 1f;
    private float sliderSpeed = 1.5f;
    private bool isMovingLeft = true;
    private bool stopMoving;




    // Start is called before the first frame update
    void Start()
    {

    }


    protected override void OnEnable()
    {
        MoveSlider();
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        stopMoving = false;

    }

    private void MoveSlider()
    {
        if (!stopMoving)
        {
            float step = sliderSpeed * Time.deltaTime;
            if (isMovingLeft)
            {
                slider.value -= step;
                if (slider.value <= minSliderValue)
                {
                    isMovingLeft = false;
                }
            }
            else
            {
                slider.value += step;
                if (slider.value >= maxSliderValue)
                {
                    isMovingLeft = true;
                }
            }
        }
    }
    private void OnSliderValueChanged(float value)
    {
        int currentIndex = Mathf.RoundToInt(value * (multipliers.Length - 1));
        if (currentIndex != currentMultiplierIndex)
        {
            currentMultiplierIndex = currentIndex;
        }
    }


    public void OnClickNext()
    {
        stopMoving = true;
        _coinsCollectedTxt.text = (minRewardAmount * multipliers[currentMultiplierIndex]).ToString();
         int multiplier = multipliers[currentMultiplierIndex];
        AppController appInstance = FindObjectOfType<AppController>();
        appInstance.GEMS += minRewardAmount * multiplier;
        Hide();
        levelController.NextLevel();
    }

    

    // Update is called once per frame
    void Update()
    {
        MoveSlider();
    }
}
