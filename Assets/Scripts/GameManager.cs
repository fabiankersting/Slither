using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Day Scene
    private bool houseEntered = false;      //Player enters house for the first time, enables snake trigger
    private bool snakeSeen = false;         //Player has seen the snake for the first time
    private bool foodPickedUp = false;      //Player has picked up the snake food, snake now feedable
    private bool snakeFed = false;          //Player has fed the snake
    private bool generatorOn = false;       //Player turned the generator on, lights can be turned on and bed now interactable if snake fed
    //Night Scene
    private bool snakeChecked = false;      //Player checks if the snake is there, phone rings
    private bool lightsOut = false;         //Player answers the phone, lights go out
    private bool generatorChecked = false;  //Player checks the generator, key spawns in attic
    private bool tvSnake = false;           //Snake image on Tv after generator check
    private bool cellarEntered = false;     //Player has entered cellar, game ends

    private bool[] lightsArray = new bool[8] { false, false, false, false, false, false, false, false };
    private bool[] doorsArray = new bool[24] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
    private bool tvOn = false;
    private bool night = false;



    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public bool GetHouseEntered()
    {
        return houseEntered;
    }

    public bool GetSnakeSeen()
    {
        return snakeSeen;
    }

    public bool GetFoodPickedUp()
    {
        return foodPickedUp;
    }

    public bool GetSnakeFed()
    {
        return snakeFed;
    }

    public bool GetGeneratorOn()
    {
        return generatorOn;
    }

    public bool GetSnakeChecked()
    {
        return snakeChecked;
    }

    public bool GetLightsOut()
    {
        return lightsOut;
    }

    public bool GetGeneratorChecked()
    {
        return generatorChecked;
    }

    public void SetTvSnake(bool state)
    {
        tvSnake = state;
    }

    public bool GetTvSnake()
    {
        return tvSnake;
    }
    public bool GetCellarEntered()
    {
        return cellarEntered;
    }

    public void SetHouseEntered(bool state)
    {
        houseEntered = state;
    }

    public void SetSetSnakeSeen(bool state)
    {
        snakeSeen = state;
    }

    public void SetFoodPickedUp(bool state)
    {
        foodPickedUp = state;
    }

    public void SetSnakeFed(bool state)
    {
        snakeFed = state;
    }

    public void SetGeneratorOn(bool state)
    {
        generatorOn = state;
    }

    public void SetSnakeChecked(bool state)
    {
        snakeChecked = state;
    }

    public void SetLightsOut(bool state)
    {
        lightsOut = state;
    }

    public void SetGeneratorChecked(bool state)
    {
        generatorChecked = state;
    }

    public void SetCellarEntered(bool state)
    {
        cellarEntered = state;
    }

    public bool GetTVState()
    {
        return tvOn;
    }

    public void SetTVState(bool state)
    {
        tvOn = state;
    }

    public bool GetNightState()
    {
        return night;
    }

    public void SetNightState(bool state)
    {
        night = state;
    }

    public void ChangeLightStateInArray(int lightId, bool state)
    {
        lightsArray[lightId] = state;
    }

    public bool GetLightStateFromArray(int lightId)
    {
        return lightsArray[lightId];
    }

    public void ChangeDoorStateInArray(int doorId, bool state)
    {
        doorsArray[doorId] = state;
    }

    public bool GetDoorStateFromArray(int doorId)
    {
        return doorsArray[doorId];
    }
}