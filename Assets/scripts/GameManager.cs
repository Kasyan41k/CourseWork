using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _road;
    [SerializeField]
    private Text _endGameStatistic;
    [SerializeField]
    private GameObject _statistic;
    [SerializeField]
    private GameObject _buttonRestart;

    [SerializeField]
    private GameObject _startScreen;
    [SerializeField]
    private InputField _speedContainer;
    [SerializeField]
    private CarController _carController;

    private List<GameObject> _carsInScene;
    private int countSuccessfulCars;

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    void Start()
    {
        StartCoroutine(StopProgram());
        _carsInScene = gameObject.GetComponent<CarSpawner>().GetCarsInScene();
    }

    
    private void Update()
    {
        List<GameObject> carsForDelete = new List<GameObject>();

        foreach (var car in _carsInScene)
        {            
            if ((car.transform.position.x > _road.transform.position.x + _road.transform.localScale.x / 2f + 3f))
            {
                carsForDelete.Add(car);
                countSuccessfulCars++;
            }
        }

        foreach(var car in carsForDelete)
        {
            _carsInScene.Remove(car);
            Destroy(car);
        }
    }

    private IEnumerator StopProgram()
    {
        yield return new WaitForSeconds(60f);

        Time.timeScale = 0f;

        _statistic.SetActive(true);
        _buttonRestart.SetActive(true);
        _endGameStatistic.text += countSuccessfulCars + " cars\nsuccessful\ndrove";
    }

    public void StartGame()
    {
        float speed = float.Parse(_speedContainer.text);
        if (speed > 0)
        {
            _carController._startSpeed = speed;
        }
        _startScreen.SetActive(false);
        Time.timeScale = 1f;    
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
