using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{
    public bool showWinScreen = false; // Показывать экран победы или нет
    public string labelText = "Collect all items!"; // Текст на экране
    public int maxItems = 4; // Максимальное количество предметов
    public bool showLossScreen = false; // Показывать экран поражения или нет
    //public AudioClip deathSound;
    
    //private AudioSource audioSource;
    private int itemsCollected = 0; // Количество собранных предметов
    //private bool deathSoundPlayed = false; // Флаг для предотвращения повторного проигрывания звука
    private int playerHP= 2; // Здоровье игрока


    public int Items
    {
        get 
        { 
            return itemsCollected;  
            // Возвращаем количество собранных предметов
        } 
        
        set 
        { 
            itemsCollected = value; 
            // Устанавливаем количество собранных предметов

            if(itemsCollected >= maxItems) 
            { 
    
                SetGameState("You have found all the items!", true, false, 0f);
                
            }
            else 
            { 
             labelText = "Item collected! Only " + (maxItems - itemsCollected) + " left!"; 
                // Если не все предметы собраны, выводим сообщение о продолжении игры
            }

            
            //Debug.LogFormat("Items: {0}", itemsCollected);
            // Выводим количество собранных предметов в консоль
        } 
    }



    
    public int HP
    {
        get 
        { 
            return playerHP;  
            // Возвращаем количество собранных предметов
        } 
        
        set 
        { 
            playerHP = value; 
            // Устанавливаем количество собранных предметов
            
            if(playerHP <=0)
            {
                SetGameState("You died", false, true, 0f);

                // if (!deathSoundPlayed && audioSource != null)
                // {
                //     audioSource.PlayOneShot(deathSound); // Воспроизводим звук получения урона
                //     deathSoundPlayed = true; // Устанавливаем флаг, чтобы звук не воспроизводился повторно
                // }
            }
            else 
                { 
                    labelText = "-1 HP!";
                }

           // Debug.LogFormat("Items: {0}", playerHP);
            // Выводим количество собранных предметов в консоль
        } 
    }

    void RestartLevel()
    {
        SceneManager.LoadScene("HeroBornLevel"); // Перезагрузка сцены
        Time.timeScale = 1.0f; // Возобновляем игру
    }


    void OnGUI()
    {
        // Отображаем текст на экране
        GUI.Box(new Rect (20, 20 ,150, 25), "Player Health: " + playerHP); // Здоровье игрока
        GUI.Box(new Rect (20, 50 ,150, 25), "Items collected: " + itemsCollected); // Собранные предметы
        GUI.Label(new Rect (Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText); // Текст на экране

        if(showWinScreen)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2- 50, 200, 100), "You won!"))
                {
                    // Если нажата кнопка "You won!", перезагружаем сцену
                    RestartLevel();
                }
            }

            if (showLossScreen)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2- 50, 200, 100), "You lose!"))
                {
                    // Если нажата кнопка "You lose!", перезагружаем сцену
                    RestartLevel();
                }
            }
    }

    


    private void SetGameState(string message, bool isWin, bool isLoss, float timeScale)
    {
        labelText = message;        // Устанавливаем текст сообщения
        showWinScreen = isWin;      // Устанавливаем состояние победы
        showLossScreen = isLoss;    // Устанавливаем состояние поражения
        Time.timeScale = timeScale; // Устанавливаем скорость игры

    }
}
