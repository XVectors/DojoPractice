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
             labelText = "You have found all the items!"; 
                // Если собраны все предметы, выводим сообщение о победе

             showWinScreen = true;
             Time.timeScale = 0; // Останавливаем игру
                
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
        get { 
            return playerHP;  
            // Возвращаем количество собранных предметов
        } 
        
        set { 
            playerHP = value; 
            // Устанавливаем количество собранных предметов
            
            if(playerHP <=0)
            {
                labelText = "YOU DIED";
                showLossScreen = true; // Показываем экран поражения
                Time.timeScale = 0; // Останавливаем игру
                
                
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

    void OnGUI()
    {
        // Отображаем текст на экране
        GUI.Box(new Rect (20, 20 ,150, 25), "Player Health: " + playerHP); // Здоровье игрока
        GUI.Box(new Rect (20, 50 ,150, 25), "Items collected: " + itemsCollected); // Собранные предметы
        GUI.Label(new Rect (Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText); // Текст на экране

        if(showWinScreen)
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2- 50, 200, 100), "You win!"))
        {
            // Если нажата кнопка "You win!", перезагружаем сцену
            SceneManager.LoadScene("HeroBornLevel"); // Перезагрузка сцены
            Time.timeScale = 1.0f; // Возобновляем игру
            //showWinScreen = false; // Скрываем экран победы
        }
    }

    if (showLossScreen)
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2- 50, 200, 100), "You lose!"))
        {
            // Если нажата кнопка "You lose!", перезагружаем сцену
            SceneManager.LoadScene("HeroBornLevel"); // Перезагрузка сцены
            Time.timeScale = 1.0f; // Возобновляем игру
            //showLossScreen = false; // Скрываем экран поражения
        }
    }
    }

    
}
